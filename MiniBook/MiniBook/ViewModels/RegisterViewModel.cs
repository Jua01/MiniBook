using MiniBook.Models;
using MiniBook.Mvvm.Commands;
using MiniBook.Services;
using MiniBook.ViewModels.Base;
using System;

namespace MiniBook.ViewModels
{
    public class RegisterViewModel: ViewModelBase
    {
       
        private AccountService AccountService { get;  }
        public RegisterViewModel(AccountService accountService)
        {
            AccountService = accountService;
            RegisterCommand = new DelegateCommand(Register, CanRegister);
            BackCommand = new DelegateCommand(BackPage);
        }

        

        public User User { get; set; } = new User();
        public string Password { get; set; }

        public DelegateCommand RegisterCommand { get; }
        public DelegateCommand BackCommand { get; }

        private async void BackPage()
        {
            await NavigationServices.NavigateBackAsync();
        }
        private async void Register()
        {
           var result = await AccountService.RegisterAsync(User, Password);
            if (result.Successful)
                await NavigationServices.NavigateBackAsync();
        }

        private bool CanRegister()
        {
            return true;
        }
    }
}
