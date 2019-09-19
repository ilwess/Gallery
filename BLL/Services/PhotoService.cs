using AutoMapper;
using BLL.Abstract;
using BLL.DTO;
using Domain.Abstract;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PhotoService : IPhotoService
    {
        IUnitOfWork db;
        IMapper mapper;

        public PhotoService(IUnitOfWork iow, IMapper mapper)
        {
            this.db = iow;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<PhotoDTO>> AllPhotosAsync()
        {
            IEnumerable<Photo> photos
                = await db.PhotoRepo.GetAllAsync(); 

            return mapper
                .Map<IEnumerable<PhotoDTO>>(photos);
        }

        public async Task CreatePhotoAsync(PhotoDTO newPhotoDTO)
        {
            Photo newPhoto = mapper.Map<Photo>(newPhotoDTO);
            await db.PhotoRepo.CreateAsync(newPhoto);
            await db.CommitAsync();
        }

        public async Task<PhotoDTO> GetPhotoByIdAsync(int id)
        {
            Photo photo = await 
                db.PhotoRepo.GetAsync(id);
            return mapper.Map<PhotoDTO>(photo);
        }

        public async Task<IEnumerable<PhotoDTO>> 
            GetPhotosAsync(
            Expression<Func<PhotoDTO, bool>> predicate)
        {
            Expression<Func<Photo, bool>> predic 
                = mapper
                .Map<Expression<Func<Photo, bool>>>(predicate);

            IEnumerable<Photo> photos = 
                await db.PhotoRepo.GetAsync(predic);

            return mapper
                .Map<IEnumerable<PhotoDTO>>(photos);
        }

        public async Task UpdateAsync(PhotoDTO photoDTO)
        {
            Photo photoToUpd 
                = mapper.Map<Photo>(photoDTO);

            await db.PhotoRepo.UpdateAsync(photoToUpd);
        }
    }
}
