using EgyEagles.Domain.Enitites;
using EgyEagles.Shared.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyEagles.BLL.Interfaces
{
    public interface IUserService
    {
        Task<string> CreateUserAsync(CreateUserDto dto);
        Task<User> LoginAsync(UserLoginDto dto);
        Task<List<UserDto>> GetUsersByCompanyAsync(string companyId);
        Task<User> GetUserByIdAsync(string id);
    }
}
