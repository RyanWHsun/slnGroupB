using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGroupB.Models
{
    public class CEventImage
    {
        public int fEventImageId { get; set; }
        // 活動ID
        public int fEventId { get; set; }
        // 圖片
        public byte[] fEventImage { get; set; }
    }
}
