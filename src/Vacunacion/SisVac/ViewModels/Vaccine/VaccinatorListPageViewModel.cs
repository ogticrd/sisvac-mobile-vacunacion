using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using SisVac.Framework.Domain;
using SisVac.Framework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SisVac.ViewModels.Vaccine
{
    public class VaccinatorListPageViewModel : BaseViewModel
    {
        List<ApplicationUser> _vaccinators;
        ApplicationUser _vaccinator;
        static readonly string _emptyListName = "Lista vacía";

        public VaccinatorListPageViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService,
            ICacheService cacheService) : base(navigationService, dialogService,  cacheService)
        {

            SelectedItemCommand = new DelegateCommand(OnSelectedItemCommandExecute);
        }

        public List<string> VaccinatorsName { get; set; } = new List<string>() { _emptyListName };
        public int IndexSelected { get; set; }

        public ICommand SelectedItemCommand { get; set; }

        async Task Init()
        {
            try
            {
                if (_vaccinator == null)
                    _vaccinator = await _cacheService.GetLocalObject<ApplicationUser>(CacheKeyDictionary.VaccinatorInfo);

                if (_vaccinators == null)                
                    _vaccinators = await _cacheService.GetLocalObject<List<ApplicationUser>>(CacheKeyDictionary.VaccinatorsList);
                    
                if (_vaccinators?.Count > 0)
                {
                    IndexSelected = _vaccinators.IndexOf(_vaccinator);
                }
                else
                {
                    _vaccinators = new List<ApplicationUser>();

                    if (_vaccinator != null)
                        _vaccinators.Add(_vaccinator);
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                UpdateUI();
            }
        }

        private async void OnSelectedItemCommandExecute()
        {
            if (_vaccinators?.Count > 0 && VaccinatorsName[IndexSelected] != _emptyListName)
            {
                IndexSelected = _vaccinators.IndexOf(_vaccinator);
                var vaccinatorDefault = _vaccinators[IndexSelected];

                if (vaccinatorDefault != null)
                {
                    await _cacheService.InsertLocalObject(CacheKeyDictionary.VaccinatorInfo, vaccinatorDefault);
                    await _navigationService.GoBackAsync();
                }
            }
            else
            {
                await _dialogService.DisplayAlertAsync("Necesitas un vacunador", "Para continuar necesitas un vacunador previamente registrado.", "OK");
            }
        }

        void UpdateUI()
        {
            if (_vaccinators?.Count > 0)
                VaccinatorsName = _vaccinators.Select(v => v.FullName).ToList();
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("vaccinatorAdded"))
            {
                var newVaccinator = parameters.GetValue<ApplicationUser>("vaccinatorAdded");

                if (newVaccinator != null)
                {
                    _vaccinators.Add(newVaccinator);
                }                
            }

            await Init();
        }
    }
}
