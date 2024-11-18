using prjGroupB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjGroupB.Views
{
    public partial class FrmUserPosts : Form
    {
        private CPost _selected;
        private CPostManager _manager = new CPostManager();
        public FrmUserPosts()
        {
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            FrmPostEditor f = new FrmPostEditor();
            f.ShowDialog();
            if (f.isOK == DialogResult.OK)
            {
                _manager.insert(f.post);
                displayUserPosts();
            }
        }

        private void btnInsertCategory_Click(object sender, EventArgs e)
        {
            FrmPostCategoryEditor f = new FrmPostCategoryEditor();
            f.ShowDialog();
            if (f.isOK == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(f.message))
                    return;
                _manager.insertCategory(f.message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selected == null)
                return;
            _manager.delete(_selected);
            _selected = null;
            displayUserPosts();
        }
        private void selectUserPost(CPost post)
        {
            _selected = post;
        }

        private void FrmUserPosts_Load(object sender, EventArgs e)
        {
            displayUserPosts();
        }
        private void displayUserPosts()
        {
            flpUserPosts.Controls.Clear();
            List<CPost> userPosts = _manager.getUserPosts();
            foreach (CPost userPost in userPosts)
            {
                PostBox postBox = new PostBox();
                postBox.post = userPost;
                postBox.DselectUserPost += this.selectUserPost;
                flpUserPosts.Controls.Add(postBox);
            }
        }
    }
}
