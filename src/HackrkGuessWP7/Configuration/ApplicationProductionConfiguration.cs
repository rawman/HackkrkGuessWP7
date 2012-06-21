namespace HackrkGuessWP7.Configuration
{
    public class ApplicationProductionConfiguration : IApplicationConfiguration
    {
        public string ApiUrl
        {
            get { return "http://hackkrk-guess.herokuapp.com"; }
        }
    }
}