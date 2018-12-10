﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Views.InputMethods;
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
    public class MainActivity : MvxCachingFragmentCompatActivity<MainViewModel>, INavigationActivity
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

        public override void OnBeforeFragmentChanging(MvvmCross.Droid.Shared.Caching.IMvxCachedFragmentInfo fragmentInfo, Android.Support.V4.App.FragmentTransaction transaction)
        {
            transaction.SetCustomAnimations(
                Resource.Animation.abc_fade_in,
                Resource.Animation.abc_fade_out,
                Resource.Animation.abc_fade_in,
                Resource.Animation.abc_fade_out);

            base.OnBeforeFragmentChanging(fragmentInfo, transaction);
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

        public override void OnBackPressed()
        {
            if (Drawer != null && Drawer.IsDrawerOpen(GravityCompat.Start))
                Drawer.CloseDrawers();
            else
                Finish();
        }

        public void HideSoftKeyboard()
        {
            if (CurrentFocus == null) return;

            InputMethodManager inputMethodManager = (InputMethodManager)GetSystemService(InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(CurrentFocus.WindowToken, 0);

            CurrentFocus.ClearFocus();
        }
    }

    public interface INavigationActivity
    {
        DrawerLayout Drawer { get; set; }
        void HideSoftKeyboard();
    }
}