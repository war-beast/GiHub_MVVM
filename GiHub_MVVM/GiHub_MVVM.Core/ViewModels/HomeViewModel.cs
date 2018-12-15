using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GiHub_MVVM.Core.Common;
using GiHub_MVVM.Core.Models;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;

namespace GiHub_MVVM.Core.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel(IMvxNavigationService  navigationService) : base(navigationService)
        {

        }

        private MvxAsyncCommand showSummaryCommand;
        public IMvxAsyncCommand ShowSummaryCommand
        {
            get {
                return showSummaryCommand = showSummaryCommand ?? new MvxAsyncCommand(async () => { await _navigationService.Navigate<SummaryViewModel, GitRepository>(SelectedItem); });
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
            //Загрузка данных при инициализации ViewModel
        }

        private List<GitRepository> _items;
        public List<GitRepository> Items
        {
            get
            {
                return _items;
            }
            set
            {
                SetProperty(ref _items, value);
            }
        }

        private GitRepository _selectedItem;
        public GitRepository SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        public virtual ICommand ItemSelected
        {
            get
            {
                return new MvxCommand<GitRepository>(item =>
                {
                    SelectedItem = item;
                    //Task.Run(async() => { await _navigationService.Navigate<SummaryViewModel, GitRepository>(SelectedItem); });
                });
            }
        }

        public override async Task ReloadData()
        {
            await base.ReloadData();
            // By default return a completed Task
            //await Task.Delay(5000);

            var reader = new GitApiReader();
            Items = await reader.GetRepositories();
        }
    }
}
