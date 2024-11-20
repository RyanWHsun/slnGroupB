﻿namespace prjGroupB.Views
{
    partial class FrmPostEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPostEditor));
            this.rtbContent = new System.Windows.Forms.RichTextBox();
            this.picPost = new System.Windows.Forms.PictureBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblUpdatedName = new System.Windows.Forms.Label();
            this.lblUpdated = new System.Windows.Forms.Label();
            this.btnPost = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnIsPicture = new System.Windows.Forms.Button();
            this.cmbIsPublic = new System.Windows.Forms.ComboBox();
            this.lblPublic = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblHint = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picPost)).BeginInit();
            this.SuspendLayout();
            // 
            // rtbContent
            // 
            this.rtbContent.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rtbContent.Location = new System.Drawing.Point(73, 119);
            this.rtbContent.Name = "rtbContent";
            this.rtbContent.Size = new System.Drawing.Size(348, 28);
            this.rtbContent.TabIndex = 1;
            this.rtbContent.Text = "";
            this.rtbContent.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // picPost
            // 
            this.picPost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(231)))), ((int)(((byte)(203)))));
            this.picPost.Location = new System.Drawing.Point(73, 159);
            this.picPost.Name = "picPost";
            this.picPost.Size = new System.Drawing.Size(348, 200);
            this.picPost.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPost.TabIndex = 2;
            this.picPost.TabStop = false;
            this.picPost.DoubleClick += new System.EventHandler(this.picPost_DoubleClick);
            // 
            // txtTitle
            // 
            this.txtTitle.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtTitle.Location = new System.Drawing.Point(73, 39);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(348, 29);
            this.txtTitle.TabIndex = 3;
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(73, 79);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(220, 28);
            this.cmbCategory.TabIndex = 4;
            // 
            // lblUpdatedName
            // 
            this.lblUpdatedName.AutoSize = true;
            this.lblUpdatedName.Location = new System.Drawing.Point(19, 619);
            this.lblUpdatedName.Name = "lblUpdatedName";
            this.lblUpdatedName.Size = new System.Drawing.Size(77, 12);
            this.lblUpdatedName.TabIndex = 5;
            this.lblUpdatedName.Text = "最後更新時間";
            this.lblUpdatedName.Visible = false;
            // 
            // lblUpdated
            // 
            this.lblUpdated.AutoSize = true;
            this.lblUpdated.Location = new System.Drawing.Point(102, 619);
            this.lblUpdated.Name = "lblUpdated";
            this.lblUpdated.Size = new System.Drawing.Size(33, 12);
            this.lblUpdated.TabIndex = 6;
            this.lblUpdated.Text = "label1";
            this.lblUpdated.Visible = false;
            // 
            // btnPost
            // 
            this.btnPost.FlatAppearance.BorderSize = 0;
            this.btnPost.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPost.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnPost.Image = ((System.Drawing.Image)(resources.GetObject("btnPost.Image")));
            this.btnPost.Location = new System.Drawing.Point(414, 601);
            this.btnPost.Name = "btnPost";
            this.btnPost.Size = new System.Drawing.Size(45, 45);
            this.btnPost.TabIndex = 7;
            this.btnPost.UseVisualStyleBackColor = true;
            this.btnPost.Click += new System.EventHandler(this.btnPost_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTitle.Location = new System.Drawing.Point(16, 40);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(54, 26);
            this.lblTitle.TabIndex = 8;
            this.lblTitle.Text = "標題";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblCategory.Location = new System.Drawing.Point(16, 79);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(54, 26);
            this.lblCategory.TabIndex = 9;
            this.lblCategory.Text = "分類";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(16, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 26);
            this.label1.TabIndex = 10;
            this.label1.Text = "內文";
            // 
            // btnIsPicture
            // 
            this.btnIsPicture.FlatAppearance.BorderSize = 0;
            this.btnIsPicture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIsPicture.Image = ((System.Drawing.Image)(resources.GetObject("btnIsPicture.Image")));
            this.btnIsPicture.Location = new System.Drawing.Point(424, 34);
            this.btnIsPicture.Name = "btnIsPicture";
            this.btnIsPicture.Size = new System.Drawing.Size(35, 35);
            this.btnIsPicture.TabIndex = 11;
            this.btnIsPicture.UseVisualStyleBackColor = true;
            this.btnIsPicture.Click += new System.EventHandler(this.btnIsPicture_Click);
            // 
            // cmbIsPublic
            // 
            this.cmbIsPublic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIsPublic.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmbIsPublic.FormattingEnabled = true;
            this.cmbIsPublic.Location = new System.Drawing.Point(362, 79);
            this.cmbIsPublic.Name = "cmbIsPublic";
            this.cmbIsPublic.Size = new System.Drawing.Size(59, 28);
            this.cmbIsPublic.TabIndex = 12;
            // 
            // lblPublic
            // 
            this.lblPublic.AutoSize = true;
            this.lblPublic.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblPublic.Location = new System.Drawing.Point(305, 79);
            this.lblPublic.Name = "lblPublic";
            this.lblPublic.Size = new System.Drawing.Size(54, 26);
            this.lblPublic.TabIndex = 13;
            this.lblPublic.Text = "分享";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnFirst
            // 
            this.btnFirst.FlatAppearance.BorderSize = 0;
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFirst.Image = ((System.Drawing.Image)(resources.GetObject("btnFirst.Image")));
            this.btnFirst.Location = new System.Drawing.Point(165, 365);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(35, 35);
            this.btnFirst.TabIndex = 14;
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.FlatAppearance.BorderSize = 0;
            this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevious.Image = ((System.Drawing.Image)(resources.GetObject("btnPrevious.Image")));
            this.btnPrevious.Location = new System.Drawing.Point(206, 365);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(35, 35);
            this.btnPrevious.TabIndex = 15;
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnLast
            // 
            this.btnLast.FlatAppearance.BorderSize = 0;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.Image = ((System.Drawing.Image)(resources.GetObject("btnLast.Image")));
            this.btnLast.Location = new System.Drawing.Point(288, 365);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(35, 35);
            this.btnLast.TabIndex = 16;
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
            this.btnNext.Location = new System.Drawing.Point(247, 365);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(35, 35);
            this.btnNext.TabIndex = 17;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblHint
            // 
            this.lblHint.AutoSize = true;
            this.lblHint.Location = new System.Drawing.Point(19, 640);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(110, 12);
            this.lblHint.TabIndex = 18;
            this.lblHint.Text = "* 雙擊圖片即可刪除";
            // 
            // FrmPostEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(231)))), ((int)(((byte)(203)))));
            this.ClientSize = new System.Drawing.Size(484, 661);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnFirst);
            this.Controls.Add(this.lblPublic);
            this.Controls.Add(this.cmbIsPublic);
            this.Controls.Add(this.btnIsPicture);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnPost);
            this.Controls.Add(this.lblUpdated);
            this.Controls.Add(this.lblUpdatedName);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.picPost);
            this.Controls.Add(this.rtbContent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmPostEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmPostEditor";
            ((System.ComponentModel.ISupportInitialize)(this.picPost)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox rtbContent;
        private System.Windows.Forms.PictureBox picPost;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblUpdatedName;
        private System.Windows.Forms.Label lblUpdated;
        private System.Windows.Forms.Button btnPost;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnIsPicture;
        private System.Windows.Forms.ComboBox cmbIsPublic;
        private System.Windows.Forms.Label lblPublic;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblHint;
    }
}