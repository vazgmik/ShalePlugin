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
            this.button1 = new System.Windows.Forms.Button();
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(140, 75);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dropTarget1
            // 
            this.dropTarget1.AllowDrop = true;
            this.dropTarget1.Location = new System.Drawing.Point(4, 29);
            this.dropTarget1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dropTarget1.Name = "dropTarget1";
            this.dropTarget1.Size = new System.Drawing.Size(35, 28);
            this.dropTarget1.TabIndex = 0;
            this.dropTarget1.DragDrop += new System.Windows.Forms.DragEventHandler(this.WellLog_DragDrop);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 17);
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(83, 20);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(375, 203);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.dropTarget2, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 105);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(125, 54);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // dropTarget2
            // 
            this.dropTarget2.AllowDrop = true;
            this.dropTarget2.Location = new System.Drawing.Point(4, 22);
            this.dropTarget2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dropTarget2.Name = "dropTarget2";
            this.dropTarget2.Size = new System.Drawing.Size(35, 28);
            this.dropTarget2.TabIndex = 0;
            this.dropTarget2.DragDrop += new System.Windows.Forms.DragEventHandler(this.sonic_DragDrop);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Sonic Log";
            // 
            // boxSonic
            // 
            this.boxSonic.Location = new System.Drawing.Point(191, 105);
            this.boxSonic.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.boxSonic.Name = "boxSonic";
            this.boxSonic.Size = new System.Drawing.Size(133, 25);
            this.boxSonic.TabIndex = 3;
            // 
            // boxWellLog
            // 
            this.boxWellLog.Location = new System.Drawing.Point(191, 4);
            this.boxWellLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.boxWellLog.Name = "boxWellLog";
            this.boxWellLog.Size = new System.Drawing.Size(133, 25);
            this.boxWellLog.TabIndex = 4;
            // 
            // btn_Apply
            // 
            this.btn_Apply.Location = new System.Drawing.Point(456, 334);
            this.btn_Apply.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_Apply.Name = "btn_Apply";
            this.btn_Apply.Size = new System.Drawing.Size(100, 28);
            this.btn_Apply.TabIndex = 2;
            this.btn_Apply.Text = "Apply";
            this.btn_Apply.UseVisualStyleBackColor = true;
            this.btn_Apply.Click += new System.EventHandler(this.btn_Apply_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(609, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Add well";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ShaleWorkstepUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_Apply);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ShaleWorkstepUI";
            this.Size = new System.Drawing.Size(710, 377);
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
        private System.Windows.Forms.Button button1;
    }
}
