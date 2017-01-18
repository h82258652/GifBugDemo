using System.ComponentModel;
using Windows.UI.Xaml;

namespace SoftwareKobo.AppThemes
{
    public abstract class AppThemeManagerBase : DependencyObject, INotifyPropertyChanged
    {
        protected const string ThemesFolderName = "AppThemes";

        private ResourceDictionary _r;

        internal AppThemeManagerBase()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ResourceDictionary R
        {
            get
            {
                return _r;
            }
            protected set
            {
                if (_r != value)
                {
                    _r = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(R)));
                }
            }
        }
    }
}