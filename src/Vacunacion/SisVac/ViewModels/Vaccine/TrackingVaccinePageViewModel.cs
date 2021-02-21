using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
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
        IPageDialogService _dialogService;

        public TrackingVaccinePageViewModel(
            INavigationService navigationService,
            IScannerService scannerService,
            IPageDialogService dialogService) : base(navigationService, scannerService)
        {
            _dialogService = dialogService;
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
                return $"Paso {PositionView + 1} de 4";
            }
        }

        public ICommand NextCommand { get; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand BackCommand { get; }

        private async void OnConfirmCommandExecute()
        {
            //TODO Send confirmation to the server
            var result = await _dialogService.DisplayAlertAsync("Confirmación para la aplicación de dosis", "Primero aplica la dosis de la vacuna al paciente, después confirma la aplicación de la dosis.", "Confirmar", "Cancelar");

            if (result)
            {
                await _navigationService.NavigateAsync("/NavigationPage/HomePage");
                await MaterialDialog.Instance.SnackbarAsync(message: "Proceso terminado satisfactoriamente.",
                                            actionButtonText: "OK",
                                            msDuration: 8000);
            }
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
