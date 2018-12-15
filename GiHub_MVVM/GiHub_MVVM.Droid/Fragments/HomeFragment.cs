using System;
using System.Windows.Input;
using Android.OS;
using Android.Runtime;
using Android.Views;
using GiHub_MVVM.Core.Models;
using GiHub_MVVM.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Droid.Views;
using MvvmCross.Droid.Views.Attributes;
using MvvmCross.Platform;

namespace GiHub_MVVM.Droid.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register(nameof(HomeFragment))]
    public class HomeFragment : BaseFragment<HomeViewModel>
    {
        protected override int FragmentId => Resource.Layout.fragment_home;

        private MvxSwipeRefreshLayout swipeToRefresh;
        private MvxRecyclerView recyclerView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.my_recycler_view);
            swipeToRefresh = view.FindViewById<MvxSwipeRefreshLayout>(Resource.Id.refresher);
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            ViewModel.Title = GetString(Resource.String.app_name);

            //var presenter = (MvxAppCompatViewPresenter)Mvx.Resolve<IMvxAndroidViewPresenter>();
            //var initialFragment = new HomeFragment { ViewModel = ViewModel };
            //presenter.(FragmentManager, initialFragment);

            return view;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Items": swipeToRefresh.Refreshing = false;
                    break;
                case "SelectedItem": SwitchFragments(ViewModel.SelectedItem);
                    break;
            }
        }

        private void SwitchFragments(GitRepository itemSelected)
        {
            ViewModel.ShowSummaryCommand.Execute();
        }
    }
}