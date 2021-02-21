using System;
using Prism.Navigation;
using Prism.Services;

namespace SisVac.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        public HomePageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
        }
    }
}
