using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using SisVac.Framework.Domain;
using SisVac.Framework.Http;
using SisVac.Framework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using XF.Material.Forms.UI.Dialogs;

namespace SisVac.ViewModels.Vaccine
{
    public class TrackingVaccinePageViewModel : ScanDocumentViewModel
    {
        public TrackingVaccinePageViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService,
            IScannerService scannerService,
            ICitizensApiClient citizensApiClient,
            ICacheService cacheService) : base(navigationService, dialogService, scannerService, cacheService, citizensApiClient)
        {
            _dialogService = dialogService;
            NextCommand = new DelegateCommand(OnNextCommandExecute);
            BackCommand = new DelegateCommand(OnBackCommandExecute);
            ConfirmCommand = new DelegateCommand(OnConfirmCommandExecute);
            ProgressBarIndicator = 0.0f;

            DocumentScanned = id => OnNextCommandExecute();
        }
        public int PositionView { get; set; }
        public bool IsBackButtonVisible { get; set; } = false;
        public bool IsNextButtonVisible { get; set; } = true;
        public bool IsConfirmButtonVisible { get; set; } = false;
        public double ProgressBarIndicator { get; set; }
        public string ProgressTextIndicator
        {
            get
            {
                return $"Paso {PositionView + 1} de 4";
            }
        }
        public Person Patient { get; set; } = new Person();

        public ICommand NextCommand { get; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand BackCommand { get; }

        public string LotName { get; set; }

        private async void OnConfirmCommandExecute()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            var result = await _dialogService.DisplayAlertAsync("Confirmación para la aplicación de dosis", "Primero aplica la dosis de la vacuna al paciente, después confirma la aplicación de la dosis.", "Confirmar", "Cancelar");
            
            if (result)
            {
                using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Validando..."))
                {
                    // TODO: Call API Here
                    // TODO: Send confirmation to the server
                }

                await _navigationService.NavigateAsync("/NavigationPage/HomePage");
                    await MaterialDialog.Instance.SnackbarAsync(message: "Proceso terminado satisfactoriamente.",
                                                actionButtonText: "OK",
                                                msDuration: 8000);
             }
            
            IsBusy = false;
        }

        private async void OnNextCommandExecute()
        {
            switch (PositionView)
            {
                case 0:
                    if (DocumentID.Validate())
                    {
                        var patientData = await GetDocumentData(DocumentID.Value);
                        Patient = new Person
                        {
                            Age = patientData.Age,
                            Document = DocumentID.Value,
                            FullName = patientData.Name
                        };

                        IsBackButtonVisible = true;
                        PositionView = 1;
                    }
                    break;
                case 1:
                    PositionView = 2;
                    break;
                case 2:
                    IsNextButtonVisible = false;
                    IsConfirmButtonVisible = true;
                    PositionView = 3;
                    break;
                case 3:
                    break;
            }
            ProgressBarIndicator = PositionView / 3.0f;
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
                    IsNextButtonVisible = true;
                    IsConfirmButtonVisible = false;
                    PositionView = 2;
                    break;
            }
            ProgressBarIndicator = PositionView / 3.0f;
        }
    }
}
