using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.Localization;
using MvvmCross.Platform;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using GiHub_MVVM.Core.Models;

namespace GiHub_MVVM.Core.ViewModels
{
    public class BaseViewModel<TRefObject> : BaseViewModel where TRefObject : class
    {
        public BaseViewModel(IMvxNavigationService navigationService) : base(navigationService)
        {
            
        }

        public void Prepare(TRefObject repository)
        {
            // first callback. Initialize parameter-agnostic stuff here
            Item = repository;
        }

        private TRefObject _item;
        public TRefObject Item
        {
            get
            {
                return _item;
            }
            set
            {
                SetProperty(ref _item, value);
            }
        }
    }

    public class BaseViewModel : MvxViewModel
    {
        public IMvxTextProvider TextProvider { get; }
        public readonly IMvxNavigationService _navigationService;

        public BaseViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            TextProvider = Mvx.Resolve<IMvxTextProvider>();
        }

        private string _title = string.Empty;
        public virtual string Title
        {
            get
            {
                return !string.IsNullOrEmpty(_title) ? _title : TextProvider.GetText(Constants.LocalizationNamespace, this.GetType().Name, "title");
            }
            set
            {
                SetProperty(ref _title, value);
            }
        }

        private bool _isLoading = false;
        public virtual bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                SetProperty(ref _isLoading, value);
                RaisePropertyChanged(() => IsFullyLoaded);
            }
        }

        private bool _isRefreshing = false;
        public virtual bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                SetProperty(ref _isRefreshing, value);
            }
        }

        private bool _filterEnabled;
        public virtual bool FilterEnabled
        {
            get
            {
                return _filterEnabled;
            }
            set
            {
                SetProperty(ref _filterEnabled, value);
            }
        }

        private bool _isFullyLoaded;
        public virtual bool IsFullyLoaded
        {
            get
            {
                if (FilterEnabled)
                    return true;

                return _isFullyLoaded;
            }
            protected set
            {
                SetProperty(ref _isFullyLoaded, value);
            }
        }

        public virtual IMvxAsyncCommand ReloadCommand
        {
            get
            {
                return new MvxAsyncCommand(ReloadData);
            }
        }

        public virtual Task ReloadData()
        {
            IsRefreshing = false;
            return Task.FromResult(true);
        }

        public IMvxLanguageBinder TextSource
        {
            get { return new MvxLanguageBinder(Constants.LocalizationNamespace, GetType().Name); }
        }
    }
}
