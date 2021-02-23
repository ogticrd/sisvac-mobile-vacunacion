using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using SisVac.Framework.Domain;
using SisVac.Framework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace SisVac.ViewModels.Login
{
    public class SearchCenterPageViewModel : BaseViewModel
    {
        public SearchCenterPageViewModel(INavigationService navigationService, IPageDialogService dialogService, ICacheService cacheService) : base(navigationService, dialogService, cacheService)
        {
            SelectedItemCommand = new DelegateCommand(OnSelectedItemCommandExecute);
            FilterTextChangedCommand = new DelegateCommand<string>(OnFilterTextChangedCommandExecute);
            Centers = new List<string>{
                "Lista vacía"
            };
        }

        public ICommand FilterTextChangedCommand { get; set; }
        public ICommand SelectedItemCommand { get; set; }
        public List<string> Centers { get; set; }
        public IEnumerable<ClinicLocation> ClinicLocations { get; set; }

        public string SearchText { get; set; }
        public int CenterIndexSelected { get; set; }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            IsBusy = true;
            ClinicLocations = await App.Database.Connection.Table<ClinicLocation>().OrderBy(x=>x.Name).Take(10).ToListAsync();
            Centers = ClinicLocations.Select(x=>x.Name).ToList();
            IsBusy = false;
        }

        private async void OnFilterTextChangedCommandExecute(string newText)
        {
            IsBusy = true;
            var lowerCaseText = newText.ToLower();
            ClinicLocations = await App.Database.Connection.Table<ClinicLocation>().Where(x=>x.Name.ToLower().Contains(lowerCaseText)).OrderBy(x => x.Name).Take(10).ToListAsync();
            Centers = ClinicLocations.Select(x => x.Name).ToList();
            IsBusy = false;
        }

        private async void OnSelectedItemCommandExecute()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            var centerName = Centers.ElementAt(CenterIndexSelected);
            //TODO Find a way to match the radio button to the Clinic Location object
            var clinic = ClinicLocations.Where(x=>x.Name == centerName).FirstOrDefault();

            var navigationParams = new NavigationParameters
            {
                { "selectedClinicLocationName", centerName },
                { "selectedClinicLocationId", clinic.Id }
            };

            await _navigationService.GoBackAsync(navigationParams);

            IsBusy = false;
        }

    }
}
