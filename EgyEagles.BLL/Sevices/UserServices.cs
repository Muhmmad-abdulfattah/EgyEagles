using EgyEagles.BLL.Helpers;
using EgyEagles.BLL.Interfaces;
using EgyEagles.Domain.Enitites;
using EgyEagles.Domain.Enums;
using EgyEagles.Domain.Interfaces;
using EgyEagles.Shared.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyEagles.BLL.Sevices
{
    public class UserServices : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public UserServices(IUserRepository userRepository,ICompanyRepository companyRepository,JwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<string> CreateUserAsync(CreateUserDto dto)
        {
            var existing = await _userRepository.GetByEmailAsync(dto.Email);
            if (existing != null)
                throw new Exception("Email already in use.");

            var role = Enum.Parse<UserRole>(dto.Role);

            if (role != UserRole.SuperAdmin)
            {
                var company = await _companyRepository.GetByIdAsync(dto.CompanyId);
                if (company == null)
                    throw new Exception("Company not found.");
            }

            var user = new User
            {
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = role,
                CompanyId = dto.CompanyId,
                Permissions = dto.Permissions
            };

            await _userRepository.AddAsync(user);

            if (role != UserRole.SuperAdmin)
            {
                var company = await _companyRepository.GetByIdAsync(dto.CompanyId);
                company.UserIds.Add(user.Id);
                await _companyRepository.UpdateAsync(company);
            }

            return user.Id;
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<List<UserDto>> GetUsersByCompanyAsync(string companyId)
        {
            var users = await _userRepository.FindAsync(u => u.CompanyId == companyId);

            return users.Select(user => new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role.ToString(),
                CompanyId = user.CompanyId,
                Permissions = user.Permissions
            }).ToList();
        }

        public async Task<User> LoginAsync(UserLoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new Exception("Invalid email or password");

            return user;
        }

    }
}
