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
        public HomePageViewModel(INavigationService navigationService, IPageDialogService dialogService, ICacheService cacheService) : base(navigationService, dialogService, cacheService)
        {
            _cacheService = cacheService;
        }

        public ApplicationUser User { get; set; }
        public ApplicationUser Vaccinator { get; set; }
        public ClinicLocation Location { get; set; }

        private async Task Init()
        {

            User = await _cacheService.GetLocalObject<ApplicationUser>(CacheKeyDictionary.UserInfo);
            Vaccinator = await _cacheService.GetLocalObject<ApplicationUser>(CacheKeyDictionary.VaccinatorInfo);
            Location = await _cacheService.GetLocalObject<ClinicLocation>(CacheKeyDictionary.CenterInfo);
        }


        public override async void OnNavigatedTo(INavigationParameters parameters)
        {

            await Init();
            //if (parameters.ContainsKey("user"))
            //    User = parameters.GetValue<ApplicationUser>("user");
        }
    }
}
