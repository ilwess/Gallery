using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IUserManagementService
    {
        bool IsValidUser(string userName, string password);
    }
}
