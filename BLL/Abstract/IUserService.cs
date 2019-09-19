using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> AllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(int id);
        Task<IEnumerable<UserDTO>> GetUsersAsync(
            Expression<Func<UserDTO, bool>> predicate);
        Task UpdateAsync(UserDTO user);
        Task CreateUserAsync(UserDTO newUser);
        Task<bool> IsExist(string userName, string email);
    }
}
