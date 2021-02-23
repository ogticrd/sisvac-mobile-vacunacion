using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using SisVac.Framework.Domain;
using SisVac.Framework.Http;
using SisVac.Framework.Services;
using System;
using System.Windows.Input;
using XF.Material.Forms.UI.Dialogs;

namespace SisVac.ViewModels.Report
{
    public class ReportEffectsPageViewModel : ScanDocumentViewModel
    {
        public ReportEffectsPageViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService,
            IScannerService scannerService,
            ICitizensApiClient citizensApiClient, 
            ICacheService cacheService) : base(navigationService, dialogService, scannerService, cacheService, citizensApiClient)
        {
            NextCommand = new DelegateCommand(OnNextCommandExecute);
            BackCommand = new DelegateCommand(OnBackCommandExecute);
            ValidateDocumentCommand = new DelegateCommand(OnValidateDocumentCommandExecute);
            DocumentScanned = id => OnConfirmCommandExecute();
        }

        #region Properties
        public Person Patient { get; set; } = new Person();
        public int PositionView { get; set; }
        public string ButtonPrimaryText { get; set; } = "Siguiente";
        public bool IsBackButtonVisible { get; set; } = false;
        public double ProgressBarIndicator { get; set; }
        public string ProgressTextIndicator
        {
            get
            {
                return $"Paso {PositionView + 1} de 2";
            }
        } 
        #endregion


        #region Commands
        public ICommand NextCommand { get; set; }
        public ICommand ValidateDocumentCommand { get; set; }
        public ICommand BackCommand { get; }
        #endregion

        private async void OnNextCommandExecute()
        {
            switch (PositionView)
            {
                case 0:
                    OnValidateDocumentCommandExecute();
                    break;
                case 1:
                    OnConfirmCommandExecute();
                    break;
                default:
                    break;
            }

        }

        private async void OnValidateDocumentCommandExecute()
        {
            if (DocumentID.Validate())
            {
                var userData = await GetDocumentData(DocumentID.Value);
                if (userData != null)
                {
                    Patient = new ApplicationUser
                    {
                        Age = userData.Age,
                        Document = userData.Cedula,
                        FullName = userData.Name
                    };

                    UpdateUI(true, 1, "Confirmar");
                }
                else
                {
                    await _dialogService.DisplayAlertAsync("Ups", "No pudimos validar este documento.", "OK");
                }
            }           
        }

        private async void OnConfirmCommandExecute()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Validando..."))
            {
                // TODO: Call API Here
                // TODO: Send confirmation to the server
            }

            await _dialogService.DisplayAlertAsync("Proceso finalizado", "Has terminado satisfactoriamente.", "Ok");
            await _navigationService.GoBackAsync();

            IsBusy = false;
        }

        private void OnBackCommandExecute()
        {
            switch (PositionView)
            {
                case 0:
                    break;
                case 1:
                    UpdateUI();
                    break;
                default:
                    break;
            }
        }

        void UpdateUI(bool isBackButtonVisible = false, int position = 0, string nextButtonText = "Siguiente") 
        {
            IsBackButtonVisible = isBackButtonVisible;
            PositionView = position;
            ProgressBarIndicator = PositionView / 2.0f;
            ButtonPrimaryText = nextButtonText;
        }
    }
}
