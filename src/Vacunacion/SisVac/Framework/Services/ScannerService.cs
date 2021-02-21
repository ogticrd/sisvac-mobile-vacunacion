using System;
using System.Threading.Tasks;
using ZXing;

namespace SisVac.Framework.Services
{
    public class ScannerService : IScannerService
    {
        public async Task<string> Scan(Action<string> callback, string topText = "", string bottomText = "")
        {
            var scanner = new ZXing.Mobile.MobileBarcodeScanner
            {
                BottomText = bottomText,
                TopText = topText,
                FlashButtonText = "Flash",
            };

            var result = await scanner.Scan(new ZXing.Mobile.MobileBarcodeScanningOptions
            {
                DisableAutofocus = false
            });
            if (result != null)
            {
                if(callback != null)
                    callback(result.Text);
                return result.Text;
            }
            if (callback != null)
                callback(string.Empty);
            return string.Empty;
        }
    }

    public interface IScannerService
    {
        Task<string> Scan(Action<string> callback, string topText = "", string bottomText="");
    }
}
