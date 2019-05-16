using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfytainmentAPI.Models
{
    public class ShowTimings
    {
        public int ShowId { get; set; }
        public int? MovieId { get; set; }
        public int DayofTheWeek { get; set; }
        public TimeSpan Time { get; set; }
        public int ScreenId { get; set; }
    }
}
