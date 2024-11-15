namespace prjGroupB.Views
{
    partial class FrmOrders
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOrders));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnCreateOrder = new System.Windows.Forms.ToolStripButton();
            this.btnEditOrder = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteOrder = new System.Windows.Forms.ToolStripButton();
            this.txtQuery = new System.Windows.Forms.ToolStripTextBox();
            this.btnQuery = new System.Windows.Forms.ToolStripButton();
            this.cmbOrderStatus = new System.Windows.Forms.ToolStripComboBox();
            this.btnReset = new System.Windows.Forms.ToolStripButton();
            this.btnStatusAmend = new System.Windows.Forms.ToolStripButton();
            this.dgvOrder = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.LemonChiffon;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCreateOrder,
            this.btnEditOrder,
            this.btnDeleteOrder,
            this.btnReset,
            this.cmbOrderStatus,
            this.btnQuery,
            this.txtQuery,
            this.btnStatusAmend});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1385, 36);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnCreateOrder
            // 
            this.btnCreateOrder.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnCreateOrder.Image = ((System.Drawing.Image)(resources.GetObject("btnCreateOrder.Image")));
            this.btnCreateOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCreateOrder.Name = "btnCreateOrder";
            this.btnCreateOrder.Size = new System.Drawing.Size(124, 30);
            this.btnCreateOrder.Text = "訂單作業";
            // 
            // btnEditOrder
            // 
            this.btnEditOrder.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnEditOrder.Image = ((System.Drawing.Image)(resources.GetObject("btnEditOrder.Image")));
            this.btnEditOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditOrder.Name = "btnEditOrder";
            this.btnEditOrder.Size = new System.Drawing.Size(82, 30);
            this.btnEditOrder.Text = "修改";
            // 
            // btnDeleteOrder
            // 
            this.btnDeleteOrder.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnDeleteOrder.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteOrder.Image")));
            this.btnDeleteOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteOrder.Name = "btnDeleteOrder";
            this.btnDeleteOrder.Size = new System.Drawing.Size(82, 30);
            this.btnDeleteOrder.Text = "刪除";
            // 
            // txtQuery
            // 
            this.txtQuery.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.txtQuery.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.Size = new System.Drawing.Size(150, 36);
            this.txtQuery.Text = "關鍵字查詢";
            this.txtQuery.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnQuery
            // 
            this.btnQuery.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnQuery.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(82, 30);
            this.btnQuery.Text = "查詢";
            // 
            // cmbOrderStatus
            // 
            this.cmbOrderStatus.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cmbOrderStatus.Name = "cmbOrderStatus";
            this.cmbOrderStatus.Size = new System.Drawing.Size(150, 36);
            this.cmbOrderStatus.Text = "狀態選取";
            // 
            // btnReset
            // 
            this.btnReset.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnReset.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnReset.Image = ((System.Drawing.Image)(resources.GetObject("btnReset.Image")));
            this.btnReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(82, 30);
            this.btnReset.Text = "重置";
            // 
            // btnStatusAmend
            // 
            this.btnStatusAmend.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnStatusAmend.Image = ((System.Drawing.Image)(resources.GetObject("btnStatusAmend.Image")));
            this.btnStatusAmend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStatusAmend.Name = "btnStatusAmend";
            this.btnStatusAmend.Size = new System.Drawing.Size(124, 30);
            this.btnStatusAmend.Text = "狀態更新";
            // 
            // dgvOrder
            // 
            this.dgvOrder.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.dgvOrder.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrder.Location = new System.Drawing.Point(0, 36);
            this.dgvOrder.Name = "dgvOrder";
            this.dgvOrder.RowHeadersWidth = 72;
            this.dgvOrder.RowTemplate.Height = 35;
            this.dgvOrder.Size = new System.Drawing.Size(1385, 680);
            this.dgvOrder.TabIndex = 2;
            // 
            // FrmOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1385, 716);
            this.Controls.Add(this.dgvOrder);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "FrmOrders";
            this.Text = "FrmOrders";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmOrders_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnCreateOrder;
        private System.Windows.Forms.ToolStripButton btnEditOrder;
        private System.Windows.Forms.ToolStripButton btnDeleteOrder;
        private System.Windows.Forms.ToolStripButton btnQuery;
        private System.Windows.Forms.ToolStripTextBox txtQuery;
        private System.Windows.Forms.ToolStripButton btnReset;
        private System.Windows.Forms.ToolStripComboBox cmbOrderStatus;
        private System.Windows.Forms.ToolStripButton btnStatusAmend;
        private System.Windows.Forms.DataGridView dgvOrder;
    }
}