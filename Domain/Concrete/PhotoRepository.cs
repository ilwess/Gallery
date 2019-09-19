using Domain.Abstract;
using Domain.EFContext;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class PhotoRepository : IRepository<Photo>
    {
        private GalleryContext db;

        public PhotoRepository(GalleryContext db)
        {
            this.db = db;

        }

        public async Task CreateAsync(Photo newModel)
        {
            await db.AddAsync(newModel);
        }

        public async Task DeleteAsync(Photo model)
        {
            db.Remove(model);
            
        }

        public async Task DeleteAsync(int id)
        {
            Photo photoToDel =
                await db.Photos.FindAsync(id);
            db.Remove(photoToDel);
        }

        public async Task<Photo> GetAsync(int id)
        {
            return await db.Photos.FindAsync(id);
        }

        public async Task<IEnumerable<Photo>>
            GetAllAsync()
        {
            return await db
                .Photos
                .ToAsyncEnumerable()
                .ToList();
        }

        public async Task<IEnumerable<Photo>>
            GetAsync(
            Expression<Func<Photo, bool>> predicate)
        {
            return await db
                .Photos
                .Where(predicate)
                .ToAsyncEnumerable()
                .ToList();
        }

        public async Task UpdateAsync(Photo model)
        {
            db.Update(model);
        }
    }
}
