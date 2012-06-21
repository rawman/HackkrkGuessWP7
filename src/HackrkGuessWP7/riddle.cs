using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace HackrkGuessWP7
{
    public class riddle
    {
        public int id { get; set; }
        public string question { get; set; }
        public string photo_url { get; set; }
        public string author { get; set; }
        public string created_at { get; set; }
        public int attempted_by { get; set; }
        public int solved_by { get; set; }
        public int points { get; set; }
        public bool solved { get; set; }
    }
}
