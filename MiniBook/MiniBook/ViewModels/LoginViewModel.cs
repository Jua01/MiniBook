using MiniBook.Mvvm.Commands;
using MiniBook.Services;
using MiniBook.ViewModels.Base;


namespace MiniBook.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        string _email;
        string _password;
        AccountService AccountService { get; }
        public LoginViewModel(AccountService accountService) {
            AccountService = accountService;

            LoginCommand = new DelegateCommand(Login, CanLogin)
                .ObservesProperty(()=>IsBusy)
                .ObservesProperty(() => Email)
                .ObservesProperty(() => Password);

            RegisterCommand = new DelegateCommand(Register);

            Title = String.Localization.GetString("Login_Title");
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private bool CanLogin()
        {
            return IsNotBusy && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
        }
        private void Register()
        {
            NavigationServices.NavigateToAsync<RegisterViewModel>();
        }

        private async void Login()
        {
            IsBusy = true;
            if(await AccountService.LoginAsync(Email, Password))
            {
               await NavigationServices.NavigateToAsync<DashboardViewModel>();
            }
            else
            {
                await DialogService.AlertAsync(String.Localization.GetString("Login_failMessage"), String.Localization.GetString("Login_Title"));
            }
            IsBusy = false;
        }

        public DelegateCommand LoginCommand { get; }
        public DelegateCommand RegisterCommand { get; }

        private void GoToDashboard()
        {
            NavigationServices.NavigateToAsync<DashboardViewModel>();
        }

    }
}
