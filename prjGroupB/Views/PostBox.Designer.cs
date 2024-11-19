namespace prjGroupB.Views
{
    partial class PostBox
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtTag = new System.Windows.Forms.TextBox();
            this.txtCreatedAt = new System.Windows.Forms.TextBox();
            this.pbFirstImage = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFirstImage)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.txtTag, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtCreatedAt, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.pbFirstImage, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(140, 200);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // txtTag
            // 
            this.txtTag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTag.Location = new System.Drawing.Point(3, 143);
            this.txtTag.Name = "txtTag";
            this.txtTag.ReadOnly = true;
            this.txtTag.Size = new System.Drawing.Size(134, 22);
            this.txtTag.TabIndex = 0;
            this.txtTag.Click += new System.EventHandler(this.txtTag_Click);
            // 
            // txtCreatedAt
            // 
            this.txtCreatedAt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCreatedAt.Location = new System.Drawing.Point(3, 173);
            this.txtCreatedAt.Name = "txtCreatedAt";
            this.txtCreatedAt.ReadOnly = true;
            this.txtCreatedAt.Size = new System.Drawing.Size(134, 22);
            this.txtCreatedAt.TabIndex = 1;
            this.txtCreatedAt.Click += new System.EventHandler(this.txtCreatedAt_Click);
            // 
            // pbFirstImage
            // 
            this.pbFirstImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbFirstImage.Location = new System.Drawing.Point(3, 3);
            this.pbFirstImage.Name = "pbFirstImage";
            this.pbFirstImage.Size = new System.Drawing.Size(134, 134);
            this.pbFirstImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbFirstImage.TabIndex = 2;
            this.pbFirstImage.TabStop = false;
            this.pbFirstImage.Click += new System.EventHandler(this.pbFirstImage_Click);
            // 
            // PostBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PostBox";
            this.Size = new System.Drawing.Size(140, 200);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFirstImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtTag;
        private System.Windows.Forms.TextBox txtCreatedAt;
        private System.Windows.Forms.PictureBox pbFirstImage;
    }
}
