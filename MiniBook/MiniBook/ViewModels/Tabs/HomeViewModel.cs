using MiniBook.Models;
using MiniBook.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniBook.ViewModels.Tabs
{
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel()
        {
            User = AppContext.Current.Profile;
        }
        public User User { get; set; }
    }
}
