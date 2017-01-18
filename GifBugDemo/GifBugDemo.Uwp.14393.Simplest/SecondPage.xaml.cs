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
    }
}