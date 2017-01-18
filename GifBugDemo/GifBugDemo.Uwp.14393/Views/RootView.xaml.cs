using System;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;

namespace GifBugDemo.Uwp.Views
{
    public sealed partial class RootView
    {
        public RootView()
        {
            InitializeComponent();
        }

        public RootView(SplashScreen splashScreen) : this()
        {
            var extendedSplashScreen = new ExtendedSplashScreen(splashScreen);
            EventHandler completedHandler = null;
            completedHandler = async (sender, e) =>
            {
                extendedSplashScreen.Completed -= completedHandler;
                RootFrame.Navigate(typeof(MainView));
                await extendedSplashScreen.DismissAsync();
                RootGrid.Children.Remove(extendedSplashScreen);
            };
            extendedSplashScreen.Completed += completedHandler;
            RootGrid.Children.Add(extendedSplashScreen);
        }

        public override ContentControl PreviousPageHost => ContentControl;

        public override Frame RootFrame => Frame;
    }
}