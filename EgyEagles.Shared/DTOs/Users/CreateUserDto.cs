using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyEagles.Shared.DTOs.Users
{
    public class CreateUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }  // "SuperAdmin", "CompanyAdmin", "RegularUser"
        public string CompanyId { get; set; }
        public List<string> Permissions { get; set; } = new();
    }
}
