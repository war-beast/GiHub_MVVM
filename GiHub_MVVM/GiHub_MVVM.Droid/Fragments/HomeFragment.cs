using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using GiHub_MVVM.Core.ViewModels;
using MvvmCross.Binding.Droid;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Droid.Views.Attributes;

namespace GiHub_MVVM.Droid.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register("gihub_mvvm.droid.fragments.HomeFragment")]
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

            return view;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Items": swipeToRefresh.Refreshing = false;
                    break;
            }
        }
    }
}