namespace Contable
{
    partial class frmAyuda_Cuentas_Contables
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
            this.grdAyuda_Cuentas = new System.Windows.Forms.DataGridView();
            this.txtID_Cuenta = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.btnSalir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdAyuda_Cuentas)).BeginInit();
            this.SuspendLayout();
            // 
            // grdAyuda_Cuentas
            // 
            this.grdAyuda_Cuentas.AllowUserToAddRows = false;
            this.grdAyuda_Cuentas.AllowUserToDeleteRows = false;
            this.grdAyuda_Cuentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdAyuda_Cuentas.Location = new System.Drawing.Point(12, 39);
            this.grdAyuda_Cuentas.Name = "grdAyuda_Cuentas";
            this.grdAyuda_Cuentas.ReadOnly = true;
            this.grdAyuda_Cuentas.RowHeadersVisible = false;
            this.grdAyuda_Cuentas.Size = new System.Drawing.Size(430, 289);
            this.grdAyuda_Cuentas.TabIndex = 2;
            this.grdAyuda_Cuentas.DoubleClick += new System.EventHandler(this.GrdAyuda_Cuentas_DoubleClick);
            this.grdAyuda_Cuentas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GrdAyuda_Cuentas_KeyDown);
            // 
            // txtID_Cuenta
            // 
            this.txtID_Cuenta.Location = new System.Drawing.Point(12, 13);
            this.txtID_Cuenta.Name = "txtID_Cuenta";
            this.txtID_Cuenta.Size = new System.Drawing.Size(102, 20);
            this.txtID_Cuenta.TabIndex = 0;
            this.txtID_Cuenta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtID_Cuenta_KeyDown);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(120, 13);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(295, 20);
            this.txtDescripcion.TabIndex = 1;
            this.txtDescripcion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDescripcion_KeyDown);
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnSalir.FlatAppearance.BorderSize = 0;
            this.btnSalir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnSalir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.ForeColor = System.Drawing.Color.LightGray;
            this.btnSalir.Location = new System.Drawing.Point(301, 334);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(141, 40);
            this.btnSalir.TabIndex = 10;
            this.btnSalir.Text = "SALIR";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.BtnSalir_Click);
            // 
            // frmAyuda_Cuentas_Contables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 386);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.txtID_Cuenta);
            this.Controls.Add(this.grdAyuda_Cuentas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAyuda_Cuentas_Contables";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cuentas Contables";
            this.Load += new System.EventHandler(this.FrmAyuda_Cuentas_Contables_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdAyuda_Cuentas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grdAyuda_Cuentas;
        private System.Windows.Forms.TextBox txtID_Cuenta;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Button btnSalir;
    }
}