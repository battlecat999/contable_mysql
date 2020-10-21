namespace Contable
{
    partial class frmListado_Plan_Cuentas
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
            this.label3 = new System.Windows.Forms.Label();
            this.cboCuentas_Desde = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboCuentas_Hasta = new System.Windows.Forms.ComboBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnEmite_Reporte = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(30, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 17);
            this.label3.TabIndex = 24;
            this.label3.Text = "CÓDIGO DESDE:";
            // 
            // cboCuentas_Desde
            // 
            this.cboCuentas_Desde.BackColor = System.Drawing.Color.Black;
            this.cboCuentas_Desde.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCuentas_Desde.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCuentas_Desde.ForeColor = System.Drawing.Color.DimGray;
            this.cboCuentas_Desde.FormattingEnabled = true;
            this.cboCuentas_Desde.Location = new System.Drawing.Point(135, 26);
            this.cboCuentas_Desde.Name = "cboCuentas_Desde";
            this.cboCuentas_Desde.Size = new System.Drawing.Size(177, 29);
            this.cboCuentas_Desde.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(30, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 17);
            this.label1.TabIndex = 26;
            this.label1.Text = "CÓDIGO HASTA:";
            // 
            // cboCuentas_Hasta
            // 
            this.cboCuentas_Hasta.BackColor = System.Drawing.Color.Black;
            this.cboCuentas_Hasta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCuentas_Hasta.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCuentas_Hasta.ForeColor = System.Drawing.Color.DimGray;
            this.cboCuentas_Hasta.FormattingEnabled = true;
            this.cboCuentas_Hasta.Location = new System.Drawing.Point(135, 85);
            this.cboCuentas_Hasta.Name = "cboCuentas_Hasta";
            this.cboCuentas_Hasta.Size = new System.Drawing.Size(177, 29);
            this.cboCuentas_Hasta.TabIndex = 25;
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnSalir.FlatAppearance.BorderSize = 0;
            this.btnSalir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnSalir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.ForeColor = System.Drawing.Color.LightGray;
            this.btnSalir.Location = new System.Drawing.Point(171, 152);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(141, 40);
            this.btnSalir.TabIndex = 27;
            this.btnSalir.Text = "SALIR";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.BtnSalir_Click);
            // 
            // btnEmite_Reporte
            // 
            this.btnEmite_Reporte.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnEmite_Reporte.FlatAppearance.BorderSize = 0;
            this.btnEmite_Reporte.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnEmite_Reporte.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnEmite_Reporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmite_Reporte.ForeColor = System.Drawing.Color.LightGray;
            this.btnEmite_Reporte.Location = new System.Drawing.Point(24, 152);
            this.btnEmite_Reporte.Name = "btnEmite_Reporte";
            this.btnEmite_Reporte.Size = new System.Drawing.Size(141, 40);
            this.btnEmite_Reporte.TabIndex = 28;
            this.btnEmite_Reporte.Text = "EMITE REPORTE";
            this.btnEmite_Reporte.UseVisualStyleBackColor = false;
            this.btnEmite_Reporte.Click += new System.EventHandler(this.BtnEmite_Reporte_Click);
            // 
            // frmListado_Plan_Cuentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 220);
            this.Controls.Add(this.btnEmite_Reporte);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboCuentas_Hasta);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboCuentas_Desde);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmListado_Plan_Cuentas";
            this.Text = "Listado Plan de Cuentas";
            this.Load += new System.EventHandler(this.FrmListado_Plan_Cuentas_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboCuentas_Desde;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboCuentas_Hasta;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnEmite_Reporte;
    }
}