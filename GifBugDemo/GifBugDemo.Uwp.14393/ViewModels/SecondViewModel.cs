using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace GifBugDemo.Uwp.ViewModels
{
    public class SecondViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        private RelayCommand _goBackCommand;

        public SecondViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public RelayCommand GoBackCommand
        {
            get
            {
                _goBackCommand = _goBackCommand ?? new RelayCommand(() => _navigationService.GoBack());
                return _goBackCommand;
            }
        }
    }
}