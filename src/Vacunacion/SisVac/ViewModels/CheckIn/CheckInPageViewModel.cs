using Microsoft.AppCenter.Crashes;
using Plugin.ValidationRules;
using Plugin.ValidationRules.Formatters;
using Plugin.ValidationRules.Rules;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Prism.Services.Dialogs;
using Refit;
using SisVac.Framework.Domain;
using SisVac.Framework.Extensions;
using SisVac.Framework.Http;
using SisVac.Framework.Services;
using SisVac.Helpers.Rules;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using XF.Material.Forms.UI.Dialogs;

namespace SisVac.ViewModels.CheckIn
{
    public class CheckInPageViewModel : ScanDocumentViewModel
    {
        ValidationUnit _emergencyContactUnit;
        bool _signatureIsValid;

        public CheckInPageViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService,
            IScannerService scannerService,
            ICitizensApiClient citizensApiClient, ICacheService cacheService) : base(navigationService, dialogService, scannerService, cacheService, citizensApiClient)
        {
            ProgressBarIndicator = 0.0f;

            DocumentScanned = async (id) => await GoNextAfterDocumentRead(id);

            EmergencyContactName = new Validatable<string>();
            EmergencyContactName.Validations.Add(new IsNotNullOrEmptyRule());

            EmergencyPhoneNumber = new Validatable<string>();
            EmergencyPhoneNumber.Validations.Add(new IsNotNullOrEmptyRule());
            EmergencyPhoneNumber.ValueFormatter = new MaskFormatter("(XXX)XXX-XXXX");

            _emergencyContactUnit = new ValidationUnit(EmergencyContactName, EmergencyPhoneNumber);


            NextCommand = new DelegateCommand(OnNextCommandExecute);
            BackCommand = new DelegateCommand(OnBackCommandExecute);
            ConfirmCommand = new DelegateCommand(OnConfirmCommandExecute);
            ClearSignatureCommand = new DelegateCommand(() => _signatureIsValid = false);
            CompletedSignatureCommand = new DelegateCommand(() => _signatureIsValid = true);
        }

        #region Properties
        public Func<Task<byte[]>> SignatureFromStream { get; set; }
        public int PositionView { get; set; }
        public bool IsBackButtonVisible { get; set; } = false;
        public bool IsNextButtonVisible { get; set; } = true;
        public bool IsConfirmButtonVisible { get; set; } = false;
        public double ProgressBarIndicator { get; set; }
        public string ProgressTextIndicator
        {
            get
            {
                return $"Paso {PositionView + 1} de 6";
            }
        }
        public string DocumentLabel
        {
            get
            {
                return $"Cédula: {Patient.Document}";
            }
        }

        public Person Patient { get; set; } = new Person();
        public Consent Consent { get; set; } = new Consent();
        public Consent InverterConsent { get; set; } = new Consent();
        public Validatable<string> EmergencyContactName { get; set; }
        public Validatable<string> EmergencyPhoneNumber { get; set; }
        public bool SignatureIsBlank { get; set; } 
        #endregion


        #region Commands
        public ICommand NextCommand { get; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand BackCommand { get; }
        public ICommand ClearSignatureCommand { get; }
        public ICommand CompletedSignatureCommand { get; } 
        #endregion


        private async void OnConfirmCommandExecute()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            if (_signatureIsValid)
            {
                using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Validando..."))
                {
                    try
                    {
                        using (var ms = new MemoryStream(await SignatureFromStream()))
                        {
                            var signatureStreamPart = new StreamPart(ms, "signature.jpg");
                            var result = await _citizensApiClient.PostConsent(Patient.Document, Consent.HasCovid, Consent.IsPregnant, Consent.HadFever, Consent.IsVaccinated, Consent.HadReactions, Consent.IsAllergic, Consent.IsMedicated, Consent.HasTransplant, signatureStreamPart);
                        }
                    }
                    catch (Exception ex)
                    {
                        Crashes.TrackError(ex);
                        IsBusy = false;
                        await _dialogService.DisplayAlertAsync("Ocurrió algo inesperado", "Ocurrió un problema de comunicación con el servidor", "OK");
                        return;
                    }
                }

                await _dialogService.DisplayAlertAsync("Registro completado", "Has terminado la tarea satisfactoriamente.", "Ok");
                await _navigationService.GoBackAsync();
            }
            else
            {
                await _dialogService.DisplayAlertAsync("Ups", "Necesitas la firma del paciente para continuar.", "Ok");
            }

            IsBusy = false;
        }


