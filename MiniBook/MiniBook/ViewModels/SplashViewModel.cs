using MiniBook.Services;
using MiniBook.Services.Navigation;
using MiniBook.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniBook.ViewModels
{
    public class SplashViewModel : ViewModelBase
    {
        public SplashViewModel() { }
        public override async Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            if (await ServiceLocator.Instance.Resolve<AccountService>().RestoreAsync())
            {
                
                await NavigationServices.NavigateToAsync<DashboardViewModel>();
            }
            else
            {
                //await NavigationServices.NavigateToAsync<DashboardViewModel>();
               
                await NavigationServices.NavigateToAsync<LoginViewModel>();
            }
        }
    }
}
