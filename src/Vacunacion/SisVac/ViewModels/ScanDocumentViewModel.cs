using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.ValidationRules;
using Plugin.ValidationRules.Formatters;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using SisVac.Framework.Domain;
using SisVac.Framework.Http;
using SisVac.Framework.Services;
using SisVac.Helpers.Rules;
using XF.Material.Forms.UI.Dialogs;

namespace SisVac.ViewModels
{
    public class ScanDocumentViewModel : BaseViewModel
    {
        private IScannerService _scannerService;
        protected ICitizensApiClient _citizensApiClient;
        public ScanDocumentViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService,
            IScannerService scannerService,
            ICitizensApiClient citizensApiClient) : base(navigationService, dialogService)
        {
            _scannerService = scannerService;
            _citizensApiClient = citizensApiClient;
            ScanDocumentCommand = new DelegateCommand(OnScanDocumentCommandExecute);

            DocumentID = new Validatable<string>();
            DocumentID.Validations.Add(new CedulaRule());
            DocumentID.ValueFormatter = new MaskFormatter("XXX-XXXXXXX-X");
        }

        public Validatable<string> DocumentID { get; set; }
        public ICommand ScanDocumentCommand { get; set; }

        private async void OnScanDocumentCommandExecute()
        {
            var result = await _scannerService.Scan(null);

            if(result.Length > 11)
            {
                result = result.Substring(0,11);
            }
            DocumentID.Value = result;
        }

        protected async Task<UserResponse> GetDocumentData(string document)
        {

            if (IsBusy)
                return null;

            IsBusy = true;
            UserResponse result = null;

            using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Validando..."))
            {
                try
                { 
                    result = await _citizensApiClient.GetBasicData(document.Replace("-",""));
                    IsBusy = false;
                    return result;
                }
                catch(Exception ex)
                {
                }
            }

            IsBusy = false;
            return result;
        }
    }
}
