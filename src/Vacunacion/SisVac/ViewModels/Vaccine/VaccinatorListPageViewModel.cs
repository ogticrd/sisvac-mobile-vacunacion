using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using SisVac.Framework.Domain;
using SisVac.Framework.Services;
using SisVac.Framework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SisVac.ViewModels.Vaccine
{
    public class VaccinatorListPageViewModel : BaseViewModel
    {
        List<ApplicationUser> _vaccinatorsList;
        ApplicationUser _vaccinatorUser;
        static readonly string _emptyListName = "Lista vacía";

        public VaccinatorListPageViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService,
            ICacheService cacheService) : base(navigationService, dialogService,  cacheService)
        {

            SelectedItemCommand = new DelegateCommand(OnSelectedItemCommandExecute);
        }

        public List<string> VaccinatorsName { get; set; } = new List<string>() { _emptyListName };
        public int IndexSelected { get; set; } = 0;

        public ICommand SelectedItemCommand { get; set; }

        async Task Init()
        {
            try
            {
                if (_vaccinatorUser == null)
                    _vaccinatorUser = await _cacheService.GetLocalObject<ApplicationUser>(CacheKeyDictionary.VaccinatorInfo);

                if (_vaccinatorsList == null)                
                    _vaccinatorsList = await _cacheService.GetLocalObject<List<ApplicationUser>>(CacheKeyDictionary.VaccinatorsList);
                    
                if (_vaccinatorsList == null && _vaccinatorUser != null)                
                    _vaccinatorsList = new List<ApplicationUser>() { _vaccinatorUser };
            }
            catch (Exception)
            {
            }
            finally
            {
                // Update the List in UI
                if (_vaccinatorsList?.Count > 0)
                {
                    VaccinatorsName = _vaccinatorsList.Select(v => v.FullName).ToList();
                    await Task.Run(() => IndexSelected = _vaccinatorsList.FindIndex(v => v.Document == _vaccinatorUser.Document));
                }    
            }
        }

        private async void OnSelectedItemCommandExecute()
        {
            if (_vaccinatorsList?.Count > 0 && IndexSelected != -1)
            {
                var vaccinatorDefault = _vaccinatorsList[IndexSelected];

                if (vaccinatorDefault != null)
                {
                    var response = await _dialogService.DisplayAlertAsync("Confirmación", $"¿Estás seguro de seleccionar este vacunador por defecto?", "Si", "No");
                    if (response)
                    {
                        await _cacheService.InsertLocalObject(CacheKeyDictionary.VaccinatorInfo, vaccinatorDefault);
                        await _navigationService.GoBackAsync();
                    }
                }
            }
            else
            {
                await _dialogService.DisplayAlertAsync("Necesitas un vacunador", "Para continuar necesitas un vacunador previamente registrado.", "OK");
            }
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(NavigationKeys.VaccinatorAdded))
            {
                var vaccinators = parameters.GetValue<List<ApplicationUser>>(NavigationKeys.VaccinatorAdded);

                if (vaccinators != null)                
                    _vaccinatorsList = vaccinators;      
            }

            await Init();
        }
    }
}
