using Application.Contracts.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Application.Dtos.CarDtos;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _service;
        public CarController(ICarService service) => _service = service;

        [HttpGet]
        public Task<List<CarDto>> Get([FromQuery] string? make) => _service.GetCarsAsync(make);
    }
}
