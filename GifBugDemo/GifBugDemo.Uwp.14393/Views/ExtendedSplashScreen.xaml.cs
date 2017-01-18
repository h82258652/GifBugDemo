using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Graphics.Display;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GifBugDemo.Uwp.Helpers;
using WinRTXamlToolkit.AwaitableUI;

namespace GifBugDemo.Uwp.Views
{
    public sealed partial class ExtendedSplashScreen
    {
        private readonly SplashScreen _splashScreen;

        public ExtendedSplashScreen()
        {
            InitializeComponent();
        }

        public ExtendedSplashScreen(SplashScreen splashScreen) : this()
        {
            _splashScreen = splashScreen;
        }

        public event EventHandler Completed;

        public async Task DismissAsync()
        {
            await DismissStoryboard.BeginAsync();
        }

        private void ExtendedSplashScreen_Loaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged += Window_SizeChanged;
            UpdateSplashScreenImagePosition();
        }

        private void ExtendedSplashScreen_Unloaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged -= Window_SizeChanged;
        }

        private async void SplashScreenImage_ImageOpened(object sender, RoutedEventArgs e)
        {
            // activate window after image opened.
            Window.Current.Activate();

            // init app here, such as TitleBar, BackgroundTask.
            // simulate app init.
            await Task.Delay(TimeSpan.FromSeconds(3));

            Completed?.Invoke(this, EventArgs.Empty);
        }

        private void UpdateSplashScreenImagePosition()
        {
            if (_splashScreen != null)
            {
                var rect = _splashScreen.ImageLocation;
                Canvas.SetLeft(SplashScreenImage, rect.Left);
                Canvas.SetTop(SplashScreenImage, rect.Top);

                if (DeviceFamilyHelper.IsMobile)
                {
                    var scaleFactor = (double)DisplayInformation.GetForCurrentView().ResolutionScale / 100.0d;
                    SplashScreenImage.Width = rect.Width / scaleFactor;
                    SplashScreenImage.Height = rect.Height / scaleFactor;
                }
                else
                {
                    SplashScreenImage.Width = rect.Width;
                    SplashScreenImage.Height = rect.Height;
                }
            }
        }

        private void Window_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            UpdateSplashScreenImagePosition();
        }
    }
}