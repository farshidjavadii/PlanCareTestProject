using Application.Contracts;
using Application.Contracts.Infra;
using Application.Contracts.Service;
using Application.Dtos;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Dtos.CarDtos;

namespace Application.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public CarService(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }


        public Task<List<CarDtos.CarStatusDto>> CheckRegistrationStatusAsync()
        {
            var result = _mapper.Map<List<CarStatusDto>>(_carRepository.GetAllCarsAsync().Result);
            return Task.FromResult(result);
        }

        public async Task<List<CarDtos.CarDto>> GetCarsAsync(string? make)
        {
            List<Car> cars = await _carRepository.GetCarsAsync(make) ?? new List<Car>();
            List<CarDtos.CarDto> result = _mapper.Map<List<CarDto>>(cars);
            if (result == null || result.Count == 0) {
                throw new NotFoundException(nameof(make), make??"");
            }
            return result;
        }
    }
}
