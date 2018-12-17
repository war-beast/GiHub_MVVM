using Android.Content.Res;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using GiHub_MVVM.Droid.Activities;
using GiHub_MVVM.Core.ViewModels;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Core.Navigation;

namespace GiHub_MVVM.Droid.Fragments
{
    public abstract class BaseFragment<TViewModel> : BaseFragment where TViewModel : class, IMvxViewModel
    {
        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }

    public abstract class BaseFragment : MvxFragment
    {
        protected abstract int FragmentId { get; }

        private Toolbar _toolbar;
        private MvxActionBarDrawerToggle _drawerToggle;

        public MvxAppCompatActivity ParentActivity
        {
            get
            {
                return ((MvxAppCompatActivity)Activity);
            }
        }

        protected BaseFragment()
        {
            RetainInstance = true;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(FragmentId, null);

            _toolbar = view.FindViewById<Toolbar>(Resource.Id.toolbar);
            if (_toolbar != null && ParentActivity != null)
            {
                ParentActivity.SetSupportActionBar(_toolbar);
                ParentActivity.SupportActionBar.SetDisplayHomeAsUpEnabled(true);

                _drawerToggle = new MvxActionBarDrawerToggle(
                    ParentActivity, // host Activity
                    (ParentActivity as INavigationActivity).Drawer, // DrawerLayout object
                    _toolbar, // nav drawer icon to replace 'Up' caret
                    Resource.String.drawer_open, // "open drawer" description
                    Resource.String.drawer_close // "close drawer" description
                );
                //_drawerToggle.DrawerOpened +=
                //    (object sender, ActionBarDrawerEventArgs e) => ParentActivity.HideSoftKeyboard();
                (ParentActivity as INavigationActivity).Drawer.AddDrawerListener(_drawerToggle);
            }

            return view;
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            if (_drawerToggle != null)
            {
                _drawerToggle.OnConfigurationChanged(newConfig);
            }
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            if (_drawerToggle != null)
            {
                _drawerToggle.SyncState();
            }
        }
    }
}
