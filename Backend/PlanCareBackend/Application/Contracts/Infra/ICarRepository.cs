using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Dtos.CarDtos;

namespace Application.Contracts.Infra
{
    public interface ICarRepository
    {
        Task<List<Car>> GetCarsAsync(string? make);
        Task<List<Car>> GetAllCarsAsync();
    }
}
