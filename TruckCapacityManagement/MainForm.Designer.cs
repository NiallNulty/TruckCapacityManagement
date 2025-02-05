namespace TruckCapacityManagement
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblSelectedFileName = new System.Windows.Forms.Label();
            this.lblOrdersFoundCount = new System.Windows.Forms.Label();
            this.tableLayoutPanelHeader = new System.Windows.Forms.TableLayoutPanel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.lblErrorMessage = new System.Windows.Forms.Label();
            this.tableLayoutPanelTruckCapacity = new System.Windows.Forms.TableLayoutPanel();
            this.lblTruckCapacity = new System.Windows.Forms.Label();
            this.numericUpDownTruckCapacity = new System.Windows.Forms.NumericUpDown();
            this.lblSetReductionPercentage = new System.Windows.Forms.Label();
            this.numericUpDownMaximumReductionPercentage = new System.Windows.Forms.NumericUpDown();
            this.btnGenerateNewOrders = new System.Windows.Forms.Button();
            this.tableLayoutPanelMain.SuspendLayout();
            this.tableLayoutPanelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.tableLayoutPanelTruckCapacity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTruckCapacity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaximumReductionPercentage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnOpenFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(52)))), ((int)(((byte)(134)))));
            this.btnOpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenFile.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenFile.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnOpenFile.Location = new System.Drawing.Point(344, 57);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(108, 37);
            this.btnOpenFile.TabIndex = 0;
            this.btnOpenFile.Text = "Open File";
            this.btnOpenFile.UseVisualStyleBackColor = false;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.Controls.Add(this.btnOpenFile, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.lblSelectedFileName, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.lblOrdersFoundCount, 0, 3);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelHeader, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.lblErrorMessage, 0, 5);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelTruckCapacity, 0, 4);
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 6;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.6701F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44.3299F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 198F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(797, 447);
            this.tableLayoutPanelMain.TabIndex = 1;
            // 
            // lblSelectedFileName
            // 
            this.lblSelectedFileName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblSelectedFileName.AutoSize = true;
            this.lblSelectedFileName.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedFileName.Location = new System.Drawing.Point(318, 97);
            this.lblSelectedFileName.Name = "lblSelectedFileName";
            this.lblSelectedFileName.Size = new System.Drawing.Size(160, 21);
            this.lblSelectedFileName.TabIndex = 2;
            this.lblSelectedFileName.Text = "lblSelectedFileName";
            // 
            // lblOrdersFoundCount
            // 
            this.lblOrdersFoundCount.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblOrdersFoundCount.AutoSize = true;
            this.lblOrdersFoundCount.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrdersFoundCount.Location = new System.Drawing.Point(314, 120);
            this.lblOrdersFoundCount.Name = "lblOrdersFoundCount";
            this.lblOrdersFoundCount.Size = new System.Drawing.Size(168, 21);
            this.lblOrdersFoundCount.TabIndex = 3;
            this.lblOrdersFoundCount.Text = "lblOrdersFoundCount";
            // 
            // tableLayoutPanelHeader
            // 
            this.tableLayoutPanelHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelHeader.ColumnCount = 2;
            this.tableLayoutPanelHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.53856F));
            this.tableLayoutPanelHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85.46144F));
            this.tableLayoutPanelHeader.Controls.Add(this.lblHeader, 1, 0);
            this.tableLayoutPanelHeader.Controls.Add(this.pictureBoxLogo, 0, 0);
            this.tableLayoutPanelHeader.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelHeader.Name = "tableLayoutPanelHeader";
            this.tableLayoutPanelHeader.RowCount = 1;
            this.tableLayoutPanelHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelHeader.Size = new System.Drawing.Size(791, 48);
            this.tableLayoutPanelHeader.TabIndex = 4;
            // 
            // lblHeader
            // 
            this.lblHeader.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(118, 13);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(82, 21);
            this.lblHeader.TabIndex = 5;
            this.lblHeader.Text = "lblHeader";
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxLogo.Image")));
            this.pictureBoxLogo.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(108, 41);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLogo.TabIndex = 1;
            this.pictureBoxLogo.TabStop = false;
            // 
            // lblErrorMessage
            // 
            this.lblErrorMessage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblErrorMessage.AutoSize = true;
            this.lblErrorMessage.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            this.lblErrorMessage.Location = new System.Drawing.Point(334, 356);
            this.lblErrorMessage.Name = "lblErrorMessage";
            this.lblErrorMessage.Size = new System.Drawing.Size(129, 21);
            this.lblErrorMessage.TabIndex = 5;
            this.lblErrorMessage.Text = "lblErrorMessage";
            this.lblErrorMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tableLayoutPanelTruckCapacity
            // 
            this.tableLayoutPanelTruckCapacity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelTruckCapacity.ColumnCount = 3;
            this.tableLayoutPanelTruckCapacity.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelTruckCapacity.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelTruckCapacity.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelTruckCapacity.Controls.Add(this.lblTruckCapacity, 0, 0);
            this.tableLayoutPanelTruckCapacity.Controls.Add(this.numericUpDownTruckCapacity, 0, 1);
            this.tableLayoutPanelTruckCapacity.Controls.Add(this.lblSetReductionPercentage, 2, 0);
            this.tableLayoutPanelTruckCapacity.Controls.Add(this.numericUpDownMaximumReductionPercentage, 2, 1);
            this.tableLayoutPanelTruckCapacity.Controls.Add(this.btnGenerateNewOrders, 1, 2);
            this.tableLayoutPanelTruckCapacity.Location = new System.Drawing.Point(3, 161);
            this.tableLayoutPanelTruckCapacity.Name = "tableLayoutPanelTruckCapacity";
            this.tableLayoutPanelTruckCapacity.RowCount = 4;
            this.tableLayoutPanelTruckCapacity.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 38.57143F));
            this.tableLayoutPanelTruckCapacity.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 61.42857F));
            this.tableLayoutPanelTruckCapacity.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanelTruckCapacity.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tableLayoutPanelTruckCapacity.Size = new System.Drawing.Size(791, 192);
            this.tableLayoutPanelTruckCapacity.TabIndex = 6;
            // 
            // lblTruckCapacity
            // 
            this.lblTruckCapacity.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTruckCapacity.AutoSize = true;
            this.lblTruckCapacity.Enabled = false;
            this.lblTruckCapacity.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTruckCapacity.Location = new System.Drawing.Point(74, 0);
            this.lblTruckCapacity.Name = "lblTruckCapacity";
            this.lblTruckCapacity.Size = new System.Drawing.Size(115, 21);
            this.lblTruckCapacity.TabIndex = 4;
            this.lblTruckCapacity.Text = "Truck Capacity";
            // 
            // numericUpDownTruckCapacity
            // 
            this.numericUpDownTruckCapacity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.numericUpDownTruckCapacity.Enabled = false;
            this.numericUpDownTruckCapacity.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownTruckCapacity.Location = new System.Drawing.Point(71, 32);
            this.numericUpDownTruckCapacity.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownTruckCapacity.Name = "numericUpDownTruckCapacity";
            this.numericUpDownTruckCapacity.Size = new System.Drawing.Size(120, 29);
            this.numericUpDownTruckCapacity.TabIndex = 5;
            // 
            // lblSetReductionPercentage
            // 
            this.lblSetReductionPercentage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblSetReductionPercentage.AutoSize = true;
            this.lblSetReductionPercentage.Enabled = false;
            this.lblSetReductionPercentage.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSetReductionPercentage.Location = new System.Drawing.Point(535, 0);
            this.lblSetReductionPercentage.Name = "lblSetReductionPercentage";
            this.lblSetReductionPercentage.Size = new System.Drawing.Size(247, 21);
            this.lblSetReductionPercentage.TabIndex = 6;
            this.lblSetReductionPercentage.Text = "Maximum Reduction Percentage";
            // 
            // numericUpDownMaximumReductionPercentage
            // 
            this.numericUpDownMaximumReductionPercentage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.numericUpDownMaximumReductionPercentage.DecimalPlaces = 2;
            this.numericUpDownMaximumReductionPercentage.Enabled = false;
            this.numericUpDownMaximumReductionPercentage.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownMaximumReductionPercentage.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numericUpDownMaximumReductionPercentage.Location = new System.Drawing.Point(598, 32);
            this.numericUpDownMaximumReductionPercentage.Name = "numericUpDownMaximumReductionPercentage";
            this.numericUpDownMaximumReductionPercentage.Size = new System.Drawing.Size(120, 29);
            this.numericUpDownMaximumReductionPercentage.TabIndex = 7;
            // 
            // btnGenerateNewOrders
            // 
            this.btnGenerateNewOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerateNewOrders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(52)))), ((int)(((byte)(134)))));
            this.btnGenerateNewOrders.Enabled = false;
            this.btnGenerateNewOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateNewOrders.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerateNewOrders.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnGenerateNewOrders.Location = new System.Drawing.Point(266, 78);
            this.btnGenerateNewOrders.Name = "btnGenerateNewOrders";
            this.btnGenerateNewOrders.Size = new System.Drawing.Size(257, 38);
            this.btnGenerateNewOrders.TabIndex = 8;
            this.btnGenerateNewOrders.Text = "Generate New Orders";
            this.btnGenerateNewOrders.UseVisualStyleBackColor = false;
            this.btnGenerateNewOrders.Click += new System.EventHandler(this.btnGenerateNewOrders_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(816, 489);
            this.MinimumSize = new System.Drawing.Size(816, 489);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Truck Capacity Management";
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            this.tableLayoutPanelHeader.ResumeLayout(false);
            this.tableLayoutPanelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.tableLayoutPanelTruckCapacity.ResumeLayout(false);
            this.tableLayoutPanelTruckCapacity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTruckCapacity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaximumReductionPercentage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Label lblSelectedFileName;
        private System.Windows.Forms.Label lblOrdersFoundCount;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblErrorMessage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelTruckCapacity;
        private System.Windows.Forms.Label lblTruckCapacity;
        private System.Windows.Forms.NumericUpDown numericUpDownTruckCapacity;
        private System.Windows.Forms.Label lblSetReductionPercentage;
        private System.Windows.Forms.NumericUpDown numericUpDownMaximumReductionPercentage;
        private System.Windows.Forms.Button btnGenerateNewOrders;
    }
}

