using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
//using System.Data.OleDb;
using System.Data.Common;
using System.Text.RegularExpressions;

using System.Globalization;

namespace Contable
{
    public partial class frm_Alta_Factura_Despachante : Form
    {

        private bool alta = false;
        public bool Alta
        {
            get { return alta; }
            set { alta = value; }
        }

        public frm_Alta_Factura_Despachante()
        {
            InitializeComponent();
        }

        private void frm_Alta_Factura_Despachante_Load(object sender, EventArgs e)
        {
            Inicia();
        }

        private void Inicia()
        {
            funciones_varias fv = new funciones_varias();
            fv.PrepararForm(this);



            Cursor.Current = Cursors.WaitCursor;

            this.cboEmpresas.SelectedValueChanged -= new System.EventHandler(this.cboEmpresas_SelectedValueChanged);

            Carga_Combo_Empresas();
            
            this.cboEmpresas.SelectedValueChanged += new System.EventHandler(this.cboEmpresas_SelectedValueChanged);

            this.cboProveedores.SelectedValueChanged -= new System.EventHandler(this.cboProveedores_SelectedValueChanged);

            Carga_Combo_Proveedores();

            //devuelvo el control a el combo proveedores
            this.cboProveedores.SelectedValueChanged += new System.EventHandler(this.cboProveedores_SelectedValueChanged);

            Carga_Combo_Aduanas();
            Carga_Combo_Tipos_Comprobantes();
            Carga_Combo_Condiciones_IVA();
            Carga_Combo_Condiciones_Ganancias();
            Carga_Combo_Estados_Comprobantes_Despachante();
            Carga_Combo_Monedas();
            Carga_Combo_Letras_Comprobantes();

            //QUITA FLECHAS UP/DOWN DE LOS CONTROLES NUMÉRICOS.
            this.txtNeto.Controls.RemoveAt(0);
            this.txtIVA_21.Controls.RemoveAt(0);
            this.txtIVA_10.Controls.RemoveAt(0);
            this.txtGanancias.Controls.RemoveAt(0);
            this.txtDerechos.Controls.RemoveAt(0);
            this.txtIIBB_Prov.Controls.RemoveAt(0);
            this.txtIIBB.Controls.RemoveAt(0);
            this.txtOtros.Controls.RemoveAt(0);
            this.txtPercepcion_IVA.Controls.RemoveAt(0);
            this.txtTotal.Controls.RemoveAt(0);

            //SETEO AL COMBO PARA QUE PUEDA BUSCAR MIENTRAS SE VA TIPEANDO EL DATO
            //EMPRESAS
            this.cboEmpresas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboEmpresas.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cboEmpresas.AutoCompleteSource = AutoCompleteSource.ListItems;

            //COMPRAS BU
            this.cboComprasBU.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboComprasBU.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cboComprasBU.AutoCompleteSource = AutoCompleteSource.ListItems;

            //PROVEEDORES
            this.cboProveedores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboProveedores.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cboProveedores.AutoCompleteSource = AutoCompleteSource.ListItems;

            Limpia_Controles();

            this.alta = true;

            ////devuelvo el control a el combo proveedores
            //this.cboProveedores.SelectedValueChanged += new System.EventHandler(this.cboProveedores_SelectedValueChanged);
            Cursor.Current = Cursors.Default;

            this.cboEmpresas.Focus();

        }

        private void Carga_Combo_Empresas()
        {

            System.Data.DataSet dsEmpresas = new System.Data.DataSet("Empresas");

            string strConsulta = "";

            strConsulta = "select IdEmpresa, RazonSocial From Empresa Order By RazonSocial ";

            dsEmpresas = Entidades.GetDataSet(strConsulta);

            cboEmpresas.DataSource = dsEmpresas.Tables["Table"];

            this.cboEmpresas.DisplayMember = "RazonSocial";
            this.cboEmpresas.ValueMember = "IdEmpresa";

            this.cboEmpresas.SelectedIndex = -1;

        }

        private void Carga_Combo_Aduanas()
        {

            System.Data.DataSet dsAduanas = new System.Data.DataSet("Aduanas");

            string strConsulta = "";

            strConsulta = "Exec Carga_Aduanas ";

            dsAduanas = Entidades.GetDataSet(strConsulta);

            cboAduanas.DataSource = dsAduanas.Tables["Table"];

            this.cboAduanas.DisplayMember = "Descripcion";
            this.cboAduanas.ValueMember = "Codigo";

        }

