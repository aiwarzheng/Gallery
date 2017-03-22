using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.Domain.Entities
{
    /// <summary>
    /// image for page display
    /// </summary>
    public class UserImage
    {
        public long ImageID { get; set; }
        public string Url { get; set; }
        public int Type { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// user preference, 0 for none, 1 for like, 2 for dislike
        /// </summary>
        public int LikeType { get; set; }
    }
}
