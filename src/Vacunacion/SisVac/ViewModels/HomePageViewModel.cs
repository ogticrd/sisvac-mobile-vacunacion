using System;
using System.Threading.Tasks;
using Prism.Navigation;
using Prism.Services;
using SisVac.Framework.Domain;
using SisVac.Framework.Services;

namespace SisVac.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        ICacheService _cacheService;
        public HomePageViewModel(INavigationService navigationService, IPageDialogService dialogService, ICacheService cacheService) : base(navigationService, dialogService)
        {
            _cacheService = cacheService;
        }

        private async Task Init()
        {

            User = await _cacheService.GetLocalObject<ApplicationUser>(CacheKeyDictionary.UserInfo);
            Vaccionator = await _cacheService.GetLocalObject<ApplicationUser>(CacheKeyDictionary.VaccinatorInfo);
        }


        public override async void OnNavigatedTo(INavigationParameters parameters)
        {

            await Init();
            //if (parameters.ContainsKey("user"))
            //    User = parameters.GetValue<ApplicationUser>("user");
        }
    }
}
