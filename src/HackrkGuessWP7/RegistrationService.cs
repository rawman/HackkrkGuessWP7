using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Spring.Rest.Client;

namespace HackrkGuessWP7
{
    public class RegistrationService : ITokenProvider
    {
        public event EventHandler<RegistrationFinishedEventArgs> RegistrationFinished = delegate { }; 

        private string _token;
        private readonly RestTemplate _restTemplate;

        public RegistrationService()
        {
            _restTemplate = new RestTemplate(Api.Host);
            _restTemplate.MessageConverters.Add(new NJsonHttpMessageConverter());
        }

        public void Register(string userName, string password)
        {
            var body = new RegistrationRequest() { username = userName, password = password };

            _restTemplate.PostForObjectAsync<RegistrationResponse>("/user", body, r =>
            {
                if (r.Error == null)
                {
                    _token = r.Response.token;
                }

                RegistrationFinished(this, new RegistrationFinishedEventArgs(r.Error));
            });
        }

        public void Login(string userName, string password)
        {
            var uri = string.Format("/user?username={0}password={1}", userName, password);
            _restTemplate.GetForObjectAsync<RegistrationResponse>(uri, r =>
            {
                if (r.Error == null)
                {
                    _token = r.Response.token;
                }

                RegistrationFinished(this, new RegistrationFinishedEventArgs(r.Error));
            });
        }

        public string GetToken()
        {
            return _token;
        }
    }
    
    public class RegistrationFinishedEventArgs : EventArgs
    {
        public Exception Exception { get; private set; }

        public RegistrationFinishedEventArgs(Exception exception)
        {
            Exception = exception;
        }
    }
}
