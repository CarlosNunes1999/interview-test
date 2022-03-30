using Xamarin.Forms;
using MvvmHelpers;
using InterviewApp.Views;
using InterviewApp.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using InterviewApp.Interfaces;
namespace InterviewApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Properties
        private readonly IAlerts _Alerts;
        private string _username = "";
        public string username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _password = "";
        public string password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        public Command LoginCommand { get; }
        public Command RegisterCommand { get; }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public LoginViewModel()
        {
            this._Alerts = DependencyService.Get<IAlerts>();
            LoginCommand = new Command(Login);
            RegisterCommand = new Command(Register);
        }

        #region Methods

        private void Register() => RegisterAsync().SafeFireAndForget();
        private void Login() => LoginAsync().SafeFireAndForget();

        /// <summary>
        /// Try to connect with the given username and password
        /// </summary>
        /// <returns></returns>
        private async Task LoginAsync()
        {
            List<Account> ListAcc = await DataStore.GetAccountAsync(username, password);
            if (ListAcc.Count > 0 && ListAcc != null)
            {
                await _Alerts.ShowAlertsAsync("Connection Successful", "Connection was sucessfull, redirection you to the app!");
                Shell.Current.GoToAsync($"//{nameof(AboutPage)}").SafeFireAndForget();
                // Reset text
                username = "";
                password = "";
            }
            else
            {
                await _Alerts.ShowAlertsAsync("Connection Error", "Sorry but we couldn't find an account with that Username or Password! Please try again later..");
                Shell.Current.GoToAsync($"//{nameof(LoginPage)}").SafeFireAndForget();
                //Reset Text
                username = "";
                password = "";
            }

        }

        /// <summary>
        /// Try to register the user account
        /// </summary>
        /// <returns></returns>
        private async Task RegisterAsync()
        {
            Account account = new Account()
            {
                Id = Guid.NewGuid(),
                Username = username,
                Password = password
            };
            await DataStore.AddAccountAsync(account);
            Shell.Current.GoToAsync($"//{nameof(AboutPage)}").SafeFireAndForget();
        } 
        #endregion
    }
}
