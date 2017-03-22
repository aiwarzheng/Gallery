using Gallery.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gallery.WebUI.Controllers
{
    public class GalleryController : Controller
    {
        // GET: Gallery
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// get pagination
        /// </summary>
        /// <param name="total">total images</param>
        /// <param name="itemPerPage">image per page</param>
        /// <param name="page">current page</param>
        /// <returns>pagination html</returns>
        public PartialViewResult Paginate(int total, int itemPerPage, int page)
        {
            PageInfo pageInfo = new PageInfo
            {
                CurrentPage = page,
                ItemPerPage = itemPerPage,
                TotalItems = total
            };

            return PartialView("Paginate", pageInfo);
        }
    }
}