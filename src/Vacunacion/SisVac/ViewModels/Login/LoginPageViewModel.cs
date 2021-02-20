using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SisVac.Framework.Domain;
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
        public ICommand ScanDocumentCommand { get; set; }
        public ICommand LoginCommand { get; set; }

        public LoginPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Document = "000-0000000-0";
            ScanDocumentCommand = new DelegateCommand(ScanDocumentCommandExecute);
            LoginCommand = new DelegateCommand(LoginCommandExecute);
        }

        async void ScanDocumentCommandExecute()
        {
            var scanner = new ZXing.Mobile.MobileBarcodeScanner();
            var result = await scanner.Scan();
            if(result != null)
                Document = result.Text;
        }
        async void LoginCommandExecute()
        {
            //{prism:NavigateTo 'ConfirmSignIn'}"
            //TODO Get user info from web service
            App.User = new ApplicationUser
            {
                Age=30,
                Document=Document,
                FullName = "Isbel C. Bautista"
            };
            await _navigationService.NavigateAsync("ConfirmSignIn");
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            HasNavigationBar = false;
        }
    }
}
