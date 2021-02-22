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
        ICacheService _cacheService;

        public LoginPageViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService,
            IScannerService scannerService,
            ICitizensApiClient citizensApiClient, ICacheService cacheService) : base(navigationService, dialogService, scannerService, citizensApiClient)
        {
            _cacheService = cacheService;
            LoginCommand = new DelegateCommand(OnLoginCommandExecute);

            DocumentScanned = id => OnLoginCommandExecute();
        }

        public bool DocumentHasError { get; set; }
        public ICommand LoginCommand { get; set; }

        async void OnLoginCommandExecute()
        {
            if (DocumentID.Validate())
            {
                var userData = await GetDocumentData(DocumentID.Value);
                if (userData != null)
                {
                    var user = new ApplicationUser
                    {
                        Age = userData.Age,
                        Document = DocumentID.Value,
                        FullName = userData.Name,
                        LocationName = string.Empty
                    };
                    App.User = user;
                    await _cacheService.InsertLocalObject(CacheKeyDictionary.UserInfo, user);
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
