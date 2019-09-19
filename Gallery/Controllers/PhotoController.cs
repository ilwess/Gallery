using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BLL.Abstract;
using BLL.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gallery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService photoService;
        private readonly IUserService userService;
        private readonly IHostingEnvironment environment;

        public PhotoController(
            IPhotoService photoService,
            IHostingEnvironment env,
            IUserService userService)
        {
            this.photoService = photoService;
            environment = env;
            this.userService = userService;
        }

        [HttpPost]
        [Route("download")]
        public async Task<ActionResult> DownloadImage(
            IFormFile file,
            [FromForm] string userName)
        {
            long size = file.Length;

            var filePath = Path.GetTempFileName();

            var upload = Path.Combine(environment.WebRootPath, "images");
            var fullPath = Path.Combine(upload, GetUniqueName(file.FileName));
            file.CopyTo(new FileStream(fullPath, FileMode.Create));
            UserDTO user = ( await userService.AllUsersAsync())
                .Where(u => u.UserName == userName)
                .FirstOrDefault();
            PhotoDTO newPhoto = new PhotoDTO()
            {
                ImageLink = fullPath,
                User = user,
            };
            await photoService.CreatePhotoAsync(newPhoto);

            PhotoDTO photo =  photoService
                .AllPhotosAsync()
                .Result
                .Where(p => p.ImageLink == newPhoto.ImageLink)
                .FirstOrDefault();

            string uri = "https://localhost:44390/api/photo/get/" + photo.Id;
            return Created(uri, photo);
        }

        private string GetUniqueName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                + "_"
                + Guid.NewGuid().ToString().Substring(0, 5)
                + Path.GetExtension(fileName);
        }

        [HttpGet]
        [Route("get")]
        public async Task<ActionResult> Get(int id)
        {
            IEnumerable<PhotoDTO> photos
                = await photoService.AllPhotosAsync();

            if(photos.Count() <= 0)
            {
                return NotFound();
            }

            return Ok(photos.Where(p => p.Id == id).FirstOrDefault());
        }

        [HttpGet]
        [Route("userphotos")]
        public async Task<ActionResult> 
            Get([FromForm] string userName)
        {
            var user = (await userService.AllUsersAsync())
                .Where(u => u.UserName == userName)
                .FirstOrDefault();
            if(user == null)
            {
                return NotFound();
            }
            var photos = await photoService.AllPhotosAsync();
            var userPhotos = photos
                .Where(p => p.User.UserName == user.UserName);
            if(userPhotos == null)
            {
                return NoContent();
            }

            return Ok(userPhotos);
        }
    }
}