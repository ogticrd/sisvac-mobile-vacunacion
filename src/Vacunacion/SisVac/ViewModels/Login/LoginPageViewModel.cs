using Plugin.ValidationRules;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SisVac.Framework.Domain;
using SisVac.Framework.Extensions;
using SisVac.Framework.Services;
using SisVac.Helpers.Rules;
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
            LoginCommand = new DelegateCommand(OnLoginCommandExecute);
        }

        async void OnLoginCommandExecute()
        {
            if (DocumentID.Validate())
            {
                //{prism:NavigateTo 'ConfirmSignIn'}"
                //TODO Get user info from web service
                App.User = new ApplicationUser
                {
                    Age = 30,
                    Document = DocumentID.Value,
                    FullName = "Isbel C. Bautista"
                };

                var parameters = new NavigationParameters();
                parameters.Add("user", User);

                await _navigationService.NavigateAsync("ConfirmLoginPage", parameters);
            }
        }
    }
}
