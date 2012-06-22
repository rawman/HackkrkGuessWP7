using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Caliburn.Micro;
using HackrkGuessWP7.Views;
using Microsoft.Phone;
using Spring.Http;
using Spring.Rest.Client;

namespace HackrkGuessWP7.ViewModels
{
    public class RiddleListViewModel : Screen
    {
        private readonly RegistrationService _registrationService;

        public RiddleListViewModel(RegistrationService registrationService)
        {
            _registrationService = registrationService;
            Riddles = new ObservableCollection<riddle>();
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            RestTemplate template = new RestTemplate(Api.Host);
            template.MessageConverters.Add(new NJsonHttpMessageConverter());

            var body = new GetRiddlesRequest();
            HttpEntity entity = new HttpEntity(body);
            entity.Headers.Add("X-Auth-Token", _registrationService.GetToken());

            template.GetForObjectAsync<GetRiddlesResponse>("/riddles", r =>
            {
                if(r.Error == null)
                {
                    Execute.OnUIThread( () =>
                    {
                        RiddleListView view = (RiddleListView)GetView();
                        ListBox riddlesList = view.riddles;
                        foreach (var riddle in r.Response.riddles)
                        {
                            StackPanel panel = new StackPanel();
                            panel.Children.Add(new TextBlock() {Text = riddle.author});
                            
                            Image img = new Image();
                            panel.Children.Add(img);
                            panel.Tap += OnRiddleTap;
                            panel.Tag = riddle;

                            riddlesList.Items.Add(panel);
                            
                            DownloadImage(img, riddle.photo_url);
                            
                        }
                    } );
                }
            });
        }

        private void OnRiddleTap(object sender, GestureEventArgs e)
        {
          
        }

        public ObservableCollection<riddle> Riddles { get; set; } 

        public void DownloadImage(Image img, string uri)
        {
            WebClient wc = new WebClient();
            wc.OpenReadCompleted += ImageReadCompleted;
            wc.OpenReadAsync(new Uri(uri), img); 
        }

        void ImageReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error == null && !e.Cancelled)
            {
                try
                {
                    Execute.OnUIThread(() =>
                    {
                        Image img = (Image)e.UserState;
                        BitmapImage image = new BitmapImage();
                        image.SetSource(e.Result);
                        img.Source = image;
                    });
                }
                catch (Exception ex)
                {
                    //Exception handle appropriately for your app  
                }
            }
            else
            {
                //Either cancelled or error handle appropriately for your app  
            }  
        }
    }


    public class GetRiddlesRequest
    {
        public int page { get; set; }
        public int per_page { get; set; }

        public GetRiddlesRequest()
        {
            page = 1;
            per_page = int.MaxValue;
        }
    }

    public class GetRiddlesResponse
    {
        public int total { get; set; }
        public int page { get; set; }
        public int page_count { get; set; }
        public riddle[] riddles { get; set; }
    }
}
