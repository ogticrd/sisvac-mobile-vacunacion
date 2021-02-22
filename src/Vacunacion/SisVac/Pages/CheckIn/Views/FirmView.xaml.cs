using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisVac.Helpers;
using SisVac.ViewModels.CheckIn;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SisVac.Pages.CheckIn.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirmView : ContentView
    {
        public FirmView()
        {
            InitializeComponent();
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            var vm = (CheckInPageViewModel)BindingContext;

            vm.SignatureFromStream = async () =>
            {
                if (signatureView.Points.Count() > 0)
                {
                    using (var stream = await signatureView.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png))
                    {
                        return await ImageConverter.ReadFully(stream);
                    }
                }

                return await Task.Run(() => (byte[])null);
            };
        }
    }
}