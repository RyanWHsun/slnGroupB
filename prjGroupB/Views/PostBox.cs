using prjGroupB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjGroupB.Views
{
    public delegate void DselectPost(CPost post);
    public partial class PostBox : UserControl
    {
        private CPost _post;
        public event DselectPost DselectUserPost;
        
        public PostBox()
        {
            InitializeComponent();
        }
        public CPost post
        {
            get { return _post; }
            set 
            {
                _post = value;
                if (_post.fImages.Count > 0)
                {
                    Stream s = new MemoryStream(_post.fImages[0]);
                    pbFirstImage.Image = Bitmap.FromStream(s);
                }
                txtCreatedAt.Text = _post.fCreatedAt.ToString();
                if (_post.fCreatedAt < _post.fUpdatedAt)
                    txtCreatedAt.Text = _post.fUpdatedAt.ToString();
                txtTag.Text = "";
                string pattern = @"#([^#\s]+)";
                MatchCollection matches = Regex.Matches(_post.fContent, pattern);
                foreach (Match match in matches)
                {
                    txtTag.Text += match.ToString() + " ";
                }
            }
        }

        private void pbFirstImage_Click(object sender, EventArgs e)
        {
            if (DselectUserPost != null)
                DselectUserPost(post);
        }

        private void txtTag_Click(object sender, EventArgs e)
        {
            if (DselectUserPost != null)
                DselectUserPost(post);
        }

        private void txtCreatedAt_Click(object sender, EventArgs e)
        {
            if (DselectUserPost != null)
                DselectUserPost(post);
        }
    }
}
