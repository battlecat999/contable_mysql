namespace Contable
{
    partial class frmListado_Libro_Diario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListado_Libro_Diario));
            this.label3 = new System.Windows.Forms.Label();
            this.cboEmpresas = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.datFecha_Hasta = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.datFecha_Desde = new System.Windows.Forms.DateTimePicker();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnProcesar = new System.Windows.Forms.Button();
            this.chkOrdenar_Por_Numero_Asiento = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboAsiento_Desde = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboAsiento_Hasta = new System.Windows.Forms.ComboBox();
            this.chkSeleccionar_Entre_Fechas = new System.Windows.Forms.CheckBox();
            this.pMinimizar = new System.Windows.Forms.PictureBox();
            this.pCerrar = new System.Windows.Forms.PictureBox();
            this.chkLiberaFechas = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pMinimizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCerrar)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(50, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 17);
            this.label3.TabIndex = 102;
            this.label3.Text = "EMPRESA:";
            // 
            // cboEmpresas
            // 
            this.cboEmpresas.BackColor = System.Drawing.Color.Black;
            this.cboEmpresas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboEmpresas.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboEmpresas.ForeColor = System.Drawing.Color.White;
            this.cboEmpresas.FormattingEnabled = true;
            this.cboEmpresas.Location = new System.Drawing.Point(139, 60);
            this.cboEmpresas.Name = "cboEmpresas";
            this.cboEmpresas.Size = new System.Drawing.Size(207, 25);
            this.cboEmpresas.TabIndex = 101;
            this.cboEmpresas.SelectedIndexChanged += new System.EventHandler(this.CboEmpresas_SelectedIndexChanged);
            this.cboEmpresas.SelectedValueChanged += new System.EventHandler(this.cboEmpresas_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(172, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 20);
            this.label1.TabIndex = 100;
            this.label1.Text = "LIBRO DIARIO";
            // 
            // lblEstado
            // 
            this.lblEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstado.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblEstado.Location = new System.Drawing.Point(38, 386);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(246, 17);
            this.lblEstado.TabIndex = 99;
            this.lblEstado.Text = "lblEstado";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(44, 207);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 17);
            this.label2.TabIndex = 98;
            this.label2.Text = "FECHA HASTA;";
            // 
            // datFecha_Hasta
            // 
            this.datFecha_Hasta.CalendarForeColor = System.Drawing.Color.White;
            this.datFecha_Hasta.CalendarMonthBackground = System.Drawing.Color.Black;
            this.datFecha_Hasta.CalendarTitleForeColor = System.Drawing.Color.White;
            this.datFecha_Hasta.CustomFormat = "dd/MM/yyyy";
            this.datFecha_Hasta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datFecha_Hasta.Location = new System.Drawing.Point(139, 207);
            this.datFecha_Hasta.Name = "datFecha_Hasta";
            this.datFecha_Hasta.Size = new System.Drawing.Size(90, 20);
            this.datFecha_Hasta.TabIndex = 96;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(44, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 17);
            this.label4.TabIndex = 97;
            this.label4.Text = "FECHA DESDE;";
            // 
            // datFecha_Desde
            // 
            this.datFecha_Desde.CalendarForeColor = System.Drawing.Color.White;
            this.datFecha_Desde.CalendarMonthBackground = System.Drawing.Color.Black;
            this.datFecha_Desde.CalendarTitleForeColor = System.Drawing.Color.White;
            this.datFecha_Desde.CustomFormat = "dd/MM/yyyy";
            this.datFecha_Desde.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datFecha_Desde.Location = new System.Drawing.Point(139, 171);
            this.datFecha_Desde.Name = "datFecha_Desde";
            this.datFecha_Desde.Size = new System.Drawing.Size(90, 20);
            this.datFecha_Desde.TabIndex = 95;
            this.datFecha_Desde.ValueChanged += new System.EventHandler(this.datFecha_Desde_ValueChanged);
            this.datFecha_Desde.KeyDown += new System.Windows.Forms.KeyEventHandler(this.datFecha_Desde_KeyDown);
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSalir.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSalir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.Location = new System.Drawing.Point(229, 351);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(85, 23);
            this.btnSalir.TabIndex = 94;
            this.btnSalir.Text = "SALIR";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnProcesar
            // 
            this.btnProcesar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnProcesar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnProcesar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnProcesar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcesar.ForeColor = System.Drawing.Color.White;
            this.btnProcesar.Location = new System.Drawing.Point(139, 351);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(85, 23);
            this.btnProcesar.TabIndex = 93;
            this.btnProcesar.Text = "PROCESAR";
            this.btnProcesar.UseVisualStyleBackColor = false;
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // chkOrdenar_Por_Numero_Asiento
            // 
            this.chkOrdenar_Por_Numero_Asiento.AutoSize = true;
            this.chkOrdenar_Por_Numero_Asiento.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkOrdenar_Por_Numero_Asiento.ForeColor = System.Drawing.Color.White;
            this.chkOrdenar_Por_Numero_Asiento.Location = new System.Drawing.Point(51, 249);
            this.chkOrdenar_Por_Numero_Asiento.Name = "chkOrdenar_Por_Numero_Asiento";
            this.chkOrdenar_Por_Numero_Asiento.Size = new System.Drawing.Size(198, 21);
            this.chkOrdenar_Por_Numero_Asiento.TabIndex = 103;
            this.chkOrdenar_Por_Numero_Asiento.Text = "Listar por Número de Asiento";
            this.chkOrdenar_Por_Numero_Asiento.UseVisualStyleBackColor = true;
            this.chkOrdenar_Por_Numero_Asiento.CheckedChanged += new System.EventHandler(this.chkOrdenar_Por_Numero_Asiento_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(48, 279);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 17);
            this.label5.TabIndex = 105;
            this.label5.Text = "ASIENTO DESDE:";
            // 
            // cboAsiento_Desde
            // 
            this.cboAsiento_Desde.BackColor = System.Drawing.Color.Black;
            this.cboAsiento_Desde.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboAsiento_Desde.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboAsiento_Desde.ForeColor = System.Drawing.Color.White;
            this.cboAsiento_Desde.FormattingEnabled = true;
            this.cboAsiento_Desde.Location = new System.Drawing.Point(154, 276);
            this.cboAsiento_Desde.Name = "cboAsiento_Desde";
            this.cboAsiento_Desde.Size = new System.Drawing.Size(190, 25);
            this.cboAsiento_Desde.TabIndex = 104;
            this.cboAsiento_Desde.SelectedValueChanged += new System.EventHandler(this.cboAsiento_Desde_SelectedValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(48, 310);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 17);
            this.label6.TabIndex = 107;
            this.label6.Text = "ASIENTO HASTA:";
            // 
            // cboAsiento_Hasta
            // 
            this.cboAsiento_Hasta.BackColor = System.Drawing.Color.Black;
            this.cboAsiento_Hasta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboAsiento_Hasta.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboAsiento_Hasta.ForeColor = System.Drawing.Color.White;
            this.cboAsiento_Hasta.FormattingEnabled = true;
            this.cboAsiento_Hasta.Location = new System.Drawing.Point(154, 307);
            this.cboAsiento_Hasta.Name = "cboAsiento_Hasta";
            this.cboAsiento_Hasta.Size = new System.Drawing.Size(190, 25);
            this.cboAsiento_Hasta.TabIndex = 106;
            // 
            // chkSeleccionar_Entre_Fechas
            // 
            this.chkSeleccionar_Entre_Fechas.AutoSize = true;
            this.chkSeleccionar_Entre_Fechas.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSeleccionar_Entre_Fechas.ForeColor = System.Drawing.Color.White;
            this.chkSeleccionar_Entre_Fechas.Location = new System.Drawing.Point(47, 105);
            this.chkSeleccionar_Entre_Fechas.Name = "chkSeleccionar_Entre_Fechas";
            this.chkSeleccionar_Entre_Fechas.Size = new System.Drawing.Size(177, 21);
            this.chkSeleccionar_Entre_Fechas.TabIndex = 108;
            this.chkSeleccionar_Entre_Fechas.Text = "Seleccionar Entre Fechas";
            this.chkSeleccionar_Entre_Fechas.UseVisualStyleBackColor = true;
            this.chkSeleccionar_Entre_Fechas.CheckedChanged += new System.EventHandler(this.chkSeleccionar_Entre_Fechas_CheckedChanged);
            // 
            // pMinimizar
            // 
            this.pMinimizar.Image = ((System.Drawing.Image)(resources.GetObject("pMinimizar.Image")));
            this.pMinimizar.Location = new System.Drawing.Point(421, -2);
            this.pMinimizar.Name = "pMinimizar";
            this.pMinimizar.Size = new System.Drawing.Size(22, 20);
            this.pMinimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pMinimizar.TabIndex = 110;
            this.pMinimizar.TabStop = false;
            this.pMinimizar.Click += new System.EventHandler(this.PMinimizar_Click);
            // 
            // pCerrar
            // 
            this.pCerrar.Image = ((System.Drawing.Image)(resources.GetObject("pCerrar.Image")));
            this.pCerrar.Location = new System.Drawing.Point(460, -2);
            this.pCerrar.Name = "pCerrar";
            this.pCerrar.Size = new System.Drawing.Size(20, 20);
            this.pCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pCerrar.TabIndex = 109;
            this.pCerrar.TabStop = false;
            this.pCerrar.Click += new System.EventHandler(this.PCerrar_Click);
            // 
            // chkLiberaFechas
            // 
            this.chkLiberaFechas.AutoSize = true;
            this.chkLiberaFechas.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.chkLiberaFechas.ForeColor = System.Drawing.Color.DimGray;
            this.chkLiberaFechas.Location = new System.Drawing.Point(70, 132);
            this.chkLiberaFechas.Name = "chkLiberaFechas";
            this.chkLiberaFechas.Size = new System.Drawing.Size(179, 21);
            this.chkLiberaFechas.TabIndex = 111;
            this.chkLiberaFechas.Text = "Ingreso de fechas Manual";
            this.chkLiberaFechas.UseVisualStyleBackColor = true;
            this.chkLiberaFechas.CheckedChanged += new System.EventHandler(this.ChkLiberaFechas_CheckedChanged);
            // 
            // frmListado_Libro_Diario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(481, 435);
            this.Controls.Add(this.chkLiberaFechas);
            this.Controls.Add(this.pMinimizar);
            this.Controls.Add(this.pCerrar);
            this.Controls.Add(this.chkSeleccionar_Entre_Fechas);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboAsiento_Hasta);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cboAsiento_Desde);
            this.Controls.Add(this.chkOrdenar_Por_Numero_Asiento);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboEmpresas);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.datFecha_Hasta);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.datFecha_Desde);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnProcesar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmListado_Libro_Diario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmListado_Libro_Diario";
            this.Load += new System.EventHandler(this.frmListado_Libro_Diario_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoverForm_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pMinimizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCerrar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboEmpresas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker datFecha_Hasta;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker datFecha_Desde;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnProcesar;
        private System.Windows.Forms.CheckBox chkOrdenar_Por_Numero_Asiento;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboAsiento_Desde;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboAsiento_Hasta;
        private System.Windows.Forms.CheckBox chkSeleccionar_Entre_Fechas;
        private System.Windows.Forms.PictureBox pMinimizar;
        private System.Windows.Forms.PictureBox pCerrar;
        private System.Windows.Forms.CheckBox chkLiberaFechas;
    }
}