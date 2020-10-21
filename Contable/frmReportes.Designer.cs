namespace Contable
{
    partial class frmReportes
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
            this.cryReportViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // cryReportViewer
            // 
            this.cryReportViewer.ActiveViewIndex = -1;
            this.cryReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cryReportViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.cryReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cryReportViewer.Location = new System.Drawing.Point(0, 0);
            this.cryReportViewer.Name = "cryReportViewer";
            this.cryReportViewer.ShowLogo = false;
            this.cryReportViewer.ShowParameterPanelButton = false;
            this.cryReportViewer.Size = new System.Drawing.Size(1167, 640);
            this.cryReportViewer.TabIndex = 0;
            this.cryReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // frmReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1167, 640);
            this.Controls.Add(this.cryReportViewer);
            this.Name = "frmReportes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Listado";
            this.Load += new System.EventHandler(this.frmReportes_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer cryReportViewer;
    }
}