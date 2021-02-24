using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using Prism;
using Prism.Ioc;
using Prism.Navigation;
using Refit;
using SisVac.Framework.Data;
using SisVac.Framework.Domain;
using SisVac.Framework.Http;
using SisVac.Framework.Http.Loggin;
using SisVac.Framework.Services;
using SisVac.Framework.Utils;
using SisVac.Pages.Base;
using SisVac.Pages.CheckIn;
using SisVac.Pages.Login;
using SisVac.Pages.Report;
using SisVac.Pages.Vaccinator;
using SisVac.Pages.Vaccine;
using SisVac.ViewModels;
using SisVac.ViewModels.CheckIn;
using SisVac.ViewModels.Login;
using SisVac.ViewModels.Report;
using SisVac.ViewModels.Vaccine;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SisVac
{
    public partial class App
    {
        public static string CitizenApiBaseUrl => AppSettingsManager.Settings["CitizenApiBaseUrl"];

        public static LocalDatabase Database { get; set; }

        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            XF.Material.Forms.Material.Init(this);
            InitializeComponent();
            XF.Material.Forms.Material.Use("Material.Configuration");
            Application.Current.UserAppTheme = OSAppTheme.Light;

            if (Settings.IsLoggedIn)
                await NavigationService.NavigateAsync($"NavigationPage/{nameof(HomePage)}");
            else
                await NavigationService.NavigateAsync($"NavigationPage/{nameof(LoginPage)}");
        }

        protected override async void OnStart()
        {
            base.OnStart();

            Database = new LocalDatabase();
            await Database.Initialize();

            AppCenter.Start("android=89389cb2-d03e-4a44-a48c-d1e4ce35f9f1;" +
                            "ios=7af5f42f-0ac9-423c-a1af-d61192e0e45e;",
                            typeof(Analytics), typeof(Crashes), typeof(Distribute));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Pages
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<ConfirmLoginPage, ConfirmLoginPageViewModel>();
            containerRegistry.RegisterForNavigation<SearchCenterPage, SearchCenterPageViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<CheckInPage, CheckInPageViewModel>();
            containerRegistry.RegisterForNavigation<ReportEffectsPage, ReportEffectsPageViewModel>();
            containerRegistry.RegisterForNavigation<TrackingVaccinePage, TrackingVaccinePageViewModel>();
            containerRegistry.RegisterForNavigation<AddVaccinatorPage>();
            containerRegistry.RegisterForNavigation<VaccineStatusPage>();
            containerRegistry.RegisterForNavigation<VaccinatorListPage>();

            //Services
            containerRegistry.Register<IScannerService, ScannerService>();
            containerRegistry.Register<ICacheService, CacheService>();

            var httpClient = new HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(CitizenApiBaseUrl) };

            var citizensClient = RestService.For<ICitizensApiClient>(httpClient);
            containerRegistry.RegisterInstance<ICitizensApiClient>(citizensClient);
        }
    }
}
