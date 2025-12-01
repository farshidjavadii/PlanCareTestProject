using Application.Contracts;
using Application.Contracts.Infra;
using Application.Contracts.Service;
using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository) => _carRepository = carRepository;


        public Task<List<CarDtos.CarStatusDto>> CheckRegistrationStatusAsync()
        {
            return _carRepository.CheckRegistrationStatusAsync();
        }

        public Task<List<CarDtos.CarDto>> GetCarsAsync(string? make)
        {
            return _carRepository.GetCarsAsync(make);
        }
    }
}
