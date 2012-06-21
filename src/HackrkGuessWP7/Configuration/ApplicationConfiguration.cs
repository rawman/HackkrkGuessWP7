namespace HackrkGuessWP7.Configuration
{
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        public string ApiUrl { get; set; }

        private static ApplicationConfiguration _default;

        public static ApplicationConfiguration Default
        {
            get { return _default; }
            set
            {
                if (null == _default)
                    _default = value;
            }
        }

        public ApplicationConfiguration()
        {
            Default = (ApplicationConfiguration)App.Current.Resources["Configuration"];
        }
    }
}