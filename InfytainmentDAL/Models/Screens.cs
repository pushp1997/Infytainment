using System;
using System.Collections.Generic;

namespace InfytainmentDAL.Models
{
    public partial class Screens
    {
        public Screens()
        {
            Booking = new HashSet<Booking>();
            Seats = new HashSet<Seats>();
            ShowTimings = new HashSet<ShowTimings>();
        }

        public int ScreenId { get; set; }
        public int SeatingCapacity { get; set; }

        public ICollection<Booking> Booking { get; set; }
        public ICollection<Seats> Seats { get; set; }
        public ICollection<ShowTimings> ShowTimings { get; set; }
    }
}
