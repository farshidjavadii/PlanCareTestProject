using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static Application.Dtos.CarDtos;


namespace Application.Mapper
{
    public class CarMapper : Profile
    {
        public CarMapper()
        {
            
            // Car -> CarDto
            CreateMap<Car, CarDto>();

            // Car -> CarStatusDto
            CreateMap<Car, CarStatusDto>()
                .ForMember(dest => dest.IsValid, opt => opt.MapFrom(src => src.IsRegistrationValid));
        }
    }
}
