using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ParkAPI.Dtos;
using ParkAPI.Models;

namespace ParkAPI.ParkMapper
{
    public class ParkMapping : Profile
    {
        public ParkMapping()
        {
            CreateMap<NationalPark, NationalParkDto>().ReverseMap();
        }
    }
}