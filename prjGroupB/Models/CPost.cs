using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGroupB.Models
{
    public class CPost
    {
        private List<byte[]> _fImages;
        private List<string> _fTags;
        public int fPostId { get; set; }
        public string fTitle { get; set; }
        public string fContent { get; set; }
        public DateTime fCreatedAt { get; set; }
        public DateTime fUpdatedAt { get; set; }
        public bool fIsPublic { get; set; }
        public string fCategory { get; set; }
        public List<byte[]> fImages 
        {
            get 
            {
                if (_fImages == null)
                    _fImages = new List<byte[]> ();
                return _fImages; 
            }
            set { _fImages = value; } 
        }
        public List<string> fTags 
        {
            get 
            { 
                if (_fTags == null)
                    _fTags = new List<string> ();
                return _fTags; 
            }
            set { _fTags = value; } 
        }
    }
}
