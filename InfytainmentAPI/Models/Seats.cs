using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfytainmentAPI.Models
{
    public class Seats
    {
        public string SeatId { get; set; }
        public int Status { get; set; }
        public int ScreenId { get; set; }
        public int? BookId { get; set; }
    }
}
