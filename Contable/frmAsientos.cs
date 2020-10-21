using System;
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
using System.Text.RegularExpressions;

namespace Contable
{

    public partial class frmAsientos : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wpara, int lparam);

        private static string _strCuenta_Contable_Ayuda;
        public string Cuenta_Contable_Ayuda
        {
            get // this makes you to access value in form2
            {
                return _strCuenta_Contable_Ayuda;
            }
            set // this makes you to change value in form2
            {
                _strCuenta_Contable_Ayuda = value;
            }
        }

        private void MoverForm_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private DataGridViewCell _celWasEndEdit;
        public Int16 Empresa { get; set; }

        System.Data.DataSet _dsCuentas = new System.Data.DataSet("Cuentas_Contables");
        BindingSource _bindingSource = new BindingSource();
        System.Data.DataSet _dsAsientos = new System.Data.DataSet("Asientos");

        private int _intAnio_Anterior;
        private int _intMes_Anterior;
        //int _intDía_Anterior;

        public frmAsientos()
        {
            InitializeComponent();
            funciones_varias fv = new funciones_varias();
            fv.PrepararForm(this);
        }

        private void Configura_Grilla()
        {

            DataGridViewColumn colItem = new DataGridViewTextBoxColumn();

            //https://stackoverflow.com/questions/45498268/dataset-and-combobox-of-datagridview
            //var colCuenta = new DataGridViewComboBoxColumn();

            //colCuenta.DataSource = _dsCuentas.Tables["Table"];
            //colCuenta.ValueMember = "IdCuenta";
            //colCuenta.DisplayMember = "IdCuenta";

            //colCuenta.DefaultCellStyle.BackColor = Color.White;
            //colCuenta.DefaultCellStyle.SelectionBackColor = Color.Blue;
            //colCuenta.FlatStyle = FlatStyle.Flat;

            DataGridViewColumn colCuenta = new DataGridViewTextBoxColumn();

            DataGridViewColumn colNombre = new DataGridViewTextBoxColumn();
            DataGridViewColumn colLeyenda = new DataGridViewTextBoxColumn();
            DataGridViewColumn colDebe = new DataGridViewTextBoxColumn();
            DataGridViewColumn colHaber = new DataGridViewTextBoxColumn();
            //DataGridViewColumn colEstado = new DataGridViewTextBoxColumn();

            grdAsientos.AutoGenerateColumns = false;
            //grdAsientos.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);

            grdAsientos.Columns.Add(colItem);
            grdAsientos.Columns.Add(colCuenta);
            grdAsientos.Columns.Add(colNombre);
            grdAsientos.Columns.Add(colLeyenda);
            grdAsientos.Columns.Add(colDebe);
            grdAsientos.Columns.Add(colHaber);
            //grdAsientos.Columns.Add(colEstado);

            //grdAsientos.ColumnCount = 5;
            grdAsientos.Columns[0].Name = "Item";
            grdAsientos.Columns[1].Name = "Cuenta";
            grdAsientos.Columns[2].Name = "Nombre";
            grdAsientos.Columns[3].Name = "Leyenda";
            grdAsientos.Columns[4].Name = "Debe";
            grdAsientos.Columns[5].Name = "Haber";
            //grdAsientos.Columns[6].Name = "Estado";

            grdAsientos.Columns[4].DefaultCellStyle.Format = "N2";
            grdAsientos.Columns[5].DefaultCellStyle.Format = "N2";

            grdAsientos.Columns[0].HeaderText = "Ítem";
            grdAsientos.Columns[1].HeaderText = "Cuenta";
            grdAsientos.Columns[2].HeaderText = "Nombre";
            grdAsientos.Columns[3].HeaderText = "Leyenda";
            grdAsientos.Columns[4].HeaderText = "Debe";
            grdAsientos.Columns[5].HeaderText = "Haber";
            //grdAsientos.Columns[6].HeaderText = "Estado";

            grdAsientos.Columns["Item"].DataPropertyName = "ItemAsiento";
            grdAsientos.Columns["Cuenta"].DataPropertyName = "Cuenta";
            grdAsientos.Columns["Nombre"].DataPropertyName = "Nombre";
            grdAsientos.Columns["Leyenda"].DataPropertyName = "LeyendaItem";
            grdAsientos.Columns["Debe"].DataPropertyName = "Debe";
            grdAsientos.Columns["Haber"].DataPropertyName = "Haber";
            //grdAsientos.Columns["Estado"].DataPropertyName = "Estado";

            //OCULTA COLUMNAS
            grdAsientos.Columns["Item"].Visible = false;

            grdAsientos.Columns["Item"].ReadOnly = true;
            grdAsientos.Columns["Item"].Width = 10;

            /*
            DataGridViewCheckBoxColumn chkAgrupa_Tarjeta = new DataGridViewCheckBoxColumn();
            chkAgrupa_Tarjeta.Name = "Agrupa_Tarjeta";
            chkAgrupa_Tarjeta.HeaderText = "Agrupa Tarjeta";
            grdParametros.Columns.Add(chkAgrupa_Tarjeta);
            */

            this.grdAsientos.Columns["Debe"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.grdAsientos.Columns["Haber"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            grdAsientos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //SELECTION MODE
            //grdParametros.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdAsientos.MultiSelect = false;

        }

        private void Carga_Combo_Asientos()
        {

            System.Data.DataSet dsEmpresas = new System.Data.DataSet("Asientos");

            string strConsulta = string.Empty;

            Byte intEmpresa = (Byte)this.cboEmpresas.SelectedValue;

            //strConsulta = "exec Carga_Asiento @intEmpresa = " + this.Empresa + ", @intAnio = " + this.txtAnio.Value.ToString();
            strConsulta = "exec Carga_Asiento @intEmpresa = " + intEmpresa + ", @intAnio = " + this.txtAnio.Value.ToString();

            dsEmpresas = Entidades.GetDataSet(strConsulta);

            _dsAsientos = dsEmpresas;
            _bindingSource.DataSource = null;
            _bindingSource.DataSource = dsEmpresas.Tables["Table"];

            this.cboAsientos.DataSource = _dsAsientos.Tables["Table"];

            this.cboAsientos.DisplayMember = "IdAsientto";
            this.cboAsientos.ValueMember = "IdAsiento";

            this.cboAsientos.Text = string.Empty;
            this.cboAsientos.SelectedIndex = -1;

        }

        private void Carga_Combo_Comprobantes()
        {

            System.Data.DataSet dsComprobantes = new System.Data.DataSet("Comprobantes");

            string strConsulta = string.Empty;

            Byte intEmpresa = (Byte)this.cboEmpresas.SelectedValue;

            //strConsulta = "exec Carga_Asiento @intEmpresa = " + this.Empresa + ", @intAnio = " + this.txtAnio.Value.ToString();
            strConsulta = "exec Carga_Asiento @intEmpresa = " + intEmpresa + ", @intAnio = " + this.txtAnio.Value.ToString();

            //if (this.cboAsientos.SelectedIndex != -1)
            //{
            //    strConsulta += ", @intID_Asiento = " + this.cboAsientos.SelectedValue;
            //}

            dsComprobantes = Entidades.GetDataSet(strConsulta);

            this.cboComprobantes.DataSource = dsComprobantes.Tables["Table"];

            this.cboComprobantes.DisplayMember = "Numero_Comprobante";
            this.cboComprobantes.ValueMember = "IdAsiento";

            this.cboComprobantes.Text = string.Empty;
            this.cboComprobantes.SelectedIndex = -1;

        }

        private void Carga_Grilla()
        {

            System.Data.DataSet dsEmpresas = new System.Data.DataSet("Asientos");

            string strConsulta = string.Empty;

            Byte intEmpresa = (Byte)this.cboEmpresas.SelectedValue;
            Int32 intAsiento = (Int32)this.cboAsientos.SelectedValue;

            //strConsulta = " exec Carga_Asiento @intEmpresa = " + this.Empresa + ", @intAnio = " + this.txtAnio.Value.ToString();
            strConsulta = " exec Carga_Item_Asiento @intEmpresa = " + intEmpresa + ", @intAnio = " + this.txtAnio.Value.ToString() + ", @intAsiento = " + intAsiento;

            dsEmpresas = Entidades.GetDataSet(strConsulta);

            this.grdAsientos.AutoGenerateColumns = false;
            this.grdAsientos.DataSource = dsEmpresas;
            this.grdAsientos.DataMember = "Table";

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

        private void Carga_Combo_Estados_Asientos()
        {

            System.Data.DataSet dsEstados = new System.Data.DataSet("Estados");

            string strConsulta = "";

            strConsulta = "exec Carga_Estados_Asientos ";

            dsEstados = Entidades.GetDataSet(strConsulta);

            this.cboEstados.DataSource = dsEstados.Tables["Table"];

            this.cboEstados.DisplayMember = "Descripcion";
            this.cboEstados.ValueMember = "Descripcion";

            this.cboEstados.SelectedIndex = 0;

        }

        private void frmAsientos_Load(object sender, EventArgs e)
        {

            Inicia();
            //this.txtAnio.Focus();
            this.cboEmpresas.Focus();

        }

        private void Inicia()
        {

            this.datFecha_Inicio_Ejercicio.ResetText();
            this.datFecha_Cierre_Ejercicio.ResetText();

            this.datFecha_Inicio_Ejercicio_Anterior.ResetText();
            this.datFecha_Cierre_Ejercicio_Anterior.ResetText();

            this.cboEmpresas.SelectedValueChanged -= new System.EventHandler(this.cboEmpresas_SelectedValueChanged);

            Carga_Combo_Empresas();

            this.cboEmpresas.SelectedIndex = -1;

            this.cboEmpresas.SelectedValueChanged += new System.EventHandler(this.cboEmpresas_SelectedValueChanged);

            //SETEO AL COMBO PARA QUE PUEDA BUSCAR MIENTRAS SE VA TIPEANDO EL DATO
            //EMPRESAS
            this.cboEmpresas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboEmpresas.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cboEmpresas.AutoCompleteSource = AutoCompleteSource.ListItems;

            this.txtAnio.Enabled = false;

            this.cboAsientos.SelectedValueChanged -= new System.EventHandler(this.cboAsientos_SelectedValueChanged);

            //this.cboAsientos.Enabled = false;
            this.cboAsientos.SelectedIndex = -1;

            this.cboAsientos.SelectedValueChanged += new System.EventHandler(this.cboAsientos_SelectedValueChanged);

            //this.cboComprobantes.Enabled = false;
            this.cboComprobantes.SelectedIndex = -1;

            this.txtLeyenda_Asiento.Text = string.Empty;

            this.txtAnio.Value = (Int16)DateTime.Today.Year;

            this.optActual.Checked = true;

            //CARGA UN DATASET CON EL PLAN DE CUENTAS ENLAZADO A UNA COLUMNA DE LA GRILLA
            Carga_DataSet_Plan_Cuentas();

            Configura_Grilla();

            this.txtTotal_Debe.Text = "0";
            this.txtTotal_Haber.Text = "0";

            this.grdAsientos.Rows.Clear();
            this.grdAsientos.DataSource = null;

            //SETEO AL COMBO PARA QUE PUEDA BUSCAR MIENTRAS SE VA TIPEANDO EL DATO
            this.cboAsientos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboAsientos.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cboAsientos.AutoCompleteSource = AutoCompleteSource.ListItems;

            this.cboComprobantes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboComprobantes.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cboComprobantes.AutoCompleteSource = AutoCompleteSource.ListItems;

            //SETEA AL "." COMO SEPARADOR DECIMAL
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            _intAnio_Anterior = this.datFecha.Value.Year;
            _intMes_Anterior = this.datFecha.Value.Month;

            this.Cuenta_Contable_Ayuda = string.Empty;

            Carga_Combo_Estados_Asientos();

            this.Refresh();

            this.cboEmpresas.Focus();

            this.datFecha.Validating += new System.ComponentModel.CancelEventHandler(this.datFecha_SomethingChanged);

        }

        private void Busca_Fecha_Inicio_Ejercicio()
        {



        }

        private void Limpia_Controles()
        {

            //this.datFecha_Inicio_Ejercicio.ResetText();
            //this.datFecha_Cierre_Ejercicio.ResetText();

            //this.datFecha_Inicio_Ejercicio_Anterior.ResetText();
            //this.datFecha_Cierre_Ejercicio_Anterior.ResetText();

            //this.cboEmpresas.SelectedValueChanged -= new System.EventHandler(this.cboEmpresas_SelectedValueChanged);
            //this.cboEmpresas.SelectedIndex = -1;
            //this.cboEmpresas.SelectedValueChanged += new System.EventHandler(this.cboEmpresas_SelectedValueChanged);

            this.cboAsientos.SelectedValueChanged -= new System.EventHandler(this.cboAsientos_SelectedValueChanged);
            this.cboAsientos.SelectedIndex = -1;
            this.cboAsientos.SelectedValueChanged += new System.EventHandler(this.cboAsientos_SelectedValueChanged);

            this.cboComprobantes.SelectedIndex = -1;
            this.cboComprobantes.Text = string.Empty;

            this.optActual.Checked = true;
            this.txtLeyenda_Asiento.Text = string.Empty;

            this.txtAnio.Value = (Int16)DateTime.Today.Year;

            //CARGA UN DATASET CON EL PLAN DE CUENTAS ENLAZADO A UNA COLUMNA DE LA GRILLA
            Carga_DataSet_Plan_Cuentas();

            this.txtTotal_Debe.Text = "0";
            this.txtTotal_Haber.Text = "0";

            if (this.grdAsientos.Rows.Count > 1)
            {
                this.grdAsientos.Rows.Clear();
            }

            this.grdAsientos.DataSource = null;

            this.cboEstados.SelectedIndex = 0;

            //this.cboEmpresas.Focus();
            this.datFecha.ResetText();
            this.datFecha.Focus();

            this.Refresh();

        }

        private void txtAnio_Leave(object sender, EventArgs e)
        {

            if (this.cboEmpresas.SelectedIndex != -1)
            {

                this.cboAsientos.SelectedValueChanged -= new System.EventHandler(this.cboAsientos_SelectedValueChanged);

                Carga_Combo_Asientos();

                this.cboAsientos.Enabled = true;
                //this.cboComprobantes.Enabled = true;

                this.cboAsientos.SelectedIndex = -1;
                //this.cboComprobantes.SelectedIndex = -1;

                this.cboAsientos.SelectedValueChanged += new System.EventHandler(this.cboAsientos_SelectedValueChanged);

            }
            else
            {
                this.cboEmpresas.Focus();
            }

        }

        private void txtAnio_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Return )
            //{
            //    //this.Empresa = 1;

            //    if (this.cboEmpresas.SelectedIndex != -1)
            //    {

            //        this.cboAsientos.SelectedValueChanged -= new System.EventHandler(this.cboAsientos_SelectedValueChanged);

            //        Carga_Combo_Asientos();
            //        Carga_Combo_Comprobantes();

            //        this.cboAsientos.Enabled = true;
            //        this.cboComprobantes.Enabled = true;

            //        this.cboAsientos.SelectedIndex = -1;
            //        this.cboComprobantes.SelectedIndex = -1;

            //        this.cboAsientos.SelectedValueChanged += new System.EventHandler(this.cboAsientos_SelectedValueChanged);

            //    }
            //    else
            //    {
            //        this.cboEmpresas.Focus();
            //    }

            //}

        }

        private void cboAsientos_SelectedValueChanged(object sender, EventArgs e)
        {

            if (this.cboAsientos.SelectedValue == null) { return; };

            this.cboComprobantes.SelectedValue = this.cboAsientos.SelectedValue;

            DataRow[] returnedRows;
            returnedRows = _dsAsientos.Tables["Table"].Select("IdAsiento = " + this.cboAsientos.SelectedValue);
            DataRow dr1;
            dr1 = returnedRows[0];

            this.txtLeyenda_Asiento.Text = dr1["Leyenda_Asiento"].ToString();

            this.cboEstados.SelectedValue = dr1["Estado"].ToString();

            this.datFecha.Value = DateTime.Parse(dr1["Fecha_Asiento"].ToString());
            this.txtFecha.Text = DateTime.Parse(dr1["Fecha_Asiento"].ToString()).Date.ToString("dd/MM/yyyy");

            //if (this.txtLeyenda_Asiento.DataBindings.Count == 0)
            //{ 
            //    this.txtLeyenda_Asiento.DataBindings.Add(new Binding("Text", _bindingSource, ((DataTable)_bindingSource.DataSource).Columns["Leyenda_Asiento"].ColumnName));

            //}

            //Carga_Datos_Asiento();
            Carga_Grilla();
            Calcula_Totales();

        }

        private void Calcula_Totales()
        {

            Decimal decTotal_Debe = 0;
            Decimal decTotal_Haber = 0;

            this.txtTotal_Debe.Text = "0";
            this.txtTotal_Haber.Text = "0";

            foreach (DataGridViewRow row in grdAsientos.Rows)
            {
                if (row.Cells["Debe"].Value != null || !string.IsNullOrEmpty(row.Cells["Debe"].Value as string))
                {
                    //decTotal_Debe += (Decimal)row.Cells["Debe"].Value;
                    decTotal_Debe += Convert.ToDecimal(row.Cells["Debe"].Value);
                }

                if (row.Cells["Haber"].Value != null || !string.IsNullOrEmpty(row.Cells["Haber"].Value as string))
                {
                    //decTotal_Haber += (Decimal)row.Cells["Haber"].Value;
                    decTotal_Haber += Convert.ToDecimal(row.Cells["Haber"].Value);
                }
            }

            this.txtTotal_Debe.Text = decTotal_Debe.ToString();
            this.txtTotal_Haber.Text = decTotal_Haber.ToString();
        }

        private void Carga_Datos_Asiento()
        {

        }

        private void cboEmpresas_KeyUp(object sender, KeyEventArgs e)
        {

            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }

        }

        private void txtAnio_KeyUp(object sender, KeyEventArgs e)
        {

            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }

        }

        private void cboAsientos_KeyUp(object sender, KeyEventArgs e)
        {

            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }

        }

        private void Carga_DataSet_Plan_Cuentas()
        {

            string strConsulta = string.Empty;

            strConsulta = "exec Carga_Plan_Cuenta ";

            _dsCuentas = Entidades.GetDataSet(strConsulta);

        }

        private void Graba_Cabecera()
        {

            Byte intEmpresa = (Byte)this.cboEmpresas.SelectedValue;
            Int16 intAnio = (Int16)this.txtAnio.Value;
            Int32 intAsiento = 0;
            string strNumero_Comprobante = "";

            DateTime datFecha_Asiento = (DateTime)this.datFecha.Value;
            String strLeyenda_Asiento = this.txtLeyenda_Asiento.Text.Trim();

            String strEstado = this.cboEstados.SelectedValue.ToString();

            if (this.cboAsientos.SelectedIndex != -1)
            {
                intAsiento = (Int32)this.cboAsientos.SelectedValue;
            }

            if (this.cboComprobantes.SelectedIndex != -1)
            {
                strNumero_Comprobante = this.cboComprobantes.SelectedValue.ToString();
            }
            else
            {
                strNumero_Comprobante = this.cboComprobantes.Text.Trim();
            }

            DbParameter[] Parametros = new DbParameter[8];

            Parametros[0] = new SqlParameter("@intEmpresa", intEmpresa);
            Parametros[1] = new SqlParameter("@intAnio", intAnio);

            if (this.cboAsientos.SelectedIndex != -1)
            {
                Parametros[2] = new SqlParameter("@intAsiento", intAsiento);
            }
            else
            {
                Parametros[2] = new SqlParameter("@intAsiento", DBNull.Value);
            }

            Parametros[3] = new SqlParameter("@datFecha_Asiento", datFecha_Asiento);
            Parametros[4] = new SqlParameter("@strLeyenda_Asiento", strLeyenda_Asiento);
            Parametros[5] = new SqlParameter("@intNumero_Interno_Asiento", DBNull.Value);
            Parametros[6] = new SqlParameter("@strEstado", strEstado);
            Parametros[7] = new SqlParameter("@strNumero_Comprobante", strNumero_Comprobante);

            int intRta = Entidades.EjecutaNonQuery("Alta_Asiento", Parametros);

        }

        private void Graba_Grilla()
        {

            string strCuenta = "";
            string strLeyenda_Item = "";
            Decimal decDebe = 0;
            Decimal decHaber = 0;

            int intRta = 0;
            //Int16 intItem = 0;

            string strSql = "";

            Byte intEmpresa = (Byte)this.cboEmpresas.SelectedValue;
            Int16 intAnio = (Int16)this.txtAnio.Value;

            Int32 intAsiento = 0;

            if (this.cboAsientos.SelectedIndex != -1)
            {
                intAsiento = (Int32)this.cboAsientos.SelectedValue;
            }

            string strNumero_Comprobante = ""; //this.cboComprobantes.SelectedValue.ToString();

            if (this.cboComprobantes.SelectedIndex != -1)
            {
                strNumero_Comprobante = this.cboComprobantes.SelectedValue.ToString();
            }
            else
            {
                strNumero_Comprobante = this.cboComprobantes.Text.Trim();
            }

            //Sí se seleccionó algún Asiento lo elimina para volver a generar los ítems.
            if (this.cboAsientos.SelectedIndex != -1)
            {

                DbParameter[] Parametros_Eliminar = new DbParameter[3];

                Parametros_Eliminar[0] = new SqlParameter("@intEmpresa", intEmpresa);
                Parametros_Eliminar[1] = new SqlParameter("@intAnio", intAnio);
                Parametros_Eliminar[2] = new SqlParameter("@intAsiento", intAsiento);

                intRta = Entidades.EjecutaNonQuery("Elimina_ItemAsiento", Parametros_Eliminar);

                if (intRta == -1) { return; }

            }

            foreach (DataGridViewRow row in grdAsientos.Rows)
            {
                //intItem++;

                if (row.Cells["Cuenta"].Value != null)
                {

                    strCuenta = (string)row.Cells["Cuenta"].Value;

                    strLeyenda_Item = "";
                    if (row.Cells["Leyenda"].Value != null)
                    {
                        strLeyenda_Item = (string)row.Cells["Leyenda"].Value.ToString();
                    }

                    decDebe = Convert.ToDecimal(row.Cells["Debe"].Value);
                    decHaber = Convert.ToDecimal(row.Cells["Haber"].Value);

                    strSql = "";

                    if (this.cboAsientos.SelectedIndex != -1)
                    {
                        strSql = "exec Alta_ItemAsiento @intEmpresa = " + intEmpresa + ", @intAnio = " + intAnio + ", @intAsiento = " + intAsiento + ", @strCuenta = '" + strCuenta + "', @strLeyenda_Item = '" + strLeyenda_Item + "', @decDebe = " + decDebe + ", @decHaber = " + decHaber + ", @strNumero_Comprobante = '" + strNumero_Comprobante + "' ";
                    }
                    else
                    {
                        strSql = "exec Alta_ItemAsiento @intEmpresa = " + intEmpresa + ", @intAnio = " + intAnio + ", @intAsiento = Null, @strCuenta = '" + strCuenta + "', @strLeyenda_Item = '" + strLeyenda_Item + "', @decDebe = " + decDebe + ", @decHaber = " + decHaber + ", @strNumero_Comprobante = '" + strNumero_Comprobante + "' ";
                    }

                    intRta = Entidades.Ejecuta_Consulta(strSql);

                }

            }

            if (intRta != -1)
            {
                MessageBox.Show("Proceso finalizado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpia_Controles();
            }

        }
        private void grdAsientos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            _celWasEndEdit = grdAsientos[e.ColumnIndex, e.RowIndex];

            //this.grdAsientos.CurrentRow.Cells[e.ColumnIndex].Value = Convert.ToDecimal(this.grdAsientos.CurrentRow.Cells[e.ColumnIndex].Value);



            Calcula_Totales();

        }

        private Boolean Valida()
        {

            Decimal decTotal_Debe = 0;
            Decimal decTotal_Haber = 0;

            Decimal.TryParse(this.txtTotal_Debe.Text.ToString(), out decTotal_Debe);
            Decimal.TryParse(this.txtTotal_Haber.Text.ToString(), out decTotal_Haber);

            MessageBoxButtons button = MessageBoxButtons.OK;
            MessageBoxIcon icon = MessageBoxIcon.Error;

            if (decTotal_Debe != decTotal_Haber)
            {
                MessageBox.Show("Los asientos no balancean", "Error", button, icon);
                return false;
            }

            if (this.cboEmpresas.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una Empresa", "Error", button, icon);
                this.cboEmpresas.Focus();
                return false;
            }

            if (this.grdAsientos.Rows.Count < 1)
            {
                MessageBox.Show("La grilla de ítems no puede estár vacia", "Error", button, icon);
                return false;
            }

            //EJERCICIO ACTUAL
            if (this.optActual.Checked)
            {
                if (datFecha.Value < datFecha_Inicio_Ejercicio.Value || datFecha.Value > datFecha_Cierre_Ejercicio.Value)
                {
                    MessageBox.Show("La Fecha del Asiento se encuentra fuera de las fechas de Inicio y Cierre del Ejercicio Actual ", "Error", button, icon);
                    return false;
                }
            }
            else
            {
                //EJERCICIIO ANTERIOR
                if (datFecha.Value < datFecha_Inicio_Ejercicio_Anterior.Value || datFecha.Value > datFecha_Cierre_Ejercicio_Anterior.Value)
                {
                    MessageBox.Show("La Fecha del Asiento se encuentra fuera de las fechas de Inicio y Cierre del Ejercicio Anterior ", "Error", button, icon);
                    return false;
                }

            }

            return true;
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {

            if (Valida())
            {

                Graba_Cabecera();

                //SI ES UN ALTA POSICIONA CARGA AL COMBO NUEVAMENTE Y LO POSICIONA EN EL ÚLTIMO ÍTEM
                if (this.cboAsientos.SelectedIndex == -1)

                {
                    this.cboAsientos.SelectedValueChanged -= new System.EventHandler(this.cboAsientos_SelectedValueChanged);

                    Carga_Combo_Asientos();

                    //this.cboAsientos.SelectedIndex = this.cboAsientos.Items.Count - 1;
                    this.cboAsientos.SelectedIndex = 0;

                    this.cboAsientos.SelectedValueChanged += new System.EventHandler(this.cboAsientos_SelectedValueChanged);
                }

                Graba_Grilla();
            }

        }

        private void grdAsientos_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["Debe"].Value = 0;
            e.Row.Cells["Haber"].Value = 0;
        }

        private void cboEmpresas_Leave(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboComprobantes_KeyUp(object sender, KeyEventArgs e)
        {

            //string strMes = datFecha.Value.Month.ToString("00");
            //string strNumero_Comprobante = "";

            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {

                this.cboAsientos.SelectedValueChanged -= new System.EventHandler(this.cboAsientos_SelectedValueChanged);

                if (this.cboComprobantes.SelectedIndex == -1)
                {

                    //strNumero_Comprobante = strMes + this.cboComprobantes.Text.ToString();
                    //this.cboComprobantes.Text = strNumero_Comprobante;

                    this.cboAsientos.SelectedIndex = -1;
                    this.txtLeyenda_Asiento.Text = string.Empty;
                    //this.optActual.Checked = false;
                    //this.optAnterior.Checked = false;
                    this.txtTotal_Debe.Text = string.Empty;
                    this.txtTotal_Haber.Text = string.Empty;

                    //Limpia la grilla
                    this.grdAsientos.DataSource = null;

                }
                else
                {
                    this.cboAsientos.SelectedValue = this.cboComprobantes.SelectedValue;
                    //PROVOCO EVENTO PARA REFRESCAR PANTALLA
                    cboAsientos_SelectedValueChanged(cboAsientos, new EventArgs());
                    Carga_Grilla();
                }

                this.cboAsientos.SelectedValueChanged += new System.EventHandler(this.cboAsientos_SelectedValueChanged);

                this.SelectNextControl((Control)sender, true, true, true, true);

            }

        }

        private void datFecha_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {

                //int intAnio = this.datFecha.Value.Year;

                //this.txtAnio.Value = intAnio;

                this.SelectNextControl((Control)sender, true, true, true, true);
            }

        }

        private void grdAsientos_KeyDown(object sender, KeyEventArgs e)
        {

            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                e.SuppressKeyPress = true;

                int iColumn = grdAsientos.CurrentCell.ColumnIndex;
                int iRow = grdAsientos.CurrentCell.RowIndex;

                if (iColumn == grdAsientos.ColumnCount - 1)
                {
                    if (grdAsientos.RowCount > (iRow + 1))
                    {
                        //grdAsientos.Rows.Add();
                        grdAsientos.CurrentCell = grdAsientos[1, iRow + 1];
                    }
                    else
                    {
                        //focus next control
                    }
                }
                else
                {
                    grdAsientos.CurrentCell = grdAsientos[iColumn + 1, iRow];
                }

            }
            else
            {

                if ((e.KeyCode == Keys.Decimal) && ((sender as TextBox).Text.IndexOf('.') > -1))
                {
                    e.Handled = true;
                }
                else
                {

                    if (e.KeyCode == Keys.F2 && grdAsientos.CurrentCell.ColumnIndex == 1) //Ayuda Cuentas
                    {

                        this.Cuenta_Contable_Ayuda = string.Empty;

                        Form fAyuda_Cuentas = new frmAyuda_Cuentas_Contables();
                        fAyuda_Cuentas.ShowDialog(this);

                        if (this.Cuenta_Contable_Ayuda != string.Empty)
                        {
                            grdAsientos.Rows[grdAsientos.CurrentRow.Index].Cells["Cuenta"].Value = this.Cuenta_Contable_Ayuda.ToString();
                        }

                    }

                }

            }

            //if (e.KeyData == Keys.Enter)
            //{
            //    int col = grdAsientos.CurrentCell.ColumnIndex;
            //    int row = grdAsientos.CurrentCell.RowIndex;

            //    if (col < grdAsientos.ColumnCount - 1)
            //    {
            //        col++;
            //    }
            //    else
            //    {
            //        col = 1;
            //        row++;
            //    }

            //    //if (row == grdAsientos.RowCount)
            //    //    grdAsientos.Rows.Add();

            //    grdAsientos.CurrentCell = grdAsientos[col, row];
            //    e.Handled = true;
            //}

        }

        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{

        //    if (!grdAsientos.Focused && !grdAsientos.IsCurrentCellInEditMode)
        //    {
        //        return base.ProcessCmdKey(ref msg, keyData);
        //    }

        //    if (keyData != Keys.Return )
        //    {
        //        return base.ProcessCmdKey(ref msg, keyData);
        //    }

        //    DataGridViewCell cell = grdAsientos.CurrentCell;

        //    int intColumna = cell.ColumnIndex;
        //    int intLinea = cell.RowIndex;

        //    if (intColumna == grdAsientos.Columns.Count -1)
        //    {
        //        if (intLinea == grdAsientos.Rows.Count -2)
        //        {
        //            cell = grdAsientos.Rows[intLinea++].Cells[1];
        //        }
        //        else
        //        {
        //            cell = grdAsientos.Rows[intLinea + 1].Cells[1];
        //        }
        //    }
        //    else
        //    {
        //        cell = grdAsientos.Rows[intLinea].Cells[intColumna + 1];
        //    }


        //    //DataGridViewCell cell = new DataGridViewTextBoxCell();



        //    //return base.ProcessCmdKey(ref msg, keyData);
        //    return true;

        //}

        public class MyDataGridView : DataGridView
        {
            protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
            {
                bool enterKeyDetected = keyData == Keys.Enter;
                bool isLastRow = this.CurrentCell.RowIndex == this.RowCount - 1;

                if (enterKeyDetected && isLastRow)
                {
                    Console.WriteLine("Enter detected on {0},{1}", this.CurrentCell.RowIndex, this.CurrentCell.ColumnIndex);

                    this.CurrentCell = this[0, this.CurrentCell.RowIndex + 1];
                }

                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void datFecha_ValueChanged(object sender, EventArgs e)
        {

            this.datFecha.Format = DateTimePickerFormat.Custom;
            this.datFecha.CustomFormat = "dd/MM/yyyy";

            int intAnio = this.datFecha.Value.Year;
            int intMes = this.datFecha.Value.Month;

            this.txtAnio.Value = intAnio;

            if (_intAnio_Anterior != intAnio)
            {

                if (this.cboEmpresas.SelectedIndex != -1)
                {

                    Carga_Combo_Asientos();
                    Carga_Combo_Comprobantes();

                    this.txtLeyenda_Asiento.Text = string.Empty;

                    this.grdAsientos.DataSource = null;

                    Calcula_Totales();

                }

            }
            else
            {

                if (_intMes_Anterior != intMes)
                {
                    if (this.cboComprobantes.Text.ToString().Trim().Length != 0)
                    {
                        cboComprobantes_Leave(sender, e);
                    }

                }

            }

            _intAnio_Anterior = this.datFecha.Value.Year;
            _intMes_Anterior = this.datFecha.Value.Month;
        }

        private void datFecha_KeyDown(object sender, KeyEventArgs e)
        {

            //if (e.KeyCode == Keys.Return)
            //{
            //    //this.Empresa = 1;

            //    int intAnio = this.datFecha.Value.Year;

            //    this.txtAnio.Value = intAnio;

            //    if (this.cboEmpresas.SelectedIndex != -1)
            //    {

            //        Carga_Combo_Asientos();
            //        Carga_Combo_Comprobantes();

            //        //this.cboAsientos.Enabled = true;
            //        //this.cboComprobantes.Enabled = true;

            //        this.cboAsientos.SelectedValueChanged -= new System.EventHandler(this.cboAsientos_SelectedValueChanged);
            //        this.cboComprobantes.SelectedValueChanged -= new System.EventHandler(this.cboComprobantes_SelectedIndexChanged);

            //        this.cboAsientos.SelectedIndex = -1;
            //        this.cboComprobantes.SelectedIndex = -1;

            //        this.cboComprobantes.SelectedValueChanged += new System.EventHandler(this.cboComprobantes_SelectedIndexChanged);
            //        this.cboAsientos.SelectedValueChanged += new System.EventHandler(this.cboAsientos_SelectedValueChanged);

            //        Calcula_Totales();

            //    }
            //    else
            //    {
            //        this.cboEmpresas.Focus();
            //    }

            //}


        }

        private void datFecha_Leave(object sender, EventArgs e)
        {

            //if (this.cboEmpresas.SelectedIndex != -1)
            //{

            //    int intAnio = this.datFecha.Value.Year;
            //    this.txtAnio.Value = intAnio;

            //    Carga_Combo_Asientos();
            //    Carga_Combo_Comprobantes();

            //    this.cboAsientos.Enabled = true;
            //    this.cboComprobantes.Enabled = true;

            //    this.cboAsientos.SelectedValueChanged -= new System.EventHandler(this.cboAsientos_SelectedValueChanged);

            //    this.cboAsientos.SelectedIndex = -1;
            //    //this.cboComprobantes.SelectedIndex = -1;

            //    this.cboAsientos.SelectedValueChanged += new System.EventHandler(this.cboAsientos_SelectedValueChanged);

            //    Calcula_Totales();

            //}
            //else
            //{
            //    this.cboEmpresas.Focus();
            //}

        }

        private void txtLeyenda_Asiento_KeyUp(object sender, KeyEventArgs e)
        {

            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);

                //if (grdAsientos.Rows.Count -1 == 0)
                //{
                //    var index = grdAsientos.Rows.Add();
                //    grdAsientos.Rows[index].Cells["Cuenta"].Value = "";
                //}

                //grdAsientos.CurrentCell = grdAsientos.Rows[0].Cells["Cuenta"];

                //DataGridView1.CurrentCell = DataGridView1.Rows[rowindex].Cells[columnindex]

                if (grdAsientos.Rows.Count == 1)
                {
                    DataGridViewRow row = (DataGridViewRow)grdAsientos.Rows[0].Clone();
                    grdAsientos.Rows.Add(row);
                    grdAsientos.CurrentCell = grdAsientos[1, 0];
                }

            }

        }

        private void txtLeyenda_Asiento_TextChanged(object sender, EventArgs e)
        {

        }

        private void grdAsientos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            if (e.Control.GetType() != typeof(DataGridViewComboBoxEditingControl)) return;

            if (((ComboBox)e.Control).SelectedIndex == 0)
            {
                ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
            }
            else
            {
                if (((ComboBox)e.Control).DropDownStyle != ComboBoxStyle.DropDown) return;
                ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDownList;
            }

            //// only allow one decimal point
            if ((sender as TextBox).Text.IndexOf('.') > -1)
            {
                return;
            }

        }

        private void grdAsientos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (grdAsientos.Columns[e.ColumnIndex].Name == "Cuenta")
            {
                //DataGridViewComboBoxCell combo = grdAsientos.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewComboBoxCell;

                // DataRow[] registro;

                string strCuenta = grdAsientos[e.ColumnIndex, e.RowIndex].Value.ToString();

                var dv = _dsCuentas.Tables[0].DefaultView;
                dv.RowFilter = "idCuenta = '" + strCuenta + "' ";

                var newDS = new DataSet();
                var newDT = dv.ToTable();
                newDS.Tables.Add(newDT);

                //registro = _dsCuentas.Tables[0].Select("idCuenta = " + combo.Value.ToString());
                //registro = _dsCuentas.Tables[0].Select("idCuenta = " + strCuenta );

                if (newDS.Tables[0].Rows.Count != 0)
                {
                    grdAsientos.Rows[e.RowIndex].Cells["Nombre"].Value = newDS.Tables[0].Rows[0]["Descripcion"];
                    //_dsCuentas.Tables[0].Rows[0]["Descripcion"].ToString();
                }
                else
                {
                    MessageBox.Show("Cuenta inexistente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

        }

        private void cboEmpresas_SelectedValueChanged(object sender, EventArgs e)
        {

            this.datFecha_Inicio_Ejercicio.ResetText();
            this.datFecha_Cierre_Ejercicio.ResetText();

            this.datFecha_Inicio_Ejercicio_Anterior.ResetText();
            this.datFecha_Cierre_Ejercicio_Anterior.ResetText();

            //Limpia_Controles();

            if (this.cboEmpresas.SelectedIndex == -1)
            {
                return;
            }

            Carga_Datos_Generales();

            this.cboAsientos.SelectedValueChanged -= new System.EventHandler(this.cboAsientos_SelectedValueChanged);

            Carga_Combo_Asientos();

            this.cboAsientos.SelectedValueChanged += new System.EventHandler(this.cboAsientos_SelectedValueChanged);

            this.cboComprobantes.SelectedIndexChanged -= new System.EventHandler(this.cboComprobantes_SelectedIndexChanged);
            Carga_Combo_Comprobantes();
            this.cboComprobantes.SelectedIndexChanged += new System.EventHandler(this.cboComprobantes_SelectedIndexChanged);

            this.cboAsientos.SelectedIndex = -1;
            this.cboComprobantes.SelectedIndex = -1;

        }

        private void btnLimpiar_Pantalla_Click(object sender, EventArgs e)
        {
            Limpia_Controles();
        }

        private void grdAsientos_SelectionChanged(object sender, EventArgs e)
        {

            if (MouseButtons != 0) return;

            //            if (grdAsientos.Columns[grdAsientos.CurrentCell.ColumnIndex].Name == "Cuenta" )
            //            { 

            if (_celWasEndEdit != null && grdAsientos.CurrentCell != null)
            {
                // if we are currently in the next line of last edit cell
                if (grdAsientos.CurrentCell.RowIndex == _celWasEndEdit.RowIndex + 1 &&
                    grdAsientos.CurrentCell.ColumnIndex == _celWasEndEdit.ColumnIndex)
                {
                    int iColNew;
                    int iRowNew = 0;
                    if (_celWasEndEdit.ColumnIndex >= grdAsientos.ColumnCount - 1)
                    {
                        //iColNew = 0; //En la columna 0 no se puede porque está invisible.
                        iColNew = 1;
                        iRowNew = grdAsientos.CurrentCell.RowIndex;
                    }
                    else
                    {
                        iColNew = _celWasEndEdit.ColumnIndex + 1;
                        iRowNew = _celWasEndEdit.RowIndex;
                    }

                    grdAsientos.CurrentCell = grdAsientos[iColNew, iRowNew];

                }

            }

            _celWasEndEdit = null;

            //            }

        }

        private void cboComprobantes_SelectedIndexChanged(object sender, EventArgs e)
        {

            //string strMes = datFecha.Value.Month.ToString("00");
            //string strNumero_Comprobante = "";

            if (this.cboComprobantes.SelectedIndex == -1)
            {

                //strNumero_Comprobante = strMes + this.cboComprobantes.Text.ToString();
                //this.cboComprobantes.Text = strNumero_Comprobante;
                this.cboAsientos.SelectedValueChanged -= new System.EventHandler(this.cboAsientos_SelectedValueChanged);
                this.cboAsientos.SelectedIndex = -1;
                this.cboAsientos.SelectedValueChanged += new System.EventHandler(this.cboAsientos_SelectedValueChanged);

                this.txtLeyenda_Asiento.Text = string.Empty;
                
                //this.optActual.Checked = false;
                //this.optAnterior.Checked = false;
                
                //this.txtTotal_Debe.Text = string.Empty;
                //this.txtTotal_Haber.Text = string.Empty;

                //Limpia la grilla
                this.grdAsientos.DataSource = null;

            }
            else
            {

                if (this.cboComprobantes.SelectedValue == null)
                { return; }

                this.cboAsientos.SelectedValue = this.cboComprobantes.SelectedValue;

                //PROVOCO EVENTO PARA REFRESCAR PANTALLA
                cboAsientos_SelectedValueChanged(cboAsientos, new EventArgs());

                Carga_Grilla();
            }



        }

        private void optActual_Click(object sender, EventArgs e)
        {

            //DialogResult dr = MessageBox.Show("Está seguro que desea cambiar el Ejercicio?", "Pregunta", MessageBoxButtons.YesNo);

            //switch (dr)
            //{
            //    case DialogResult.Yes:

            //this.cboEmpresas.SelectedIndex = -1;
            //this.cboEmpresas.Text = string.Empty;

            //Limpia_Controles();

            //this.cboEmpresas.Focus();

            //        break;

            //    case DialogResult.No:
            //        break;
            //}

        }

        private void optAnterior_Click(object sender, EventArgs e)
        {

            //DialogResult dr = MessageBox.Show("Está seguro que desea cambiar el Ejercicio?", "Pregunta", MessageBoxButtons.YesNo);

            //switch (dr)
            //{
            //    case DialogResult.Yes:

            //        this.cboEmpresas.SelectedIndex = -1;
            //        this.cboEmpresas.Text = string.Empty;

            //Limpia_Controles();

            //this.cboEmpresas.Focus();

            //        break;

            //    case DialogResult.No:
            //        break;
            //}

        }

        private void Carga_Datos_Generales()
        {

            Byte intEmpresa = (Byte)this.cboEmpresas.SelectedValue;

            BindingSource bindingSource_DG = new BindingSource();

            System.Data.DataSet dsDatos_Generales = new System.Data.DataSet("Datos_Generales");

            string strConsulta = "";

            strConsulta = "Exec Carga_Datos_Generales @intEmpresa = " + intEmpresa;

            dsDatos_Generales = Entidades.GetDataSet(strConsulta);

            bindingSource_DG.DataSource = dsDatos_Generales.Tables["Table"];

            this.datFecha_Inicio_Ejercicio.DataBindings.Clear();
            this.datFecha_Cierre_Ejercicio.DataBindings.Clear();
            this.datFecha_Inicio_Ejercicio_Anterior.DataBindings.Clear();
            this.datFecha_Cierre_Ejercicio_Anterior.DataBindings.Clear();

            this.datFecha_Inicio_Ejercicio.DataBindings.Add(new Binding("Value", bindingSource_DG, ((DataTable)bindingSource_DG.DataSource).Columns["Fecha_Inicio_Ejercicio"].ColumnName));
            this.datFecha_Cierre_Ejercicio.DataBindings.Add(new Binding("Value", bindingSource_DG, ((DataTable)bindingSource_DG.DataSource).Columns["Fecha_Cierre_Ejercicio"].ColumnName));
            this.datFecha_Inicio_Ejercicio_Anterior.DataBindings.Add(new Binding("Value", bindingSource_DG, ((DataTable)bindingSource_DG.DataSource).Columns["Fecha_Inicio_Ejercicio_Anterior"].ColumnName));
            this.datFecha_Cierre_Ejercicio_Anterior.DataBindings.Add(new Binding("Value", bindingSource_DG, ((DataTable)bindingSource_DG.DataSource).Columns["Fecha_Cierre_Ejercicio_Anterior"].ColumnName));

        }

        private void optAnterior_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void optActual_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void grdAsientos_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            grdAsientos[e.ColumnIndex, e.RowIndex].ReadOnly = false;
            //grdAsientos.BeginEdit(true);

        }

    private void cboComprobantes_Leave(object sender, EventArgs e)
        {

            string strMes = datFecha.Value.Month.ToString("00");
            //string strNumero_Comprobante = string.Empty;
            //string strLeyenda = string.Empty;

            string strTexto = string.Empty;

            if (this.cboComprobantes.Text.Trim() != string.Empty)
            {
                if(this.cboComprobantes.Text.Trim().Length != 5)
                {
                    strTexto = string.Empty;
                    strTexto = this.cboComprobantes.Text.Trim().PadLeft(3,'0');

                    this.cboComprobantes.Text = string.Empty;
                    this.cboComprobantes.Text = strMes + strTexto;
                }
                

            }

            this.cboAsientos.SelectedValueChanged -= new System.EventHandler(this.cboAsientos_SelectedValueChanged);

            if (this.cboComprobantes.SelectedIndex == -1)
            {

                this.cboAsientos.SelectedIndex = -1;
                this.txtLeyenda_Asiento.Text = string.Empty;
                //this.optActual.Checked = false;
                //this.optAnterior.Checked = false;
                this.txtTotal_Debe.Text = string.Empty;
                this.txtTotal_Haber.Text = string.Empty;

                //Limpia la grilla
                this.grdAsientos.DataSource = null;

            }
            else
            {
                this.cboAsientos.SelectedValue = this.cboComprobantes.SelectedValue;
                //PROVOCO EVENTO PARA REFRESCAR PANTALLA
                cboAsientos_SelectedValueChanged(cboAsientos, new EventArgs());
                Carga_Grilla();
            }

            this.cboAsientos.SelectedValueChanged += new System.EventHandler(this.cboAsientos_SelectedValueChanged);

            //this.SelectNextControl((Control)sender, true, true, true, true);

            //}

        }

        private void TxtFecha_Enter(object sender, EventArgs e)
        {
            this.datFecha.ValueChanged -= new System.EventHandler(this.datFecha_ValueChanged);


        }

        private void TxtFecha_Leave(object sender, EventArgs e)
        {

            string strTexto = string.Empty;

            CultureInfo myCIintl = new CultureInfo("es-AR", false);

            //if (this.txtFecha.Text == string.Empty )
            //{

            //}

            DateTime dt;

            //Dim dateStringStyles = { "ddMMyyyy", "dd/MM/yyyy"}

            if (DateTime.TryParseExact(this.txtFecha.Text, "dd/MM/yyyy", myCIintl, System.Globalization.DateTimeStyles.None , out dt))
            {
                datFecha.Value = dt;
                txtFecha.Text = dt.ToString("dd/MM/yyyy");
            }
            else
            {
                MessageBox.Show("Fecha incorrecta");
            }

            if (this.cboComprobantes.Text.Trim() != string.Empty)
            {

                strTexto = string.Empty;

                if (this.cboComprobantes.Text.Trim().Length > 2)
                {
                    strTexto = this.cboComprobantes.Text.Trim().Substring(2, (this.cboComprobantes.Text.Trim().Length - 2));
                }

                this.cboComprobantes.Text = string.Empty;
                this.cboComprobantes.Text = this.datFecha.Value.Month.ToString().PadLeft(2, '0') + strTexto;

            }

            this.datFecha.Validating += new System.ComponentModel.CancelEventHandler(this.datFecha_SomethingChanged);

        }

        private void DatFecha_Enter(object sender, EventArgs e)
        {
            this.txtFecha.Leave -= new System.EventHandler(this.txtAnio_Leave);
        }

        private void datFecha_SomethingChanged(object sender, EventArgs e)
        {

            object myDatFecha = datFecha;
            DateTimePicker dtp = (DateTimePicker)myDatFecha;

            this.txtFecha.Text = dtp.Value.ToString("dd/MM/yyyy");

            this.txtFecha.Leave += new System.EventHandler(this.TxtFecha_Leave);

        }

        private void TxtFecha_KeyUp(object sender, KeyEventArgs e)
        {

            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {

                this.TxtFecha_Leave(sender, e);

                this.SelectNextControl((Control)sender, true, true, true, true);

            }
            else
            {

                string strFecha = this.txtFecha.Text.Trim();

//                Console.WriteLine(e.KeyCode);

                if (strFecha.Length == 2)
                {
                    if (e.KeyCode != Keys.Divide && e.KeyCode != Keys.Back)
                    {
                        this.txtFecha.Text += "/";

                        this.txtFecha.SelectionStart = this.txtFecha.Text.Length;
                        this.txtFecha.SelectionLength = 0;

                    }
                }
                else
                {
                    if (strFecha.Length == 5)
                    {
                        if (e.KeyCode != Keys.Divide && e.KeyCode != Keys.Back)
                        {
                            this.txtFecha.Text += "/";

                            this.txtFecha.SelectionStart = this.txtFecha.Text.Length;
                            this.txtFecha.SelectionLength = 0;

                        }
                    }
                    else
                    {
                        if (strFecha.Length > 10)
                        {
                            this.txtFecha.Text = string.Empty;
                        }
                    }
                }

                //this.txtFecha.SelectionStart = this.txtFecha.Text.Length;
                //this.txtFecha.SelectionLength = 0;

            }

        }

        private void TxtFecha_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '/'))
            {
                e.Handled = true;
            }

            //// only allow one decimal point
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}

        }

        private void CboComprobantes_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (this.cboComprobantes.SelectionStart < this.cboComprobantes.Text.Trim().Length)
            {
                this.cboComprobantes.Text = string.Empty;
            }

        }

        private void GrdAsientos_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {

            //SOLO PERMITE EL INGRESO DE NÚMEROS, INCLUSO NEGATIVOS Y 1(UNO) SOLO PUNTO DECIMAL
            //https://stackoverflow.com/questions/39982127/restrict-dot-in-datagridview-cell-at-start-of-the-cell

            var cell = grdAsientos.CurrentCell;

            if (cell.ColumnIndex != 4 && cell.ColumnIndex != 5)
                return;

            string value = cell.EditedFormattedValue.ToString();
            string pattern = @"^-$ | ^-?0$ | ^-?0\.\d*$ | ^-?[1-9]\d*\.?\d*$";

            if (Regex.IsMatch(value, pattern, RegexOptions.IgnorePatternWhitespace))
            {
                grdAsientos.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            else
            {
                grdAsientos.CancelEdit();
            }

        }
    }

}