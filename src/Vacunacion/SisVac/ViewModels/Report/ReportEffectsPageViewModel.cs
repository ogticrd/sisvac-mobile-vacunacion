using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using SisVac.Framework.Http;
using SisVac.Framework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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

        }

        public ICommand ConfirmCommand { get; set; }

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

            await _navigationService.GoBackAsync();
            await MaterialDialog.Instance.SnackbarAsync(message: "Proceso terminado satisfactoriamente.",
                                        actionButtonText: "OK",
                                        msDuration: 8000);

            IsBusy = false;
        }
    }
}
