using EgyEagles.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyEagles.Domain.Enitites
{
    public class Company : BaseModel
    {
        public string Name { get; set; }
        public List<string> UserIds { get; set; } = new List<string>();
        public List<string> VehicleIds { get; set; } = new List<string>();
    }
}
