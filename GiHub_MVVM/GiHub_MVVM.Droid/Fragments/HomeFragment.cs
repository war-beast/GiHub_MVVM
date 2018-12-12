using Android.Runtime;
using GiHub_MVVM.Core.ViewModels;
using MvvmCross.Droid.Views.Attributes;

namespace GiHub_MVVM.Droid.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register("gihub_mvvm.droid.fragments.HomeFragment")]
    public class HomeFragment : BaseFragment<HomeViewModel>
    {
        protected override int FragmentId => Resource.Layout.fragment_home;
    }
}