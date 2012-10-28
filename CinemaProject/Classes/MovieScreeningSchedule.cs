using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaProject.Classes
{
    public class MovieScreeningSchedule
    {
        public string MovieName { get; set; }
        public int ScreenNum { get; set; }
        public string Date { get; set; }
        public List<string> ScreeningsList { get; set; }
        public string Screenings
        {
            get
            {
                string str = String.Empty;

                foreach (string screening in ScreeningsList)
                {
                    str += screening + ", ";
                }

                str = str.Remove(str.LastIndexOf(','));

                return str;
            }
        }
    }
}
