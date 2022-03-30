using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InterviewApp.Interfaces
{
    public interface IAlerts
    {
        Task ShowAlertsAsync(string Title, string Message);
    }
}
