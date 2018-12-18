using GiHub_MVVM.Core.Services;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GiHub_MVVM.Core.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly ISettingsService settingsService;

        public SettingsViewModel(IMvxNavigationService navigationService) : base(navigationService)
        {
            settingsService = Mvx.Resolve<ISettingsService>();
        }

        private int _repoCount;
        public int RepoCount
        {
            get
            {
                return _repoCount;
            }
            set
            {
                SetProperty(ref _repoCount, value);
            }
        }

        public virtual ICommand RepoCountSave
        {
            get
            {
                return new MvxCommand<int>(item =>
                {
                    settingsService.SetListCount(RepoCount);
                });
            }
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            RepoCount = settingsService.GetListCount();
        }
    }
}
