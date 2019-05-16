using System;
using System.Collections.Generic;

namespace InfytainmentDAL.Models
{
    public partial class ShowTimings
    {
        public ShowTimings()
        {
            Booking = new HashSet<Booking>();
        }

        public int ShowId { get; set; }
        public int? MovieId { get; set; }
        public int DayofTheWeek { get; set; }
        public TimeSpan Time { get; set; }
        public int ScreenId { get; set; }

        public Movies Movie { get; set; }
        public Screens Screen { get; set; }
        public ICollection<Booking> Booking { get; set; }
    }
}
