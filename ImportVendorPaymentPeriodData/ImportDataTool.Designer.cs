namespace ImportVendorPaymentPeriodData
{
    partial class ImportDataTool
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportDataTool));
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.llbExportModel = new System.Windows.Forms.LinkLabel();
            this.btnInputData = new System.Windows.Forms.Button();
            this.llbSelection = new System.Windows.Forms.LinkLabel();
            this.tbxFilePath = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.lbStatus = new System.Windows.Forms.Label();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.llbExportModel);
            this.groupBox6.Controls.Add(this.btnInputData);
            this.groupBox6.Controls.Add(this.llbSelection);
            this.groupBox6.Controls.Add(this.tbxFilePath);
            this.groupBox6.Controls.Add(this.label24);
            this.groupBox6.Location = new System.Drawing.Point(12, 12);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(673, 56);
            this.groupBox6.TabIndex = 31;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "导入最新账龄数据";
            // 
            // llbExportModel
            // 
            this.llbExportModel.AutoSize = true;
            this.llbExportModel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.llbExportModel.Location = new System.Drawing.Point(601, 23);
            this.llbExportModel.Name = "llbExportModel";
            this.llbExportModel.Size = new System.Drawing.Size(53, 12);
            this.llbExportModel.TabIndex = 25;
            this.llbExportModel.TabStop = true;
            this.llbExportModel.Text = "生成模板";
            this.llbExportModel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbExportModel_LinkClicked);
            // 
            // btnInputData
            // 
            this.btnInputData.Location = new System.Drawing.Point(504, 18);
            this.btnInputData.Name = "btnInputData";
            this.btnInputData.Size = new System.Drawing.Size(75, 23);
            this.btnInputData.TabIndex = 24;
            this.btnInputData.Text = "导  入";
            this.btnInputData.UseVisualStyleBackColor = true;
            this.btnInputData.Click += new System.EventHandler(this.btnInputData_Click);
            // 
            // llbSelection
            // 
            this.llbSelection.AutoSize = true;
            this.llbSelection.Location = new System.Drawing.Point(452, 23);
            this.llbSelection.Name = "llbSelection";
            this.llbSelection.Size = new System.Drawing.Size(29, 12);
            this.llbSelection.TabIndex = 5;
            this.llbSelection.TabStop = true;
            this.llbSelection.Text = "选择";
            this.llbSelection.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbSelection_LinkClicked);
            // 
            // tbxFilePath
            // 
            this.tbxFilePath.Enabled = false;
            this.tbxFilePath.Location = new System.Drawing.Point(84, 20);
            this.tbxFilePath.Name = "tbxFilePath";
            this.tbxFilePath.Size = new System.Drawing.Size(353, 21);
            this.tbxFilePath.TabIndex = 4;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(13, 23);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(65, 12);
            this.label24.TabIndex = 3;
            this.label24.Text = "选择文件：";
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(25, 82);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(0, 12);
            this.lbStatus.TabIndex = 32;
            // 
            // ImportDataTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 103);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.groupBox6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ImportDataTool";
            this.Text = "U8-BPM供应商账期导入工具";
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.LinkLabel llbExportModel;
        private System.Windows.Forms.Button btnInputData;
        private System.Windows.Forms.LinkLabel llbSelection;
        private System.Windows.Forms.TextBox tbxFilePath;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label lbStatus;
    }
}

