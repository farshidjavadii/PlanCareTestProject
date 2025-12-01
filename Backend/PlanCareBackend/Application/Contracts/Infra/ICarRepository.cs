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
        Task<List<CarDto>> GetCarsAsync(string? make);
        Task<List<CarStatusDto>> CheckRegistrationStatusAsync();
    }
}
