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
        public string LocationName { get; set; }
        public bool ShowLocationErrorMessage { get; set; }

        public ICommand LocationSelectedCommand { get; set; }
        public ICommand ConfirmLoginCommand { get; set; }

        public ConfirmLoginPageViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService,
            IScannerService scannerService,
            ICitizensApiClient citizensApiClient) : base(navigationService, dialogService, scannerService, citizensApiClient)
        {
            LocationSelectedCommand = new Command<string>(OnLocationSelectedCommandExecute);
            ConfirmLoginCommand = new Command(OnConfirmLoginCommandExecute);
        }

        void OnLocationSelectedCommandExecute(string selectedOption)
        {
            LocationName = selectedOption;
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
                    App.Vaccinator = new ApplicationUser
                    {
                        Age = userData.Age,
                        Document = DocumentID.Value,
                        FullName = userData.Name,
                        LocationName = LocationName
                    };
                    var parameters = new NavigationParameters();
                    parameters.Add("user", User);

                    await _navigationService.NavigateAsync("/NavigationPage/HomePage", parameters);
                }
                else
                {
                    await _dialogService.DisplayAlertAsync("Ocurrió algo inesperado", "El número de cédula no existe", "OK");
                }
            }
        }
    }
}