        private void Carga_Combo_Proveedores()
        {
            //VERSIÓN DE PRUEBA

            //System.Data.DataSet dsProveedores = new System.Data.DataSet("Proveedores");

            string strConsulta = "";


            //strConsulta = "Exec Carga_Proveedores ";

            //dsProveedores = Entidades.GetDataSet(strConsulta);

            //cboProveedores.DataSource = dsProveedores.Tables["Table"];

            //this.cboProveedores.DisplayMember = "Razon_Social";
            //this.cboProveedores.ValueMember = "Codigo";

            //********************************************************************************
            //CALIPSO MARCO 2018-12-17

            ConexionCalipso c = new ConexionCalipso();
            System.Data.DataTable dtProveedores = new System.Data.DataTable("Proveedores");

            strConsulta = "SELECT v_proveedor.codigo as codigo, v_proveedor.denominacion as Razon_Social, v_persona.cuit " +
                            "FROM v_proveedor INNER JOIN v_persona ON v_persona.id=v_proveedor.enteasociado_id " +
                            "WHERE v_proveedor.activestatus = 0 ORDER BY v_proveedor.denominacion asc; ";

            dtProveedores = c.getTablas_Calipso(strConsulta);//Entidades.GetDataSet(strConsulta);

            cboProveedores.DataSource = dtProveedores;


            this.cboProveedores.DisplayMember = "Razon_Social";
            this.cboProveedores.ValueMember = "Codigo";


            //****************************************************************************************

            //codigo para insertar proveedores de calipso, solo usar una sola vez


            ////Entidades.Ejecuta_Consulta("delete from calipso_proveedores");

            ////foreach (DataRow fila in dtProveedores.Rows)
            ////{
            ////    int codigo = Convert.ToInt32(fila[0]);
            ////    string denominacion = RemoveSpecialCharacters(fila[1].ToString());
            ////    string cuit = fila[2].ToString();

            ////    strConsulta = "INSERT INTO calipso_proveedores (codigo,denominacion,cuit) " +
            ////        "values ("+codigo+",'"+denominacion +"','"+cuit+"')";

            ////    Entidades.Ejecuta_Consulta(strConsulta);

            ////}



            //

        }
        private string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, @"[\[\]']", " ", RegexOptions.Compiled);
        }
        private void Carga_Combo_Tipos_Comprobantes()
        {

            System.Data.DataSet dsTipos_Comprobantes = new System.Data.DataSet("Tipos_Comprobantes");

            string strConsulta = "";

            strConsulta = "Exec sgm_Carga_Tipos_Comprobantes_CompraBU ";

            dsTipos_Comprobantes = Entidades.GetDataSet(strConsulta);

            this.cboTipos_Comprobantes.DataSource = dsTipos_Comprobantes.Tables["Table"];

            this.cboTipos_Comprobantes.DisplayMember = "Tipo_Comprobante";
            this.cboTipos_Comprobantes.ValueMember = "Tipo_Comprobante";
        }

        private void Carga_Combo_Condiciones_IVA()
        {

            System.Data.DataSet dsCondiciones_IVA = new System.Data.DataSet("Condiciones_IVA");

            string strConsulta = "";

            strConsulta = " exec sgm_Carga_Condiciones_IVA ";

            dsCondiciones_IVA = Entidades.GetDataSet(strConsulta);

            this.cboCondiciones_IVAS.DataSource = dsCondiciones_IVA.Tables["Table"];

            this.cboCondiciones_IVAS.DisplayMember = "Descripcion";
            this.cboCondiciones_IVAS.ValueMember = "IdCondicionIVA";

            this.cboCondiciones_IVAS.SelectedIndex = -1;

        }

        private void Carga_Combo_Condiciones_Ganancias()
        {

            System.Data.DataSet dsCondiciones_Ganancias = new System.Data.DataSet("Condiciones_Ganancias");

            string strConsulta = "";

            strConsulta = "exec Carga_Condiciones_Ganancias ";

            dsCondiciones_Ganancias = Entidades.GetDataSet(strConsulta);

            this.cboCondiciones_Ganancias.DataSource = dsCondiciones_Ganancias.Tables["Table"];

            this.cboCondiciones_Ganancias.DisplayMember = "Descripcion";
            this.cboCondiciones_Ganancias.ValueMember = "IdGanancia";

            this.cboCondiciones_Ganancias.SelectedIndex = -1;

        }

        private void Carga_Combo_Letras_Comprobantes()
        {

            System.Data.DataSet dsLetras_Comprobantes = new System.Data.DataSet("Letras_Comprobantes");

            string strConsulta = "";

            strConsulta = "Exec Carga_Letras_Comprobantes ";

            dsLetras_Comprobantes = Entidades.GetDataSet(strConsulta);

            this.cboLetra_Comprobante.DataSource = dsLetras_Comprobantes.Tables["Table"];

            this.cboLetra_Comprobante.DisplayMember = "Letra_Comprobante";
            this.cboLetra_Comprobante.ValueMember = "Letra_Comprobante";

        }


        private void Carga_Combo_Estados_Comprobantes_Despachante()
        {

            System.Data.DataSet dsEstados_Comprobantes_Despachante = new System.Data.DataSet("Estados_Comprobantes_Despachante");

            string strConsulta = "";

            strConsulta = "exec Carga_Estados_Comprobante_Despachante ";

            dsEstados_Comprobantes_Despachante = Entidades.GetDataSet(strConsulta);

            this.cboEstados.DataSource = dsEstados_Comprobantes_Despachante.Tables["Table"];

            this.cboEstados.DisplayMember = "Descripcion";
            this.cboEstados.ValueMember = "Descripcion";

            this.cboEstados.SelectedIndex = 0;

        }

        private void Carga_Combo_Monedas()
        {

            System.Data.DataSet dsMonedas = new System.Data.DataSet("Monedas");

            string strConsulta = "";

            strConsulta = "exec Carga_Monedas ";

            dsMonedas = Entidades.GetDataSet(strConsulta);

            this.cboMonedas.DataSource = dsMonedas.Tables["Table"];

            this.cboMonedas.DisplayMember = "Descripcion";
            this.cboMonedas.ValueMember = "Codigo";

            this.cboMonedas.SelectedIndex = -1;

        }

        private void Limpia_Controles()
        {

            //this.cboEmpresas.SelectedValueChanged -= new System.EventHandler(this.cboEmpresas_SelectedValueChanged);
            //this.cboEmpresas.SelectedIndex = -1;
            //this.cboEmpresas.SelectedValueChanged += new System.EventHandler(this.cboEmpresas_SelectedValueChanged);
            this.cboProveedores.SelectedValueChanged -= new EventHandler(this.cboProveedores_SelectedValueChanged);

            this.cboProveedores.SelectedIndex = -1;

            this.cboProveedores.SelectedValueChanged += new EventHandler(this.cboProveedores_SelectedValueChanged);

            this.cboComprasBU.SelectedValueChanged -= new EventHandler(this.cboComprasBU_SelectedValueChanged);

            this.cboComprasBU.SelectedIndex = -1;

            this.cboComprasBU.SelectedValueChanged += new EventHandler(this.cboComprasBU_SelectedValueChanged);

            this.txtCAI.Text = string.Empty;
            
            this.txtDespacho.Text = string.Empty;
            this.txtDigito_Despacho.Text = string.Empty;

            //this.datFecha_Contable.MinDate = Convert.ToDateTime("01/01/2000");
            //this.datFecha_Comprobante.MinDate = Convert.ToDateTime("01/01/2000");
            //this.datFecha_Vence_Comprobante.MinDate = Convert.ToDateTime("01/01/2000");

            //this.datFecha_Contable.Value = DateTimePicker.MinimumDateTime;
            //this.datFecha_Comprobante.Value = DateTimePicker.MinimumDateTime;
            //this.datFecha_Vence_Comprobante.Value = DateTimePicker.MinimumDateTime;

            this.txtNumero_Carpeta.Text = string.Empty;
            this.cboDestination.SelectedIndex = -1;
            this.cboTipos_Comprobantes.SelectedIndex = -1;
            this.cboAduanas.SelectedIndex = -1;
            this.cboCondiciones_IVAS.SelectedIndex = -1;
            this.cboCondiciones_Ganancias.SelectedIndex = -1;
            
            this.cboEstados.SelectedIndex = 0;//-1;

            this.txtCotizacion.Value = 0;
            this.cboMonedas.SelectedIndex = -1;

            this.cboOperaciones.SelectedIndex = -1;


            this.chkControlador_Fiscal.Checked = false;

            this.txtCentro_Emision.Value = 0;
            this.txtNumero_Comprobante.Value = 0;

            this.txtNeto.Value = 0;
            this.txtIVA_21.Value = 0;
            this.txtIVA_10.Value = 0;
            this.txtGanancias.Value = 0;
            this.txtDerechos.Value = 0;
            this.txtIIBB_Prov.Value = 0;
            this.txtIIBB.Value = 0;
            this.txtOtros.Value = 0;
            this.txtPercepcion_IVA.Value = 0;
            this.txtTotal.Value = 0;

            this.txtDetalle_Compra.Text = string.Empty;
            this.txtCUIT.Text = string.Empty;
            //this.alta = false;

            //if (this.alta) 
            //{
            //    this.cboComprasBU.Enabled = false;
            //}
            //else
            //{
            //Carga_Combo_ComprasBU(Convert.ToInt16(this.cboEmpresas.SelectedValue));
            //    this.cboComprasBU.Enabled = true;
            //}

        }


        private void Carga_Combo_ComprasBU(Int16 intEmpresa)
        {
            Cursor.Current = Cursors.WaitCursor;
            System.Data.DataSet dsCompraBU = new System.Data.DataSet("CompraBU");

            string strConsulta = "";

            strConsulta = "Exec Carga_CompraBU @intEmpresa = " + intEmpresa;

            dsCompraBU = Entidades.GetDataSet(strConsulta);

            cboComprasBU.DataSource = dsCompraBU.Tables["Table"];

            this.cboComprasBU.DisplayMember = "IdCompraBU";
            this.cboComprasBU.ValueMember = "IdCompraBU";
            //MPS 20190219
            //this.cboComprasBU.SelectedIndex = -1;
            Cursor.Current = Cursors.Default ;
        }

        private void cboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void cboProveedores_SelectedValueChanged(object sender,EventArgs e)
        {
            string intProveedor;

            DataRow[] foundRows;

            //BindingSource bs=(BindingSource)

            intProveedor = this.cboProveedores.SelectedValue.ToString(); 

            DataTable dtProveedor = new DataTable();
            dtProveedor = (DataTable)this.cboProveedores.DataSource;
            foundRows = dtProveedor.Select("Codigo=" + intProveedor);
            this.txtCUIT.Text = foundRows[0][2].ToString ();
        }

        private void cboEmpresas_SelectedValueChanged(object sender, EventArgs e)
        {
            this.cboComprasBU.SelectedValueChanged -= new System.EventHandler(this.cboComprasBU_SelectedValueChanged);

            ComboBox Combo_Empresa = (ComboBox)sender;

            //Limpia_Controles();

            Byte intEmpresa = (Byte)this.cboEmpresas.SelectedValue;

            Carga_Combo_ComprasBU(intEmpresa);                

            if (this.alta)
            {
                this.cboComprasBU.SelectedIndex = -1;          
            }

            this.cboComprasBU.SelectedValueChanged += new System.EventHandler(this.cboComprasBU_SelectedValueChanged);
        }

        bool Validar()
        {

            if ( this.cboEmpresas.SelectedIndex == -1 )
            {
                MessageBox.Show("Debe seleccionar una Empresa", "Sr. Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.cboEmpresas.Focus();
                return false; 
            }

            if (this.cboProveedores.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un Proveedor", "Sr. Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.cboProveedores.Focus();
                return false;
            }

            if (!this.Alta)
            { 
                if (this.cboComprasBU.SelectedIndex == -1)
                {
                    //MPS 20190219
                    //MessageBox.Show("Debe seleccionar un Nº de Compra", "Sr. Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //this.cboComprasBU.Focus();
                    //return false;
                }
            }

            if (this.cboTipos_Comprobantes.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un Tipo de Comprobante", "Sr. Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.cboTipos_Comprobantes.Focus();
                return false;
            }

            if (this.txtNumero_Carpeta.Value == 0)
            {
                MessageBox.Show("Debe ingresar un Nº de Carpeta", "Sr. Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtNumero_Carpeta.Focus();
                return false;
            }

            //if (this.txtCAI.Text == string.Empty )
            //{
            //    MessageBox.Show("Debe ingresar un Nº de Autorización", "Sr. Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    this.txtCAI.Focus();
            //    return false;
            //}

            //if (this.txtDespacho.Text == string.Empty)
            //{
            //    MessageBox.Show("Debe ingresar un Nº de Despacho", "Sr. Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    this.txtDespacho.Focus();
            //    return false;
            //}

            //if (this.txtDigito_Despacho.Text == string.Empty)
            //{
            //    MessageBox.Show("Debe ingresar un Nº de Dígito de Despacho", "Sr. Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    this.txtDigito_Despacho.Focus();
            //    return false;
            //}

            if (this.txtDetalle_Compra.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar un Detalle de Compra", "Sr. Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtDetalle_Compra.Focus();
                return false;
            }

            //if (this.cboAduanas.SelectedIndex == -1)
            //{
            //    MessageBox.Show("Debe seleccionar una Aduana", "Sr. Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    this.cboAduanas.Focus();
            //    return false;
            //}

            if (this.cboLetra_Comprobante.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una Letra del Comprobante", "Sr. Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.cboLetra_Comprobante.Focus();
                return false;
            }

            if (this.txtCentro_Emision.Value == 0)
            {
                MessageBox.Show("Debe ingresar un Nº de Centro de Emisión", "Sr. Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtCentro_Emision.Focus();
                return false;
            }

            if (this.txtNumero_Comprobante.Value == 0)
            {
                MessageBox.Show("Debe ingresar un Nº de Comprobante", "Sr. Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtNumero_Comprobante.Focus();
                return false;
            }


            //if (this.cboCondiciones_IVAS.SelectedIndex == -1)
            //{
            //    MessageBox.Show("Debe seleccionar una Condición de IVA", "Sr. Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    this.cboCondiciones_IVAS.Focus();
            //    return false;
            //}

            //if (this.cboCondiciones_Ganancias.SelectedIndex == -1)
            //{
            //    MessageBox.Show("Debe seleccionar una Condición de Ganancias", "Sr. Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    this.cboCondiciones_Ganancias.Focus();
            //    return false;
            //}

            if (this.cboEstados.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un Estado", "Sr. Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.cboEstados.Focus();
                return false;
            }

            if (this.txtCotizacion.Value == 0 )
            {
                MessageBox.Show("Debe ingresar un valor de Cotización para la Moneda", "Sr. Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtCotizacion.Focus();
                return false;
            }

            if (this.cboMonedas.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una Moneda", "Sr. Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.cboMonedas.Focus();
                return false;
            }

            if (this.txtCAI.Text.Trim() != "")
            {

                TimeSpan tsDiferencia = (this.datFecha_CAI.Value - this.datFecha_Comprobante.Value);
                int intDiferencia = tsDiferencia.Days;
                if (intDiferencia <= 0)
                {
                    MessageBox.Show("El Comprobante se encuentra vencido", "Sr. Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.datFecha_Comprobante.Focus();
                    return false;
                }

            }


            return true;

        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;

            if ( Validar() )
            {
                Grabar();

                Byte intEmpresa = (Byte)this.cboEmpresas.SelectedValue;

                //Carga_Combo_ComprasBU(intEmpresa);

                //int intUltima_CompraBU = this.cboComprasBU.Items.Count - 1;

                //this.cboComprasBU.SelectedIndex = intUltima_CompraBU;

                //this.cboComprasBU.Focus();

                Limpia_Controles();

                if (this.alta )
                {
                    this.cboComprasBU.SelectedValueChanged -= new System.EventHandler(this.cboComprasBU_SelectedValueChanged);

                    Carga_Combo_ComprasBU(intEmpresa);

                    this.cboComprasBU.SelectedValueChanged += new System.EventHandler(this.cboComprasBU_SelectedValueChanged);

                    this.alta = false;
                }

                this.cboEmpresas.Focus();

            }

            Cursor.Current = Cursors.Default;

            this.btnNuevo.Enabled = true;
            this.btnEditar.Enabled = true;

        }

        private void Grabar()
        {

            int intResultado;

            DbParameter[] Parametros = new DbParameter[42];

            Parametros[0] = new SqlParameter("@intEmpresa", this.cboEmpresas.SelectedValue);

            if (!this.Alta)
            { 
                Parametros[1] = new SqlParameter("@intIdCompraBU", this.cboComprasBU.SelectedValue);
            }
            else
            {
                Parametros[1] = new SqlParameter("@intIdCompraBU", DBNull.Value);
            }

            Parametros[2] = new SqlParameter("@datFechaCompraBU", this.datFecha_Comprobante.Value);
            Parametros[3] = new SqlParameter("@intProveedor", this.cboProveedores.SelectedValue);
            Parametros[4] = new SqlParameter("@strTipoComprobante",  this.cboTipos_Comprobantes.SelectedValue);
            Parametros[5] = new SqlParameter("@strLetraComprobante", this.cboLetra_Comprobante.SelectedValue);
            Parametros[6] = new SqlParameter("@intCentro_Emision", this.txtCentro_Emision.Value);
            Parametros[7] = new SqlParameter("@strNComprobante", this.txtNumero_Comprobante.Value);
            Parametros[8] = new SqlParameter("@strDetalleCompraBU", this.txtDetalle_Compra.Text);
            Parametros[9] = new SqlParameter("@decImporteNeto", this.txtNeto.Value);
            Parametros[10] = new SqlParameter("@decIVA", this.txtIVA_21.Value);
            Parametros[11] = new SqlParameter("@decIngresosBrutos", this.txtIIBB.Value);
            Parametros[12] = new SqlParameter("@decTotalPesos", this.txtTotal.Value);
            Parametros[13] = new SqlParameter("@strEstado", this.cboEstados.SelectedValue.ToString() );
            Parametros[14] = new SqlParameter("@intNRemito", 10);
            Parametros[15] = new SqlParameter("@decIVA27", this.txtIIBB_Prov.Value); //Importe IIBB Provincia
            Parametros[16] = new SqlParameter("@decIVA105", this.txtIVA_10.Value); //Importe Aduana
            Parametros[17] = new SqlParameter("@decResol3337", 0);
            Parametros[18] = new SqlParameter("@decSaldoBU", this.txtTotal.Value);

            if (this.cboCondiciones_IVAS.SelectedIndex != -1)
            {
                Parametros[19] = new SqlParameter("@intCondicionIVA", this.cboCondiciones_IVAS.SelectedValue);
            }
            else
            {
                Parametros[19] = new SqlParameter("@intCondicionIVA", DBNull.Value);
            }

            Parametros[20] = new SqlParameter("@intCondicionGanancia", this.cboCondiciones_Ganancias.SelectedValue);
            Parametros[21] = new SqlParameter("@datFechaVencimiento", this.datFecha_Vence_Comprobante.Value);
            Parametros[22] = new SqlParameter("@intBienUso", 0);
            Parametros[23] = new SqlParameter("@strNCarpeta", this.txtNumero_Carpeta.Value);
            Parametros[24] = new SqlParameter("@decGanancias", this.txtGanancias.Value);
            Parametros[25] = new SqlParameter("@decDerechosAd", this.txtDerechos.Value);
            Parametros[26] = new SqlParameter("@decIVA9", 0);
            Parametros[27] = new SqlParameter("@decOtros", this.txtOtros.Value);
            Parametros[28] = new SqlParameter("@decCotizacion", this.txtCotizacion.Value);
            Parametros[29] = new SqlParameter("@strCAI", this.txtCAI.Text.Trim() );
            Parametros[30] = new SqlParameter("@datfeCAI", this.datFecha_CAI.Value);
            Parametros[31] = new SqlParameter("@datfeComp", this.datFecha_Comprobante.Value);


            if (this.cboAduanas.SelectedIndex != -1)
            {
                Parametros[32] = new SqlParameter("@strAduana", this.cboAduanas.SelectedValue);
            }
            else
            {
                Parametros[32] = new SqlParameter("@strAduana", "");
            }
            
            Parametros[33] = new SqlParameter("@strDestinacion", this.cboDestination.SelectedValue);
            Parametros[34] = new SqlParameter("@strDespacho", this.txtDespacho.Text);
            Parametros[35] = new SqlParameter("@strVerifica", string.Empty);
            Parametros[36] = new SqlParameter("@strMoneda", this.cboMonedas.SelectedValue.ToString() );
            Parametros[37] = new SqlParameter("@intFiscal", this.chkControlador_Fiscal.Checked);
            Parametros[38] = new SqlParameter("@strcodOpe", string.Empty);
            Parametros[39] = new SqlParameter("@decPercepcion_IVA", this.txtPercepcion_IVA.Value);
            Parametros[40] = new SqlParameter("@strNombreProveedor", this.cboProveedores.Text) ;
            Parametros[41] = new SqlParameter("@strCUIT",this.txtCUIT.Text);

            intResultado = Entidades.EjecutaNonQuery("Alta_CompraBU", Parametros);

        }


        private void cboComprasBU_KeyUp(object sender, KeyEventArgs e)
        {
            
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {

                if (this.cboComprasBU.SelectedIndex != -1)
                {
                    alta = false;
                }
                else
                {
                    Nuevo();
                }

                this.SelectNextControl((Control)sender, true, true, true, true);
                
            }

        }


        private void pMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {
            
            //this.btnNuevo.Enabled = false;

            Limpia_Controles();

            this.alta = true;

            //Carga_Combo_ComprasBU(Convert.ToInt16(this.cboEmpresas.SelectedValue));

            this.cboComprasBU.SelectedIndex = -1;

            //this.cboComprasBU.Enabled = false;

            //this.cboEmpresas.Focus();

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void Calcula_Importe_Total()
        {

            Decimal decImporte_Total = 0;
            this.txtNeto.Value = Convert.ToDecimal(this.txtNeto.Value, CultureInfo.InvariantCulture);

            decImporte_Total = this.txtNeto.Value + this.txtIVA_21.Value + this.txtIVA_10.Value + this.txtGanancias.Value + this.txtDerechos.Value + this.txtIIBB.Value + this.txtIIBB_Prov.Value  + this.txtOtros.Value + this.txtPercepcion_IVA.Value;

            this.txtTotal.Value = decImporte_Total;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

            this.alta = false;
            this.btnNuevo.Enabled = false;
            this.btnEditar.Enabled = false;

            this.cboEmpresas.Focus();

        }

        private void cboTipos_Comprobantes_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void cboComprasBU_SelectedValueChanged(object sender, EventArgs e)
        {

            if (this.cboEmpresas.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una Empresa", "Sr. Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.cboEmpresas.Focus();
                return;
            }

            Byte intEmpresa = (Byte)this.cboEmpresas.SelectedValue;

            Int32 intID_CompraBU = (Int32)this.cboComprasBU.SelectedValue;

            Cursor.Current = Cursors.WaitCursor;

            //Limpia_Controles();

            Carga_Datos_CompraBU(intEmpresa, intID_CompraBU);

            //SÍ SELECCIONÓ ALGO DESDE EL COMBO DE COMPRASBU NO ES UN ALTA, POR LO TANTO, APAGA LA SENIAL
            this.alta = false;

            if (this.cboEstados.SelectedValue.ToString() == "ANULADA")
            { this.btnGrabar.Enabled = false; }
            else
            { this.btnGrabar.Enabled = true; }

            Cursor.Current = Cursors.Default;
        }

        private void Carga_Datos_CompraBU(Int16 intEmpresa, Int64 intID_Compra_BU)
        {

            BindingSource bindingSource = new BindingSource();

            System.Data.DataSet dsCompraBU = new System.Data.DataSet("CompraBU");

            string strConsulta = "";

            strConsulta = "Exec Carga_CompraBU @intEmpresa = " + intEmpresa + ", @intIdCompraBU = " + intID_Compra_BU;

            dsCompraBU = Entidades.GetDataSet(strConsulta);

            bindingSource.DataSource = dsCompraBU.Tables["Table"];

            this.cboProveedores.DataBindings.Clear();
            this.cboComprasBU.DataBindings.Clear();

            this.txtCAI.DataBindings.Clear();
            this.txtDespacho.DataBindings.Clear();
            this.txtDigito_Despacho.DataBindings.Clear();
            this.txtNumero_Carpeta.DataBindings.Clear();
            this.cboDestination.DataBindings.Clear();
            this.cboProveedores.DataBindings.Clear();

            this.cboTipos_Comprobantes.DataBindings.Clear();
            this.cboAduanas.DataBindings.Clear();
            this.cboCondiciones_IVAS.DataBindings.Clear();
            this.cboCondiciones_Ganancias.DataBindings.Clear();
            this.cboEstados.DataBindings.Clear();
            this.txtCotizacion.DataBindings.Clear();
            this.cboMonedas.DataBindings.Clear();
            this.cboOperaciones.DataBindings.Clear();
            this.chkControlador_Fiscal.DataBindings.Clear();

            this.cboLetra_Comprobante.DataBindings.Clear();
            this.txtCentro_Emision.DataBindings.Clear();
            this.txtNumero_Comprobante.DataBindings.Clear();

            this.txtNeto.DataBindings.Clear();
            this.txtIVA_21.DataBindings.Clear();
            this.txtIVA_10.DataBindings.Clear();
            this.txtGanancias.DataBindings.Clear();
            this.txtDerechos.DataBindings.Clear();
            this.txtIIBB_Prov.DataBindings.Clear();
            this.txtIIBB.DataBindings.Clear();
            this.txtOtros.DataBindings.Clear();
            this.txtTotal.DataBindings.Clear();
            this.txtDetalle_Compra.DataBindings.Clear();

            this.datFecha_Contable.DataBindings.Clear();
            this.datFecha_CAI.DataBindings.Clear();
            this.datFecha_Comprobante.DataBindings.Clear();
            this.datFecha_Vence_Comprobante.DataBindings.Clear();

            this.txtPercepcion_IVA.DataBindings.Clear();

            this.txtCUIT.DataBindings.Clear();

            //TEXTBOX
            this.txtDetalle_Compra.DataBindings.Add(new Binding("Text", bindingSource, ((DataTable)bindingSource.DataSource).Columns["DetalleCompraBU"].ColumnName));
            this.txtCAI.DataBindings.Add(new Binding("Text", bindingSource, ((DataTable)bindingSource.DataSource).Columns["CAI"].ColumnName));
            this.txtDespacho.DataBindings.Add(new Binding("Text", bindingSource, ((DataTable)bindingSource.DataSource).Columns["Despacho"].ColumnName));
            //this.txtDigito_Despacho.DataBindings.Add(new Binding("Text", bindingSource, ((DataTable)bindingSource.DataSource).Columns[""].ColumnName));
            this.txtNumero_Carpeta.DataBindings.Add(new Binding("Text", bindingSource, ((DataTable)bindingSource.DataSource).Columns["NCarpeta"].ColumnName));
            this.txtCUIT.DataBindings.Add(new Binding("Text", bindingSource, ((DataTable)bindingSource.DataSource).Columns["CUIT"].ColumnName));

            //COMBOS
            this.cboLetra_Comprobante.DataBindings.Add(new Binding("SelectedValue", bindingSource, ((DataTable)bindingSource.DataSource).Columns["LetraComprobante"].ColumnName));
            this.cboProveedores.DataBindings.Add(new Binding("SelectedValue", bindingSource, ((DataTable)bindingSource.DataSource).Columns["Proveedor"].ColumnName));
            this.cboDestination.DataBindings.Add(new Binding("SelectedValue", bindingSource, ((DataTable)bindingSource.DataSource).Columns["Destinacion"].ColumnName));
            this.cboTipos_Comprobantes.DataBindings.Add(new Binding("SelectedValue", bindingSource, ((DataTable)bindingSource.DataSource).Columns["TipoComprobante"].ColumnName));
            this.cboAduanas.DataBindings.Add(new Binding("SelectedValue", bindingSource, ((DataTable)bindingSource.DataSource).Columns["Aduana"].ColumnName));
            this.cboCondiciones_IVAS.DataBindings.Add(new Binding("SelectedValue", bindingSource, ((DataTable)bindingSource.DataSource).Columns["CondicionIVA"].ColumnName));
            this.cboCondiciones_Ganancias.DataBindings.Add(new Binding("SelectedValue", bindingSource, ((DataTable)bindingSource.DataSource).Columns["CondicionGanancia"].ColumnName));
            this.cboEstados.DataBindings.Add(new Binding("SelectedValue", bindingSource, ((DataTable)bindingSource.DataSource).Columns["Estado"].ColumnName));
            this.cboMonedas.DataBindings.Add(new Binding("SelectedValue", bindingSource, ((DataTable)bindingSource.DataSource).Columns["Moneda"].ColumnName));
            //this.cboOperaciones.DataBindings(new Binding("SelectedValue", bindingSource, ((DataTable)bindingSource.DataSource).Columns[""].ColumnName));
            this.cboOperaciones.DataBindings.Add(new Binding("SelectedValue", bindingSource, ((DataTable)bindingSource.DataSource).Columns["CodOpe"].ColumnName));

            //CAMPOS NUMÉRICOS
            this.txtCentro_Emision.DataBindings.Add(new Binding("Value", bindingSource, ((DataTable)bindingSource.DataSource).Columns["Centro_Emision"].ColumnName));
            this.txtNumero_Comprobante.DataBindings.Add(new Binding("Value", bindingSource, ((DataTable)bindingSource.DataSource).Columns["NComprobante"].ColumnName));//no esta grabando los 0000000
            this.txtNeto.DataBindings.Add(new Binding("Value", bindingSource, ((DataTable)bindingSource.DataSource).Columns["ImporteNeto"].ColumnName));
            this.txtIVA_21.DataBindings.Add(new Binding("Value", bindingSource, ((DataTable)bindingSource.DataSource).Columns["IVA"].ColumnName));
            this.txtIVA_10.DataBindings.Add(new Binding("Value", bindingSource, ((DataTable)bindingSource.DataSource).Columns["IVA105"].ColumnName));
            this.txtGanancias.DataBindings.Add(new Binding("Value", bindingSource, ((DataTable)bindingSource.DataSource).Columns["Ganancias"].ColumnName));
            this.txtDerechos.DataBindings.Add(new Binding("Value", bindingSource, ((DataTable)bindingSource.DataSource).Columns["DerechosAd"].ColumnName));
            this.txtIIBB_Prov.DataBindings.Add(new Binding("Value", bindingSource, ((DataTable)bindingSource.DataSource).Columns["IVA27"].ColumnName));
            this.txtIIBB.DataBindings.Add(new Binding("Value", bindingSource, ((DataTable)bindingSource.DataSource).Columns["IngresosBrutos"].ColumnName));
            this.txtOtros.DataBindings.Add(new Binding("Value", bindingSource, ((DataTable)bindingSource.DataSource).Columns["Otros"].ColumnName));
            this.txtCotizacion.DataBindings.Add(new Binding("Value", bindingSource, ((DataTable)bindingSource.DataSource).Columns["Cotizacion"].ColumnName));
            this.txtTotal.DataBindings.Add(new Binding("Value", bindingSource, ((DataTable)bindingSource.DataSource).Columns["TotalPesos"].ColumnName));
            this.txtPercepcion_IVA.DataBindings.Add(new Binding("Value", bindingSource, ((DataTable)bindingSource.DataSource).Columns["Percepcion_IVA"].ColumnName));

            //Fechas
            this.datFecha_Contable.DataBindings.Add(new Binding("Value", bindingSource, ((DataTable)bindingSource.DataSource).Columns["FechaCompraBU"].ColumnName));
            this.datFecha_Comprobante.DataBindings.Add(new Binding("Value", bindingSource, ((DataTable)bindingSource.DataSource).Columns["FeComp"].ColumnName));
            this.datFecha_Vence_Comprobante.DataBindings.Add(new Binding("Value", bindingSource, ((DataTable)bindingSource.DataSource).Columns["FechaVencimiento"].ColumnName));
            this.datFecha_CAI.DataBindings.Add(new Binding("Value", bindingSource, ((DataTable)bindingSource.DataSource).Columns["FeCAI"].ColumnName));

        }


        private void Proximo_Control_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        bool selectByMouse = false;
        private void SelectText_Enter(object sender, EventArgs e)
        {
            NumericUpDown txt = (NumericUpDown)sender;
            //txt.Select(0, txt.Value.ToString().Length);
            //txt.Select (0, txt.Text.Length);
            txt.Select();
            txt.Select(0, txt.Text.Length);
            if (MouseButtons==MouseButtons.Left)
            {
                selectByMouse = true;
            }
        }
        private void SelectText_MouseDown(object sender,MouseEventArgs e)
        {
            NumericUpDown txt = (NumericUpDown)sender;
            if(selectByMouse)
            {
                txt.Select(0, txt.Text.Length);
                selectByMouse = false;
            }
        }
        private void CalcularTotales_ValueChange(object sender, EventArgs e)
        {


            Calcula_Importe_Total();
        }

        private void cboTipos_Comprobantes_SelectedIndexChanged(object sender, EventArgs e)
        {
             
        }



        private void SonidoTextBox_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void SonidoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }


            //if (e.KeyChar  == 13)
            //{
            //    this.txtNeto.Value = Convert.ToDecimal(this.txtNeto.Value, CultureInfo.InvariantCulture);
            //    e.Handled = true;
            //}
        }

        private void DatFecha_Contable_ValueChanged(object sender, EventArgs e)
        {

            this.datFecha_Comprobante.Value = this.datFecha_Contable.Value;
            this.datFecha_Vence_Comprobante.Value  = this.datFecha_Contable.Value;

        }

        private void TxtNeto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

            }
        }

        private void TxtIVA_21_KeyDown(object sender, KeyEventArgs e)
        {

            //Ajuste para quitar el sonido
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

            }

        }

        private void TxtIVA_10_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

            }

        }

        private void TxtGanancias_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

            }

        }

        private void TxtDerechos_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

            }

        }

        private void TxtIIBB_Prov_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

            }

        }

        private void TxtIIBB_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

            }

        }

        private void TxtOtros_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

            }

        }

        private void TxtPercepcion_IVA_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

            }

        }

        private void TxtTotal_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

            }

        }

        private void TxtCotizacion_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }

        }

        private void TxtCotizacion_ValueChanged(object sender, EventArgs e)
        {
            this.txtCotizacion.Value = Convert.ToDecimal(this.txtCotizacion.Value, CultureInfo.InvariantCulture);
        }

        private void CalculoNeto_IVA20(object sender, EventArgs e)
        {
            decimal dIva21=0;
            decimal dIva20 =0;
            decimal dNeto =0;
            string s;
            dIva21 = decimal.Parse(this.txtIVA_21.Value.ToString());
            if (dIva21 != 0)
            {
                dNeto = dIva21 / (decimal)0.21;
                if(MessageBox.Show("¿Calcula IVA 20%?","Consulta",MessageBoxButtons.YesNo )==DialogResult.Yes)
                {
                    dIva20 = dNeto * (decimal)0.20;
                }
                this.txtNeto.Value = dNeto;
                this.txtIVA_10.Value = dIva20;

            }
        }
    }
}
