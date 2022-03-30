using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InterviewApp.Interfaces;

namespace InterviewApp.Services
{
    class AlertsImplementation : IAlerts
    {
        public async Task ShowAlertsAsync(string Title, string Message)
        {
            await App.Current.MainPage.DisplayAlert(Title, Message, "Ok");
        }

    }
}
