using InterviewApp.Interfaces;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace InterviewApp.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        #region Properties
        public ICommand UpdateCommand { get; }
        public ICommand OpenWebCommand { get; }

        private string _platformString = "";
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public AboutViewModel()
        {
            Title = "About";

            UpdateCommand = new Command(Update);
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        #region Methods

        /// <summary>
        /// Variable PlatformString
        /// </summary>
        public string PlatformString
        {
            get => _platformString;
            set => SetProperty(ref _platformString, value);
        }
        /// <summary>
        /// Updates the Platform String
        /// </summary>
        private void Update()
        {
            PlatformString = PlatformService.GetPlatformSpecificString();
        } 

        #endregion
    }
}