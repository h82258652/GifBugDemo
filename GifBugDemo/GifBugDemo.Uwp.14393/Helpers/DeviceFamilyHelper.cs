using Windows.System.Profile;

namespace GifBugDemo.Uwp.Helpers
{
    public static class DeviceFamilyHelper
    {
        public static bool IsMobile => AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile";
    }
}