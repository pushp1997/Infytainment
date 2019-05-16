using System;
using System.Collections.Generic;

namespace InfytainmentDAL.Models
{
    public partial class Movies
    {
        public Movies()
        {
            Booking = new HashSet<Booking>();
            ShowTimings = new HashSet<ShowTimings>();
        }

        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public int Rating { get; set; }
        public TimeSpan Duration { get; set; }
        public string Description { get; set; }
        //public byte[] ImageSmall { get; set; }
        //public byte[] ImageLarge { get; set; }

        public ICollection<Booking> Booking { get; set; }
        public ICollection<ShowTimings> ShowTimings { get; set; }
    }
}
