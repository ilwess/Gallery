
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gallery.ViewModels
{
    public class DownloadImageViewModel
    {
        IFormFile file { get; set; }
        string Name { get; set; }
    }
}
