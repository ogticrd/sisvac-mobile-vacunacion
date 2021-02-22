using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace SisVac.ViewModels.Login
{
    public class SearchCenterPageViewModel : BaseViewModel
    {
        public SearchCenterPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            SelectedItemCommand = new DelegateCommand(SelectedItemCommandExecute);

            Centers = new List<string>();
            Centers.Add("Centro 1");
            Centers.Add("Centro 2");
        }

        public ICommand SelectedItemCommand { get; set; }
        public List<string> Centers { get; set; }
        public int CenterIndexSelected { get; set; }

        private async void SelectedItemCommandExecute()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            var navigationParams = new NavigationParameters
            {
                { "selectedItem", Centers[CenterIndexSelected] }
            };

            await _navigationService.GoBackAsync(navigationParams);

            IsBusy = false;
        }

    }
}
