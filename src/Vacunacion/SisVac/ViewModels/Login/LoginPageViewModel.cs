using Plugin.ValidationRules;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using SisVac.Framework.Domain;
using SisVac.Framework.Extensions;
using SisVac.Framework.Http;
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


        public LoginPageViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService,
            IScannerService scannerService,
            ICitizensApiClient citizensApiClient) : base(navigationService, dialogService, scannerService, citizensApiClient)
        {
            LoginCommand = new DelegateCommand(OnLoginCommandExecute);
        }

        async void OnLoginCommandExecute()
        {
            if (DocumentID.Validate())
            {
                var userData = await GetDocumentData(DocumentID.Value);
                if (userData != null)
                {
                    App.User = new ApplicationUser
                    {
                        Age = userData.Age,
                        Document = DocumentID.Value,
                        FullName = userData.Name,
                        LocationName = string.Empty
                    };
                    await _navigationService.NavigateAsync("ConfirmLoginPage");
                }
                else
                {
                    await _dialogService.DisplayAlertAsync("Ocurrió algo inesperado", "El número de cédula no existe", "OK");
                }
            }
        }
    }
}
