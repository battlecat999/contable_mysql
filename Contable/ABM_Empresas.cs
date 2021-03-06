﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.Common;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

using System.Collections;
using System.Globalization;
using MySql.Data.MySqlClient;

namespace Contable
{
    public partial class ABM_Empresas : Form
    {
        bool esNueva = false;

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wpara, int lparam);

        System.Data.DataSet _dsPais = new System.Data.DataSet("Paises");
        BindingSource _bindingSource = new BindingSource();
        System.Data.DataSet _dsProvincias = new System.Data.DataSet("Provincias");

        private void MoverForm_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        public Int16 Empresa { get; set; } 
        public ABM_Empresas()
        {
            InitializeComponent();
            funciones_varias fv = new funciones_varias();
            fv.PrepararForm(this);
            Inicia();
        }
        private void Inicia()
        {
            this.cboEmpresas.SelectedValueChanged -= new System.EventHandler(this.cboEmpresas_SelectedValueChanged);

            Carga_Combo_Empresas();
            Carga_Combo_Provincias();
            Carga_Combo_Paises();
                       
            this.cboEmpresas.SelectedIndex = -1;

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

            this.cboEmpresas.SelectedIndex = -1;

        }
        private void CmdGrabar_Click(object sender, EventArgs e)
        {

            byte intEmpresa;
            int intProvincia = 0;
            int intPais = 0;
            string strRazonSocial;
            string strDireccion = this.txtDireccion.Text.ToString();
            string strLocalidad = this.txtLocalidad.Text.ToString();
            string strCUIT = this.mskCUIT.Text.ToString();

            if (this.cboEmpresas.SelectedIndex != -1)
            {
                intEmpresa = (byte)this.cboEmpresas.SelectedValue;

                esNueva = false;
            }
            else
            {
                intEmpresa = 0;
                esNueva = true;
            }
            strRazonSocial = this.cboEmpresas.Text.ToString();

            if (this.cboProvincia.SelectedIndex != -1)
            {
                intProvincia = Convert.ToInt32(this.cboProvincia.SelectedValue);
            }

            if (this.cboPais.SelectedIndex != -1)
            {
                intPais = Convert.ToInt32(this.cboPais.SelectedValue);
            }


            MySqlParameter[] Parametros = new MySqlParameter[7];

            Parametros[0] = new MySqlParameter("@intEmpresa", (int)intEmpresa);
            Parametros[1] = new MySqlParameter("@strRazonSocial", strRazonSocial);
            Parametros[2] = new MySqlParameter("@strDomicilio", strDireccion);
            Parametros[3] = new MySqlParameter("@strLocalidad", strLocalidad);
            Parametros[4] = new MySqlParameter("@intPais", intPais);
            Parametros[5] = new MySqlParameter("@intProvincia", intProvincia);
            Parametros[6] = new MySqlParameter("@strCUIT", this.mskCUIT.Text.ToString());


            if (esNueva == true)
            {
                int intRta = Entidades.EjecutaNonQuery("sp_alta_empresa", Parametros);
            }
            else
            {
                int intRta = Entidades.EjecutaNonQuery("sp_actualiza_empresa", Parametros);
            }

            Carga_Combo_Empresas();
            Limpiar_Controles();
        }
        private void cboEmpresas_SelectedValueChanged(object sender, EventArgs e)
        {


            //Limpia_Controles();

            if (this.cboEmpresas.SelectedIndex == -1)
            {
                esNueva = false;
                return;
            }
            esNueva = true;
            //Carga_Datos_Generales();
            System.Data.DataSet dsEmpresas = new System.Data.DataSet();

            string strConsulta = string.Empty;

            Byte intEmpresa = (Byte)this.cboEmpresas.SelectedValue;
            

            //strConsulta = " exec Carga_Asiento @intEmpresa = " + this.Empresa + ", @intAnio = " + this.txtAnio.Value.ToString();
            strConsulta = $"CALL `sgi_pop`.`cargar_datos_empresa`({intEmpresa});";

            dsEmpresas = Entidades.GetDataSet(strConsulta);

            DataRow[] returnedRows;
            returnedRows = dsEmpresas.Tables["Table1"].Select("IdEmpresa = " + this.cboEmpresas.SelectedValue);
            DataRow dr1;
            dr1 = returnedRows[0];
            //IdEmpresa RazonSocial                    Domicilio                      Localidad                      Pais   Provincia CUIT
           
            this.txtDireccion.Text = dr1["Domicilio"].ToString();
            this.txtLocalidad.Text = dr1["Localidad"].ToString();
            this.cboPais .SelectedValue = dr1["Pais"].ToString();
            this.cboProvincia.SelectedValue = dr1["Provincia"].ToString();
            this.mskCUIT .Text = dr1["CUIT"].ToString();




        }
        private void Carga_Combo_Provincias()
        {
            System.Data.DataSet dsEmpresas = new System.Data.DataSet("Provincias");

            string strConsulta = string.Empty;

           // Byte intEmpresa = (Byte)this.cboEmpresas.SelectedValue;

            strConsulta = "CALL `sgi_pop`.`sp_carga_Provincias`();";

            dsEmpresas = Entidades.GetDataSet(strConsulta);

            _dsProvincias = dsEmpresas;
            _bindingSource.DataSource = null;
            _bindingSource.DataSource = dsEmpresas.Tables["Table1"];

            this.cboProvincia.DataSource = _dsProvincias.Tables["Table1"];

            this.cboProvincia.ValueMember = "IdProvincia";
            this.cboProvincia.DisplayMember = "NombreProvincia";

            this.cboProvincia.Text = string.Empty;
            this.cboProvincia.SelectedIndex = -1;
        }
        private void Carga_Combo_Paises()
        {
            System.Data.DataSet dsEmpresas = new System.Data.DataSet("Paises");

            string strConsulta = string.Empty;

           // Byte intEmpresa = (Byte)this.cboEmpresas.SelectedValue;

            strConsulta = "CALL `sgi_pop`.`sp_carga_Paises`();";

            dsEmpresas = Entidades.GetDataSet(strConsulta);

            _dsPais = dsEmpresas;
            _bindingSource.DataSource = null;
            _bindingSource.DataSource = dsEmpresas.Tables["Table1"];

            this.cboPais.DataSource = _dsPais.Tables["Table1"];

            this.cboPais.ValueMember = "IdPais";
            this.cboPais.DisplayMember = "NombrePais";

            this.cboPais.Text = string.Empty;
            this.cboPais.SelectedIndex = -1;
        }

        private void CmdLimpiar_Click(object sender, EventArgs e)
        {
            Carga_Combo_Empresas();
            Limpiar_Controles();
        }
        private void Limpiar_Controles()
        {
            this.txtDireccion.Text = string.Empty;
            this.txtLocalidad.Text = string.Empty;
            this.cboProvincia.Text = string.Empty;
            this.cboProvincia.SelectedIndex = -1;
            this.cboPais .Text = string.Empty;
            this.cboPais.SelectedIndex = -1;
            this.mskCUIT.Text  = string.Empty ;

        }

    }
}
