namespace Contable
{
    partial class frmDatos_Generales
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDatos_Generales));
            this.pMinimizar = new System.Windows.Forms.PictureBox();
            this.pCerrar = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.datFecha_Cierre_Ejercicio_Actual = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.datFecha_Inicio_Ejercicio_Actual = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.datFecha_Cierre_Ejercicio_Anterior = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.datFecha_Inicio_Ejercicio_Anterior = new System.Windows.Forms.DateTimePicker();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.lblError_Datos_Generales = new System.Windows.Forms.Label();
            this.cmdSalir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pMinimizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCerrar)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pMinimizar
            // 
            this.pMinimizar.Image = ((System.Drawing.Image)(resources.GetObject("pMinimizar.Image")));
            this.pMinimizar.Location = new System.Drawing.Point(763, 12);
            this.pMinimizar.Name = "pMinimizar";
            this.pMinimizar.Size = new System.Drawing.Size(22, 20);
            this.pMinimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pMinimizar.TabIndex = 15;
            this.pMinimizar.TabStop = false;
            this.pMinimizar.Click += new System.EventHandler(this.pMinimizar_Click);
            // 
            // pCerrar
            // 
            this.pCerrar.Image = ((System.Drawing.Image)(resources.GetObject("pCerrar.Image")));
            this.pCerrar.Location = new System.Drawing.Point(802, 12);
            this.pCerrar.Name = "pCerrar";
            this.pCerrar.Size = new System.Drawing.Size(20, 20);
            this.pCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pCerrar.TabIndex = 14;
            this.pCerrar.TabStop = false;
            this.pCerrar.Click += new System.EventHandler(this.pCerrar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(84, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(598, 33);
            this.label1.TabIndex = 13;
            this.label1.Text = "ALTA Y MODIFICACIÓN DE DATOS GENERALES";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.datFecha_Cierre_Ejercicio_Actual);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.datFecha_Inicio_Ejercicio_Actual);
            this.groupBox1.ForeColor = System.Drawing.Color.DimGray;
            this.groupBox1.Location = new System.Drawing.Point(48, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(356, 133);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "EJERCICIO ACTUAL";
            // 
            // datFecha_Cierre_Ejercicio_Actual
            // 
            this.datFecha_Cierre_Ejercicio_Actual.CalendarForeColor = System.Drawing.Color.White;
            this.datFecha_Cierre_Ejercicio_Actual.CalendarMonthBackground = System.Drawing.Color.Black;
            this.datFecha_Cierre_Ejercicio_Actual.CalendarTitleForeColor = System.Drawing.Color.White;
            this.datFecha_Cierre_Ejercicio_Actual.CustomFormat = "dd/MM/yyyy";
            this.datFecha_Cierre_Ejercicio_Actual.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datFecha_Cierre_Ejercicio_Actual.Location = new System.Drawing.Point(178, 81);
            this.datFecha_Cierre_Ejercicio_Actual.Name = "datFecha_Cierre_Ejercicio_Actual";
            this.datFecha_Cierre_Ejercicio_Actual.Size = new System.Drawing.Size(119, 20);
            this.datFecha_Cierre_Ejercicio_Actual.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(65, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 17);
            this.label3.TabIndex = 19;
            this.label3.Text = "FECHA DE CIERRE:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(65, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 17);
            this.label2.TabIndex = 18;
            this.label2.Text = "FECHA DE INICIO:";
            // 
            // datFecha_Inicio_Ejercicio_Actual
            // 
            this.datFecha_Inicio_Ejercicio_Actual.CalendarForeColor = System.Drawing.Color.White;
            this.datFecha_Inicio_Ejercicio_Actual.CalendarMonthBackground = System.Drawing.Color.Black;
            this.datFecha_Inicio_Ejercicio_Actual.CalendarTitleForeColor = System.Drawing.Color.White;
            this.datFecha_Inicio_Ejercicio_Actual.CustomFormat = "dd/MM/yyyy";
            this.datFecha_Inicio_Ejercicio_Actual.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datFecha_Inicio_Ejercicio_Actual.Location = new System.Drawing.Point(178, 42);
            this.datFecha_Inicio_Ejercicio_Actual.Name = "datFecha_Inicio_Ejercicio_Actual";
            this.datFecha_Inicio_Ejercicio_Actual.Size = new System.Drawing.Size(119, 20);
            this.datFecha_Inicio_Ejercicio_Actual.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.datFecha_Cierre_Ejercicio_Anterior);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.datFecha_Inicio_Ejercicio_Anterior);
            this.groupBox2.ForeColor = System.Drawing.Color.DimGray;
            this.groupBox2.Location = new System.Drawing.Point(431, 67);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(356, 133);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "EJERCICIO ANTERIOR";
            // 
            // datFecha_Cierre_Ejercicio_Anterior
            // 
            this.datFecha_Cierre_Ejercicio_Anterior.CalendarForeColor = System.Drawing.Color.White;
            this.datFecha_Cierre_Ejercicio_Anterior.CalendarMonthBackground = System.Drawing.Color.Black;
            this.datFecha_Cierre_Ejercicio_Anterior.CalendarTitleForeColor = System.Drawing.Color.White;
            this.datFecha_Cierre_Ejercicio_Anterior.CustomFormat = "dd/MM/yyyy";
            this.datFecha_Cierre_Ejercicio_Anterior.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datFecha_Cierre_Ejercicio_Anterior.Location = new System.Drawing.Point(182, 81);
            this.datFecha_Cierre_Ejercicio_Anterior.Name = "datFecha_Cierre_Ejercicio_Anterior";
            this.datFecha_Cierre_Ejercicio_Anterior.Size = new System.Drawing.Size(119, 20);
            this.datFecha_Cierre_Ejercicio_Anterior.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(69, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 17);
            this.label4.TabIndex = 23;
            this.label4.Text = "FECHA DE CIERRE:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(69, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 17);
            this.label5.TabIndex = 22;
            this.label5.Text = "FECHA DE INICIO:";
            // 
            // datFecha_Inicio_Ejercicio_Anterior
            // 
            this.datFecha_Inicio_Ejercicio_Anterior.CalendarForeColor = System.Drawing.Color.White;
            this.datFecha_Inicio_Ejercicio_Anterior.CalendarMonthBackground = System.Drawing.Color.Black;
            this.datFecha_Inicio_Ejercicio_Anterior.CalendarTitleForeColor = System.Drawing.Color.White;
            this.datFecha_Inicio_Ejercicio_Anterior.CustomFormat = "dd/MM/yyyy";
            this.datFecha_Inicio_Ejercicio_Anterior.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datFecha_Inicio_Ejercicio_Anterior.Location = new System.Drawing.Point(182, 38);
            this.datFecha_Inicio_Ejercicio_Anterior.Name = "datFecha_Inicio_Ejercicio_Anterior";
            this.datFecha_Inicio_Ejercicio_Anterior.Size = new System.Drawing.Size(119, 20);
            this.datFecha_Inicio_Ejercicio_Anterior.TabIndex = 21;
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnAceptar.FlatAppearance.BorderSize = 0;
            this.btnAceptar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnAceptar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.ForeColor = System.Drawing.Color.LightGray;
            this.btnAceptar.Location = new System.Drawing.Point(214, 244);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(194, 40);
            this.btnAceptar.TabIndex = 19;
            this.btnAceptar.Text = "GRABAR DATOS";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // lblError_Datos_Generales
            // 
            this.lblError_Datos_Generales.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.lblError_Datos_Generales.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.lblError_Datos_Generales.Location = new System.Drawing.Point(222, 212);
            this.lblError_Datos_Generales.Name = "lblError_Datos_Generales";
            this.lblError_Datos_Generales.Size = new System.Drawing.Size(408, 19);
            this.lblError_Datos_Generales.TabIndex = 20;
            this.lblError_Datos_Generales.Text = "Error";
            this.lblError_Datos_Generales.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblError_Datos_Generales.Visible = false;
            // 
            // cmdSalir
            // 
            this.cmdSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.cmdSalir.FlatAppearance.BorderSize = 0;
            this.cmdSalir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.cmdSalir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmdSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdSalir.ForeColor = System.Drawing.Color.LightGray;
            this.cmdSalir.Location = new System.Drawing.Point(414, 244);
            this.cmdSalir.Name = "cmdSalir";
            this.cmdSalir.Size = new System.Drawing.Size(194, 40);
            this.cmdSalir.TabIndex = 21;
            this.cmdSalir.Text = "SALIR";
            this.cmdSalir.UseVisualStyleBackColor = false;
            this.cmdSalir.Click += new System.EventHandler(this.cmdSalir_Click);
            // 
            // frmDatos_Generales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(834, 311);
            this.Controls.Add(this.cmdSalir);
            this.Controls.Add(this.lblError_Datos_Generales);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pMinimizar);
            this.Controls.Add(this.pCerrar);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDatos_Generales";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Datos Generales";
            this.Load += new System.EventHandler(this.frmDatos_Generales_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoverForm_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pMinimizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCerrar)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pMinimizar;
        private System.Windows.Forms.PictureBox pCerrar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker datFecha_Cierre_Ejercicio_Actual;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker datFecha_Inicio_Ejercicio_Actual;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker datFecha_Cierre_Ejercicio_Anterior;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker datFecha_Inicio_Ejercicio_Anterior;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label lblError_Datos_Generales;
        private System.Windows.Forms.Button cmdSalir;
    }
}