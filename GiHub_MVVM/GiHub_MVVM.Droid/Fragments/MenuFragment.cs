using GiHub_MVVM.Core.ViewModels;
using GiHub_MVVM.Droid.Activities;
using System;
using System.Threading.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using MvvmCross.Droid.Shared.Attributes;

namespace GiHub_MVVM.Droid.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.navigation_frame, false)]
    public class MenuFragment : BaseFragment<MenuViewModel>, NavigationView.IOnNavigationItemSelectedListener
    {
        protected override int FragmentId => Resource.Layout.fragment_menu;
        private NavigationView navigationView;
        private IMenuItem previousMenuItem;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            navigationView = view.FindViewById<NavigationView>(Resource.Id.navigation_view);
            navigationView.SetNavigationItemSelectedListener(this);

            return view;
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            Navigate(item.ItemId);

            return true;
        }

        private async Task Navigate(int itemId)
        {
            (ParentActivity as INavigationActivity).Drawer.CloseDrawers();
            await Task.Delay(TimeSpan.FromMilliseconds(250));

            switch (itemId)
            {
                case Resource.Id.nav_home:
                    ViewModel.Navigate<HomeViewModel>();
                    break;
            }
        }
    }
}