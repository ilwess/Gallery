using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string ImageLink { get; set; }
        public string Name { get; set; }

        public User User { get; set; }
    }
}
