using System;
using System.Windows.Input;
using Plugin.ValidationRules;
using Plugin.ValidationRules.Formatters;
using Prism.Commands;
using Prism.Navigation;
using SisVac.Framework.Services;
using SisVac.Helpers.Rules;

namespace SisVac.ViewModels
{
    public class ScanDocumentViewModel : BaseViewModel
    {
        private IScannerService _scannerService;

        public ScanDocumentViewModel(INavigationService navigationService, IScannerService scannerService) : base(navigationService)
        {
            _scannerService = scannerService;
            ScanDocumentCommand = new DelegateCommand(OnScanDocumentCommandExecute);

            DocumentID = new Validatable<string>();
            DocumentID.Validations.Add(new CedulaRule());
            DocumentID.ValueFormatter = new MaskFormatter("XXX-XXXXXXX-X");
        }

        public Validatable<string> DocumentID { get; set; }
        public ICommand ScanDocumentCommand { get; set; }

        private async void OnScanDocumentCommandExecute()
        {
            await _scannerService.Scan((x) => DocumentID.Value = x);
        }
    }
}
