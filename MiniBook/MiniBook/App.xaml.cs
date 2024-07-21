using MiniBook.Services.Navigation;
using MiniBook.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MiniBook
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            BuildDependencies();

            InitNavigation();
        }

        private void InitNavigation()
        {
            ServiceLocator.Instance.Resolve<INavigationService>()
                .NavigateToAsync<SplashViewModel>();
        }

        private void BuildDependencies()
        {
            if (ServiceLocator.Instance.Built != false)
            {
                return;
            }
            // Register dependencies
            ServiceLocator.Instance.RegisterInstance<Services.Dialog.IDialogService, Services.Dialog.DialogService>();
            ServiceLocator.Instance.RegisterInstance<INavigationService, NavigationService>();
            ServiceLocator.Instance.Register<Services.HttpService>();
            ServiceLocator.Instance.Register<Services.AccountService>();

            ServiceLocator.Instance.RegisterViewModels();

            ServiceLocator.Instance.Build();
        }

        //protected override void OnStart()
        //{
        //    ServiceLocator.Instance.Resolve<INavigationService>()
        //       .NavigateToAsync<SplashViewModel>();
        //}

        //protected override void OnSleep()
        //{
        //    MessagingCenter.Send(this, "OnAppSleep");
        //}

        //protected override void OnResume()
        //{
        //}
    }
}
