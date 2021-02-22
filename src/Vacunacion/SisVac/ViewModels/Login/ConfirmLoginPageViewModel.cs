using System;
using System.Windows.Input;
using Prism.Navigation;
using Prism.Services;
using SisVac.Framework.Domain;
using SisVac.Framework.Http;
using SisVac.Framework.Services;
using Xamarin.Forms;

namespace SisVac.ViewModels.Login
{
    public class ConfirmLoginPageViewModel : ScanDocumentViewModel
    {
        ICacheService _cacheService;

        public ConfirmLoginPageViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService,
            IScannerService scannerService,
            ICitizensApiClient citizensApiClient, ICacheService cacheService) : base(navigationService, dialogService, scannerService, citizensApiClient)
        {
            _cacheService = cacheService;
            ConfirmLoginCommand = new Command(OnConfirmLoginCommandExecute);
            DocumentScanned = id => OnConfirmLoginCommandExecute();
        }

        public string LocationId { get; set; }
        public string LocationName { get; set; } = "Centros";

        public bool ShowLocationErrorMessage { get; set; }

        public ICommand LocationSelectedCommand { get; set; }
        public ICommand ConfirmLoginCommand { get; set; }


        async void OnConfirmLoginCommandExecute()
        {
            if(String.IsNullOrEmpty(LocationId))
            {
                ShowLocationErrorMessage = true;
            }
            else
            {
                ShowLocationErrorMessage = false;

                if(DocumentID.Value == User.Document)
                {
                    DocumentID.IsValid = false;
                    DocumentID.Error = "La persona encargada del registro, no puede ser vacunador";
                }
                else
                { 
                    if (DocumentID.Validate())
                    { 
                        var userData = await GetDocumentData(DocumentID.Value);
                        if (userData != null)
                        {
                            var user = new ApplicationUser
                            {
                                Age = userData.Age,
                                Document = DocumentID.Value,
                                FullName = userData.Name,
                                LocationId = LocationId,
                                LocationName = LocationName
                            };

                            App.Vaccinator = user;

                            await _cacheService.InsertLocalObject(CacheKeyDictionary.VaccinatorInfo, user);
                   
                            await _navigationService.NavigateAsync("/NavigationPage/HomePage");
                        }
                        else
                        {
                            await _dialogService.DisplayAlertAsync("Ocurrió algo inesperado", "El número de cédula no existe", "OK");
                        }
                    }
                }
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("selectedClinicLocationName"))
            {
                LocationName = parameters.GetValue<string>("selectedClinicLocationName");
                LocationId = parameters.GetValue<string>("selectedClinicLocationId");
            }
        }
    }
}
