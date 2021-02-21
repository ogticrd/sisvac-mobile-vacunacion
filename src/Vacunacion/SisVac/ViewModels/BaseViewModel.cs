using System;
using System.Windows.Input;
using Prism.Navigation;
using Prism.Services;
using PropertyChanged;
using SisVac.Framework.Domain;

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
        public ApplicationUser Vaccionator
        {
            get => App.Vaccinator;
            set { App.Vaccinator = value; }
        }
        
        public string Title { get; set; }
        public bool IsBusy { get; set; }

        protected IPageDialogService _dialogService;
        protected INavigationService _navigationService;

        public BaseViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
        }


        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }
    }

}
