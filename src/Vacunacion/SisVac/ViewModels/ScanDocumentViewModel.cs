using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.AppCenter.Crashes;
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
        public Action<string> DocumentScanned;

        public ScanDocumentViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService,
            IScannerService scannerService,
            ICacheService cacheService,
            ICitizensApiClient citizensApiClient) : base(navigationService, dialogService, cacheService)
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
        public Qualification Qualification { get; set; }

        private async void OnScanDocumentCommandExecute()
        {
            var result = await _scannerService.Scan(null);

            if(result.Length > 11)
            {
                DocumentID.Value = "";
            }
            else
            { 
                DocumentID.Value = result;
            }
            if(DocumentScanned != null && !string.IsNullOrEmpty(result))
                DocumentScanned.Invoke(result);
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
                    if(document.Length == 13)
                        document = document.Replace("-", "");
                    result = await _citizensApiClient.GetBasicData(document);
                    IsBusy = false;
                    return result;
                }
                catch(Exception ex)
                {
                    Crashes.TrackError(ex);
                }
            }

            IsBusy = false;
            return result;
        }
    }
}
