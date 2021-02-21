using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SisVac.Framework.Domain;
using SisVac.Framework.Extensions;
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
        public string Document { get; set; }
        public bool DocumentHasError { get; set; }

        public ICommand ScanDocumentCommand { get; set; }
        public ICommand LoginCommand { get; set; }

        public LoginPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Document = "";
            ScanDocumentCommand = new DelegateCommand(OnScanDocumentCommandExecute);
            LoginCommand = new DelegateCommand(OnLoginCommandExecute);
        }

        async void OnScanDocumentCommandExecute()
        {
            var scanner = new ZXing.Mobile.MobileBarcodeScanner();
            var result = await scanner.Scan();
            if(result != null)
                Document = result.Text;
        }
        async void OnLoginCommandExecute()
        {
            if (!string.IsNullOrWhiteSpace(Document) && Document.IsValidDocument())
            {
                DocumentHasError = false;
                //{prism:NavigateTo 'ConfirmSignIn'}"
                //TODO Get user info from web service
                App.User = new ApplicationUser
                {
                    Age = 30,
                    Document = Document,
                    FullName = "Isbel C. Bautista"
                };
                await _navigationService.NavigateAsync("ConfirmLoginPage");
            }
            else
            {
                DocumentHasError = true;
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            HasNavigationBar = false;
        }
    }
}
