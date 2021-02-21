using System;
using System.Windows.Input;
using Prism.Navigation;
using SisVac.Framework.Services;

namespace SisVac.ViewModels
{
    public class ScanDocumentViewModel : BaseViewModel
    {
        public string Document { get; set; }
        public ICommand ScanDocumentCommand { get; set; }

        private IScannerService _scannerService;
        public ScanDocumentViewModel(INavigationService navigationService, IScannerService scannerService) : base(navigationService)
        {
            _scannerService = scannerService;
        }
    }
}
