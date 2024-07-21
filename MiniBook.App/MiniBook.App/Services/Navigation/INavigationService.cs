using MiniBook.App.Services.Navigation;
using MiniBook.App.ViewModels.Base;
using System;
using System.Threading.Tasks;

namespace MiniBook.App.Services.Navigation
{
    public interface INavigationService
    {
        Task InitializeAsync();

        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;

        Task NavigateToAsync<TViewModel>(NavigationParameters parameters) where TViewModel : ViewModelBase;

        Task NavigateToAsync(Type viewModelType);

        Task NavigateToAsync(Type viewModelType, NavigationParameters parameters);

        Task NavigateBackAsync();

        Task NavigateBackAsync(NavigationParameters parameters);

        Task NavigateBackToMainPageAsync();
    }
}
