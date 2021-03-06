﻿using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using SoftwareKobo.Extensions;
using WinRTXamlToolkit.AwaitableUI;

namespace SoftwareKobo.Views
{
    public abstract class ViewBase : BindablePage
    {
        private bool _isLeaving;

        protected ViewBase()
        {
            base.Loaded += ViewBase_Loaded;
            base.Unloaded += ViewBase_Unloaded;
        }

        public new event RoutedEventHandler Loaded;

        public new event RoutedEventHandler Unloaded;

        protected virtual ContentControl PreviousPageHost
        {
            get
            {
                var rootView = Window.Current.Content as IRootView;
                return rootView?.PreviousPageHost;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            }

            FrameExtensions.SetPreviousPage(Frame, this);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            }

            var previousPage = FrameExtensions.GetPreviousPage(Frame);
            if (previousPage != null)
            {
                previousPage._isLeaving = true;
                var previousPageHost = PreviousPageHost;
                if (previousPageHost != null)
                {
                    previousPageHost.Content = previousPage;
                    await previousPage.WaitForLoadedAsync();
                    await previousPage.PlayLeaveAnimationAsync(e.NavigationMode);
                    previousPageHost.Content = null;
                }
                FrameExtensions.SetPreviousPage(Frame, null);
                previousPage._isLeaving = false;
            }
        }

        protected virtual Task PlayEnterAnimationAsync()
        {
            return Task.CompletedTask;
        }

        protected virtual Task PlayLeaveAnimationAsync(NavigationMode currentPageNavigationMode)
        {
            return Task.CompletedTask;
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.BackPressedEventArgs"))
            {
                if (Frame.CanGoBack)
                {
                    e.Handled = true;
                    Frame.GoBack();
                }
            }
        }

        private async void ViewBase_Loaded(object sender, RoutedEventArgs e)
        {
            if (_isLeaving == false)
            {
                Loaded?.Invoke(sender, e);
                await PlayEnterAnimationAsync();
            }
        }

        private void ViewBase_Unloaded(object sender, RoutedEventArgs e)
        {
            if (_isLeaving == false)
            {
                Unloaded?.Invoke(sender, e);
            }
        }
    }
}