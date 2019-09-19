using Domain.Abstract;
using Domain.EFContext;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class UserRepository : IRepository<User>
    {
        private GalleryContext db;

        public UserRepository(GalleryContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(User newModel)
        {
            await db.Users.AddAsync(newModel);
        }

        public async Task DeleteAsync(User model)
        {
            db.Users.Remove(model);
        }

        public async Task DeleteAsync(int id)
        {
            User userToDel =
                await db.Users.FindAsync(id);
            db.Remove(userToDel);
        }

        public async Task<User> GetAsync(int id)
        {
            return await db.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await db.Users.ToAsyncEnumerable().ToList();
        }

        public async Task<IEnumerable<User>> GetAsync(
            Expression<Func<User, bool>> predicate)
        {
            return await db.Users
                .Where(predicate)
                .ToAsyncEnumerable()
                .ToList();
        }

        public async Task UpdateAsync(User model)
        {
            db.Update(model);
        }
    }
}
