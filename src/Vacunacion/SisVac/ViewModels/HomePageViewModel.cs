using System;
using Prism.Navigation;
using Prism.Services;
using SisVac.Framework.Domain;

namespace SisVac.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        public HomePageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
        }

        public new ApplicationUser User { get; set; }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("user"))
                User = parameters.GetValue<ApplicationUser>("user");
        }
    }
}
