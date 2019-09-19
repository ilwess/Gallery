using Domain.Abstract;
using Domain.EFContext;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private GalleryContext db;
        private IRepository<User> userRepo;
        private IRepository<Photo> photoRepo;
        private bool isDisposed = false;
 
        public UnitOfWork(GalleryContext db)
        {
            this.db = db;
        }

        public IRepository<User> UserRepo
        {
            get
            {
                return userRepo ?? new UserRepository(db);
            }
        }

        public IRepository<Photo> PhotoRepo
        {
            get
            {
                return photoRepo ?? new PhotoRepository(db);
            }
        }

        public async Task CommitAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposeDb)
        {
            if (!isDisposed)
            {
                if (disposeDb)
                {
                    db.Dispose();
                }
                isDisposed = true;
            }
        }
    }
}
