using System;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using Prism;
using Prism.Ioc;
using Prism.Navigation;
using Refit;
using SisVac.Framework.Domain;
using SisVac.Framework.Http;
using SisVac.Framework.Services;
using SisVac.Pages.Base;
using SisVac.Pages.CheckIn;
using SisVac.Pages.Login;
using SisVac.Pages.Report;
using SisVac.Pages.Vaccine;
using SisVac.ViewModels;
using SisVac.ViewModels.CheckIn;
using SisVac.ViewModels.Login;
using SisVac.ViewModels.Vaccine;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SisVac
{
    public partial class App
    {
        public static ApplicationUser User { get; set; }
        public static ApplicationUser Vaccinator { get; set; }
        public static string CitizenApiBaseUrl => AppSettingsManager.Settings["CitizenApiBaseUrl"];

        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            XF.Material.Forms.Material.Init(this);
            InitializeComponent();
            XF.Material.Forms.Material.Use("Material.Configuration");
            Application.Current.UserAppTheme = OSAppTheme.Light;

            AppCenter.Start("android=89389cb2-d03e-4a44-a48c-d1e4ce35f9f1;" +
                            "ios=7af5f42f-0ac9-423c-a1af-d61192e0e45e;",
                  typeof(Analytics), typeof(Crashes), typeof(Distribute));

            await NavigationService.NavigateAsync($"NavigationPage/{nameof(LoginPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Pages
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<ConfirmLoginPage, ConfirmLoginPageViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<CheckInPage, CheckInPageViewModel>();
            containerRegistry.RegisterForNavigation<ReportEffectsPage>();
            containerRegistry.RegisterForNavigation<TrackingVaccinePage, TrackingVaccinePageViewModel>();
            containerRegistry.RegisterForNavigation<ChangeVaccinatorPage>();
            containerRegistry.RegisterForNavigation<VaccineStatusPage>();

            //Services
            containerRegistry.Register<IScannerService, ScannerService>();
            containerRegistry.Register<ICacheService, CacheService>();
            var citizensClient = RestService.For<ICitizensApiClient>(CitizenApiBaseUrl);
            containerRegistry.RegisterInstance<ICitizensApiClient>(citizensClient);
        }
    }
}
