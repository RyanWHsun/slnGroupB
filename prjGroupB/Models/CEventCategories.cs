using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGroupB.Models
{
    public class CEventCategories
    {
        public int fEventCategoryId { get; set; }

        // 分類名稱
        public string fEventCategoryName { get; set; }

        // 類別描述
        public string fCategoryDescription { get; set; }

        // 建立日期
        public DateTime fEventCreatedDate { get; set; }
    }
}