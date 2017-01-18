namespace SoftwareKobo.AppThemes
{
    public interface IAppThemeManager<TAppTheme> where TAppTheme : struct
    {
        TAppTheme CurrentTheme
        {
            get;
            set;
        }
    }
}