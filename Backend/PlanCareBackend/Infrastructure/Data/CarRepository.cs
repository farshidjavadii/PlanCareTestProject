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
            var random = new Random();
            _cars = new List<Car>
            {
                new Car { Id = 1, Make = "Toyota", Model = "Corolla", Plate = "1ABC123", RegistrationExpiry = DateTime.UtcNow.AddDays(random.Next(-30, 100)) },
                new Car { Id = 2, Make = "Honda", Model = "Civic", Plate = "2XYZ456", RegistrationExpiry = DateTime.UtcNow.AddDays(random.Next(-30, 100)) },
                new Car { Id = 3, Make = "Nissan", Model = "Rogue", Plate = "3DEF789", RegistrationExpiry = DateTime.UtcNow.AddDays(random.Next(-30, 100)) },
                new Car { Id = 4, Make = "BMW", Model = "M3", Plate = "4GHI012", RegistrationExpiry = DateTime.UtcNow.AddDays(random.Next(-30, 100)) },
                new Car { Id = 5, Make = "Audi", Model = "A4", Plate = "5JKL345", RegistrationExpiry = DateTime.UtcNow.AddDays(random.Next(-30, 100)) },
                new Car { Id = 6, Make = "Subaru", Model = "Outback", Plate = "6MNO678", RegistrationExpiry = DateTime.UtcNow.AddDays(random.Next(-30, 100)) },
                new Car { Id = 7, Make = "Jeep", Model = "Wrangler", Plate = "7PQR901", RegistrationExpiry = DateTime.UtcNow.AddDays(random.Next(-30, 100)) },
                new Car { Id = 8, Make = "Volvo", Model = "S60", Plate = "8STU234", RegistrationExpiry = DateTime.UtcNow.AddDays(random.Next(-30, 100)) },
                new Car { Id = 9, Make = "Hyundai", Model = "Tucson", Plate = "9VWX567", RegistrationExpiry = DateTime.UtcNow.AddDays(random.Next(-30, 100)) },
                new Car { Id = 10, Make = "Kia", Model = "Telluride", Plate = "10YZ890", RegistrationExpiry = DateTime.UtcNow.AddDays(random.Next(-30, 100)) },
                new Car { Id = 11, Make = "Mazda", Model = "CX-5", Plate = "11BCD123", RegistrationExpiry = DateTime.UtcNow.AddDays(random.Next(-30, 100)) },
                new Car { Id = 12, Make = "Acura", Model = "TLX", Plate = "12EFG456", RegistrationExpiry = DateTime.UtcNow.AddDays(random.Next(-30, 100)) },
                new Car { Id = 13, Make = "Lexus", Model = "RX 350", Plate = "13HIJ789", RegistrationExpiry = DateTime.UtcNow.AddDays(random.Next(-30, 100)) },
                new Car { Id = 14, Make = "Porsche", Model = "911", Plate = "14KLM012", RegistrationExpiry = DateTime.UtcNow.AddDays(random.Next(-30, 100)) }

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
