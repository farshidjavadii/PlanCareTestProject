using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CarDtos
    {
        public record CarDto(
        int Id,
        string Make,
        string Model,
        string PlateNumber,
        DateTime RegistrationExpiry
        );

        public record CarStatusDto(
            string PlateNumber,
            bool IsValid,
            DateTime RegistrationExpiry
        );
    }
}
