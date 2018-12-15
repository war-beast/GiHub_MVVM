using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;

namespace GiHub_MVVM.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        public MenuViewModel(IMvxNavigationService navigationService) : base(navigationService) { }

        public void Navigate<TViewModel>() where TViewModel : class, IMvxViewModel
        {
            _navigationService.Navigate<TViewModel>();
        }
    }
}
