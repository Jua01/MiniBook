using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MiniBook.App
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
            ServiceLocator.Instance.Resolve<Services.Navigation.INavigationService>()
                .NavigateToAsync<ViewModels.LoginViewModel>();
        }

        private void BuildDependencies()
        {
            if (ServiceLocator.Instance.Built != false)
            {
                return;
            }
            // Register dependencies
            ServiceLocator.Instance.RegisterInstance<Services.Navigation.INavigationService, Services.Navigation.NavigationService>();

            ServiceLocator.Instance.RegisterViewModels();

            ServiceLocator.Instance.Build();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
