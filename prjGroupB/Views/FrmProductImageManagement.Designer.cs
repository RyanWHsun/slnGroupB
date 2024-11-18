namespace prjGroupB.Views
{
    partial class FrmProductImageManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProductImageManagement));
            this.dgvProductPic = new System.Windows.Forms.DataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtProductNamePic = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnDeletePic = new System.Windows.Forms.Button();
            this.txtProductId = new System.Windows.Forms.TextBox();
            this.btnSaveImage = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnUpload = new System.Windows.Forms.Button();
            this.picProductBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picProductBox)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvProductPic
            // 
            this.dgvProductPic.AllowUserToAddRows = false;
            this.dgvProductPic.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProductPic.BackgroundColor = System.Drawing.Color.LavenderBlush;
            this.dgvProductPic.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductPic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProductPic.Location = new System.Drawing.Point(0, 0);
            this.dgvProductPic.Name = "dgvProductPic";
            this.dgvProductPic.RowHeadersVisible = false;
            this.dgvProductPic.RowHeadersWidth = 72;
            this.dgvProductPic.RowTemplate.Height = 35;
            this.dgvProductPic.Size = new System.Drawing.Size(845, 854);
            this.dgvProductPic.TabIndex = 9;
            this.dgvProductPic.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductPic_RowEnter);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtProductNamePic);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
            this.splitContainer1.Panel1.Controls.Add(this.btnDeletePic);
            this.splitContainer1.Panel1.Controls.Add(this.txtProductId);
            this.splitContainer1.Panel1.Controls.Add(this.btnSaveImage);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.btnUpload);
            this.splitContainer1.Panel1.Controls.Add(this.picProductBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvProductPic);
            this.splitContainer1.Size = new System.Drawing.Size(1424, 854);
            this.splitContainer1.SplitterDistance = 575;
            this.splitContainer1.TabIndex = 10;
            // 
            // txtProductNamePic
            // 
            this.txtProductNamePic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProductNamePic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProductNamePic.Font = new System.Drawing.Font("微軟正黑體", 9.857143F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtProductNamePic.Location = new System.Drawing.Point(180, 77);
            this.txtProductNamePic.Name = "txtProductNamePic";
            this.txtProductNamePic.Size = new System.Drawing.Size(278, 38);
            this.txtProductNamePic.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 9.857143F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(55, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 31);
            this.label4.TabIndex = 14;
            this.label4.Text = "商品名稱";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 782);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(59, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            
            // 
            // btnDeletePic
            // 
            this.btnDeletePic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeletePic.BackColor = System.Drawing.Color.Pink;
            this.btnDeletePic.Font = new System.Drawing.Font("微軟正黑體", 9.857143F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnDeletePic.Location = new System.Drawing.Point(349, 690);
            this.btnDeletePic.Name = "btnDeletePic";
            this.btnDeletePic.Size = new System.Drawing.Size(138, 50);
            this.btnDeletePic.TabIndex = 3;
            this.btnDeletePic.Text = "刪除圖片";
            this.btnDeletePic.UseVisualStyleBackColor = false;
            this.btnDeletePic.Click += new System.EventHandler(this.btnDeletePic_Click);
            // 
            // txtProductId
            // 
            this.txtProductId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProductId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProductId.Font = new System.Drawing.Font("微軟正黑體", 9.857143F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtProductId.Location = new System.Drawing.Point(180, 19);
            this.txtProductId.Name = "txtProductId";
            this.txtProductId.Size = new System.Drawing.Size(46, 38);
            this.txtProductId.TabIndex = 11;
            this.txtProductId.Text = "0";
            // 
            // btnSaveImage
            // 
            this.btnSaveImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveImage.BackColor = System.Drawing.Color.HotPink;
            this.btnSaveImage.Font = new System.Drawing.Font("微軟正黑體", 9.857143F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSaveImage.Location = new System.Drawing.Point(349, 584);
            this.btnSaveImage.Name = "btnSaveImage";
            this.btnSaveImage.Size = new System.Drawing.Size(138, 58);
            this.btnSaveImage.TabIndex = 3;
            this.btnSaveImage.Text = "儲存圖片";
            this.btnSaveImage.UseVisualStyleBackColor = false;
            this.btnSaveImage.Click += new System.EventHandler(this.btnSaveImage_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 9.857143F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(55, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 31);
            this.label3.TabIndex = 10;
            this.label3.Text = "商品ID";
            // 
            // btnUpload
            // 
            this.btnUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpload.BackColor = System.Drawing.Color.HotPink;
            this.btnUpload.Font = new System.Drawing.Font("微軟正黑體", 9.857143F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnUpload.Location = new System.Drawing.Point(61, 584);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(138, 58);
            this.btnUpload.TabIndex = 2;
            this.btnUpload.Text = "上傳照片";
            this.btnUpload.UseVisualStyleBackColor = false;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // picProductBox
            // 
            this.picProductBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picProductBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picProductBox.Location = new System.Drawing.Point(61, 141);
            this.picProductBox.Name = "picProductBox";
            this.picProductBox.Size = new System.Drawing.Size(426, 345);
            this.picProductBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picProductBox.TabIndex = 1;
            this.picProductBox.TabStop = false;
            // 
            // FrmProductImageManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LavenderBlush;
            this.ClientSize = new System.Drawing.Size(1424, 854);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmProductImageManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "圖片管理";
            this.Load += new System.EventHandler(this.FrmProductImageManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductPic)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picProductBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvProductPic;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox picProductBox;
        private System.Windows.Forms.Button btnSaveImage;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.TextBox txtProductId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDeletePic;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtProductNamePic;
        private System.Windows.Forms.Label label4;
    }
}