using Gallery.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gallery.Domain.Entities;
using log4net;
using NHibernate;
using Gallery.Domain.Utils;
using NHibernate.Criterion;

namespace Gallery.Domain.Concrete
{
    /// <summary>
    /// gallery provided by MSSQL
    /// </summary>
    public class MSSQLGalleryRepository : IGalleryRepository
    {
        private ILog log = LogManager.GetLogger("MSSQLGalleryRepository");

        public bool ChangeImagePreference(User u, long imageID, int like)
        {
            try
            {
                using (ISession session = HibernateUtil.OpenSession())
                {
                    GalleryImage image = session.QueryOver<GalleryImage>()
                        .Where(m => m.ImageID == imageID).SingleOrDefault();
                    if(image != null)
                    {
                        UserPreference p = new UserPreference
                        {
                            Image = image,
                            IsLiked = like == 1 ? true : false,
                            User = u
                        };

                        using (ITransaction t = session.BeginTransaction())
                        {
                            session.SaveOrUpdate(p);
                            t.Commit();

                            return true;
                        }
                    };

                    return false;
                }
            }

            catch (Exception ex)
            {
                log.Error(ex);
                return false;
            }
        }

        public IEnumerable<UserImage> GetImages(User u, int type, int page, int itemperpage, out int totalItems)
        {
            totalItems = 0;
            try
            {
                using (ISession session = HibernateUtil.OpenSession())
                {
                    IList<GalleryImage> images = null;
                    if (type == 0)
                    {
                        totalItems = session.QueryOver<GalleryImage>().RowCount();

                        images = session.QueryOver<GalleryImage>()
                            .OrderBy(m=>m.ImageID).Asc
                            .Skip((page - 1) * itemperpage)
                            .Take(itemperpage).List<GalleryImage>();
                    }
                    else
                    {
                        totalItems = session.QueryOver<GalleryImage>()
                            .Where(m => m.Type == type)
                            .RowCount();
                        images = session.QueryOver<GalleryImage>()
                            .Where(m=>m.Type==type)
                            .OrderBy(m=>m.ImageID).Asc
                            .Skip((page - 1) * itemperpage)
                            .Take(itemperpage).List<GalleryImage>();
                    }

                    IList<long> imageIDs = new List<long>();
                    foreach(var image in images)
                    {
                        imageIDs.Add(image.ImageID);
                    }

                    IList<UserPreference> users = session.QueryOver<UserPreference>()
                        .Where(m => m.User.UserID == u.UserID)
                        .And(Restrictions.On<UserPreference>(p=>p.Image.ImageID).IsIn(imageIDs.ToArray()))
                        .List<UserPreference>();

                    var list = from img in images
                               join up in users on img.ImageID equals up.Image.ImageID into a
                               from b in a.DefaultIfEmpty(new UserPreference())
                               select new
                               {
                                   img,
                                   b.IsLiked
                               };

                    IList<UserImage> userImages = new List<UserImage>();
                    foreach(var image in list)
                    {
                        UserImage uimg = new UserImage
                        {
                            ImageID = image.img.ImageID,
                            Url = image.img.Url,
                            Title = image.img.Title,
                            Type = image.img.Type,
                            Description = image.img.Description
                        };

                        if(image.IsLiked == null)
                        {
                            uimg.LikeType = 0;
                        }
                        else if(image.IsLiked == true)
                        {
                            uimg.LikeType = 1;
                        }
                        else
                        {
                            uimg.LikeType = 2;
                        }

                        userImages.Add(uimg);
                    }

                    return userImages;
                }
            }
            catch(Exception ex)
            {
                log.Error(ex);
                return new List<UserImage>();
            }
        }

        public IEnumerable<UserImage> GetUserPreferedImages(User u, int type, int page, int itemperpage, out int totalItems)
        {
            totalItems = 0;

            try
            {
                using(ISession session = HibernateUtil.OpenSession())
                {
                    totalItems = session.QueryOver<UserPreference>()
                        .Where(m => m.User.UserID == u.UserID && m.IsLiked == (type == 1))
                        .RowCount();

                    IList<UserPreference> users = session.QueryOver<UserPreference>()
                        .Where(m => m.User.UserID==u.UserID && m.IsLiked == (type == 1))
                        .OrderBy(m => m.ID).Asc
                        .Skip((page - 1) * itemperpage)
                        .Take(itemperpage).List<UserPreference>();

                    IList<UserImage> userImages = new List<UserImage>();
                    foreach (var image in users)
                    {
                        UserImage uimg = new UserImage
                        {
                            ImageID = image.Image.ImageID,
                            Url = image.Image.Url,
                            Title = image.Image.Title,
                            Type = image.Image.Type,
                            Description = image.Image.Description
                        };

                        if (image.IsLiked == null)
                        {
                            uimg.LikeType = 0;
                        }
                        else if (image.IsLiked == true)
                        {
                            uimg.LikeType = 1;
                        }
                        else
                        {
                            uimg.LikeType = 2;
                        }

                        userImages.Add(uimg);
                    }

                    return userImages;
                }
            }
            catch(Exception ex)
            {
                log.Error(ex);
                return new List<UserImage>();
            }
        }
    }
}
