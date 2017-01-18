using GalaSoft.MvvmLight.Views;
using GifBugDemo.Uwp.Views;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace GifBugDemo.Uwp.ViewModels
{
    public class ViewModelLocator
    {
        public const string MainViewKey = "Main";

        public const string SecondViewKey = "Second";

        static ViewModelLocator()
        {
            var serviceLocator = new UnityServiceLocator(ConfigureUnityContainer());
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        public SecondViewModel Second => ServiceLocator.Current.GetInstance<SecondViewModel>();

        private static IUnityContainer ConfigureUnityContainer()
        {
            var unityContainer = new UnityContainer();

            unityContainer.RegisterInstance(CreateNavigationService());

            unityContainer.RegisterType<MainViewModel>();
            unityContainer.RegisterType<SecondViewModel>();

            return unityContainer;
        }

        private static INavigationService CreateNavigationService()
        {
            var navigationService = new SoftwareKobo.Services.NavigationService();
            navigationService.Configure(MainViewKey, typeof(MainView));
            navigationService.Configure(SecondViewKey, typeof(SecondView));
            return navigationService;
        }
    }
}