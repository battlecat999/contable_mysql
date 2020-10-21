namespace Contable
{
    partial class frmAsientos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAsientos));
            this.pMinimizar = new System.Windows.Forms.PictureBox();
            this.pCerrar = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAnio = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.cboAsientos = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.datFecha = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.cboComprobantes = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.optAnterior = new System.Windows.Forms.RadioButton();
            this.optActual = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtLeyenda_Asiento = new System.Windows.Forms.TextBox();
            this.grdAsientos = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.cboEmpresas = new System.Windows.Forms.ComboBox();
            this.btnGrabar = new System.Windows.Forms.Button();
            this.txtTotal_Debe = new System.Windows.Forms.TextBox();
            this.txtTotal_Haber = new System.Windows.Forms.TextBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnLimpiar_Pantalla = new System.Windows.Forms.Button();
            this.datFecha_Inicio_Ejercicio = new System.Windows.Forms.DateTimePicker();
            this.datFecha_Cierre_Ejercicio = new System.Windows.Forms.DateTimePicker();
            this.datFecha_Cierre_Ejercicio_Anterior = new System.Windows.Forms.DateTimePicker();
            this.datFecha_Inicio_Ejercicio_Anterior = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.cboEstados = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtFecha = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pMinimizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCerrar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAnio)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAsientos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pMinimizar
            // 
            this.pMinimizar.Image = ((System.Drawing.Image)(resources.GetObject("pMinimizar.Image")));
            this.pMinimizar.Location = new System.Drawing.Point(770, 12);
            this.pMinimizar.Name = "pMinimizar";
            this.pMinimizar.Size = new System.Drawing.Size(22, 20);
            this.pMinimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pMinimizar.TabIndex = 18;
            this.pMinimizar.TabStop = false;
            // 
            // pCerrar
            // 
            this.pCerrar.Image = ((System.Drawing.Image)(resources.GetObject("pCerrar.Image")));
            this.pCerrar.Location = new System.Drawing.Point(809, 12);
            this.pCerrar.Name = "pCerrar";
            this.pCerrar.Size = new System.Drawing.Size(20, 20);
            this.pCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pCerrar.TabIndex = 17;
            this.pCerrar.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(156, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(478, 33);
            this.label1.TabIndex = 16;
            this.label1.Text = "ALTA Y MODIFICACIÓN DE ASIENTOS";
            // 
            // txtAnio
            // 
            this.txtAnio.Location = new System.Drawing.Point(424, 70);
            this.txtAnio.Maximum = new decimal(new int[] {
            2999,
            0,
            0,
            0});
            this.txtAnio.Minimum = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.txtAnio.Name = "txtAnio";
            this.txtAnio.Size = new System.Drawing.Size(68, 20);
            this.txtAnio.TabIndex = 2;
            this.txtAnio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAnio.Value = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.txtAnio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAnio_KeyDown);
            this.txtAnio.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtAnio_KeyUp);
            this.txtAnio.Leave += new System.EventHandler(this.txtAnio_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(354, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 17);
            this.label2.TabIndex = 20;
            this.label2.Text = "AÑO:";
            // 
            // cboAsientos
            // 
            this.cboAsientos.BackColor = System.Drawing.Color.Black;
            this.cboAsientos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboAsientos.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboAsientos.ForeColor = System.Drawing.Color.DimGray;
            this.cboAsientos.FormattingEnabled = true;
            this.cboAsientos.Location = new System.Drawing.Point(98, 117);
            this.cboAsientos.Name = "cboAsientos";
            this.cboAsientos.Size = new System.Drawing.Size(177, 29);
            this.cboAsientos.TabIndex = 3;
            this.cboAsientos.SelectedValueChanged += new System.EventHandler(this.cboAsientos_SelectedValueChanged);
            this.cboAsientos.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cboAsientos_KeyUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(28, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 17);
            this.label3.TabIndex = 22;
            this.label3.Text = "ASIENTO:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(301, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 17);
            this.label4.TabIndex = 24;
            this.label4.Text = "FECHA:";
            // 
            // datFecha
            // 
            this.datFecha.CalendarForeColor = System.Drawing.Color.White;
            this.datFecha.CalendarMonthBackground = System.Drawing.Color.Black;
            this.datFecha.CalendarTitleForeColor = System.Drawing.Color.White;
            this.datFecha.CustomFormat = "dd/MM/yyyy";
            this.datFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datFecha.Location = new System.Drawing.Point(356, 100);
            this.datFecha.Name = "datFecha";
            this.datFecha.Size = new System.Drawing.Size(100, 20);
            this.datFecha.TabIndex = 20;
            this.datFecha.Visible = false;
            this.datFecha.ValueChanged += new System.EventHandler(this.datFecha_ValueChanged);
            this.datFecha.Enter += new System.EventHandler(this.DatFecha_Enter);
            this.datFecha.KeyDown += new System.Windows.Forms.KeyEventHandler(this.datFecha_KeyDown);
            this.datFecha.KeyUp += new System.Windows.Forms.KeyEventHandler(this.datFecha_KeyUp);
            this.datFecha.Leave += new System.EventHandler(this.datFecha_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(471, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 17);
            this.label5.TabIndex = 26;
            this.label5.Text = "NRO. COMPROBANTE:";
            // 
            // cboComprobantes
            // 
            this.cboComprobantes.BackColor = System.Drawing.Color.Black;
            this.cboComprobantes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboComprobantes.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboComprobantes.ForeColor = System.Drawing.Color.DimGray;
            this.cboComprobantes.FormattingEnabled = true;
            this.cboComprobantes.Location = new System.Drawing.Point(610, 122);
            this.cboComprobantes.Name = "cboComprobantes";
            this.cboComprobantes.Size = new System.Drawing.Size(219, 29);
            this.cboComprobantes.TabIndex = 5;
            this.cboComprobantes.SelectedIndexChanged += new System.EventHandler(this.cboComprobantes_SelectedIndexChanged);
            this.cboComprobantes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CboComprobantes_KeyPress);
            this.cboComprobantes.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cboComprobantes_KeyUp);
            this.cboComprobantes.Leave += new System.EventHandler(this.cboComprobantes_Leave);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.optAnterior);
            this.groupBox1.Controls.Add(this.optActual);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(610, 171);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(219, 64);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "EJERCICIO";
            // 
            // optAnterior
            // 
            this.optAnterior.AutoSize = true;
            this.optAnterior.Location = new System.Drawing.Point(119, 29);
            this.optAnterior.Name = "optAnterior";
            this.optAnterior.Size = new System.Drawing.Size(81, 17);
            this.optAnterior.TabIndex = 9;
            this.optAnterior.TabStop = true;
            this.optAnterior.Text = "ANTERIOR";
            this.optAnterior.UseVisualStyleBackColor = true;
            this.optAnterior.CheckedChanged += new System.EventHandler(this.optAnterior_CheckedChanged);
            this.optAnterior.Click += new System.EventHandler(this.optAnterior_Click);
            // 
            // optActual
            // 
            this.optActual.AutoSize = true;
            this.optActual.Location = new System.Drawing.Point(27, 29);
            this.optActual.Name = "optActual";
            this.optActual.Size = new System.Drawing.Size(67, 17);
            this.optActual.TabIndex = 8;
            this.optActual.TabStop = true;
            this.optActual.Text = "ACTUAL";
            this.optActual.UseVisualStyleBackColor = true;
            this.optActual.CheckedChanged += new System.EventHandler(this.optActual_CheckedChanged);
            this.optActual.Click += new System.EventHandler(this.optActual_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Black;
            this.groupBox2.Controls.Add(this.txtLeyenda_Asiento);
            this.groupBox2.ForeColor = System.Drawing.Color.DimGray;
            this.groupBox2.Location = new System.Drawing.Point(25, 171);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(440, 90);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "LEYENDA GENERAL DEL ASIENTO";
            // 
            // txtLeyenda_Asiento
            // 
            this.txtLeyenda_Asiento.BackColor = System.Drawing.Color.Black;
            this.txtLeyenda_Asiento.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLeyenda_Asiento.ForeColor = System.Drawing.Color.White;
            this.txtLeyenda_Asiento.Location = new System.Drawing.Point(6, 19);
            this.txtLeyenda_Asiento.Name = "txtLeyenda_Asiento";
            this.txtLeyenda_Asiento.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLeyenda_Asiento.Size = new System.Drawing.Size(428, 13);
            this.txtLeyenda_Asiento.TabIndex = 0;
            this.txtLeyenda_Asiento.TextChanged += new System.EventHandler(this.txtLeyenda_Asiento_TextChanged);
            this.txtLeyenda_Asiento.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtLeyenda_Asiento_KeyUp);
            // 
            // grdAsientos
            // 
            this.grdAsientos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdAsientos.Location = new System.Drawing.Point(25, 281);
            this.grdAsientos.Name = "grdAsientos";
            this.grdAsientos.Size = new System.Drawing.Size(804, 197);
            this.grdAsientos.TabIndex = 7;
            this.grdAsientos.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.grdAsientos_CellBeginEdit);
            this.grdAsientos.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdAsientos_CellEndEdit);
            this.grdAsientos.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdAsientos_CellValueChanged);
            this.grdAsientos.CurrentCellDirtyStateChanged += new System.EventHandler(this.GrdAsientos_CurrentCellDirtyStateChanged);
            this.grdAsientos.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.grdAsientos_DefaultValuesNeeded);
            this.grdAsientos.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.grdAsientos_EditingControlShowing);
            this.grdAsientos.SelectionChanged += new System.EventHandler(this.grdAsientos_SelectionChanged);
            this.grdAsientos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdAsientos_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DimGray;
            this.label6.Location = new System.Drawing.Point(28, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 17);
            this.label6.TabIndex = 32;
            this.label6.Text = "EMPRESA:";
            // 
            // cboEmpresas
            // 
            this.cboEmpresas.BackColor = System.Drawing.Color.Black;
            this.cboEmpresas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboEmpresas.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboEmpresas.ForeColor = System.Drawing.Color.White;
            this.cboEmpresas.FormattingEnabled = true;
            this.cboEmpresas.Location = new System.Drawing.Point(98, 67);
            this.cboEmpresas.Name = "cboEmpresas";
            this.cboEmpresas.Size = new System.Drawing.Size(207, 25);
            this.cboEmpresas.TabIndex = 1;
            this.cboEmpresas.SelectedValueChanged += new System.EventHandler(this.cboEmpresas_SelectedValueChanged);
            this.cboEmpresas.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cboEmpresas_KeyUp);
            this.cboEmpresas.Leave += new System.EventHandler(this.cboEmpresas_Leave);
            // 
            // btnGrabar
            // 
            this.btnGrabar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnGrabar.FlatAppearance.BorderSize = 0;
            this.btnGrabar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnGrabar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnGrabar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGrabar.ForeColor = System.Drawing.Color.LightGray;
            this.btnGrabar.Location = new System.Drawing.Point(172, 537);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(141, 40);
            this.btnGrabar.TabIndex = 8;
            this.btnGrabar.Text = "GRABAR";
            this.btnGrabar.UseVisualStyleBackColor = false;
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // txtTotal_Debe
            // 
            this.txtTotal_Debe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotal_Debe.Location = new System.Drawing.Point(570, 490);
            this.txtTotal_Debe.Name = "txtTotal_Debe";
            this.txtTotal_Debe.ReadOnly = true;
            this.txtTotal_Debe.Size = new System.Drawing.Size(100, 20);
            this.txtTotal_Debe.TabIndex = 86;
            this.txtTotal_Debe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotal_Debe.WordWrap = false;
            // 
            // txtTotal_Haber
            // 
            this.txtTotal_Haber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotal_Haber.Location = new System.Drawing.Point(729, 490);
            this.txtTotal_Haber.Name = "txtTotal_Haber";
            this.txtTotal_Haber.ReadOnly = true;
            this.txtTotal_Haber.Size = new System.Drawing.Size(100, 20);
            this.txtTotal_Haber.TabIndex = 87;
            this.txtTotal_Haber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotal_Haber.WordWrap = false;
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnSalir.FlatAppearance.BorderSize = 0;
            this.btnSalir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnSalir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.ForeColor = System.Drawing.Color.LightGray;
            this.btnSalir.Location = new System.Drawing.Point(319, 537);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(141, 40);
            this.btnSalir.TabIndex = 9;
            this.btnSalir.Text = "SALIR";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnLimpiar_Pantalla
            // 
            this.btnLimpiar_Pantalla.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnLimpiar_Pantalla.FlatAppearance.BorderSize = 0;
            this.btnLimpiar_Pantalla.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnLimpiar_Pantalla.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnLimpiar_Pantalla.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar_Pantalla.ForeColor = System.Drawing.Color.LightGray;
            this.btnLimpiar_Pantalla.Location = new System.Drawing.Point(25, 537);
            this.btnLimpiar_Pantalla.Name = "btnLimpiar_Pantalla";
            this.btnLimpiar_Pantalla.Size = new System.Drawing.Size(141, 40);
            this.btnLimpiar_Pantalla.TabIndex = 88;
            this.btnLimpiar_Pantalla.Text = "LIMPIAR PANTALLA";
            this.btnLimpiar_Pantalla.UseVisualStyleBackColor = false;
            this.btnLimpiar_Pantalla.Click += new System.EventHandler(this.btnLimpiar_Pantalla_Click);
            // 
            // datFecha_Inicio_Ejercicio
            // 
            this.datFecha_Inicio_Ejercicio.CalendarForeColor = System.Drawing.Color.White;
            this.datFecha_Inicio_Ejercicio.CalendarMonthBackground = System.Drawing.Color.Black;
            this.datFecha_Inicio_Ejercicio.CalendarTitleForeColor = System.Drawing.Color.White;
            this.datFecha_Inicio_Ejercicio.CustomFormat = "dd/MM/yyyy";
            this.datFecha_Inicio_Ejercicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datFecha_Inicio_Ejercicio.Location = new System.Drawing.Point(610, 66);
            this.datFecha_Inicio_Ejercicio.Name = "datFecha_Inicio_Ejercicio";
            this.datFecha_Inicio_Ejercicio.Size = new System.Drawing.Size(101, 20);
            this.datFecha_Inicio_Ejercicio.TabIndex = 89;
            // 
            // datFecha_Cierre_Ejercicio
            // 
            this.datFecha_Cierre_Ejercicio.CalendarForeColor = System.Drawing.Color.White;
            this.datFecha_Cierre_Ejercicio.CalendarMonthBackground = System.Drawing.Color.Black;
            this.datFecha_Cierre_Ejercicio.CalendarTitleForeColor = System.Drawing.Color.White;
            this.datFecha_Cierre_Ejercicio.CustomFormat = "dd/MM/yyyy";
            this.datFecha_Cierre_Ejercicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datFecha_Cierre_Ejercicio.Location = new System.Drawing.Point(610, 96);
            this.datFecha_Cierre_Ejercicio.Name = "datFecha_Cierre_Ejercicio";
            this.datFecha_Cierre_Ejercicio.Size = new System.Drawing.Size(101, 20);
            this.datFecha_Cierre_Ejercicio.TabIndex = 90;
            // 
            // datFecha_Cierre_Ejercicio_Anterior
            // 
            this.datFecha_Cierre_Ejercicio_Anterior.CalendarForeColor = System.Drawing.Color.White;
            this.datFecha_Cierre_Ejercicio_Anterior.CalendarMonthBackground = System.Drawing.Color.Black;
            this.datFecha_Cierre_Ejercicio_Anterior.CalendarTitleForeColor = System.Drawing.Color.White;
            this.datFecha_Cierre_Ejercicio_Anterior.CustomFormat = "dd/MM/yyyy";
            this.datFecha_Cierre_Ejercicio_Anterior.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datFecha_Cierre_Ejercicio_Anterior.Location = new System.Drawing.Point(717, 97);
            this.datFecha_Cierre_Ejercicio_Anterior.Name = "datFecha_Cierre_Ejercicio_Anterior";
            this.datFecha_Cierre_Ejercicio_Anterior.Size = new System.Drawing.Size(101, 20);
            this.datFecha_Cierre_Ejercicio_Anterior.TabIndex = 92;
            // 
            // datFecha_Inicio_Ejercicio_Anterior
            // 
            this.datFecha_Inicio_Ejercicio_Anterior.CalendarForeColor = System.Drawing.Color.White;
            this.datFecha_Inicio_Ejercicio_Anterior.CalendarMonthBackground = System.Drawing.Color.Black;
            this.datFecha_Inicio_Ejercicio_Anterior.CalendarTitleForeColor = System.Drawing.Color.White;
            this.datFecha_Inicio_Ejercicio_Anterior.CustomFormat = "dd/MM/yyyy";
            this.datFecha_Inicio_Ejercicio_Anterior.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datFecha_Inicio_Ejercicio_Anterior.Location = new System.Drawing.Point(717, 67);
            this.datFecha_Inicio_Ejercicio_Anterior.Name = "datFecha_Inicio_Ejercicio_Anterior";
            this.datFecha_Inicio_Ejercicio_Anterior.Size = new System.Drawing.Size(101, 20);
            this.datFecha_Inicio_Ejercicio_Anterior.TabIndex = 91;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DimGray;
            this.label7.Location = new System.Drawing.Point(28, 498);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 17);
            this.label7.TabIndex = 94;
            this.label7.Text = "ESTADO:";
            // 
            // cboEstados
            // 
            this.cboEstados.BackColor = System.Drawing.Color.Black;
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboEstados.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboEstados.ForeColor = System.Drawing.Color.White;
            this.cboEstados.FormattingEnabled = true;
            this.cboEstados.Location = new System.Drawing.Point(91, 495);
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(207, 25);
            this.cboEstados.TabIndex = 93;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(588, 46);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(240, 71);
            this.pictureBox1.TabIndex = 95;
            this.pictureBox1.TabStop = false;
            // 
            // txtFecha
            // 
            this.txtFecha.Location = new System.Drawing.Point(356, 126);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Size = new System.Drawing.Size(100, 20);
            this.txtFecha.TabIndex = 4;
            this.txtFecha.Enter += new System.EventHandler(this.TxtFecha_Enter);
            this.txtFecha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtFecha_KeyPress);
            this.txtFecha.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtFecha_KeyUp);
            this.txtFecha.Leave += new System.EventHandler(this.TxtFecha_Leave);
            // 
            // frmAsientos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(850, 603);
            this.Controls.Add(this.txtFecha);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cboEstados);
            this.Controls.Add(this.datFecha_Cierre_Ejercicio_Anterior);
            this.Controls.Add(this.datFecha_Inicio_Ejercicio_Anterior);
            this.Controls.Add(this.datFecha_Cierre_Ejercicio);
            this.Controls.Add(this.datFecha_Inicio_Ejercicio);
            this.Controls.Add(this.btnLimpiar_Pantalla);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.txtTotal_Haber);
            this.Controls.Add(this.txtTotal_Debe);
            this.Controls.Add(this.btnGrabar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboEmpresas);
            this.Controls.Add(this.grdAsientos);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cboComprobantes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.datFecha);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboAsientos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAnio);
            this.Controls.Add(this.pMinimizar);
            this.Controls.Add(this.pCerrar);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmAsientos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmAsientos_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoverForm_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pMinimizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCerrar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAnio)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAsientos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pMinimizar;
        private System.Windows.Forms.PictureBox pCerrar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown txtAnio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboAsientos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker datFecha;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboComprobantes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton optAnterior;
        private System.Windows.Forms.RadioButton optActual;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtLeyenda_Asiento;
        private System.Windows.Forms.DataGridView grdAsientos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboEmpresas;
        private System.Windows.Forms.Button btnGrabar;
        private System.Windows.Forms.TextBox txtTotal_Debe;
        private System.Windows.Forms.TextBox txtTotal_Haber;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnLimpiar_Pantalla;
        private System.Windows.Forms.DateTimePicker datFecha_Inicio_Ejercicio;
        private System.Windows.Forms.DateTimePicker datFecha_Cierre_Ejercicio;
        private System.Windows.Forms.DateTimePicker datFecha_Cierre_Ejercicio_Anterior;
        private System.Windows.Forms.DateTimePicker datFecha_Inicio_Ejercicio_Anterior;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboEstados;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtFecha;
    }
}