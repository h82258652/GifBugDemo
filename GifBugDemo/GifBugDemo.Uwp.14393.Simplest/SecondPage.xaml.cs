using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace GifBugDemo.Uwp
{
    public sealed partial class SecondPage
    {
        public SecondPage()
        {
            InitializeComponent();
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private async void GoBackButton_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
            GoBackButton.IsEnabled = true;
        }
    }
}