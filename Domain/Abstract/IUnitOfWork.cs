using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> UserRepo { get; }
        IRepository<Photo> PhotoRepo { get; }
        Task CommitAsync();
        void Dispose(bool disposeDb);
    }
}
