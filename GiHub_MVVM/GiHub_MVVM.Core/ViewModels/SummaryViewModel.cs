using GiHub_MVVM.Core.Common;
using GiHub_MVVM.Core.Models;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Localization;
using MvvmCross.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiHub_MVVM.Core.ViewModels
{
    public class SummaryViewModel : BaseViewModel<GitRepository>, IMvxViewModel<GitRepository>
    {

        //public IMvxTextProvider TextProvider { get; }

        public SummaryViewModel(IMvxNavigationService navigationService) : base(navigationService)
        {

        }

        private GitRepoSummary _summary;
        public GitRepoSummary Summary
        {
            get
            {
                return _summary;
            }
            set
            {
                SetProperty(ref _summary, value);
            }
        }

        private MvxCommand closeViewCommand;
        public IMvxCommand CloseViewCommand
        {
            get
            {
                return closeViewCommand = closeViewCommand ?? new MvxCommand(() => Close(this));
            }
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            await ReloadData();
        }

        public override async Task ReloadData()
        {
            await base.ReloadData();
            var reader = new GitApiReader();
            Summary = await reader.GitRepoSummary(Item.FullName);
        }
    }
}
