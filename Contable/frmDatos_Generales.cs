using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

using System.Data.Common;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;

namespace Contable
{
    public partial class frmDatos_Generales : Form
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
        //public IContract Empresa { get; set; }

        public Int16 Empresa { get; set; }

        public frmDatos_Generales()
        {
            InitializeComponent();

            Inicia();

        }

        private void pCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Inicia()
        {
            funciones_varias fv = new funciones_varias();
            fv.PrepararForm(this);
            Limpia_Controles();

        }

        private void Limpia_Controles()
        {
            this.datFecha_Inicio_Ejercicio_Actual.CustomFormat = " ";
            this.datFecha_Cierre_Ejercicio_Actual.CustomFormat = " ";
            this.datFecha_Inicio_Ejercicio_Anterior.CustomFormat = " ";
            this.datFecha_Cierre_Ejercicio_Anterior.CustomFormat = " ";

            this.datFecha_Inicio_Ejercicio_Actual.Text = string.Empty;
            this.datFecha_Cierre_Ejercicio_Actual.Text = string.Empty;
            this.datFecha_Inicio_Ejercicio_Anterior.Text = string.Empty;
            this.datFecha_Cierre_Ejercicio_Anterior.Text = string.Empty;
        }

        private void Carga_Datos()
        {
            string strConsulta = "";

            System.Data.DataTable dtDatos_Generales = new System.Data.DataTable();



            strConsulta = $"CALL `sgi_pop`.`sp_carga_datos_generales`({Empresa})";

            dtDatos_Generales = Entidades.GetDataTable(strConsulta);

            DateTime datFecha_Inicio_Ciclo_Actual;
            DateTime datFecha_Fin_Ciclo_Actual;
            DateTime datFecha_Inicio_Ciclo_Anterior;
            DateTime datFecha_Fin_Ciclo_Anterior;

            this.datFecha_Inicio_Ejercicio_Actual.CustomFormat = "dd/MM/yyyy";
            this.datFecha_Cierre_Ejercicio_Actual.CustomFormat = "dd/MM/yyyy";
            this.datFecha_Inicio_Ejercicio_Anterior.CustomFormat = "dd/MM/yyyy";
            this.datFecha_Cierre_Ejercicio_Anterior.CustomFormat = "dd/MM/yyyy";

            foreach (DataRow dr in dtDatos_Generales.Rows)
            {
                datFecha_Inicio_Ciclo_Actual = (DateTime)dr["Fecha_Inicio_Ejercicio"];
                datFecha_Fin_Ciclo_Actual = (DateTime)dr["Fecha_Cierre_Ejercicio"];
                datFecha_Inicio_Ciclo_Anterior = (DateTime)dr["Fecha_Inicio_Ejercicio_Anterior"];
                datFecha_Fin_Ciclo_Anterior = (DateTime)dr["Fecha_Cierre_Ejercicio_Anterior"];

                this.datFecha_Inicio_Ejercicio_Actual.Value = datFecha_Inicio_Ciclo_Actual;
                this.datFecha_Cierre_Ejercicio_Actual.Value = datFecha_Fin_Ciclo_Actual;
                this.datFecha_Inicio_Ejercicio_Anterior.Value = datFecha_Inicio_Ciclo_Anterior;
                this.datFecha_Cierre_Ejercicio_Anterior.Value = datFecha_Fin_Ciclo_Anterior;

                this.Refresh();
            }

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Grabar();
        }

        private void Grabar()
        {

            int intResultado;

            //DbParameter[] Parametros = new DbParameter[5];

            //Parametros[0] = new SqlParameter("@intEmpresa ", Empresa);
            //Parametros[1] = new SqlParameter("@datFecha_Inicio_Ejercicio ", this.datFecha_Inicio_Ejercicio_Actual.Value);
            //Parametros[2] = new SqlParameter("@datFecha_Fin_Ejercicio ", this.datFecha_Cierre_Ejercicio_Actual.Value);
            //Parametros[3] = new SqlParameter("@datFecha_Inicio_Ejercicio_Ciclo_Anterior ", this.datFecha_Inicio_Ejercicio_Anterior.Value);
            //Parametros[4] = new SqlParameter("@datFecha_Fin_Ejercicio_Cliclo_Anterior ", this.datFecha_Cierre_Ejercicio_Anterior.Value);


            MySqlParameter[] Parametros = new MySqlParameter[5];

            Parametros[0] = new MySqlParameter("@intEmpresa", Empresa);
            Parametros[1] = new MySqlParameter("@datFecha_Inicio_Ejercicio", this.datFecha_Inicio_Ejercicio_Actual.Value);
            Parametros[2] = new MySqlParameter("@datFecha_Fin_Ejercicio", this.datFecha_Cierre_Ejercicio_Actual.Value);
            Parametros[3] = new MySqlParameter("@datFecha_Inicio_Ejercicio_Ciclo_Anterior", this.datFecha_Inicio_Ejercicio_Anterior.Value);
            Parametros[4] = new MySqlParameter("@datFecha_Fin_Ejercicio_Cliclo_Anterior", this.datFecha_Cierre_Ejercicio_Anterior.Value);
     

            intResultado = Entidades.EjecutaNonQuery("sp_alta_datos_generales", Parametros);

        }

        private void frmDatos_Generales_Load(object sender, EventArgs e)
        {
            Carga_Datos();
        }

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
