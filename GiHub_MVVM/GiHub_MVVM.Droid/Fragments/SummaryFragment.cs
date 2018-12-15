using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GiHub_MVVM.Core.ViewModels;
using MvvmCross.Droid.Views.Attributes;

namespace GiHub_MVVM.Droid.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register(nameof(SummaryFragment))]
    public class SummaryFragment : BaseFragment<SummaryViewModel>
    {
        protected override int FragmentId => Resource.Layout.fragment_summary;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
                        
            ViewModel.Title = GetString(Resource.String.summary_title);

            return view;
        }
    }
}