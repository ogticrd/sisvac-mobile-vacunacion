using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SisVac.ViewModels.Login
{
    public class LoginPageViewModel : BaseViewModel
    {
        public string DocumentNumber { get; set; }
        public ICommand ScanDocumentCommand { get; set; }
        public LoginPageViewModel()
        {
            DocumentNumber = "000-0000000-0";
            ScanDocumentCommand = new DelegateCommand(ScanDocumentCommandExecute);
        }

        async void ScanDocumentCommandExecute()
        {
            var scanner = new ZXing.Mobile.MobileBarcodeScanner();
            var result = await scanner.Scan();
            if(result != null)
                DocumentNumber = result.Text;
        }
    }
}
