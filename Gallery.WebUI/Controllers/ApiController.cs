using Gallery.Domain.Abstract;
using Gallery.Domain.Entities;
using Gallery.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Gallery.WebUI.Controllers
{
    public class ApiController : Controller
    {
        private IGalleryRepository repository = null;

        const int ITEM_PER_PAGE = 6;

        public ApiController(IGalleryRepository repo)
        {
            repository = repo;
        }

        /// <summary>
        /// get images according to some conditions, 
        /// content:all, like or dislike
        /// type:house, car, animal or environment
        /// page:current page
        /// </summary>
        /// <param name="user">current user</param>
        /// <param name="content">content, all, like or dislike</param>
        /// <param name="type">hour, car, animal or environment</param>
        /// <param name="page">current page</param>
        /// <returns>GalleryImages class</returns>
        [HttpPost]
        public JsonResult Images(User user, int content, int type, int page)
        {
            int totalItems = 0;
            JsonResult json = new JsonResult();
            json.ContentEncoding = Encoding.UTF8;
            json.ContentType = "application/json";

            IEnumerable<UserImage> images = null;
            if (content == 0) //home page, get image by category
            {
                images = repository.GetImages(user, type, page, ITEM_PER_PAGE, out totalItems);
            }
            else
            {
                type = content == 1 ? 1 : 0; //1:like 0:dislike

                images = repository.GetUserPreferedImages(user, type, page, ITEM_PER_PAGE, out totalItems);
            }

            GalleryImages g = new GalleryImages
            {
                Images = images,
                Pages = new PageInfo
                {
                    CurrentPage = page,
                    ItemPerPage = ITEM_PER_PAGE,
                    TotalItems = totalItems
                }
            };

            json.Data = g;

            return json;
        }

        /// <summary>
        /// change image's state by user's preference
        /// </summary>
        /// <param name="user">current user</param>
        /// <param name="imageId">image id</param>
        /// <param name="like">preference, 0 for dislike,1 for like</param>
        /// <returns>change state, true or false</returns>
        [HttpPost]
        public JsonResult Prefer(User user, long imageId, int like)
        {
            JsonResult json = new JsonResult();
            json.ContentEncoding = Encoding.UTF8;
            json.ContentType = "application/json";

            bool b = repository.ChangeImagePreference(user, imageId, like);
            IDictionary<string, bool> status = new Dictionary<string, bool>();
            status.Add("state", b);
            json.Data = status;

            return json;
        }
    }
}