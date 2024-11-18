using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using prjGroupB.Models;

namespace prjGroupB.Views
{
    public delegate void DimageClick(CEventImage p);
    public delegate void EventClick(CEvents a);
    public partial class EventImageBox : UserControl
    {
        public event EventClick orderevent;
        public event DimageClick orderImage;
        private CEvents _Event;
        private CEventImage _EventImage;
        public EventImageBox()
        {
            InitializeComponent();
        }
        public CEvents Event
        {
            get { return _Event; }
            set
            {
                _Event = value;

                // 賦值到標籤
                labName.Text = _Event?.fEventName ?? "無名稱";
                labEventLocation.Text = _Event?.fEventLocation ?? "無地點";
            }
        }
        public CEventImage Image
        {
            get { return _EventImage; }
            set
            {
                _EventImage = value;

                if (_EventImage?.fEventImage != null)
                {
                    try
                    {
                        using (Stream s = new MemoryStream(_EventImage.fEventImage))
                        {
                            pictureBox1.Image = Bitmap.FromStream(s);
                        }
                    }
                    catch
                    {
                        // 若圖片加載失敗，設定為預設圖片或清空
                        pictureBox1.Image = null;
                    }
                }
                else
                {
                    pictureBox1.Image = null; // 若無圖片資料，清空圖片
                }
            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            orderImage?.Invoke(this._EventImage);

        }

        private void labName_Click(object sender, EventArgs e)
        {
            //if (orderevent != null)
            //    orderevent(this._Event);
            orderevent?.Invoke(this._Event);
        }

        private void labEventLocation_Click(object sender, EventArgs e)
        {
            //if (orderevent != null)
            //    orderevent(this._Event);
            orderevent?.Invoke(this._Event);
        }
    }
}
