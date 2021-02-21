using System;
using System.Windows.Input;
using Prism.Navigation;
using SisVac.Framework.Domain;
using SisVac.Framework.Services;
using Xamarin.Forms;

namespace SisVac.ViewModels.Login
{
    public class ConfirmLoginPageViewModel : ScanDocumentViewModel
    {
        public string LocationId { get; set; }
        public string LocationName { get; set; }
        public bool ShowLocationErrorMessage { get; set; }

        public ICommand LocationSelectedCommand { get; set; }
        public ICommand ConfirmLoginCommand { get; set; }

        public ConfirmLoginPageViewModel(INavigationService navigationService, IScannerService scannerService) : base(navigationService, scannerService)
        {
            LocationSelectedCommand = new Command<string>(LocationSelectedCommandExecute);
            ConfirmLoginCommand = new Command(ConfirmLoginCommandExecute);
        }
        async void LocationSelectedCommandExecute(string selectedOption)
        {
            LocationName = selectedOption;
        }

        async void ConfirmLoginCommandExecute()
        {
            if(String.IsNullOrEmpty(LocationName) && !DocumentID.Validate())
            {
                ShowLocationErrorMessage = true;
            }
            else
            {
                ShowLocationErrorMessage = false;

                //TODO Get user info from web service
                App.User = new ApplicationUser
                {
                    Age = 30,
                    Document = DocumentID.Value,
                    FullName = "Isbel C. Bautista"
                };

                var parameters = new NavigationParameters();
                parameters.Add("user", User);

                await _navigationService.NavigateAsync("/NavigationPage/HomePage", parameters);
            }
            //            { prism: NavigateTo '/NavigationPage/HomePage'}
        }
    }
}
