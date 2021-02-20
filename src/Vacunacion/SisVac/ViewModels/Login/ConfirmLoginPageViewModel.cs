using System;
using Prism.Navigation;

namespace SisVac.ViewModels.Login
{
    public class ConfirmLoginPageViewModel : BaseViewModel
    {
        public string LocationId { get; set; }

        public ConfirmLoginPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        async void ConfirmLoginCommandExecute()
        {
            await _navigationService.NavigateAsync("HomePage");
//            { prism: NavigateTo '/NavigationPage/HomePage'}
        }
    }
}
