using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGroupB.Models
{
    public class CEvents
    {
        public int fEventId { get; set; }
        // 會員ID
        public int fUserId { get; set; }
        // 活動名稱
        public string fEventName { get; set; }
        // 活動描述
        public string fEventDescription { get; set; }
        // 開始日期
        public string fEventStartDate { get; set; }
        // 結束日期
        public string fEventEndDate { get; set; }
        // 活動地點
        public string fEventLocation { get; set; }
        // 建立日期
        public DateTime fEventCreatedDate { get; set; }
        // 更新日期
        public DateTime fEventUpdatedDate { get; set; }
        //活動費用
        public decimal fEventActivityfee { get;set; }
        //活動網址
        public string fEventURL { get;set; }
    }
}
