using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaProject.Classes
{
    public class SessionDetails
    {
        public Guid MovieId { get; set; }
        public Guid ScreenId { get; set; }
        public DateTime StartScreen { get; set; }
        public DateTime EndScreen { get; set; }
        public int ScreenNum { get; set; }
        public int Seats { get; set; }
    }
}
