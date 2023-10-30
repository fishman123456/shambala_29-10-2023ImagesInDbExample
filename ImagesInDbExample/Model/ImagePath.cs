using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesInDbExample.Model
{
    internal class ImagePath
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileLocation { get; set; }

        public override string ToString()
        {
            return FileName;
        }
    }
}
