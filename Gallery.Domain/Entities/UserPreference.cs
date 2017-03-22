using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.Domain.Entities
{
    public class UserPreference
    {
        public virtual long ID { get; set; }
        public virtual User User { get; set; }
        public virtual GalleryImage Image { get; set; }
        public virtual bool? IsLiked { get; set; }
    }
}
