using Xamarin.Forms;
using InterviewApp.Models;
using InterviewApp.Services;
using InterviewApp.Interfaces;
using System;

namespace InterviewApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

#if ADVANCED
            DependencyService.Register<IDataStore<Item,Account>, SqliteDataStore>();
#else
            DependencyService.Register<IDataStore<Item,Account>, MockDataStore>();
#endif
            DependencyService.Register<InterviewApp.Interfaces.IAlerts, Services.AlertsImplementation>();
  
            MainPage = new AppShell();
        }
    }
}
