using System;
using System.Collections.Generic;

namespace InfytainmentDAL.Models
{
    public partial class Booking
    {
        public Booking()
        {
            Seats = new HashSet<Seats>();
        }

        public int BookId { get; set; }
        public int MovieId { get; set; }
        public int ShowId { get; set; }
        public int ScreenId { get; set; }
        public int? EmployeeId { get; set; }

        public Movies Movie { get; set; }
        public Screens Screen { get; set; }
        public ShowTimings Show { get; set; }
        public ICollection<Seats> Seats { get; set; }
    }
}
