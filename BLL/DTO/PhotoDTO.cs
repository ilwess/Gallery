﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class PhotoDTO
    {
        public int Id { get; set; }
        public string ImageLink { get; set; }
        public string Name { get; set; }
        public UserDTO User { get; set; }
    }
}
