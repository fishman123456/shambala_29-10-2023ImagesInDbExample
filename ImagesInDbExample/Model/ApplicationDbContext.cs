using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesInDbExample.Model
{
    internal class ApplicationDbContext : DbContext
    {
        public DbSet<ImagePath> ImagePaths { get; set; } 
        public DbSet<Image> Images { get; set; } 

        public ApplicationDbContext() : base("DefaultConnection")
        {

        }
    }
}
