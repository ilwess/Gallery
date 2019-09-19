using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IPhotoService
    {
        Task<IEnumerable<PhotoDTO>> AllPhotosAsync();
        Task<IEnumerable<PhotoDTO>> GetPhotosAsync(
            Expression<Func<PhotoDTO, bool>> predicate);
        Task<PhotoDTO> GetPhotoByIdAsync(int id);
        Task CreatePhotoAsync(PhotoDTO newPhotoDTO);
        Task UpdateAsync(PhotoDTO photoDTO);

    }
}
