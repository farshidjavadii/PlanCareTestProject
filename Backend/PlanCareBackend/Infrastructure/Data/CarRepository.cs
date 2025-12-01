using Application.Contracts.Infra;
using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Application.Dtos.CarDtos;

namespace Infrastructure.Data
{
    public class CarRepository : ICarRepository
    {
        private readonly IMapper _mapper;
        private readonly List<Car> _cars;

        public CarRepository(IMapper mapper)
        {
            _mapper = mapper;
            _cars = new List<Car>
            {
                new Car { Id = 1, Make = "Toyota", Model = "Corolla", PlateNumber = "1ABC123", RegistrationExpiry = DateTime.UtcNow.AddDays(-10) },
                new Car { Id = 2, Make = "Honda", Model = "Civic", PlateNumber = "2XYZ456", RegistrationExpiry = DateTime.UtcNow.AddDays(20) }
            };
        }

        public Task<List<CarDto>> GetCarsAsync(string? make = null)
        {
            var query = _cars.AsQueryable();
            if (!string.IsNullOrEmpty(make))
                query = query.Where(c => c.Make.Equals(make, StringComparison.OrdinalIgnoreCase));

            var result = _mapper.Map<List<CarDto>>(query.ToList());
            return Task.FromResult(result);
        }

        public Task<List<CarStatusDto>> CheckRegistrationStatusAsync()
        {
            var result = _mapper.Map<List<CarStatusDto>>(_cars);
            return Task.FromResult(result);
        }
    }
}
