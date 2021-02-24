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

        ApplicationUser _user;
        public ApplicationUser User
        {
            get => _user;
            set => _user = value; 
        }

        ApplicationUser _vaccinator;
        public ApplicationUser Vaccinator
        {
            get => _vaccinator;
            set => _vaccinator = value;
        }

        private async Task Init()
        {
            User = await _cacheService.GetLocalObject<ApplicationUser>(CacheKeyDictionary.UserInfo);
            Vaccinator = await _cacheService.GetLocalObject<ApplicationUser>(CacheKeyDictionary.VaccinatorInfo);
        }


        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            await Init();
        }
    }
}
