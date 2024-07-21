using MiniBook.Mvvm.Commands;
using MiniBook.Services.Dialog;
using MiniBook.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiniBook.ViewModels.Base
{
    public class ViewModelBase : Mvvm.BindableBase
    {
        private string _title;
        private bool _isBusy;

        protected INavigationService NavigationServices { get; }
        protected IDialogService DialogService { get; }
        public ViewModelBase()
        {
            NavigationServices = ServiceLocator.Instance.Resolve<INavigationService>();

            DialogService = ServiceLocator.Instance.Resolve<IDialogService>();
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        
        
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value, ()=>RaisePropertyChanged(nameof(IsNotBusy)));
        }

        public bool IsNotBusy => !IsBusy;

        public virtual Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            return Task.CompletedTask;
        }

        private DelegateCommand _backCommand;
        public DelegateCommand BackCommand => _backCommand ?? (_backCommand = new DelegateCommand(Back));
        private async void Back()
        {
            await NavigationServices.NavigateBackAsync();
        }
    }
}
