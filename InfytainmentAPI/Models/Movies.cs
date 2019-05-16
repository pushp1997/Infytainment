using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfytainmentAPI.Models
{
    public class Movies
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public int Rating { get; set; }
        public TimeSpan Duration { get; set; }
        public string Description { get; set; }
        //public byte[] ImageSmall { get; set; }
        //public byte[] ImageLarge { get; set; }
    }
}
