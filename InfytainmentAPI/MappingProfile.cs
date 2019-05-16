using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InfytainmentDAL.Models;
namespace InfytainmentAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movies, Models.Movies>();
            CreateMap<Models.Movies, Movies>();

            CreateMap<Screens, Models.Screens>();
            CreateMap<Models.Screens, Screens>();

            CreateMap<Booking, Models.Booking>();
            CreateMap<Models.Booking, Booking>();

            CreateMap<ShowTimings, Models.ShowTimings>();
            CreateMap<Models.ShowTimings, ShowTimings>();

            CreateMap<Seats, Models.Seats>();
            CreateMap<Models.Seats, Seats>();
        }
    }
}
