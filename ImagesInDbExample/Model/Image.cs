using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesInDbExample.Model
{
    internal class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
