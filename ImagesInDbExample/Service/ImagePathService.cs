using ImagesInDbExample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesInDbExample.Service
{
    internal class ImagePathService
    {
        public List<ImagePath> GetAllImagePaths()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.ImagePaths.ToList();
            }
        }

        public ImagePath AddImagePath(ImagePath imagePath)
        {
            using (var db = new ApplicationDbContext())
            {
                db.ImagePaths.Add(imagePath);
                db.SaveChanges();
                return imagePath;
            }
        }
    }
}
