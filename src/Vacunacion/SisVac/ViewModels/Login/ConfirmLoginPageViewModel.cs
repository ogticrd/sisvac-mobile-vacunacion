using System;
using System.Threading.Tasks;
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
        public ConfirmLoginPageViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService,
            IScannerService scannerService,
            ICitizensApiClient citizensApiClient, ICacheService cacheService) : base(navigationService, dialogService, scannerService, cacheService, citizensApiClient)
        {
            ConfirmLoginCommand = new Command(OnConfirmLoginCommandExecute);
            DocumentScanned = async (id) => await GoNext(id);
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
                    await _dialogService.DisplayAlertAsync("La persona encargada del registro, no puede ser vacunador", "Contacte al vacunador para que le facilite su número de cédula.", "OK");
                }
                else
                { 
                    if (DocumentID.Validate())
                    {
                        await GoNext(DocumentID.Value);
                    }
                }
            }
        }

        async Task GoNext(string document)
        {
            if (String.IsNullOrEmpty(LocationId))
            {
                ShowLocationErrorMessage = true;
                return;
            }
            var userData = await GetDocumentData(document);
            if (userData != null && userData.IsValid && userData.Age > 0)
            {
                if(userData.Cedula == User.Document)
                {
                    await _dialogService.DisplayAlertAsync("La persona encargada del registro, no puede ser vacunador", "Contacte al vacunador para que le facilite su número de cédula.", "OK");
                }
                else
                { 
                    var user = new ApplicationUser
                    {
                        Age = userData.Age,
                        Document = userData.Cedula,
                        FullName = userData.Name,
                        LocationId = LocationId,
                        LocationName = LocationName
                    };

                    App.Vaccinator = user;

                    await _cacheService.InsertLocalObject(CacheKeyDictionary.VaccinatorInfo, user);

                    await _navigationService.NavigateAsync("/NavigationPage/HomePage");
                }
            }
            else
            {
                await _dialogService.DisplayAlertAsync("Ocurrió algo inesperado", "El número de cédula no existe", "OK");
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
