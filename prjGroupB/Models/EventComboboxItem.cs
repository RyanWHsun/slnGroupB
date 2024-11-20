using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGroupB.Models
{
    public class EventComboboxItem
    {
        public string Text { get; set; } // 顯示在下拉選單中的名稱
        public object Value { get; set; }   // 隱藏的值，例如類別 ID
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Text}";
        }

    }
}
