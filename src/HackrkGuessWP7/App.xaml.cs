using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HackrkGuessWP7.Configuration;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace HackrkGuessWP7
{
    public partial class App : Application
    {

        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            InitializeComponent();
        }

        public ApplicationConfiguration Configuration
        {
            get { return (ApplicationConfiguration)this.Resources["Configuration"]; }
        } 

    }
}