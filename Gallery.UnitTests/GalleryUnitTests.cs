using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using Gallery.Domain.Utils;
using Gallery.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Gallery.UnitTests
{
    [TestClass]
    public class GalleryUnitTests
    {
        [TestMethod]
        public void NHibernate_Session_Test()
        {
            bool b = true;
            try
            {
                using (ISession session = HibernateUtil.OpenSession())
                {
                }
            }
            catch
            {
                b = false;
            }

            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void NHibernate_Data_Test()
        {
            IList<GalleryImage> images = new List<GalleryImage>
            {
                new GalleryImage
                {
                    Type=1,
                    Url="https://s-media-cache-ak0.pinimg.com/736x/73/de/32/73de32f9e5a0db66ec7805bb7cb3f807.jpg"
                },
                new GalleryImage
                {
                    Type=1,
                    Url="https://s-media-cache-ak0.pinimg.com/originals/34/3a/96/343a96a7cdd373f36b2ab0580f46af84.jpg"
                }
            };

            User u = new User();
            u.UserID = "test";

            using(ISession session = HibernateUtil.OpenSession())
            {
                using(ITransaction t = session.BeginTransaction())
                {
                    session.SaveOrUpdate(u);
                    foreach(GalleryImage image in images)
                    {
                        session.Save(image);

                        UserPreference up = new UserPreference
                        {
                            User = u,
                            Image = image,
                            IsLiked = true
                        };

                        session.Save(up);
                    }

                    t.Commit();
                }
            }
        }

        [TestMethod]
        public void NHibernate_Data_Init_House_Test()
        {
            IList<GalleryImage> houses = new List<GalleryImage>
            {
                new GalleryImage
                {
                    Title="House",
                    Description="Lorem ipsum dolor sit amet, dolore eiusmod quis tempor incididunt ut et dolore Ut veniam unde nostrudlaboris.",
                    Url="http://weknownyourdreamz.com/images/house/house-05.jpg",
                    Type=1
                },
                new GalleryImage
                {
                    Title="House",
                    Description="Lorem ipsum dolor sit amet, dolore eiusmod quis tempor incididunt ut et dolore Ut veniam unde nostrudlaboris.",
                    Url="http://modularhomeowners.com/wp-content/uploads/2012/12/commodoresample6.jpg",
                    Type=1
                },
                new GalleryImage
                {
                    Title="House",
                    Description="Lorem ipsum dolor sit amet, dolore eiusmod quis tempor incididunt ut et dolore Ut veniam unde nostrudlaboris.",
                    Url="http://clv.h-cdn.co/assets/15/50/980x784/gallery-1449795583-wenonah.jpg",
                    Type=1
                },
                new GalleryImage
                {
                    Title="House",
                    Description="Lorem ipsum dolor sit amet, dolore eiusmod quis tempor incididunt ut et dolore Ut veniam unde nostrudlaboris.",
                    Url="http://ei.marketwatch.com//Multimedia/2012/09/24/Photos/MG/MW-AU747_wolf_t_20120924164002_MG.jpg",
                    Type=1
                },
                new GalleryImage
                {
                    Title="House",
                    Description="Lorem ipsum dolor sit amet, dolore eiusmod quis tempor incididunt ut et dolore Ut veniam unde nostrudlaboris.",
                    Url="http://clv.h-cdn.co/assets/16/32/640x320/landscape-1470768620-perfect-fit-house-0916.jpg",
                    Type=1
                },
                new GalleryImage
                {
                    Title="House",
                    Description="Lorem ipsum dolor sit amet, dolore eiusmod quis tempor incididunt ut et dolore Ut veniam unde nostrudlaboris.",
                    Url="http://clv.h-cdn.co/assets/cm/15/09/54eb988eefedc_-_small-of-fame-yellow-house-0215-xln-81871600.jpg",
                    Type=1
                }
            };

            bool b = true;
            try
            {
                using (ISession session = HibernateUtil.OpenSession())
                {
                    using (ITransaction t = session.BeginTransaction())
                    {
                        foreach (var image in houses)
                        {
                            session.Save(image);
                        }

                        t.Commit();
                    }
                }
            }
            catch
            {
                b = false;
            }

            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void NHibernate_Data_Init_Car_Test()
        {
            IList<GalleryImage> houses = new List<GalleryImage>
            {
                new GalleryImage
                {
                    Title="Car",
                    Description="Lorem ipsum dolor sit amet, dolore eiusmod quis tempor incididunt ut et dolore Ut veniam unde nostrudlaboris.",
                    Url="http://blog.caranddriver.com/wp-content/uploads/2015/11/BMW-2-series.jpg",
                    Type=2
                },
                new GalleryImage
                {
                    Title="Car",
                    Description="Lorem ipsum dolor sit amet, dolore eiusmod quis tempor incididunt ut et dolore Ut veniam unde nostrudlaboris.",
                    Url="http://blog.caranddriver.com/wp-content/uploads/2015/11/Mazda-61.jpg",
                    Type=2
                },
                new GalleryImage
                {
                    Title="Car",
                    Description="Lorem ipsum dolor sit amet, dolore eiusmod quis tempor incididunt ut et dolore Ut veniam unde nostrudlaboris.",
                    Url="http://www.adriennely.com/wp-content/uploads/2017/02/car_4.jpg",
                    Type=2
                },
                new GalleryImage
                {
                    Title="Car",
                    Description="Lorem ipsum dolor sit amet, dolore eiusmod quis tempor incididunt ut et dolore Ut veniam unde nostrudlaboris.",
                    Url="http://uncrate.com/p/2017/01/rain-prisk-1.jpg",
                    Type=2
                },
                new GalleryImage
                {
                    Title="Car",
                    Description="Lorem ipsum dolor sit amet, dolore eiusmod quis tempor incididunt ut et dolore Ut veniam unde nostrudlaboris.",
                    Url="http://cdn.newsapi.com.au/image/v1/2ca797e04a480e552205479588075dd7",
                    Type=2
                },
                new GalleryImage
                {
                    Title="Car",
                    Description="Lorem ipsum dolor sit amet, dolore eiusmod quis tempor incididunt ut et dolore Ut veniam unde nostrudlaboris.",
                    Url="http://mediaresources.idiva.com/media/luxury/photogallery/2014/Dec/main-pic6_600x450.jpg",
                    Type=2
                }
            };

            bool b = true;
            try
            {
                using (ISession session = HibernateUtil.OpenSession())
                {
                    using (ITransaction t = session.BeginTransaction())
                    {
                        foreach (var image in houses)
                        {
                            session.Save(image);
                        }

                        t.Commit();
                    }
                }
            }
            catch
            {
                b = false;
            }

            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void NHibernate_Data_Init_Animal_Test()
        {
            IList<GalleryImage> houses = new List<GalleryImage>
            {
                new GalleryImage
                {
                    Title="Animal",
                    Description="Lorem ipsum dolor sit amet, dolore eiusmod quis tempor incididunt ut et dolore Ut veniam unde nostrudlaboris.",
                    Url="http://www.gettyimages.com.au/gi-resources/images/Embed/new/embed2.jpg",
                    Type=3
                },
                new GalleryImage
                {
                    Title="Animal",
                    Description="Lorem ipsum dolor sit amet, dolore eiusmod quis tempor incididunt ut et dolore Ut veniam unde nostrudlaboris.",
                    Url="http://i.dailymail.co.uk/i/pix/2016/09/13/14/384A98B300000578-0-image-a-63_1473774825844.jpg",
                    Type=3
                },
                new GalleryImage
                {
                    Title="Animal",
                    Description="Lorem ipsum dolor sit amet, dolore eiusmod quis tempor incididunt ut et dolore Ut veniam unde nostrudlaboris.",
                    Url="http://www.robertthompsonphotography.com/wp-content/uploads/2013/01/Purple-Emperor-Apatura-iris_RST014132.jpg",
                    Type=3
                },
                new GalleryImage
                {
                    Title="Animal",
                    Description="Lorem ipsum dolor sit amet, dolore eiusmod quis tempor incididunt ut et dolore Ut veniam unde nostrudlaboris.",
                    Url="http://www.thinkstockphotos.com.au/ts-resources/images/home/TS_AnonHP_462882495_01.jpg",
                    Type=3
                },
                new GalleryImage
                {
                    Title="Animal",
                    Description="Lorem ipsum dolor sit amet, dolore eiusmod quis tempor incididunt ut et dolore Ut veniam unde nostrudlaboris.",
                    Url="http://cdn.images.express.co.uk/img/dynamic/galleries/x701/50375.jpg",
                    Type=3
                },
                new GalleryImage
                {
                    Title="Animal",
                    Description="Lorem ipsum dolor sit amet, dolore eiusmod quis tempor incididunt ut et dolore Ut veniam unde nostrudlaboris.",
                    Url="http://www.livescience.com/images/i/000/025/221/original/squirrel.jpg",
                    Type=3
                }
            };

            bool b = true;
            try
            {
                using (ISession session = HibernateUtil.OpenSession())
                {
                    using (ITransaction t = session.BeginTransaction())
                    {
                        foreach (var image in houses)
                        {
                            session.Save(image);
                        }

                        t.Commit();
                    }
                }
            }
            catch
            {
                b = false;
            }

            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void NHibernate_Data_Init_Environment_Test()
        {
            IList<GalleryImage> houses = new List<GalleryImage>
            {
                new GalleryImage
                {
                    Title="Environment",
                    Description="Lorem ipsum dolor sit amet, dolore eiusmod quis tempor incididunt ut et dolore Ut veniam unde nostrudlaboris.",
                    Url="http://www.nsw.scouts.com.au/images/stories/enviro.jpg",
                    Type=4
                },
                new GalleryImage
                {
                    Title="Environment",
                    Description="Lorem ipsum dolor sit amet, dolore eiusmod quis tempor incididunt ut et dolore Ut veniam unde nostrudlaboris.",
                    Url="http://arna-hse.ir/wp-content/uploads/2017/01/Poribesh.jpg",
                    Type=4
                },
                new GalleryImage
                {
                    Title="Environment",
                    Description="Lorem ipsum dolor sit amet, dolore eiusmod quis tempor incididunt ut et dolore Ut veniam unde nostrudlaboris.",
                    Url="http://i.ndtvimg.com/i/2016-06/earth_625x350_81465111956.jpg",
                    Type=4
                },
                new GalleryImage
                {
                    Title="Environment",
                    Description="Lorem ipsum dolor sit amet, dolore eiusmod quis tempor incididunt ut et dolore Ut veniam unde nostrudlaboris.",
                    Url="http://images.indianexpress.com/2015/05/environment-759.jpg",
                    Type=4
                },
                new GalleryImage
                {
                    Title="Environment",
                    Description="Lorem ipsum dolor sit amet, dolore eiusmod quis tempor incididunt ut et dolore Ut veniam unde nostrudlaboris.",
                    Url="http://recyclenation.com/resources/2015/6/greenearthday.jpg",
                    Type=4
                },
                new GalleryImage
                {
                    Title="Environment",
                    Description="Lorem ipsum dolor sit amet, dolore eiusmod quis tempor incididunt ut et dolore Ut veniam unde nostrudlaboris.",
                    Url="http://www.paperonline.org/uploads/images/big/environment-1.jpg",
                    Type=4
                }
            };

            bool b = true;
            try
            {
                using (ISession session = HibernateUtil.OpenSession())
                {
                    using (ITransaction t = session.BeginTransaction())
                    {
                        foreach (var image in houses)
                        {
                            session.Save(image);
                        }

                        t.Commit();
                    }
                }
            }
            catch
            {
                b = false;
            }

            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void NHibernate_Join_Query_Test()
        {
            User u = new User();

            using (ISession session = HibernateUtil.OpenSession())
            {
                int page = 1;
                int itemperpage = 6;

                IList<GalleryImage> images = session.QueryOver<GalleryImage>()
                    .Skip((page - 1) * itemperpage)
                    .Take(itemperpage).List<GalleryImage>();

                IList<UserPreference> users = session.QueryOver<UserPreference>()
                    .Where(m => m.User.UserID == u.UserID).List<UserPreference>();

                var list = from img in images
                           join up in users on img.ImageID equals up.Image.ImageID into a
                           from b in a.DefaultIfEmpty(new UserPreference())
                           select new
                           {
                               img,
                               b.IsLiked
                           };

                Assert.AreEqual(6, list.Count());
            }
        }
    }
}
