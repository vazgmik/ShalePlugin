namespace Shale
{
    partial class ShaleWorkstepUI
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dropTarget1 = new Slb.Ocean.Petrel.UI.DropTarget();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.dropTarget2 = new Slb.Ocean.Petrel.UI.DropTarget();
            this.label2 = new System.Windows.Forms.Label();
            this.boxSonic = new Slb.Ocean.Petrel.UI.Controls.PresentationBox();
            this.boxWellLog = new Slb.Ocean.Petrel.UI.Controls.PresentationBox();
            this.btn_Apply = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.dropTarget1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(105, 61);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dropTarget1
            // 
            this.dropTarget1.AllowDrop = true;
            this.dropTarget1.Location = new System.Drawing.Point(3, 23);
            this.dropTarget1.Name = "dropTarget1";
            this.dropTarget1.Size = new System.Drawing.Size(26, 23);
            this.dropTarget1.TabIndex = 0;
            this.dropTarget1.DragDrop += new System.Windows.Forms.DragEventHandler(this.WellLog_DragDrop);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Welllog ( LLD)";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.boxSonic, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.boxWellLog, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(62, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(281, 165);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.dropTarget2, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 85);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(94, 44);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // dropTarget2
            // 
            this.dropTarget2.AllowDrop = true;
            this.dropTarget2.Location = new System.Drawing.Point(3, 17);
            this.dropTarget2.Name = "dropTarget2";
            this.dropTarget2.Size = new System.Drawing.Size(26, 23);
            this.dropTarget2.TabIndex = 0;
            this.dropTarget2.DragDrop += new System.Windows.Forms.DragEventHandler(this.sonic_DragDrop);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Sonic Log";
            // 
            // boxSonic
            // 
            this.boxSonic.Location = new System.Drawing.Point(143, 85);
            this.boxSonic.Name = "boxSonic";
            this.boxSonic.Size = new System.Drawing.Size(100, 22);
            this.boxSonic.TabIndex = 3;
            // 
            // boxWellLog
            // 
            this.boxWellLog.Location = new System.Drawing.Point(143, 3);
            this.boxWellLog.Name = "boxWellLog";
            this.boxWellLog.Size = new System.Drawing.Size(100, 22);
            this.boxWellLog.TabIndex = 4;
            // 
            // btn_Apply
            // 
            this.btn_Apply.Location = new System.Drawing.Point(342, 271);
            this.btn_Apply.Name = "btn_Apply";
            this.btn_Apply.Size = new System.Drawing.Size(75, 23);
            this.btn_Apply.TabIndex = 2;
            this.btn_Apply.Text = "Apply";
            this.btn_Apply.UseVisualStyleBackColor = true;
            this.btn_Apply.Click += new System.EventHandler(this.btn_Apply_Click);
            // 
            // ShaleWorkstepUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_Apply);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "ShaleWorkstepUI";
            this.Size = new System.Drawing.Size(436, 306);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Slb.Ocean.Petrel.UI.DropTarget dropTarget1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private Slb.Ocean.Petrel.UI.DropTarget dropTarget2;
        private System.Windows.Forms.Label label2;
        private Slb.Ocean.Petrel.UI.Controls.PresentationBox boxSonic;
        private Slb.Ocean.Petrel.UI.Controls.PresentationBox boxWellLog;
        private System.Windows.Forms.Button btn_Apply;
    }
}
