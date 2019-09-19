using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Abstract;
using BLL.DTO;
using Domain.Models;
using Gallery.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Gallery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticateService auth;
        private readonly IUserService userService;

        public AccountController(
            IAuthenticateService auth,
            IUserService userService)
        {
            this.auth = auth;
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> RegisterUser(
            [FromBody] RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("InvalidRequest");
            }

            if(await userService.IsExist(model.UserName, model.Email))
            {
                return BadRequest("This user name or email already exist");
            }

            UserDTO newUser = new UserDTO()
            {
                UserName = model.UserName,
                PasswordHash = model.PasswordHash,
                Email = model.Email,
            };

            await userService.CreateUserAsync(newUser);

            IEnumerable<UserDTO> users = await userService
                .AllUsersAsync();

            UserDTO user = users
                .Where(u => u.Email == model.Email)
                .FirstOrDefault();
           

            string uri = "https://localhost:44390/api/account/user/" + user.Id;

            return Created(uri, user);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("token")]
        public ActionResult RequestToken([FromBody] TokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Request");
            }

            string token;

            if(auth.IsAuthenticate(request, out token))
            {
                return Ok(token);
            }

            return BadRequest("Invalid Request");
        }

    }
}