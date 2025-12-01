using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string PlateNumber { get; set; } = string.Empty;
        public string Plate { get; set; } = string.Empty;
        public DateTime RegistrationExpiry { get; set; }


        public bool IsRegistrationValid => RegistrationExpiry > DateTime.UtcNow;
    }
}
