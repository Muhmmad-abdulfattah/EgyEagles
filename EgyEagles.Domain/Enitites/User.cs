using EgyEagles.Domain.Common;
using EgyEagles.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyEagles.Domain.Enitites
{
    public class User : BaseModel
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }         
        public string CompanyId { get; set; }
        public List<string> Permissions { get; set; } = new List<String>(); 
    }
}
