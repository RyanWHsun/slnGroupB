namespace prjGroupB
{
    partial class FrmMain
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

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnUsers = new System.Windows.Forms.ToolStripButton();
            this.btnPosts = new System.Windows.Forms.ToolStripButton();
            this.btnAttractions = new System.Windows.Forms.ToolStripButton();
            this.btnEvents = new System.Windows.Forms.ToolStripButton();
            this.btnProducts = new System.Windows.Forms.ToolStripButton();
            this.btnOrders = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.btnExit = new System.Windows.Forms.ToolStripButton();
            this.btnUserPosts = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip1.Font = new System.Drawing.Font("微軟正黑體", 9.857143F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnUsers,
            this.btnPosts,
            this.btnAttractions,
            this.btnEvents,
            this.btnProducts,
            this.btnOrders,
            this.toolStripSeparator1,
            this.btnClose,
            this.btnExit,
            this.toolStripSeparator2,
            this.btnUserPosts});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(101, 480);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnUsers
            // 
            this.btnUsers.Font = new System.Drawing.Font("微軟正黑體", 9.857143F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnUsers.Image = ((System.Drawing.Image)(resources.GetObject("btnUsers.Image")));
            this.btnUsers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUsers.Name = "btnUsers";
            this.btnUsers.Size = new System.Drawing.Size(98, 36);
            this.btnUsers.Text = "會員管理";
            this.btnUsers.Click += new System.EventHandler(this.btnUsers_Click);
            // 
            // btnPosts
            // 
            this.btnPosts.Font = new System.Drawing.Font("微軟正黑體", 9.857143F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnPosts.Image = ((System.Drawing.Image)(resources.GetObject("btnPosts.Image")));
            this.btnPosts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPosts.Name = "btnPosts";
            this.btnPosts.Size = new System.Drawing.Size(98, 36);
            this.btnPosts.Text = "文章管理";
            this.btnPosts.Click += new System.EventHandler(this.btnPosts_Click);
            // 
            // btnAttractions
            // 
            this.btnAttractions.Font = new System.Drawing.Font("微軟正黑體", 9.857143F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnAttractions.Image = ((System.Drawing.Image)(resources.GetObject("btnAttractions.Image")));
            this.btnAttractions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAttractions.Name = "btnAttractions";
            this.btnAttractions.Size = new System.Drawing.Size(98, 36);
            this.btnAttractions.Text = "景點管理";
            this.btnAttractions.Click += new System.EventHandler(this.btnAttractions_Click);
            // 
            // btnEvents
            // 
            this.btnEvents.Font = new System.Drawing.Font("微軟正黑體", 9.857143F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnEvents.Image = ((System.Drawing.Image)(resources.GetObject("btnEvents.Image")));
            this.btnEvents.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEvents.Name = "btnEvents";
            this.btnEvents.Size = new System.Drawing.Size(98, 36);
            this.btnEvents.Text = "活動管理";
            this.btnEvents.Click += new System.EventHandler(this.btnEvents_Click);
            // 
            // btnProducts
            // 
            this.btnProducts.Font = new System.Drawing.Font("微軟正黑體", 9.857143F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnProducts.Image = ((System.Drawing.Image)(resources.GetObject("btnProducts.Image")));
            this.btnProducts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProducts.Name = "btnProducts";
            this.btnProducts.Size = new System.Drawing.Size(98, 36);
            this.btnProducts.Text = "商品管理";
            this.btnProducts.Click += new System.EventHandler(this.btnProducts_Click);
            // 
            // btnOrders
            // 
            this.btnOrders.Font = new System.Drawing.Font("微軟正黑體", 9.857143F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnOrders.Image = ((System.Drawing.Image)(resources.GetObject("btnOrders.Image")));
            this.btnOrders.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOrders.Name = "btnOrders";
            this.btnOrders.Size = new System.Drawing.Size(98, 36);
            this.btnOrders.Text = "訂單管理";
            this.btnOrders.Click += new System.EventHandler(this.btnOrders_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(98, 6);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("微軟正黑體", 9.857143F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(98, 36);
            this.btnClose.Text = "關閉";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("微軟正黑體", 9.857143F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(98, 36);
            this.btnExit.Text = "結束";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnUserPosts
            // 
            this.btnUserPosts.Font = new System.Drawing.Font("微軟正黑體", 9.857143F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnUserPosts.Image = ((System.Drawing.Image)(resources.GetObject("btnUserPosts.Image")));
            this.btnUserPosts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUserPosts.Name = "btnUserPosts";
            this.btnUserPosts.Size = new System.Drawing.Size(98, 36);
            this.btnUserPosts.Text = "個人文章";
            this.btnUserPosts.Click += new System.EventHandler(this.btnUserPosts_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(98, 6);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 480);
            this.Controls.Add(this.toolStrip1);
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmMain";
            this.Text = "FrmMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnUsers;
        private System.Windows.Forms.ToolStripButton btnPosts;
        private System.Windows.Forms.ToolStripButton btnAttractions;
        private System.Windows.Forms.ToolStripButton btnEvents;
        private System.Windows.Forms.ToolStripButton btnProducts;
        private System.Windows.Forms.ToolStripButton btnOrders;
        private System.Windows.Forms.ToolStripButton btnExit;
        private System.Windows.Forms.ToolStripButton btnClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnUserPosts;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}

