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
            ICitizensApiClient citizensApiClient) : base(navigationService, dialogService, scannerService, citizensApiClient)
        {
            ConfirmCommand = new DelegateCommand(OnConfirmCommandExecute);
            ValidateDocumentCommand = new DelegateCommand(OnValidateDocumentCommandExecute);
            DocumentScanned = id => OnConfirmCommandExecute();
        }

        public ICommand ConfirmCommand { get; set; }
        public ICommand ValidateDocumentCommand { get; set; }
        public bool IsUserValid { get; set; }
        public Person Patient { get; set; } = new Person();

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

                    IsUserValid = true;
                }
                else
                {
                    IsUserValid = false;
                    await _dialogService.DisplayAlertAsync("Ups", "No pudimos validar este documento.", "OK");
                }
            }           
        }

        private async void OnConfirmCommandExecute()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            if (!IsUserValid)
            {
                await _dialogService.DisplayAlertAsync("Ups", "Necesitas validar el documento primero.", "OK");
                IsBusy = false;
                return;
            }

            using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Validando..."))
            {
                // TODO: Call API Here
                // TODO: Send confirmation to the server
            }

            await _navigationService.GoBackAsync();
            await MaterialDialog.Instance.SnackbarAsync(message: "Proceso terminado satisfactoriamente.",
                                        actionButtonText: "OK",
                                        msDuration: 8000);

            IsBusy = false;
        }
    }
}
