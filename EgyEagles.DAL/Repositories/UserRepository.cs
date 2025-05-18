using EgyEagles.DAL.Context;
using EgyEagles.Domain.Enitites;
using EgyEagles.Domain.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyEagles.DAL.Repositories
{
    public class UserRepository : GenericRepository<User> , IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(MongoDbContext context) : base(context)
        {
            _users = context.Users;
        }

        public async Task<User> GetByEmailAsync(string email)
            => await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
    }
}
