﻿using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using GifBugDemo.Uwp.Views;

namespace GifBugDemo.Uwp
{
    public sealed partial class App
    {
        public App()
        {
            InitializeComponent();
            Resuming += OnResuming;
            Suspending += OnSuspending;
        }

        protected override void OnActivated(IActivatedEventArgs args)
        {
            base.OnActivated(args);
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            var rootView = Window.Current.Content as RootView;
            if (rootView == null)
            {
                rootView = new RootView(e.SplashScreen);
                rootView.RootFrame.NavigationFailed += OnNavigationFailed;
                Window.Current.Content = rootView;
            }
        }

        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnResuming(object sender, object e)
        {
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            try
            {
            }
            finally
            {
                deferral.Complete();
            }
        }
    }
}