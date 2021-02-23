using System;
using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using PropertyChanged;
using SisVac.Framework.Domain;
using SisVac.Framework.Services;
using SisVac.Framework.Utils;

namespace SisVac.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : INavigationAware
    {
        public ApplicationUser User
        {
            get => App.User;
            set { App.User = value; }
        }
        public ApplicationUser Vaccinator
        {
            get => App.Vaccinator;
            set { App.Vaccinator = value; }
        }
        
        public string Title { get; set; }
        public bool IsBusy { get; set; }
        public ICommand LogoutCommand { get; set; }

        protected ICacheService _cacheService;
        protected IPageDialogService _dialogService;
        protected INavigationService _navigationService;

        public BaseViewModel(INavigationService navigationService, IPageDialogService dialogService, ICacheService cacheService)
        {
            _cacheService = cacheService;
            _navigationService = navigationService;
            _dialogService = dialogService;
            LogoutCommand = new DelegateCommand(OnLogoutCommandExecute);
        }

        private async void OnLogoutCommandExecute()
        {
            await _cacheService.RemoveLocalObject(CacheKeyDictionary.UserInfo);
            await _cacheService.RemoveLocalObject(CacheKeyDictionary.VaccinatorInfo);

            Settings.RemoveAllSettings();
            await _navigationService.NavigateAsync("/NavigationPage/LoginPage");
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }
    }
}
