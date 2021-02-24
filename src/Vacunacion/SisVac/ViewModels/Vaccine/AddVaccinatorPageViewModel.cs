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


        public ApplicationUser Vaccinator { get; set; } = new ApplicationUser();

        #region Commands
        public ICommand AddCommand { get; set; }
        public ICommand ValidateDocumentCommand { get; set; }
        #endregion

        private async void OnValidateDocumentCommandExecute()
        {
            if (DocumentID.Validate())
            {
                await GetQualificationData(DocumentID.Value);

                if (!Qualification.IsValidDocument)
                {
                    await _dialogService.DisplayAlertAsync("Ups", "Documento no valido.", "Ok");
                    return;
                }

                var userData = await GetDocumentData(DocumentID.Value);
                if (userData != null)
                {
                    Vaccinator = new ApplicationUser
                    {
                        Age = userData.Age,
                        Document = userData.Cedula,
                        FullName = userData.Name
                    };

                    var response = await _dialogService.DisplayAlertAsync("Confirmación", $"¿Estás seguro de agregar a {userData.Name} como vacunador?", "Si", "No");

                    if (response)
                    {
                        var vaccinators = await _cacheService.GetLocalObject<List<ApplicationUser>>(CacheKeyDictionary.VaccinatorsList);

                        if (vaccinators?.Count > 0)
                            vaccinators.Add(Vaccinator);
                        else
                            vaccinators = new List<ApplicationUser>() { Vaccinator };

                        await _cacheService.InsertLocalObject(CacheKeyDictionary.VaccinatorsList, vaccinators);

                        var navigationParams = new NavigationParameters
                        {
                            { NavigationKeys.VaccinatorAdded, vaccinators },
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

        private void OnAddCommandExecute()
        {
            
        }
    }
}
