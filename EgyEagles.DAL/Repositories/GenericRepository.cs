using EgyEagles.DAL.Context;
using EgyEagles.Domain.Common;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EgyEagles.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        protected readonly IMongoCollection<T> _collection;

        public GenericRepository(MongoDbContext context)
        {
            var collectionName = typeof(T).Name + "s";
            _collection = context.GetCollection<T>(collectionName);
        }
        public async Task AddAsync(T entity)
            => await _collection.InsertOneAsync(entity);

        public async Task DeleteAsync(string id) 
            => await _collection.DeleteOneAsync(e => e.Id == id);

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> filter)
         => await _collection.Find(filter).ToListAsync();   

        public async Task<List<T>> GetAllAsync() =>
             await _collection.Find(_ => true).ToListAsync();

        public async Task<T> GetByIdAsync(string id)
         => await _collection.Find(e => e.Id == id).FirstOrDefaultAsync();

        public async Task UpdateAsync(T entity)
         => await _collection.ReplaceOneAsync(e => e.Id == entity.Id,entity);
    }
}
