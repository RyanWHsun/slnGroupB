using prjGroupB.Models;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace prjGroupB.Views
{
    public partial class FrmPostEditor : Form
    {
        private CPost _post;
        public FrmPostEditor()
        {
            InitializeComponent();
        }
        public DialogResult isOK { get; set; }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            // 計算需要顯示的行數
            int lineCount = rtbContent.GetLineFromCharIndex(rtbContent.TextLength) + 1;

            // 設置每行的高度
            int maxLines = 5;
            int lineHeight = rtbContent.Font.Height;
            int maxHeight = maxLines * lineHeight + rtbContent.Margin.Top + rtbContent.Margin.Bottom;

            // 設置最大高度
            if (lineCount * lineHeight > maxHeight)
            {
                rtbContent.Height = maxHeight;
                rtbContent.ScrollBars = RichTextBoxScrollBars.Vertical;
            }
            else
            {
                rtbContent.Height = lineCount * lineHeight + rtbContent.Margin.Top + rtbContent.Margin.Bottom;
                rtbContent.ScrollBars = RichTextBoxScrollBars.None;
            }
            picPost.Top = rtbContent.Bottom + 10;
        }
        private void setRichTextBoxSize()
        {
            Font font = new Font("新細明體", 12);
            rtbContent.Font = font;

            // 假設文本為一些示例文本
            string sampleText = "This is a sample text in RichTextBox.";

            // 測量整段文本的寬度
            int textWidth = TextRenderer.MeasureText(sampleText, font).Width;

            // 設置寬度
            rtbContent.Width = textWidth + rtbContent.Margin.Left + rtbContent.Margin.Right;

            // 設置高度
            int lineHeight = font.Height;
            rtbContent.Height = lineHeight + rtbContent.Margin.Top + rtbContent.Margin.Bottom;
        }

        private void FrmPostEditor_Load(object sender, EventArgs e)
        {
            setRichTextBoxSize();
            setCmbIsPublic();
        }

        private void btnIsPicture_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "房間照片|*.png|房間照片|*.jpg";
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            picPost.Image = Bitmap.FromFile(openFileDialog1.FileName);

            FileStream imgStream = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(imgStream);
            this.post.fImage = reader.ReadBytes((int)imgStream.Length);
            reader.Close();
            imgStream.Close();
        }
        private void setCmbIsPublic()
        {
            cmbIsPublic.Items.Add("私人");
            cmbIsPublic.Items.Add("公開");
            cmbIsPublic.SelectedIndex = 0;
        }
        public CPost post
        { 
            get 
            {
                if (_post == null)
                    _post = new CPost();
                _post.fTitle = txtTitle.Text;
                if (cmbCategory.SelectedItem != null)
                    _post.fCategory = cmbCategory.SelectedItem.ToString();
                if (cmbIsPublic.SelectedItem.ToString() == "公開")
                    _post.fIsPublic = true;
                else
                    _post.fIsPublic = false;
                _post.fContent = rtbContent.Text;
                return _post;
            }
            set
            {
                _post = value;
            }
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            this.isOK = DialogResult.OK;
            Close();
        }
    }
}
