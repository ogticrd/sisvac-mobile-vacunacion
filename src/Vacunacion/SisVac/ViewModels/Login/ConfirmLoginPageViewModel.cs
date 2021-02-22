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
        public string LocationId { get; set; }
        public string LocationName { get; set; } = "Centros";

        public bool ShowLocationErrorMessage { get; set; }

        public ICommand LocationSelectedCommand { get; set; }
        public ICommand ConfirmLoginCommand { get; set; }

        public ConfirmLoginPageViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService,
            IScannerService scannerService,
            ICitizensApiClient citizensApiClient) : base(navigationService, dialogService, scannerService, citizensApiClient)
        {
            ConfirmLoginCommand = new Command(OnConfirmLoginCommandExecute);
        }


        async void OnConfirmLoginCommandExecute()
        {
            if(String.IsNullOrEmpty(LocationName) && !DocumentID.Validate())
            {
                ShowLocationErrorMessage = true;
            }
            else
            {
                ShowLocationErrorMessage = false;

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

                    var navigationParams = new NavigationParameters
                    {
                        { "user", user }
                    };
                    App.Vaccinator = user;
                    await _navigationService.NavigateAsync("/NavigationPage/HomePage", navigationParams);
                }
                else
                {
                    await _dialogService.DisplayAlertAsync("Ocurrió algo inesperado", "El número de cédula no existe", "OK");
                }
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("selectedClinicLocationName"))
            {
                LocationName = parameters.GetValue<string>("selectedClinicLocationName");
                LocationId = parameters.GetValue<string>("selectedClinicLocationName");
            }
        }
    }
}
