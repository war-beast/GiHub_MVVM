using MvvmCross.Core.ViewModels;

namespace GiHub_MVVM.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public void ShowMenu()
        {
            ShowViewModel<MenuViewModel>();
        }
    }
}
