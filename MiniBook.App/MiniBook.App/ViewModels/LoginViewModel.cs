using MiniBook.App.Services.Navigation;
using MiniBook.App.ViewModels.Base;
using MiniBook.App.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniBook.App.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel() { }
        private void GoToDashboard()
        {
            NavigationServices.NavigateToAsync<DashboardViewModel>();
        }
    }
}
