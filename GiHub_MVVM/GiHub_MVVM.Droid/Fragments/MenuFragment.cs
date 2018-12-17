using GiHub_MVVM.Core.ViewModels;
using GiHub_MVVM.Droid.Activities;
using System;
using System.Threading.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using MvvmCross.Droid.Views.Attributes;
using MvvmCross.Binding.Droid.BindingContext;
using Android.Support.V4.Widget;

namespace GiHub_MVVM.Droid.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.navigation_frame, false)]
    [Register(nameof(MenuFragment))]
    public class MenuFragment : BaseFragment<MenuViewModel>, NavigationView.IOnNavigationItemSelectedListener
    {
        protected override int FragmentId => Resource.Layout.fragment_menu;

        private NavigationView navigationView;
        private IMenuItem previousMenuItem;
        private DrawerLayout drawer;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.fragment_menu, null);

            navigationView = view.FindViewById<NavigationView>(Resource.Id.navigation_view);
            navigationView.SetNavigationItemSelectedListener(this);
            navigationView.Menu.FindItem(Resource.Id.nav_home).SetChecked(true);
            drawer = view.FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            return view;
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            item.SetCheckable(true);
            item.SetChecked(true);
            previousMenuItem?.SetChecked(false);
            previousMenuItem = item;

            Task.Run(async () =>
            {
                await Navigate(item.ItemId);
            });

            return true;
        }

        private async Task Navigate(int itemId)
        {
            var activity = ((MainActivity)Activity);
            activity.RunOnUiThread(() =>
            {
                activity.Drawer.CloseDrawers();//Необходимо вызывать в потоке UI, иначе возникает исключение.
            });

            switch (itemId)
            {
                case Resource.Id.nav_home:
                    await Task.Run(() => { ViewModel.Navigate<HomeViewModel>(); });
                    break;
                case Resource.Id.nav_settings:
                    await Task.Run(() => { ViewModel.Navigate<SettingsViewModel>(); });
                    break;
            }
        }
}
}