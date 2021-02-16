﻿using System;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Prism;
using Prism.Ioc;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vacunacion
{
    public partial class App
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            AppCenter.Start("android=89389cb2-d03e-4a44-a48c-d1e4ce35f9f1;" +
                  "uwp={Your UWP App secret here};" +
                  "ios={Your iOS App secret here}",
                  typeof(Analytics), typeof(Crashes));
            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<HomePage>();
        }
    }
}
