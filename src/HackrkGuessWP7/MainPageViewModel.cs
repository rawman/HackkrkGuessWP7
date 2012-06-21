using System;
using System.Net;
using Caliburn.Micro;
using HackrkGuessWP7.ViewModels;
using Spring.Http.Converters.Json;
using Spring.Rest.Client;

namespace HackrkGuessWP7 {
    public class MainPageViewModel : Screen
    {
        private INavigationService _navigationService;

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
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
            RestTemplate template = new RestTemplate(Api.Host);
            template.MessageConverters.Add(new NJsonHttpMessageConverter());

            var body = new RegistrationRequest() {username = UserName, password = Password};

            template.PostForObjectAsync<RegistrationResponse>("/user", body, r =>
            {
                int k = 0;
            } );
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
