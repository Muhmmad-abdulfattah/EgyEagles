using EgyEagles.DAL.Context;
using EgyEagles.Domain.Enitites;
using EgyEagles.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyEagles.DAL.Repositories
{
    public class CompanyRepository : GenericRepository<Company> , ICompanyRepository
    {
        public CompanyRepository(MongoDbContext context) : base(context)
        {
        }
    }
}
