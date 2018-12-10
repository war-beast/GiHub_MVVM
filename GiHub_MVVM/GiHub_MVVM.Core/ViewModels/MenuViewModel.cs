using MvvmCross.Core.ViewModels;

namespace GiHub_MVVM.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        public void Navigate<TViewModel>() where TViewModel : class, IMvxViewModel
        {
            ShowViewModel<TViewModel>();
        }
    }
}
