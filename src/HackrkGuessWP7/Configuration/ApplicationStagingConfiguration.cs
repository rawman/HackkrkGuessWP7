namespace HackrkGuessWP7.Configuration
{
    public class ApplicationStagingConfiguration : IApplicationConfiguration
    {
        public string ApiUrl { get { return "http://hackkrk-guess-static.herokuapp.com"; } }
    }
}