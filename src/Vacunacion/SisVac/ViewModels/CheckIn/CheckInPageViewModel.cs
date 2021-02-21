using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace SisVac.ViewModels.CheckIn
{
    public class CheckInPageViewModel : BindableBase
    {
        public string CouponCode { get; set; }

        public CheckInPageViewModel()
        {
            NextCommand = new DelegateCommand(OnNextCommandExecute, ()=> !string.IsNullOrWhiteSpace(CouponCode));
            BackCommand = new DelegateCommand(OnBackCommandExecute);
            ScanCouponCommand = new DelegateCommand(OnScanCouponCommandExecute);
        }

        async void OnScanCouponCommandExecute()
        {
            var scanner = new ZXing.Mobile.MobileBarcodeScanner();
            var result = await scanner.Scan(new ZXing.Mobile.MobileBarcodeScanningOptions{
                    DisableAutofocus = false,
                    TryHarder = true
                });
            if (result != null)
                CouponCode = result.Text;
        }

        public int PositionView { get; set; }

        public ICommand NextCommand { get; }
        public ICommand ScanCouponCommand { get; set; }
        public ICommand BackCommand { get; }

        private void OnNextCommandExecute()
        {
            switch (PositionView)
            {
                case 0:
                    PositionView = 1;
                    break;
                case 1:
                    PositionView = 2;
                    break;
                case 2:
                    PositionView = 3;
                    break;
                case 3:
                    PositionView = 4;
                    break;
                case 4:
                    break;
            }
        }

        private void OnBackCommandExecute()
        {
            switch (PositionView)
            {
                case 0:
                    break;
                case 1:
                    PositionView = 0;
                    break;
                case 2:
                    PositionView = 1;
                    break;
                case 3:
                    PositionView = 2;
                    break;
                case 4:
                    PositionView = 3;
                    break;
            }
        }
    }
}
