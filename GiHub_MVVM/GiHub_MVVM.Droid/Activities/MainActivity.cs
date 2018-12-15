using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Views.InputMethods;
using GiHub_MVVM.Core;
using GiHub_MVVM.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace GiHub_MVVM.Droid.Activities
{
    [Activity(
        Label = "GiHub_MVVM.Droid",
        Icon = "@drawable/icon",
        Theme = "@style/AppTheme",
        LaunchMode = LaunchMode.SingleTop
    )]
    public class MainActivity : MvxAppCompatActivity<MainViewModel>, INavigationActivity
    {
        public DrawerLayout Drawer { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            Drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            if (savedInstanceState == null)
                ViewModel.ShowMenu();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Drawer.OpenDrawer(GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        private void ShowHamburguerMenu()
        {
            //TODO set toggle indicator as enabled 
            //this.DrawerToggle.DrawerIndicatorEnabled = true;

            //Unlock the menu sliding gesture
            Drawer.SetDrawerLockMode(DrawerLayout.LockModeUnlocked);
        }

        public override void OnBackPressed()
        {
            if (Drawer != null && Drawer.IsDrawerOpen(GravityCompat.Start))
                Drawer.CloseDrawers();
            else
                Finish();
        }

        //private void CheckForUpdates()
        //{
        //    // Remove this for store builds!
        //    UpdateManager.Register(this, Constants.HockeyAppAndroidDebug);
        //}

        //private void UnregisterManagers()
        //{
        //    UpdateManager.Unregister();
        //}

        protected override void OnPause()
        {
            base.OnPause();
            //UnregisterManagers();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            //UnregisterManagers();
        }
    }

    public interface INavigationActivity
    {
        DrawerLayout Drawer { get; set; }
    }
}
