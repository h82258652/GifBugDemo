using System;
using System.IO;
using System.Reflection;
using Windows.UI.Xaml;

namespace SoftwareKobo.AppThemes
{
    public class AppThemeManager<TAppTheme> : AppThemeManagerBase, IAppThemeManager<TAppTheme> where TAppTheme : struct
    {
        public static readonly DependencyProperty CurrentThemeProperty = DependencyProperty.Register(nameof(CurrentTheme), typeof(TAppTheme), typeof(AppThemeManager<TAppTheme>), new PropertyMetadata(default(TAppTheme), CurrentThemeChanged));

        static AppThemeManager()
        {
            if (typeof(TAppTheme).GetTypeInfo().IsEnum == false)
            {
                throw new ArgumentException();
            }
        }

        public AppThemeManager()
        {
            UpdateResourceDictionary();
        }

        public TAppTheme CurrentTheme
        {
            get
            {
                return (TAppTheme)GetValue(CurrentThemeProperty);
            }
            set
            {
                SetValue(CurrentThemeProperty, value);
            }
        }

        private static void CurrentThemeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (AppThemeManager<TAppTheme>)d;
            var value = (TAppTheme)e.NewValue;

            if (Enum.IsDefined(typeof(TAppTheme), value) == false)
            {
                throw new ArgumentOutOfRangeException(nameof(CurrentTheme));
            }

            obj.UpdateResourceDictionary();
        }

        private void UpdateResourceDictionary()
        {
            var themeResourceDictionarySource = Path.Combine("ms-resource:///Files", ThemesFolderName, $"{CurrentTheme}.xaml");
            R = new ResourceDictionary()
            {
                Source = new Uri(themeResourceDictionarySource, UriKind.Absolute)
            };
        }
    }
}