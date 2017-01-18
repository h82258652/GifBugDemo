using Windows.UI.Xaml;

namespace GifBugDemo.Uwp
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void NavigateToSecondPageButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SecondPage));
        }
    }
}