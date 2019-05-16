using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfytainmentAPI.Models
{
    public class Booking
    {
        public int BookId { get; set; }
        public int MovieId { get; set; }
        public int ShowId { get; set; }
        public int ScreenId { get; set; }
        public int? EmployeeId { get; set; }

    }
}
