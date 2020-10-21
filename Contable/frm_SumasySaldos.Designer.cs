namespace Contable
{
    partial class frm_SumasySaldos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_SumasySaldos));
            this.pMinimizar = new System.Windows.Forms.PictureBox();
            this.pCerrar = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboEmpresas = new System.Windows.Forms.ComboBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnEmite_Listado = new System.Windows.Forms.Button();
            this.cmdDevuelve_Todo = new System.Windows.Forms.Button();
            this.cmdDevuelve_Uno = new System.Windows.Forms.Button();
            this.cmdPasa_Todo = new System.Windows.Forms.Button();
            this.cmdPasa_Uno = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.datFecha_Hasta = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.datFecha_Desde = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstCuentas_Seleccionadas = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstCuentas_Disponibles = new System.Windows.Forms.ListBox();
            this.chkLiberaFechas = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pMinimizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCerrar)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pMinimizar
            // 
            this.pMinimizar.Image = ((System.Drawing.Image)(resources.GetObject("pMinimizar.Image")));
            this.pMinimizar.Location = new System.Drawing.Point(711, 12);
            this.pMinimizar.Name = "pMinimizar";
            this.pMinimizar.Size = new System.Drawing.Size(22, 20);
            this.pMinimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pMinimizar.TabIndex = 21;
            this.pMinimizar.TabStop = false;
            this.pMinimizar.Click += new System.EventHandler(this.pMinimizar_Click);
            // 
            // pCerrar
            // 
            this.pCerrar.Image = ((System.Drawing.Image)(resources.GetObject("pCerrar.Image")));
            this.pCerrar.Location = new System.Drawing.Point(750, 12);
            this.pCerrar.Name = "pCerrar";
            this.pCerrar.Size = new System.Drawing.Size(20, 20);
            this.pCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pCerrar.TabIndex = 20;
            this.pCerrar.TabStop = false;
            this.pCerrar.Click += new System.EventHandler(this.pCerrar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(198, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(343, 33);
            this.label1.TabIndex = 19;
            this.label1.Text = "LISTADO SUMAS Y SALDOS";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DimGray;
            this.label6.Location = new System.Drawing.Point(21, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 17);
            this.label6.TabIndex = 34;
            this.label6.Text = "EMPRESA:";
            // 
            // cboEmpresas
            // 
            this.cboEmpresas.BackColor = System.Drawing.Color.Black;
            this.cboEmpresas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboEmpresas.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboEmpresas.ForeColor = System.Drawing.Color.White;
            this.cboEmpresas.FormattingEnabled = true;
            this.cboEmpresas.Location = new System.Drawing.Point(91, 67);
            this.cboEmpresas.Name = "cboEmpresas";
            this.cboEmpresas.Size = new System.Drawing.Size(207, 25);
            this.cboEmpresas.TabIndex = 33;
            this.cboEmpresas.SelectedIndexChanged += new System.EventHandler(this.CboEmpresas_SelectedIndexChanged);
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnSalir.FlatAppearance.BorderSize = 0;
            this.btnSalir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnSalir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.ForeColor = System.Drawing.Color.LightGray;
            this.btnSalir.Location = new System.Drawing.Point(407, 446);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(194, 40);
            this.btnSalir.TabIndex = 75;
            this.btnSalir.Text = "SALIR";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnEmite_Listado
            // 
            this.btnEmite_Listado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnEmite_Listado.FlatAppearance.BorderSize = 0;
            this.btnEmite_Listado.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnEmite_Listado.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnEmite_Listado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmite_Listado.ForeColor = System.Drawing.Color.LightGray;
            this.btnEmite_Listado.Location = new System.Drawing.Point(207, 446);
            this.btnEmite_Listado.Name = "btnEmite_Listado";
            this.btnEmite_Listado.Size = new System.Drawing.Size(194, 40);
            this.btnEmite_Listado.TabIndex = 74;
            this.btnEmite_Listado.Text = "EMITE LISTADO";
            this.btnEmite_Listado.UseVisualStyleBackColor = false;
            this.btnEmite_Listado.Click += new System.EventHandler(this.btnEmite_Listado_Click);
            // 
            // cmdDevuelve_Todo
            // 
            this.cmdDevuelve_Todo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmdDevuelve_Todo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdDevuelve_Todo.ForeColor = System.Drawing.Color.White;
            this.cmdDevuelve_Todo.Location = new System.Drawing.Point(359, 297);
            this.cmdDevuelve_Todo.Name = "cmdDevuelve_Todo";
            this.cmdDevuelve_Todo.Size = new System.Drawing.Size(59, 26);
            this.cmdDevuelve_Todo.TabIndex = 73;
            this.cmdDevuelve_Todo.Text = "<<";
            this.cmdDevuelve_Todo.UseVisualStyleBackColor = false;
            this.cmdDevuelve_Todo.Click += new System.EventHandler(this.cmdDevuelve_Todo_Click);
            // 
            // cmdDevuelve_Uno
            // 
            this.cmdDevuelve_Uno.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmdDevuelve_Uno.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdDevuelve_Uno.ForeColor = System.Drawing.Color.White;
            this.cmdDevuelve_Uno.Location = new System.Drawing.Point(359, 265);
            this.cmdDevuelve_Uno.Name = "cmdDevuelve_Uno";
            this.cmdDevuelve_Uno.Size = new System.Drawing.Size(59, 26);
            this.cmdDevuelve_Uno.TabIndex = 72;
            this.cmdDevuelve_Uno.Text = "<";
            this.cmdDevuelve_Uno.UseVisualStyleBackColor = false;
            this.cmdDevuelve_Uno.Click += new System.EventHandler(this.cmdDevuelve_Uno_Click);
            // 
            // cmdPasa_Todo
            // 
            this.cmdPasa_Todo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmdPasa_Todo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdPasa_Todo.ForeColor = System.Drawing.Color.White;
            this.cmdPasa_Todo.Location = new System.Drawing.Point(359, 224);
            this.cmdPasa_Todo.Name = "cmdPasa_Todo";
            this.cmdPasa_Todo.Size = new System.Drawing.Size(59, 26);
            this.cmdPasa_Todo.TabIndex = 71;
            this.cmdPasa_Todo.Text = ">>";
            this.cmdPasa_Todo.UseVisualStyleBackColor = false;
            this.cmdPasa_Todo.Click += new System.EventHandler(this.cmdPasa_Todo_Click);
            // 
            // cmdPasa_Uno
            // 
            this.cmdPasa_Uno.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmdPasa_Uno.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdPasa_Uno.ForeColor = System.Drawing.Color.White;
            this.cmdPasa_Uno.Location = new System.Drawing.Point(359, 192);
            this.cmdPasa_Uno.Name = "cmdPasa_Uno";
            this.cmdPasa_Uno.Size = new System.Drawing.Size(59, 26);
            this.cmdPasa_Uno.TabIndex = 70;
            this.cmdPasa_Uno.Text = ">";
            this.cmdPasa_Uno.UseVisualStyleBackColor = false;
            this.cmdPasa_Uno.Click += new System.EventHandler(this.cmdPasa_Uno_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(405, 405);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 17);
            this.label2.TabIndex = 69;
            this.label2.Text = "FECHA HASTA;";
            // 
            // datFecha_Hasta
            // 
            this.datFecha_Hasta.CalendarForeColor = System.Drawing.Color.White;
            this.datFecha_Hasta.CalendarMonthBackground = System.Drawing.Color.Black;
            this.datFecha_Hasta.CalendarTitleForeColor = System.Drawing.Color.White;
            this.datFecha_Hasta.CustomFormat = "dd/MM/yyyy";
            this.datFecha_Hasta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datFecha_Hasta.Location = new System.Drawing.Point(500, 405);
            this.datFecha_Hasta.Name = "datFecha_Hasta";
            this.datFecha_Hasta.Size = new System.Drawing.Size(101, 20);
            this.datFecha_Hasta.TabIndex = 68;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(157, 405);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 17);
            this.label4.TabIndex = 67;
            this.label4.Text = "FECHA DESDE;";
            // 
            // datFecha_Desde
            // 
            this.datFecha_Desde.CalendarForeColor = System.Drawing.Color.White;
            this.datFecha_Desde.CalendarMonthBackground = System.Drawing.Color.Black;
            this.datFecha_Desde.CalendarTitleForeColor = System.Drawing.Color.White;
            this.datFecha_Desde.CustomFormat = "dd/MM/yyyy";
            this.datFecha_Desde.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datFecha_Desde.Location = new System.Drawing.Point(252, 405);
            this.datFecha_Desde.Name = "datFecha_Desde";
            this.datFecha_Desde.Size = new System.Drawing.Size(101, 20);
            this.datFecha_Desde.TabIndex = 66;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstCuentas_Seleccionadas);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(424, 117);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(346, 264);
            this.groupBox2.TabIndex = 65;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CUENTAS SELECCIONADAS";
            // 
            // lstCuentas_Seleccionadas
            // 
            this.lstCuentas_Seleccionadas.AllowDrop = true;
            this.lstCuentas_Seleccionadas.BackColor = System.Drawing.Color.Black;
            this.lstCuentas_Seleccionadas.ForeColor = System.Drawing.Color.White;
            this.lstCuentas_Seleccionadas.FormattingEnabled = true;
            this.lstCuentas_Seleccionadas.Location = new System.Drawing.Point(26, 19);
            this.lstCuentas_Seleccionadas.Name = "lstCuentas_Seleccionadas";
            this.lstCuentas_Seleccionadas.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstCuentas_Seleccionadas.Size = new System.Drawing.Size(300, 225);
            this.lstCuentas_Seleccionadas.TabIndex = 50;
            this.lstCuentas_Seleccionadas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstCuentas_Seleccionadas_MouseDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstCuentas_Disponibles);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 116);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(341, 265);
            this.groupBox1.TabIndex = 64;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CUENTAS DISPONIBLES";
            // 
            // lstCuentas_Disponibles
            // 
            this.lstCuentas_Disponibles.AllowDrop = true;
            this.lstCuentas_Disponibles.BackColor = System.Drawing.Color.Black;
            this.lstCuentas_Disponibles.ForeColor = System.Drawing.Color.White;
            this.lstCuentas_Disponibles.FormattingEnabled = true;
            this.lstCuentas_Disponibles.Location = new System.Drawing.Point(19, 20);
            this.lstCuentas_Disponibles.Name = "lstCuentas_Disponibles";
            this.lstCuentas_Disponibles.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstCuentas_Disponibles.Size = new System.Drawing.Size(300, 225);
            this.lstCuentas_Disponibles.TabIndex = 51;
            this.lstCuentas_Disponibles.DragLeave += new System.EventHandler(this.lstCuentas_Disponibles_DragLeave);
            this.lstCuentas_Disponibles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstCuentas_Disponibles_MouseDown);
            // 
            // chkLiberaFechas
            // 
            this.chkLiberaFechas.AutoSize = true;
            this.chkLiberaFechas.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.chkLiberaFechas.ForeColor = System.Drawing.Color.DimGray;
            this.chkLiberaFechas.Location = new System.Drawing.Point(45, 381);
            this.chkLiberaFechas.Name = "chkLiberaFechas";
            this.chkLiberaFechas.Size = new System.Drawing.Size(179, 21);
            this.chkLiberaFechas.TabIndex = 76;
            this.chkLiberaFechas.Text = "Ingreso de fechas Manual";
            this.chkLiberaFechas.UseVisualStyleBackColor = true;
            this.chkLiberaFechas.CheckedChanged += new System.EventHandler(this.ChkLiberaFechas_CheckedChanged);
            // 
            // frm_SumasySaldos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(788, 524);
            this.Controls.Add(this.chkLiberaFechas);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnEmite_Listado);
            this.Controls.Add(this.cmdDevuelve_Todo);
            this.Controls.Add(this.cmdDevuelve_Uno);
            this.Controls.Add(this.cmdPasa_Todo);
            this.Controls.Add(this.cmdPasa_Uno);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.datFecha_Hasta);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.datFecha_Desde);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboEmpresas);
            this.Controls.Add(this.pMinimizar);
            this.Controls.Add(this.pCerrar);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_SumasySaldos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmPart004";
            this.Load += new System.EventHandler(this.frm_Part004_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MoverForm_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pMinimizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCerrar)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pMinimizar;
        private System.Windows.Forms.PictureBox pCerrar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboEmpresas;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnEmite_Listado;
        private System.Windows.Forms.Button cmdDevuelve_Todo;
        private System.Windows.Forms.Button cmdDevuelve_Uno;
        private System.Windows.Forms.Button cmdPasa_Todo;
        private System.Windows.Forms.Button cmdPasa_Uno;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker datFecha_Hasta;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker datFecha_Desde;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lstCuentas_Seleccionadas;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstCuentas_Disponibles;
        private System.Windows.Forms.CheckBox chkLiberaFechas;
    }
}