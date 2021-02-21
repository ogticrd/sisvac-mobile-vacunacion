using System;
using System.Windows.Input;
using Prism.Navigation;
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

        public string Title { get; set; }
        public bool IsBusy { get; set; }
        public bool HasNavigationBar { get; set; }

        protected INavigationService _navigationService;

        public BaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }


        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }
    }

}
