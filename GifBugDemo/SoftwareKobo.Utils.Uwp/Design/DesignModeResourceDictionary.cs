using System;
using Windows.ApplicationModel;
using Windows.UI.Xaml;

namespace SoftwareKobo.Design
{
    public class DesignModeResourceDictionary : ResourceDictionary
    {
        public new Uri Source
        {
            get
            {
                return base.Source;
            }
            set
            {
                if (DesignMode.DesignModeEnabled)
                {
                    base.Source = value;
                }
            }
        }
    }
}