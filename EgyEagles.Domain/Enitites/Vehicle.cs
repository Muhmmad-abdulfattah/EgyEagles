using EgyEagles.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyEagles.Domain.Enitites
{
    public class Vehicle : BaseModel
    {
        public string Name { get; set; }
        public string CompanyId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