        private async Task GoNextAfterDocumentRead(string id)
        {
            await GetQualificationData(id);

            if (!Qualification.IsValidDocument)
            {
                await _dialogService.DisplayAlertAsync("Ups", "Documento no valido.", "Ok");
                return;
            }

            var patientData = await GetDocumentData(id);
            if (patientData != null && patientData.IsValid)
            {
                Patient = new Person
                {
                    Age = patientData.Age,
                    Document = patientData.Cedula,
                    FullName = patientData.Name
                };

                var consent = await _citizensApiClient.GetConsent(patientData.Cedula);

                if (consent.Citizen != null)
                {
                    await _dialogService.DisplayAlertAsync("Registro Exitoso", "Este cuidadano ya ha dado su consentimiento para vacunarse.", "OK");
                }
                else
                {
                    IsBackButtonVisible = true;
                    PositionView = 1;
                    ProgressBarIndicator = PositionView / 5.0f;
                }
            }
        }

        private async void OnNextCommandExecute()
        {
            switch (PositionView)
            {
                case 0:
                    if(DocumentID.Validate())
                    {
                        await GoNextAfterDocumentRead(DocumentID.Value);
                    }
                    break;
                case 1:
                    PositionView = 2;
                    break;
                case 2:
                    if (_emergencyContactUnit.Validate())
                    {
                        PositionView = 3;
                    }                    
                    break;
                case 3:
                    PositionView = 4;
                    break;
                case 4:
                    bool formIsValid = true;

                    if (!Consent.IsPregnant && !InverterConsent.IsPregnant)
                        formIsValid = false;

                    if (!Consent.HadFever && !InverterConsent.HadFever)
                        formIsValid = false;

                    if (!Consent.IsVaccinated && !InverterConsent.IsVaccinated)
                        formIsValid = false;

                    if (!Consent.HadReactions && !InverterConsent.HadReactions)
                        formIsValid = false;

                    if (!Consent.IsAllergic && !InverterConsent.IsAllergic)
                        formIsValid = false;

                    if (!Consent.IsInmunoDeficient && !InverterConsent.IsInmunoDeficient)
                        formIsValid = false;

                    if (!Consent.IsMedicated && !InverterConsent.IsMedicated)
                        formIsValid = false;

                    if (!Consent.HasTransplant && !InverterConsent.HasTransplant)
                        formIsValid = false;

                    if (formIsValid)
                    {
                        IsNextButtonVisible = false;
                        IsConfirmButtonVisible = true;
                        PositionView = 5;
                    }
                    else
                    {
                        await _dialogService.DisplayAlertAsync("Ups", "Necesitas completar el formulario para poder seguir adelante.", "OK");
                    }

                    break;
                case 5:
                    break;
            }
            ProgressBarIndicator = PositionView / 5.0f;
        }

        private void OnBackCommandExecute()
        {
            switch (PositionView)
            {
                case 0:
                    break;
                case 1:
                    IsBackButtonVisible = false;
                    PositionView = 0;
                    break;
                case 2:
                    PositionView = 1;
                    break;
                case 3:
                    PositionView = 2;
                    break;
                case 4:
                    PositionView = 3;
                    break;
                case 5:
                    IsNextButtonVisible = true;
                    IsConfirmButtonVisible = false;
                    PositionView = 4;
                    break;
            }
            ProgressBarIndicator = PositionView / 5.0f;
        }
    }
}
