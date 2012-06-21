using System;
using System.Net;
using System.Windows;
using Caliburn.Micro;
using HackrkGuessWP7.ViewModels;
using Spring.Http.Converters.Json;
using Spring.Rest.Client;

namespace HackrkGuessWP7 {
    public class MainPageViewModel : Screen
    {
        private INavigationService _navigationService;
        private RegistrationService _registrationService;

        public MainPageViewModel(INavigationService navigationService, RegistrationService registrationService)
        {
            _navigationService = navigationService;
            _registrationService = registrationService;
            _registrationService.RegistrationFinished += _registrationService_RegistrationFinished;
        }

        void _registrationService_RegistrationFinished(object sender, RegistrationFinishedEventArgs e)
        {
            if(e.Exception == null)
            {
                _navigationService.UriFor<RiddleListViewModel>().Navigate();
            }
            else
            {
                MessageBox.Show(e.Exception.Message);
            }
        }

        public void SignIn()
        {
           
        }

        private string Password
        {
            get { return ((MainPage) GetView()).passwordBox.Password; }
        }

        public void CreateAccount()
        {
            _registrationService.Register(UserName, Password);
        }

        public string UserName { get; set; }
    }

    public class RegistrationRequest
    {
        public string username { get; set; }

        public string password { get; set; }
    }

    public class RegistrationResponse
    {
        public string username { get; set; }
        public string token { get; set; }
    }
}
