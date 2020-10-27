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

namespace Contable
{
    public partial class frm_DatosGenerales : Form 
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wpara, int lparam);
        private void MoverForm_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        public frm_DatosGenerales()
        {
            //Inicial
            InitializeComponent();
            Inicia();
        }

        private void Inicia()
        {
            funciones_varias fv = new funciones_varias();
            fv.PrepararForm(this);
            this.cboEmpresas.SelectedValueChanged -= new System.EventHandler(this.cboEmpresas_SelectedValueChanged);
            Carga_Combo_Empresas();
            this.cboEmpresas.SelectedValueChanged += new System.EventHandler(this.cboEmpresas_SelectedValueChanged);
        }

        private void Carga_Combo_Empresas()
        {

            System.Data.DataSet dsEmpresas = new System.Data.DataSet("Empresas");

            string strConsulta = "";

            strConsulta = "CALL `sgi_pop`.`sp_empresas_select_all`();";

            dsEmpresas = Entidades.GetDataSet(strConsulta);

            cboEmpresas.DataSource = dsEmpresas.Tables["Table1"];

            this.cboEmpresas.DisplayMember = "RazonSocial";
            this.cboEmpresas.ValueMember = "IdEmpresa";

            //strConsulta = "select IdEmpresa, RazonSocial From Empresa Order By RazonSocial ";

            //dsEmpresas = Entidades.GetDataSet(strConsulta);

            //cboEmpresas.DataSource = dsEmpresas.Tables["Table"];

            //this.cboEmpresas.DisplayMember = "RazonSocial";
            //this.cboEmpresas.ValueMember = "IdEmpresa";

        }

        private void cboEmpresas_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            frmDatos_Generales fDatos_Generales = new frmDatos_Generales();
            fDatos_Generales.Empresa = (Byte)this.cboEmpresas.SelectedValue;
            fDatos_Generales.Show();
           
        }

        private void pMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frm_Part001_Load(object sender, EventArgs e)
        {

        }
    }
}
