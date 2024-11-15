using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGroupB.Models
{
    public class CPost
    {
        public int fPostId { get; set; }
        public string fTitle { get; set; }
        public string fContent { get; set; }
        public DateTime fUpdatedAt { get; set; }
        public bool fIsPublic { get; set; }
        public string fCategory { get; set; }
        public List<byte[]> fImages { get; set; }
    }
}
