using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyEagles.Shared.DTOs.Users
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string CompanyId { get; set; }
        public List<string> Permissions { get; set; }
    }
}
