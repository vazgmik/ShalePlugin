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
            this.tabControl1 = new Slb.Ocean.Petrel.UI.Controls.TabControl();
            this.tocPage = new Slb.Ocean.Petrel.UI.Controls.TabPage();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.list_Boreholes = new Slb.Ocean.Petrel.UI.Controls.ListBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.dropTarget4 = new Slb.Ocean.Petrel.UI.DropTarget();
            this.btn_DelBoreHole = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.matPage = new Slb.Ocean.Petrel.UI.Controls.TabPage();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.drpBorehole = new Slb.Ocean.Petrel.UI.DropTarget();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tocPage.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(98, 61);
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
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.15285F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.84715F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.boxSonic, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.boxWellLog, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 157);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.59686F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.40314F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(543, 166);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.dropTarget2, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 80);
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
            this.boxSonic.Location = new System.Drawing.Point(107, 80);
            this.boxSonic.Name = "boxSonic";
            this.boxSonic.Size = new System.Drawing.Size(100, 22);
            this.boxSonic.TabIndex = 3;
            // 
            // boxWellLog
            // 
            this.boxWellLog.Location = new System.Drawing.Point(107, 3);
            this.boxWellLog.Name = "boxWellLog";
            this.boxWellLog.Size = new System.Drawing.Size(100, 22);
            this.boxWellLog.TabIndex = 4;
            // 
            // btn_Apply
            // 
            this.btn_Apply.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Apply.Location = new System.Drawing.Point(3, 329);
            this.btn_Apply.Name = "btn_Apply";
            this.btn_Apply.Size = new System.Drawing.Size(75, 23);
            this.btn_Apply.TabIndex = 2;
            this.btn_Apply.Text = "Apply";
            this.btn_Apply.UseVisualStyleBackColor = true;
            this.btn_Apply.Click += new System.EventHandler(this.btn_Apply_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.AllowDrop = true;
            this.tabControl1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.tabControl1.Location = new System.Drawing.Point(56, 3);
            this.tabControl1.Multiline = false;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(551, 415);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.TabPages.AddRange(new Slb.Ocean.Petrel.UI.Controls.TabPage[] {
            this.tocPage,
            this.matPage});
            // 
            // tocPage
            // 
            this.tocPage.Controls.Add(this.tableLayoutPanel6);
            this.tocPage.Name = "tocPage";
            this.tocPage.Size = new System.Drawing.Size(543, 378);
            this.tocPage.Text = "TOC Analysis";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel8, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.btn_Apply, 0, 3);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 4;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.27801F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86.72199F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 172F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(549, 394);
            this.tableLayoutPanel6.TabIndex = 8;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.78453F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 81.21547F));
            this.tableLayoutPanel8.Controls.Add(this.list_Boreholes, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel7, 0, 0);
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 23);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(543, 128);
            this.tableLayoutPanel8.TabIndex = 11;
            // 
            // list_Boreholes
            // 
            this.list_Boreholes.AllowDrop = true;
            this.list_Boreholes.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.list_Boreholes.Location = new System.Drawing.Point(105, 3);
            this.list_Boreholes.Name = "list_Boreholes";
            this.list_Boreholes.SelectionMode = Slb.Ocean.Petrel.UI.Controls.ListBoxSelectionMode.Multiple;
            this.list_Boreholes.Size = new System.Drawing.Size(289, 122);
            this.list_Boreholes.TabIndex = 1;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.Controls.Add(this.dropTarget4, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.btn_DelBoreHole, 0, 1);
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(85, 105);
            this.tableLayoutPanel7.TabIndex = 9;
            // 
            // dropTarget4
            // 
            this.dropTarget4.AllowDrop = true;
            this.dropTarget4.Location = new System.Drawing.Point(3, 3);
            this.dropTarget4.Name = "dropTarget4";
            this.dropTarget4.Size = new System.Drawing.Size(26, 23);
            this.dropTarget4.TabIndex = 6;
            this.dropTarget4.DragDrop += new System.Windows.Forms.DragEventHandler(this.drpBorehole_DragDrop);
            // 
            // btn_DelBoreHole
            // 
            this.btn_DelBoreHole.Location = new System.Drawing.Point(3, 55);
            this.btn_DelBoreHole.Name = "btn_DelBoreHole";
            this.btn_DelBoreHole.Size = new System.Drawing.Size(75, 23);
            this.btn_DelBoreHole.TabIndex = 1;
            this.btn_DelBoreHole.Text = "remove";
            this.btn_DelBoreHole.UseVisualStyleBackColor = true;
            this.btn_DelBoreHole.Click += new System.EventHandler(this.btn_DelBoreHole_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Скважины из интерфейса";
            // 
            // matPage
            // 
            this.matPage.Name = "matPage";
            this.matPage.Size = new System.Drawing.Size(543, 378);
            this.matPage.Text = "Maturity";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.Controls.Add(this.drpBorehole, 0, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(54, 81);
            this.tableLayoutPanel5.TabIndex = 5;
            // 
            // drpBorehole
            // 
            this.drpBorehole.AllowDrop = true;
            this.drpBorehole.Enabled = false;
            this.drpBorehole.Location = new System.Drawing.Point(15, 3);
            this.drpBorehole.Margin = new System.Windows.Forms.Padding(15, 3, 3, 3);
            this.drpBorehole.Name = "drpBorehole";
            this.drpBorehole.Size = new System.Drawing.Size(35, 23);
            this.drpBorehole.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(242, 297);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(400, 100);
            this.tableLayoutPanel4.TabIndex = 7;
            // 
            // ShaleWorkstepUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "ShaleWorkstepUI";
            this.Size = new System.Drawing.Size(793, 611);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tocPage.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
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
        private Slb.Ocean.Petrel.UI.Controls.TabControl tabControl1;
        private Slb.Ocean.Petrel.UI.Controls.TabPage tocPage;
        private Slb.Ocean.Petrel.UI.Controls.TabPage matPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private Slb.Ocean.Petrel.UI.DropTarget drpBorehole;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private Slb.Ocean.Petrel.UI.Controls.ListBox list_Boreholes;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private Slb.Ocean.Petrel.UI.DropTarget dropTarget4;
        private System.Windows.Forms.Button btn_DelBoreHole;
    }
}
