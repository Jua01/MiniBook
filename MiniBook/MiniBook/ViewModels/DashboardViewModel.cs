﻿using MiniBook.Models;
using MiniBook.ViewModels.Base;
namespace MiniBook.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        public DashboardViewModel() {
            User = AppContext.Current.Profile;
        }

        public User User { get; set; }
    }
}
