using System;
using System.Collections.Generic;

namespace InfytainmentDAL.Models
{
    public partial class Seats
    {
        public string SeatId { get; set; }
        public int Status { get; set; }
        public int ScreenId { get; set; }
        public int? BookId { get; set; }

        public Booking Book { get; set; }
        public Screens Screen { get; set; }
    }
}
