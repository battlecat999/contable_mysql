namespace Contable
{
    partial class frmPlanCuentas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPlanCuentas));
            this.label3 = new System.Windows.Forms.Label();
            this.cboPlan_Cuentas = new System.Windows.Forms.ComboBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkAcepta_Movimientos = new System.Windows.Forms.CheckBox();
            this.txtNivel = new System.Windows.Forms.NumericUpDown();
            this.pMinimizar = new System.Windows.Forms.PictureBox();
            this.pCerrar = new System.Windows.Forms.PictureBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnGrabar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.txtID_Plan_Cuenta = new System.Windows.Forms.TextBox();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.grpLogin = new System.Windows.Forms.GroupBox();
            this.btnSalir_Login = new System.Windows.Forms.Button();
            this.btnAceptar_Login = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtNivel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pMinimizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCerrar)).BeginInit();
            this.grpLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(27, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "CÓDIGO PLAN DE CUENTA:";
            // 
            // cboPlan_Cuentas
            // 
            this.cboPlan_Cuentas.BackColor = System.Drawing.Color.Black;
            this.cboPlan_Cuentas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboPlan_Cuentas.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPlan_Cuentas.ForeColor = System.Drawing.Color.DimGray;
            this.cboPlan_Cuentas.FormattingEnabled = true;
            this.cboPlan_Cuentas.Location = new System.Drawing.Point(196, 105);
            this.cboPlan_Cuentas.Name = "cboPlan_Cuentas";
            this.cboPlan_Cuentas.Size = new System.Drawing.Size(177, 29);
            this.cboPlan_Cuentas.TabIndex = 1;
            this.cboPlan_Cuentas.SelectedValueChanged += new System.EventHandler(this.CboPlan_Cuentas_SelectedValueChanged);
            this.cboPlan_Cuentas.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CboPlan_Cuentas_KeyUp);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(196, 154);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(297, 20);
            this.txtDescripcion.TabIndex = 4;
            this.txtDescripcion.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtDescripcion_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(27, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "DESCRIPCIÓN:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(27, 205);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "NIVEL:";
            // 
            // chkAcepta_Movimientos
            // 
            this.chkAcepta_Movimientos.AutoSize = true;
            this.chkAcepta_Movimientos.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkAcepta_Movimientos.Location = new System.Drawing.Point(30, 248);
            this.chkAcepta_Movimientos.Name = "chkAcepta_Movimientos";
            this.chkAcepta_Movimientos.Size = new System.Drawing.Size(147, 17);
            this.chkAcepta_Movimientos.TabIndex = 7;
            this.chkAcepta_Movimientos.Text = "ACEPTA MOVIMIENTOS";
            this.chkAcepta_Movimientos.UseVisualStyleBackColor = true;
            this.chkAcepta_Movimientos.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ChkAcepta_Movimientos_KeyUp);
            // 
            // txtNivel
            // 
            this.txtNivel.BackColor = System.Drawing.Color.Black;
            this.txtNivel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNivel.ForeColor = System.Drawing.Color.White;
            this.txtNivel.Location = new System.Drawing.Point(196, 205);
            this.txtNivel.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.txtNivel.Name = "txtNivel";
            this.txtNivel.Size = new System.Drawing.Size(79, 20);
            this.txtNivel.TabIndex = 6;
            this.txtNivel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNivel.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtNivel_KeyUp);
            // 
            // pMinimizar
            // 
            this.pMinimizar.Image = ((System.Drawing.Image)(resources.GetObject("pMinimizar.Image")));
            this.pMinimizar.Location = new System.Drawing.Point(550, 12);
            this.pMinimizar.Name = "pMinimizar";
            this.pMinimizar.Size = new System.Drawing.Size(22, 20);
            this.pMinimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pMinimizar.TabIndex = 32;
            this.pMinimizar.TabStop = false;
            this.pMinimizar.Click += new System.EventHandler(this.PMinimizar_Click);
            // 
            // pCerrar
            // 
            this.pCerrar.Image = ((System.Drawing.Image)(resources.GetObject("pCerrar.Image")));
            this.pCerrar.Location = new System.Drawing.Point(589, 12);
            this.pCerrar.Name = "pCerrar";
            this.pCerrar.Size = new System.Drawing.Size(20, 20);
            this.pCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pCerrar.TabIndex = 31;
            this.pCerrar.TabStop = false;
            this.pCerrar.Click += new System.EventHandler(this.PCerrar_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.DimGray;
            this.lblTitulo.Location = new System.Drawing.Point(24, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(246, 33);
            this.lblTitulo.TabIndex = 33;
            this.lblTitulo.Text = "PLAN DE CUENTAS";
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnSalir.FlatAppearance.BorderSize = 0;
            this.btnSalir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnSalir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.ForeColor = System.Drawing.Color.LightGray;
            this.btnSalir.Location = new System.Drawing.Point(468, 296);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(141, 40);
            this.btnSalir.TabIndex = 12;
            this.btnSalir.Text = "SALIR";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.BtnSalir_Click);
            // 
            // btnGrabar
            // 
            this.btnGrabar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnGrabar.FlatAppearance.BorderSize = 0;
            this.btnGrabar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnGrabar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnGrabar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGrabar.ForeColor = System.Drawing.Color.LightGray;
            this.btnGrabar.Location = new System.Drawing.Point(174, 296);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(141, 40);
            this.btnGrabar.TabIndex = 10;
            this.btnGrabar.Text = "GRABAR";
            this.btnGrabar.UseVisualStyleBackColor = false;
            this.btnGrabar.Click += new System.EventHandler(this.BtnGrabar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            this.btnEliminar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnEliminar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.ForeColor = System.Drawing.Color.LightGray;
            this.btnEliminar.Location = new System.Drawing.Point(321, 296);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(141, 40);
            this.btnEliminar.TabIndex = 11;
            this.btnEliminar.Text = "ELIMINAR";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.BtnEliminar_Click);
            // 
            // txtID_Plan_Cuenta
            // 
            this.txtID_Plan_Cuenta.Location = new System.Drawing.Point(196, 111);
            this.txtID_Plan_Cuenta.Name = "txtID_Plan_Cuenta";
            this.txtID_Plan_Cuenta.Size = new System.Drawing.Size(201, 20);
            this.txtID_Plan_Cuenta.TabIndex = 2;
            this.txtID_Plan_Cuenta.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtID_Plan_Cuenta_KeyUp);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnNuevo.FlatAppearance.BorderSize = 0;
            this.btnNuevo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnNuevo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.ForeColor = System.Drawing.Color.LightGray;
            this.btnNuevo.Location = new System.Drawing.Point(27, 296);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(141, 40);
            this.btnNuevo.TabIndex = 8;
            this.btnNuevo.Text = "NUEVO";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.BtnNuevo_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnEditar.FlatAppearance.BorderSize = 0;
            this.btnEditar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnEditar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.ForeColor = System.Drawing.Color.LightGray;
            this.btnEditar.Location = new System.Drawing.Point(468, 235);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(141, 40);
            this.btnEditar.TabIndex = 9;
            this.btnEditar.Text = "EDITAR";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Visible = false;
            this.btnEditar.Click += new System.EventHandler(this.BtnEditar_Click);
            // 
            // grpLogin
            // 
            this.grpLogin.Controls.Add(this.btnSalir_Login);
            this.grpLogin.Controls.Add(this.btnAceptar_Login);
            this.grpLogin.Controls.Add(this.label4);
            this.grpLogin.Controls.Add(this.txtPassword);
            this.grpLogin.Location = new System.Drawing.Point(2, -1);
            this.grpLogin.Name = "grpLogin";
            this.grpLogin.Size = new System.Drawing.Size(630, 378);
            this.grpLogin.TabIndex = 34;
            this.grpLogin.TabStop = false;
            // 
            // btnSalir_Login
            // 
            this.btnSalir_Login.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnSalir_Login.FlatAppearance.BorderSize = 0;
            this.btnSalir_Login.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnSalir_Login.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSalir_Login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir_Login.ForeColor = System.Drawing.Color.LightGray;
            this.btnSalir_Login.Location = new System.Drawing.Point(319, 178);
            this.btnSalir_Login.Name = "btnSalir_Login";
            this.btnSalir_Login.Size = new System.Drawing.Size(141, 40);
            this.btnSalir_Login.TabIndex = 20;
            this.btnSalir_Login.Text = "SALIR";
            this.btnSalir_Login.UseVisualStyleBackColor = false;
            this.btnSalir_Login.Click += new System.EventHandler(this.BtnSalir_Login_Click);
            // 
            // btnAceptar_Login
            // 
            this.btnAceptar_Login.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnAceptar_Login.FlatAppearance.BorderSize = 0;
            this.btnAceptar_Login.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnAceptar_Login.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnAceptar_Login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar_Login.ForeColor = System.Drawing.Color.LightGray;
            this.btnAceptar_Login.Location = new System.Drawing.Point(135, 178);
            this.btnAceptar_Login.Name = "btnAceptar_Login";
            this.btnAceptar_Login.Size = new System.Drawing.Size(141, 40);
            this.btnAceptar_Login.TabIndex = 19;
            this.btnAceptar_Login.Text = "ACEPTAR";
            this.btnAceptar_Login.UseVisualStyleBackColor = false;
            this.btnAceptar_Login.Click += new System.EventHandler(this.BtnAceptar_Login_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(132, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 17);
            this.label4.TabIndex = 17;
            this.label4.Text = "PASSWORD:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(232, 112);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(228, 20);
            this.txtPassword.TabIndex = 18;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.WordWrap = false;
            this.txtPassword.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtPassword_KeyUp);
            // 
            // frmPlanCuentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(632, 375);
            this.Controls.Add(this.grpLogin);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.txtID_Plan_Cuenta);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnGrabar);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.pMinimizar);
            this.Controls.Add(this.pCerrar);
            this.Controls.Add(this.txtNivel);
            this.Controls.Add(this.chkAcepta_Movimientos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboPlan_Cuentas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPlanCuentas";
            this.Load += new System.EventHandler(this.FrmPlanCuentas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtNivel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pMinimizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCerrar)).EndInit();
            this.grpLogin.ResumeLayout(false);
            this.grpLogin.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboPlan_Cuentas;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkAcepta_Movimientos;
        private System.Windows.Forms.NumericUpDown txtNivel;
        private System.Windows.Forms.PictureBox pMinimizar;
        private System.Windows.Forms.PictureBox pCerrar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnGrabar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.TextBox txtID_Plan_Cuenta;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.GroupBox grpLogin;
        private System.Windows.Forms.Button btnSalir_Login;
        private System.Windows.Forms.Button btnAceptar_Login;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPassword;
    }
}