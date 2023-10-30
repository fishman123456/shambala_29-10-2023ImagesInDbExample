using ImagesInDbExample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesInDbExample.Service
{
    internal class ImageService
    {
        public List<Image> GetAllImages()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Images.ToList();
            }
        }

        public Image AddImage(Image image)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Images.Add(image);
                db.SaveChanges();
                return image;
            }
        }
    }
}
