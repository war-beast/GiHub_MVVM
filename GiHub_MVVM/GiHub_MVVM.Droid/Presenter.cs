using Android.Support.V4.App;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Views;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace GiHub_MVVM.Droid
{
    public class Presenter : MvxAndroidViewPresenter
    {
        private readonly IMvxViewModelLoader _viewModelLoader;
        private readonly IFragmentTypeLookup _fragmentTypeLookup;
        private readonly IEnumerable<Assembly> _assembly;
        private FragmentManager _fragmentManager;

        public Presenter(IMvxViewModelLoader viewModelLoader, IFragmentTypeLookup fragmentTypeLookup, IEnumerable<Assembly> assembly) : base(assembly)
        {
            _fragmentTypeLookup = fragmentTypeLookup;
            _viewModelLoader = viewModelLoader;
        }

        public void RegisterFragmentManager(FragmentManager fragmentManager, MvxFragment initialFragment)
        {
            _fragmentManager = fragmentManager;
            ShowFragment(initialFragment, false);
        }

        public override void Show(MvxViewModelRequest request)
        {
            Type fragmentType;
            if (_fragmentManager == null || !_fragmentTypeLookup.TryParseFragmentType(request.ViewModelType, out fragmentType))
            {
                base.Show(request);

                return;
            }

            var fragment = (MvxFragment)Activator.CreateInstance(fragmentType);
            fragment.ViewModel = _viewModelLoader.LoadViewModel(request, null);

            ShowFragment(fragment, true);
        }

        private void ShowFragment(MvxFragment fragment, bool addToBackStack)
        {
            var transaction = _fragmentManager.BeginTransaction();

            if (addToBackStack)
                transaction.AddToBackStack(fragment.GetType().Name);

            transaction
                .Replace(Resource.Id.content_frame, fragment)
                .Commit();
        }

        public override void Close(IMvxViewModel viewModel)
        {
            var currentFragment = _fragmentManager.FindFragmentById(Resource.Id.content_frame) as MvxFragment;
            if (currentFragment != null && currentFragment.ViewModel == viewModel)
            {
                _fragmentManager.PopBackStackImmediate();

                return;
            }

            base.Close(viewModel);
        }
    }
}