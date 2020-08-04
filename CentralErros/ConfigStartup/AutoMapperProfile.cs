﻿using AutoMapper;
using CentralErros.Models;
using CentralErros.DTO;
using CentralErros.Services;
using CentralErros.Controllers;

namespace CentralErros.ConfigStartup
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Environment, EnvironmentDTO>().ReverseMap();
            CreateMap<Level, LevelDTO>().ReverseMap();
            CreateMap<ErrorOccurrence, ErrorOccurrenceDTO>().ReverseMap();
            CreateMap<ErrorOccurrence, ErrorDetailsDTO>().ReverseMap();
        }
    }
}
