using Gallery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.Domain.Abstract
{
    /// <summary>
    /// image repository
    /// </summary>
    public interface IGalleryRepository
    {
        /// <summary>
        /// Get images, according to some conditions, like image type, current page etc
        /// </summary>
        /// <param name="u">current user</param>
        /// <param name="type">image type, house, car etc</param>
        /// <param name="page">current page</param>
        /// <param name="itemperpage">item per page</param>
        /// <param name="totalItems">total image count, according to the condition</param>
        /// <returns>collection of userimage class</returns>
        IEnumerable<UserImage> GetImages(User u, int type, int page, int itemperpage, out int totalItems);

        /// <summary>
        /// Get images, according to user's preference, like or dislike
        /// </summary>
        /// <param name="u">current user</param>
        /// <param name="type">preference, 1 for like and 0 for dislike</param>
        /// <param name="page">current page</param>
        /// <param name="itemperpage">item per page</param>
        /// <param name="totalItems">total image count, according to the condition</param>
        /// <returns>collection of userimage class</returns>
        IEnumerable<UserImage> GetUserPreferedImages(User u, int type, int page, int itemperpage, out int totalItems);

        /// <summary>
        /// change image state by user's preference
        /// </summary>
        /// <param name="u">current user</param>
        /// <param name="imageID">image id</param>
        /// <param name="like">user's preference, 0 for dislike, 1 for like</param>
        /// <returns>change state</returns>
        bool ChangeImagePreference(User u, long imageID, int like);
    }
}
