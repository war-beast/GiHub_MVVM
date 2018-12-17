using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;

namespace GiHub_MVVM.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(IMvxNavigationService navigationService) : base(navigationService) { }

        public void ShowMenu()
        {
            ShowViewModel<MenuViewModel>();
        }
    }
}
