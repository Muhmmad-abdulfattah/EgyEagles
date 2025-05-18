using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyEagles.Shared.DTOs.Vehicle
{
    public class VehicleDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CompanyId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
