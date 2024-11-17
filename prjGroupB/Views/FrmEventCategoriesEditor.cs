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
    public partial class FrmEventCategoriesEditor : Form
    {
        private CEventCategories _Categories;
        public DialogResult IsOk { get; set; }

        public FrmEventCategoriesEditor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.IsOk = DialogResult.OK;
            Close();
        }

        public CEventCategories Categories
        {
            get
            {
                if (_Categories == null)
                    _Categories = new CEventCategories();
                _Categories.fEventCategoryId = string.IsNullOrEmpty(textBox1.Text) ? 0 : Convert.ToInt32(textBox1.Text);
                _Categories.fEventCategoryName = textBox2.Text;
                _Categories.fCategoryDescription = textBox3.Text;
                _Categories.fEventCreatedDate = DateTime.Now;
                return _Categories;
            }
            set
            {
                _Categories = value;
                textBox1.Text = _Categories.fEventCategoryId.ToString();
                textBox2.Text = _Categories.fEventCategoryName;
                textBox3.Text = _Categories.fCategoryDescription.ToString();
                textBox4.Text = _Categories.fEventCreatedDate.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}