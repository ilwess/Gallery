using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Abstract
{
    public interface IAuthenticateService
    {
        bool IsAuthenticate(TokenRequest request, out string token);
    }
}
