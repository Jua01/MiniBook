using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiniBook.Services.Dialog
{
    internal class DialogService : IDialogService
    {
        public Task AlertAsync(string message, string title)
        {
            return UserDialogs.Instance.AlertAsync(message, title,"Ok");
        }
        
        public Task AlertAsync(string message, string title, string OkeText)
        {
            return UserDialogs.Instance.AlertAsync(message, title, OkeText);
        }
    }
}
