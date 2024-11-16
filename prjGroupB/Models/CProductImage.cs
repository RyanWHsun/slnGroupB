using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGroupB.Models
{
    public class CProductImage
    {
        public int fProductImageId { get; set; }
        public int fProductId { get; set; }
        public byte[] fImage { get; set; } 
    }
}
