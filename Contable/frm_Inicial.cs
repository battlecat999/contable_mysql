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
using System.Configuration;

namespace Contable
{
    public partial class frm_Inicial : Form
    {
        
        public frm_Inicial()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wpara, int lparam);
        private void barraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void btnSlide_DoubleClick(object sender, EventArgs e)
        {
            if (mnuVertical.Width == 250) {
                mnuVertical.Width = 70;
            }
            else
            {
                mnuVertical.Width = 250;
            }
        }
        private void Cargar_Logo()
        {
            string strPath = System.Windows.Forms.Application.StartupPath.ToString();
            this.picLoco.Image = Image.FromFile(string.Concat(strPath,"\\empresa.jpg"));
        }

        private void pcerrar_Click(object sender, EventArgs e)
        {

            Application.Exit();
        }

        private void pmaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            prestaurar.Visible = true;
            pmaximizar.Visible = false;
        }

        private void prestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            prestaurar.Visible = false;
            pmaximizar.Visible = true;
        }

        private void pminimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        private void AbrirFormInPanel(Object formHijo)
        {
            //if (this.panelContenedor.Controls.Count > 0)
            //    this.panelContenedor.Controls.RemoveAt(0);
            Form fh = formHijo as Form;
            //fh.TopLevel = false;
            //fh.Dock = DockStyle.Fill;
            //this.panelContenedor.Controls.Add(fh);
            //this.panelContenedor.Tag = fh;
            fh.Show();
             
        }

        private void btn_diccionario_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new frm_DatosGenerales() );
            
           
        }


        private void mnuVertical_Paint(object sender, PaintEventArgs e)
        {

        }

        private void barraTitulo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSlide_Click(object sender, EventArgs e)
        {

        }

        private void btn_Asientos_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new frmAsientos() );
        }

        private void btn_Despachante_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new frm_Alta_Factura_Despachante() );
        }

        private void btn_Copia_Asientos_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new frm_EmiteMayorGeneral());
        }

        private void btnSumas_Saldos_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new frm_SumasySaldos());
        }

        private void btnListado_Subdiario_Compras_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new frmListado_Subdiario_Cpras());
        }

        private void btnListado_Libro_Diarios_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new frmListado_Libro_Diario());
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel( new frmReportes() );
        }

        private void BtnPlan_Cuentas_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new frmPlanCuentas());
        }

        private void Btn_Empresa_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new ABM_Empresas());
        }

        private void Frm_Inicial_Load(object sender, EventArgs e)
        {

            funciones_varias fv = new funciones_varias();

            if (fv.leerLicencia() == false)
            {
                MessageBox.Show("Error de parametrización. Comuniquese con Soporte Técnico", "Atención", MessageBoxButtons.OK);
                Application.Exit();
            }
            string strMuestraFacturaDespachante = string.Empty;
            strMuestraFacturaDespachante = ConfigurationManager.AppSettings["MuestraFacturaDespachante"];
            if (strMuestraFacturaDespachante == "1")
            {
                this.btn_Despachante.Visible = false;
                this.btnListado_Subdiario_Compras.Visible = false;
            }
            else
            {
                this.btn_Despachante.Visible = true;
                this.btnListado_Subdiario_Compras.Visible = true;
            }

            Cargar_Logo();

        }

        private void BtnListado_Plan_Cuentas_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new frmListado_Plan_Cuentas());
        }
    }
}
