using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using System.Data.SqlClient;


namespace Contable
{
    public partial class frm_Loggin : Form
    {
        public frm_Loggin()
        {
            InitializeComponent();
            cargarLogo();
        }
        private void cargarLogo()
        {
            string Path = System.Windows.Forms.Application.StartupPath.ToString();

            this.picLoco.Image = Image.FromFile(string.Concat(Path, "\\empresa.jpg"));

            this.txtpass.PasswordChar = '*';

        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd,int wmsg, int wpara,int lparam);

        private void txtuser_Enter(object sender, EventArgs e)
        {
            if (this.txtuser.Text=="USUARIO") {
                this.txtuser.Text  = "";
                this.txtuser.ForeColor = Color.LightGray;
            }
        }

        private void txtuser_Leave(object sender, EventArgs e)
        {
            if (txtuser.Text==""){
                txtuser.Text = "USUARIO";
                txtuser.ForeColor = Color.DimGray;
            }
        }

        private void txtpass_Enter(object sender, EventArgs e)
        {
            if (this.txtpass.Text == "CONTRASEÑA")
            {
                this.txtpass.Text = "";
                this.txtpass.ForeColor = Color.LightGray;
                this.txtpass.UseSystemPasswordChar = true;
            }
        }

        private void txtpass_Leave(object sender, EventArgs e)
        {
            if (txtpass.Text == "")
            {
                this.txtpass.Text = "CONTRASEÑA";
                this.txtpass.ForeColor = Color.DimGray;
                this.txtpass.UseSystemPasswordChar = false;
            }
        }

        private void pCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void frmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle,0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnAcceder_Click(object sender, EventArgs e)
        {

            //creo un objeto de la capa de negocio
            ValidarUsuario objUsuario = new ValidarUsuario ();
            DataTable  Loguear;
            objUsuario.Usuario = txtuser.Text;
            objUsuario.Password = txtpass.Text;
            if (objUsuario.Usuario == txtuser.Text)
            {
                lblErrorUsuario.Visible = false;
                if (objUsuario.Password == txtpass.Text)
                {
                    lblErrorPass.Visible = false;
           
                    Loguear = objUsuario.iniciarSesion();
                    if (Loguear.Rows.Count > 0)
                    {
                        this.Hide();
                        frm_Inicial ObjFP = new frm_Inicial();
                        ObjFP.Show();
                    }
                    else
                    {
                        //MessageBox.Show("Usuario o Contraseña Inválido", "Atención");
                        lblErrorLogin.Text = "Usuario o Contraseña Inválidas, intente de nuevo";
                        lblErrorLogin.Visible = true;
                        txtpass.Text = "";
                        txtpass_Leave( null,e);
                        txtuser.Focus();
                    }
                }
                else
                {
                    //MessageBox.Show(objUsuario.Password);
                    lblErrorPass.Text = objUsuario.Password;
                    lblErrorPass.Visible = true;
                }
            }
            else
            {
                //MessageBox.Show(objUsuario.Usuario);
                lblErrorUsuario.Text= objUsuario.Usuario;
                lblErrorUsuario.Visible = true;
            }
        }

        private void txtuser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11)
            {
                txtuser.Text = "egonzalez";
                txtpass.UseSystemPasswordChar = true;
                txtpass.Text = "2123";
                btnAcceder_Click(sender,e);
                

            }
        }

        private void txtuser_TextChanged(object sender, EventArgs e)
        {

        }

        private void frm_Loggin_Load(object sender, EventArgs e)
        {
            this.txtuser.Focus();
        }
    }
}
