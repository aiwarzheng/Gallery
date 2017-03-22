using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.Domain.Entities
{
    public class User
    {
        public User()
        {
            UserID = Guid.NewGuid().ToString();
            Preferences = new List<UserPreference>();
        }

        public virtual string UserID { get; set; }
        public virtual string Email { get; set; }
        public virtual IList<UserPreference> Preferences { get; set; }
    }
}
