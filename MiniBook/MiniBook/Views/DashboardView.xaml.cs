using MiniBook.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MiniBook.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardView
    {
        public DashboardView()
        {
            InitializeComponent();
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            if (CurrentPage != null)
            {
                SetViewModelbyModel(CurrentPage);
            }
        }

        private void SetViewModelbyModel(Page view)
        {

            try
            {
                if (view.BindingContext != BindingContext && view.BindingContext is ViewModelBase vm)
                {
                    vm.OnNavigationAsync(new Services.Navigation.NavigationParameters(), Services.Navigation.NavigationType.Back);
                    return;
                }
                var viewType = view.GetType();

                var viewModelType = Type.GetType(viewType.FullName.Replace("View", "ViewModel"));

                if (viewModelType == null)
                    throw new Exception($"Mapping type for {viewModelType} is not a page");

                vm = ServiceLocator.Instance.Resolve(viewModelType) as ViewModelBase;

                if (vm != null)
                {
                    view.BindingContext = vm;

                    vm.OnNavigationAsync(new Services.Navigation.NavigationParameters(), Services.Navigation.NavigationType.New);
                }


            }
            catch (Exception ex)
            {
                Debugger.Break();

                throw;
            }

        }
    }
}