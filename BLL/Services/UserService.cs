using AutoMapper;
using BLL.Abstract;
using BLL.DTO;
using Domain.Abstract;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork db;
        private readonly IMapper mapper;

        public UserService(IUnitOfWork uow, IMapper mapper)
        {
            db = uow;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> AllUsersAsync()
        {
            var users = await db.UserRepo.GetAllAsync();
            IEnumerable<UserDTO> usersDTO 
                = mapper
                .Map<IEnumerable<User>,IEnumerable<UserDTO>>(users);
            return usersDTO;
        }

        public async Task CreateUserAsync(UserDTO newUser)
        {
            User user = new User
            {
                Email = newUser.Email,
                UserName = newUser.UserName,
                PasswordHash = newUser.PasswordHash,
            };
            await db.UserRepo.CreateAsync(user);
            await db.CommitAsync();
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            User user = await db.UserRepo.GetAsync(id);
            UserDTO userDTO = mapper.Map<User, UserDTO>(user);
            return userDTO;
        }

        public async Task<IEnumerable<UserDTO>> GetUsersAsync(
            Expression<Func<UserDTO, bool>> predicate)
        {
            Expression<Func<UserDTO, bool>> expression = predicate; 
               Expression<Func<User, bool>> expr = mapper
                .Map<Expression<Func<User, bool>>>(predicate);
            return mapper.Map<IEnumerable<UserDTO>>(
                    await db.UserRepo.GetAsync(expr));
        }

        public async Task<bool> IsExist(string userName, string email)
        {
            //IEnumerable<UserDTO> users = 
            //    await GetUsersAsync(u => u.UserName == userName || u.Email == email);
            IEnumerable<UserDTO> users =
                await AllUsersAsync();
            users = users
                .Where(o => o.UserName == userName || o.Email == email);
            if (users.Count() > 0)
            {
                return true;
            }
            return false;
        }

        public async Task UpdateAsync(UserDTO model)
        {
            await db.UserRepo.UpdateAsync(mapper.Map<User>(model));
        }
    }
}
