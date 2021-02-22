using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using SisVac.Framework.Domain;
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

        }

        public ICommand SelectedItemCommand { get; set; }
        public IEnumerable<string> Centers { get; set; }
        public IEnumerable<ClinicLocation> ClinicLocations { get; set; }
        public int CenterIndexSelected { get; set; }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            ClinicLocations = (await App.Database.Connection.DeferredQueryAsync<ClinicLocation>(""));
            Centers = ClinicLocations.Select(x=>x.Name);
        }

        private async void SelectedItemCommandExecute()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            var centerName = Centers.ElementAt(CenterIndexSelected);
            //TODO Find a way to match the radio button to the Clinic Location object
            var clinicId = ClinicLocations.Where(x=>x.Name == centerName);

            var navigationParams = new NavigationParameters
            {
                { "selectedClinicLocationName", centerName },
                { "selectedClinicLocationid", clinicId }
            };

            await _navigationService.GoBackAsync(navigationParams);

            IsBusy = false;
        }

    }
}
