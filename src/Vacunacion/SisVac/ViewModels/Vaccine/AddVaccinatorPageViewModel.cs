using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using SisVac.Framework.Domain;
using SisVac.Framework.Http;
using SisVac.Framework.Services;
using SisVac.Framework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace SisVac.ViewModels.Vaccine
{
    public class AddVaccinatorPageViewModel : ScanDocumentViewModel
    {
        List<ApplicationUser> _vaccinatorsList;
        ApplicationUser _vaccinatorUser = new ApplicationUser();

        public AddVaccinatorPageViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService,
            IScannerService scannerService,
            ICitizensApiClient citizensApiClient,
            ICacheService cacheService) : base(navigationService, dialogService, scannerService, cacheService, citizensApiClient)
        {
            ValidateDocumentCommand = new DelegateCommand(OnValidateDocumentCommandExecute);
            DocumentScanned = id => OnValidateDocumentCommandExecute();
        }


        #region Commands
        public ICommand AddCommand { get; set; }
        public ICommand ValidateDocumentCommand { get; set; }
        #endregion

        private async void OnValidateDocumentCommandExecute()
        {
            if (DocumentID.Validate())
            {
                int duplicates = _vaccinatorsList.GroupBy(i => i.Document)
                                                 .Where(i => i.Count() > 1)
                                                 .Sum(i => i.Count());

                if (duplicates > 0)
                {
                    await _dialogService.DisplayAlertAsync("Ups", "Ya tienes esta persona registrada como vacunador.", "Ok");
                    return;
                }

                var userData = await GetDocumentData(DocumentID.Value);
                if (userData != null)
                {
                    _vaccinatorUser = new ApplicationUser
                    {
                        Age = userData.Age,
                        Document = userData.Cedula,
                        FullName = userData.Name
                    };

                    var response = await _dialogService.DisplayAlertAsync("Confirmación", $"¿Estás seguro de agregar a {userData.Name} como vacunador?", "Si", "No");

                    if (response)
                    {
                        if (_vaccinatorsList?.Count > 0)
                            _vaccinatorsList.Add(_vaccinatorUser);
                        else
                            _vaccinatorsList = new List<ApplicationUser>() { _vaccinatorUser };

                        await _cacheService.InsertLocalObject(CacheKeyDictionary.VaccinatorsList, _vaccinatorsList);

                        var navigationParams = new NavigationParameters
                        {
                            { NavigationKeys.VaccinatorAdded, _vaccinatorsList },
                        };
                        await _navigationService.GoBackAsync(navigationParams);
                    }
                }
                else
                {
                    await _dialogService.DisplayAlertAsync("Ups", "No pudimos validar este documento.", "OK");
                }
            }
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            _vaccinatorsList = await _cacheService.GetLocalObject<List<ApplicationUser>>(CacheKeyDictionary.VaccinatorsList);
        }
    }
}
