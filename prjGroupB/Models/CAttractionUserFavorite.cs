using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGroupB.Models {
    public class CAttractionUserFavorite {
        public int fFavoriteId {  get; set; }
        public int fUserId { get; set; }
        public int fAttractionId {  get; set; }
        public DateTime fCreatedDate { get; set; }
    }
}
