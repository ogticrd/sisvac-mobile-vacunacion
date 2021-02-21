using System;
using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Forms;

namespace SisVac.ViewModels.Login
{
    public class ConfirmLoginPageViewModel : BaseViewModel
    {
        public string LocationId { get; set; }
        public string LocationName { get; set; }
        public bool ShowLocationErrorMessage { get; set; }

        public ICommand LocationSelectedCommand { get; set; }
        public ICommand ConfirmLoginCommand { get; set; }

        public ConfirmLoginPageViewModel(INavigationService navigationService) : base(navigationService)
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
            if(String.IsNullOrEmpty(LocationName))
            {
                ShowLocationErrorMessage = true;
            }
            else
            {
                ShowLocationErrorMessage = false;
                await _navigationService.NavigateAsync("/NavigationPage/HomePage");
            }
            //            { prism: NavigateTo '/NavigationPage/HomePage'}
        }
    }
}
