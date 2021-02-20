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

    }
}
