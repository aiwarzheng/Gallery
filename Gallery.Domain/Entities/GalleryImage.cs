using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.Domain.Entities
{
    /// <summary>
    /// images
    /// </summary>
    public class GalleryImage
    {
        public virtual long ImageID { get; set; }
        public virtual string Url { get; set; }
        /// <summary>
        /// image type, 1 for house, 2 for car, 3 for animal, 4 for environment
        /// </summary>
        public virtual int Type { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
    }
}
