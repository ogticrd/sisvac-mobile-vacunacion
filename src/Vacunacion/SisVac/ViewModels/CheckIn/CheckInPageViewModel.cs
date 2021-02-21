using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Prism.Services.Dialogs;
using SisVac.Framework.Services;
using System.Windows.Input;
using XF.Material.Forms.UI.Dialogs;

namespace SisVac.ViewModels.CheckIn
{
    public class CheckInPageViewModel : ScanDocumentViewModel
    {
        public CheckInPageViewModel(
            INavigationService navigationService,
            IScannerService scannerService) : base(navigationService, scannerService)
        {
            NextCommand = new DelegateCommand(OnNextCommandExecute);
            BackCommand = new DelegateCommand(OnBackCommandExecute);
            ConfirmCommand = new DelegateCommand(OnConfirmCommandExecute);
            ProgressBarIndicator = 0.0f;
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
                return $"Paso {PositionView+1} de 5";
            }
        }
        public ICommand NextCommand { get; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand BackCommand { get; }

        private async void OnConfirmCommandExecute()
        {
            //TODO Send confirmation to the server
            await _navigationService.NavigateAsync("/NavigationPage/HomePage");
            await MaterialDialog.Instance.SnackbarAsync(message: "Proceso terminado satisfactoriamente.",
                                        actionButtonText: "OK",
                                        msDuration: 8000);
        }

        private void OnNextCommandExecute()
        {
            switch (PositionView)
            {
                case 0:
                    IsBackButtonVisible = true;
                    PositionView = 1;
                    break;
                case 1:
                    PositionView = 2;
                    break;
                case 2:
                    PositionView = 3;
                    break;
                case 3:
                    IsNextButtonVisible = false;
                    IsConfirmButtonVisible = true;
                    PositionView = 4;
                    break;
                case 4:
                    break;
            }
            ProgressBarIndicator = PositionView / 4.0f;
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
                    IsNextButtonVisible = true;
                    IsConfirmButtonVisible = false;
                    PositionView = 3;
                    break;
            }
            ProgressBarIndicator = PositionView / 4.0f;
        }
    }
}
