using System;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Prism;
using Prism.Ioc;
using Prism.Navigation;
using SisVac.Pages.Base;
using SisVac.Pages.CheckIn;
using SisVac.Pages.Login;
using SisVac.Pages.Report;
using SisVac.Pages.Vaccine;
using SisVac.ViewModels.CheckIn;
using SisVac.ViewModels.Login;
using SisVac.ViewModels.Vaccine;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SisVac
{
    public partial class App
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            XF.Material.Forms.Material.Init(this);
            InitializeComponent();
            XF.Material.Forms.Material.Use("Material.Configuration");

            AppCenter.Start("android=89389cb2-d03e-4a44-a48c-d1e4ce35f9f1;" +
                  "uwp={Your UWP App secret here};" +
                  "ios={Your iOS App secret here}",
                  typeof(Analytics), typeof(Crashes));

            await NavigationService.NavigateAsync("NavigationPage/LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<ConfirmSignIn>();
            containerRegistry.RegisterForNavigation<HomePage>();
            containerRegistry.RegisterForNavigation<CheckInPage, CheckInPageViewModel>();
            containerRegistry.RegisterForNavigation<ReportEffectsPage>();
            containerRegistry.RegisterForNavigation<TrackingVaccinePage, TrackingVaccinePageViewModel>();
            containerRegistry.RegisterForNavigation<ChangeVaccinatorPage>();
            containerRegistry.RegisterForNavigation<VaccineStatusPage>();
        }
    }
}
