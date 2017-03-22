using Gallery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gallery.WebUI.Models
{
    public class GalleryImages
    {
        public IEnumerable<UserImage> Images { get; set; }
        public PageInfo Pages { get; set; }
    }
}