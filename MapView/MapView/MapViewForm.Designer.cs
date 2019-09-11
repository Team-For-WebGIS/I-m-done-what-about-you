namespace MapView
{
    partial class MapViewForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapViewForm));
            this.label1 = new System.Windows.Forms.Label();
            this.txtStateName = new System.Windows.Forms.TextBox();
            this.btnPointQuery = new System.Windows.Forms.Button();
            this.btnFixedZoomIn = new System.Windows.Forms.Button();
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 288);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "StateName";
            // 
            // txtStateName
            // 
            this.txtStateName.Location = new System.Drawing.Point(76, 284);
            this.txtStateName.Name = "txtStateName";
            this.txtStateName.Size = new System.Drawing.Size(100, 21);
            this.txtStateName.TabIndex = 3;
            this.txtStateName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtStateName_KeyUp);
            // 
            // btnPointQuery
            // 
            this.btnPointQuery.Location = new System.Drawing.Point(203, 283);
            this.btnPointQuery.Name = "btnPointQuery";
            this.btnPointQuery.Size = new System.Drawing.Size(75, 23);
            this.btnPointQuery.TabIndex = 4;
            this.btnPointQuery.Text = "点查询";
            this.btnPointQuery.UseVisualStyleBackColor = true;
            this.btnPointQuery.Click += new System.EventHandler(this.btnPointQuery_Click);
            // 
            // btnFixedZoomIn
            // 
            this.btnFixedZoomIn.Location = new System.Drawing.Point(290, 282);
            this.btnFixedZoomIn.Name = "btnFixedZoomIn";
            this.btnFixedZoomIn.Size = new System.Drawing.Size(75, 23);
            this.btnFixedZoomIn.TabIndex = 5;
            this.btnFixedZoomIn.Text = "居中放大";
            this.btnFixedZoomIn.UseVisualStyleBackColor = true;
            this.btnFixedZoomIn.Click += new System.EventHandler(this.btnFixedZoomIn_Click);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.Location = new System.Drawing.Point(377, 282);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(75, 23);
            this.btnZoomIn.TabIndex = 6;
            this.btnZoomIn.Text = "拉框放大";
            this.btnZoomIn.UseVisualStyleBackColor = true;
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(40, 183);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 8;
            // 
            // axMapControl1
            // 
            this.axMapControl1.Location = new System.Drawing.Point(2, 0);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(459, 276);
            this.axMapControl1.TabIndex = 7;
            this.axMapControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
            this.axMapControl1.OnMouseUp += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseUpEventHandler(this.axMapControl1_OnMouseUp);
            this.axMapControl1.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.axMapControl1_OnMouseMove);
            // 
            // MapViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 324);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.axMapControl1);
            this.Controls.Add(this.btnZoomIn);
            this.Controls.Add(this.btnFixedZoomIn);
            this.Controls.Add(this.btnPointQuery);
            this.Controls.Add(this.txtStateName);
            this.Controls.Add(this.label1);
            this.Name = "MapViewForm";
            this.Text = "MapView";
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

  
       
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStateName;
        private System.Windows.Forms.Button btnPointQuery;
        private System.Windows.Forms.Button btnFixedZoomIn;
        private System.Windows.Forms.Button btnZoomIn;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
    }
}

