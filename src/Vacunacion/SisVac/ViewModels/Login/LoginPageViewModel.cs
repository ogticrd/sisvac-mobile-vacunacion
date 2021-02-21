using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SisVac.Framework.Domain;
using SisVac.Framework.Extensions;
using SisVac.Framework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SisVac.ViewModels.Login
{
    public class LoginPageViewModel : ScanDocumentViewModel
    {
        public bool DocumentHasError { get; set; }
        public ICommand LoginCommand { get; set; }

        public LoginPageViewModel(INavigationService navigationService, IScannerService scannerService) : base(navigationService, scannerService)
        {
            Document = "";
            LoginCommand = new DelegateCommand(OnLoginCommandExecute);
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
    }
}
