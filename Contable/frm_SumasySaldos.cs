using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Reflection;
using Microsoft.Office.Interop.Excel;

using System.Configuration;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

using System.Data.Common;
using System.Data.SqlClient;

using System.Runtime.InteropServices;

namespace Contable
{

    public partial class frm_SumasySaldos : Form
    {

        public object lb_item = null;

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wpara, int lparam);

        BindingSource _bindingSource = new BindingSource();
        private void MoverForm_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        public frm_SumasySaldos()
        {
            InitializeComponent();
        }

        private void Inicia()
        {

            funciones_varias fv = new funciones_varias();
            fv.PrepararForm(this);

            DateTime datFecha_Inicial = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime datFecha_Final = datFecha_Inicial.AddMonths(1).AddDays(-1);

            this.datFecha_Desde.Value = datFecha_Inicial;
            this.datFecha_Hasta.Value = datFecha_Final;

            this.cboEmpresas.SelectedIndexChanged -= new System.EventHandler(this.CboEmpresas_SelectedIndexChanged);

            Carga_Combo_Empresas();

            this.cboEmpresas.SelectedIndexChanged += new System.EventHandler(this.CboEmpresas_SelectedIndexChanged);

            Carga_Lista_Cuentas_Disponibles();

            this.cboEmpresas.SelectedIndex = -1;
            this.lstCuentas_Disponibles.SelectedIndex = -1;

            //_Cuentas_Seleccionadas = _dsCuentas_Seleccionadas.Tables.Add("Cuentas_Seleccionadas");

            //_Cuentas_Seleccionadas.Columns.Add("IdCuenta");
            //_Cuentas_Seleccionadas.Columns.Add("Descripcion");

            //_Cuentas_Disponibles = _dsCuentas_Disponibles.Tables.Add("Cuentas_Disponibles");

            //_Cuentas_Disponibles.Columns.Add("IdCuenta");
            //_Cuentas_Disponibles.Columns.Add("Descripcion");

            //this.cmdPasa_Uno.Enabled = false;
            //this.cmdPasa_Todo.Enabled = true;

            //this.cmdDevuelve_Uno.Enabled = false;
            //this.cmdDevuelve_Todo.Enabled = true;

            this.cboEmpresas.Focus();

        }

        private void Carga_Lista_Cuentas_Disponibles()
        {

            System.Data.DataSet dsCuentas_Disponibles = new System.Data.DataSet("Cuentas_Disponibles");

            string strConsulta = "";

            strConsulta = "exec Carga_Plan_Cuentas ";

            dsCuentas_Disponibles = Entidades.GetDataSet(strConsulta);

            foreach (System.Data.DataRow dr in dsCuentas_Disponibles.Tables["Table"].Rows)
            {
                this.lstCuentas_Disponibles.Items.Add(dr["IdCuenta"]);
            }

        }

        private void frm_Part004_Load(object sender, EventArgs e)
        {
            Inicia();
        }

        private void cmdPasa_Uno_Click(object sender, EventArgs e)
        {
            MoveListBoxItems(this.lstCuentas_Disponibles, this.lstCuentas_Seleccionadas);
        }

        private void MoveListBoxItems(System.Windows.Forms.ListBox lstOrigen, System.Windows.Forms.ListBox lstDestino)
        {
            System.Windows.Forms.ListBox.SelectedObjectCollection sourceItems = lstOrigen.SelectedItems;

            foreach (var item in sourceItems)
            {
                lstDestino.Items.Add(item);
            }
            while (lstOrigen.SelectedItems.Count > 0)
            {
                lstOrigen.Items.Remove(lstOrigen.SelectedItems[0]);
            }
        }

        private void cmdPasa_Todo_Click(object sender, EventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;

            for (int intI = 0; intI < this.lstCuentas_Disponibles.Items.Count; intI++)
            {
                this.lstCuentas_Disponibles.SetSelected(intI, true);
            }

            MoveListBoxItems(this.lstCuentas_Disponibles, this.lstCuentas_Seleccionadas);


            Cursor.Current = Cursors.Default;

        }

        private void cmdDevuelve_Uno_Click(object sender, EventArgs e)
        {

            MoveListBoxItems(this.lstCuentas_Seleccionadas, this.lstCuentas_Disponibles);

        }

        private void cmdDevuelve_Todo_Click(object sender, EventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;

            for (int intI = 0; intI < this.lstCuentas_Seleccionadas.Items.Count; intI++)
            {
                this.lstCuentas_Seleccionadas.SetSelected(intI, true);
            }

            MoveListBoxItems(this.lstCuentas_Seleccionadas, this.lstCuentas_Disponibles);

            Cursor.Current = Cursors.Default;

        }

        //public List<Cuentas_Seleccionadas> Busca_Cuentas_Seleccionadas()
        //{

        //    List<Cuentas_Seleccionadas> lstCuentas = new List<Cuentas_Seleccionadas>();

        //    for (int intI = 0; intI < _dsCuentas_Seleccionadas.Tables["Cuentas_Seleccionadas"].Rows.Count; intI++)
        //    {
        //        lstCuentas.Add(new Cuentas_Seleccionadas()
        //        {
        //            IdCuenta = _dsCuentas_Seleccionadas.Tables["Cuentas_Seleccionadas"].Rows[intI]["IdCuenta"].ToString(),
        //            Descripcion = _dsCuentas_Seleccionadas.Tables["Cuentas_Seleccionadas"].Rows[intI]["Descripcion"].ToString()
        //        }
        //        );
        //    }

        //    return lstCuentas;

        //}

        private Boolean Valida()
        {

            MessageBoxButtons button = MessageBoxButtons.OK;
            MessageBoxIcon icon = MessageBoxIcon.Error;

            if (this.cboEmpresas.SelectedIndex == -1)
            {

                MessageBox.Show("Debe seleccionar una Empresa", "Error", button, icon);
                this.cboEmpresas.Focus();
                return false;

            }

            else
            {

                if (this.lstCuentas_Seleccionadas.Items.Count == 0)
                {

                    MessageBox.Show("Debe seleccionar al menos una Cuenta", "Error", button, icon);
                    return false;

                }

            }

            return true;

        }


        private void btnEmite_Listado_Click(object sender, EventArgs e)
        {

            if (Valida())
            {
                Guarda_Cuentas_Seleccionadas();
                Emite_Reporte();
                //Genera_Excel();
            }

        }

        private void Emite_Reporte()
        {

            ReportDocument cryRpt = new ReportDocument();

            string filePath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "rptSumas_Saldos.rpt");

            //FieldDefinition FieldDef;

            Byte intEmpresa = (Byte)this.cboEmpresas.SelectedValue;

            #region Variables_Parametros_Reporte
            string strFecha_Desde = string.Empty;
            string strFecha_Hasta = string.Empty;

            strFecha_Desde = this.datFecha_Desde.Value.ToString("yyyyMMdd");
            strFecha_Hasta = this.datFecha_Hasta.Value.ToString("yyyyMMdd");

            #endregion

            #region Parametros_Reporte
            ParameterDiscreteValue parameterDiscreteValue = new ParameterDiscreteValue();
            ParameterValues currentParameterValues = new ParameterValues();
            ParameterField parameterField = new ParameterField();
            ParameterFields parameterFields = new ParameterFields();

            parameterField.Name = "@intEmpresa";
            parameterDiscreteValue.Value = (int)intEmpresa;
            parameterField.CurrentValues.Add(parameterDiscreteValue);
            parameterFields.Add(parameterField);

            parameterField = new ParameterField();
            parameterField.Name = "@strFecha_Desde";
            parameterDiscreteValue = new ParameterDiscreteValue();
            parameterDiscreteValue.Value = (string)strFecha_Desde;
            parameterField.CurrentValues.Add(parameterDiscreteValue);
            parameterFields.Add(parameterField);

            parameterField = new ParameterField();
            parameterField.Name = "@strFecha_Hasta";
            parameterDiscreteValue = new ParameterDiscreteValue();
            parameterDiscreteValue.Value = (string)strFecha_Hasta;
            parameterField.CurrentValues.Add(parameterDiscreteValue);
            parameterFields.Add(parameterField);

            #endregion

            #region Variables_Login_Reporte
            string strServerName = ConfigurationManager.AppSettings["ServerName"];
            string strDatabaseName = ConfigurationManager.AppSettings["DatabaseName"];
            string strUserID = ConfigurationManager.AppSettings["UserID"];
            string strPassword = ConfigurationManager.AppSettings["Password"];
            #endregion

            #region Seteo_Report_View
            cryRpt.Load(@filePath);

            cryRpt.SetDatabaseLogon(strUserID, strPassword, strServerName, strDatabaseName);
            #endregion

            frmReportes frmReporte = new frmReportes(cryRpt, parameterFields);
            frmReporte.ShowDialog(this);

        }


        private void Genera_Excel()
        {

            this.btnEmite_Listado.Enabled = false;

            Byte intEmpresa = (Byte)this.cboEmpresas.SelectedValue;

            Cursor.Current = Cursors.WaitCursor;

            #region Limpia_Tabla_Temporal_Cuentas_Seleccionadas

            string strSql = string.Empty;

            strSql = "Delete CuentasTMP_Listados ";

            int intResultado = Entidades.Ejecuta_Consulta(strSql);

            for (int intI = 0; intI < this.lstCuentas_Seleccionadas.Items.Count; intI++)
            {
                strSql = string.Empty;
                strSql = "Insert Into CuentasTMP_Listados (NroCuentaTMP) Values ('" + this.lstCuentas_Seleccionadas.Items[intI] + "')";

                intResultado = Entidades.Ejecuta_Consulta(strSql);
            }

            #endregion

            strSql = string.Empty;
            strSql = "Carga_Balance_Sumas_y_Saldos @codEmpresa = " + intEmpresa + ", @FechaDesde = '" + this.datFecha_Desde.Value.ToString("yyyyMMdd") + "', @FechaHasta = '" + this.datFecha_Hasta.Value.ToString("yyyyMMdd") + "' ";

            System.Data.DataTable dtSUMAS_Y_SALDOS = new System.Data.DataTable();
            dtSUMAS_Y_SALDOS = Entidades.GetDataTable_New(strSql);

            Int32 intLinea = 0;
            Int32 intPrimer_Linea = 0;

            Boolean blnPrimera_Vez = true;

            string strCuenta = string.Empty;
            string strPrimer_Digito_Cuenta = string.Empty;

            string strSub_Cuenta = string.Empty;

            double dblSuma_Cuenta_Debe = 0;
            double dblSuma_Cuenta_Haber = 0;
            double dblSuma_Saldo_Cuenta_Deudor = 0;
            double dblSuma_Saldo_Cuenta_Acreedor = 0;

            double dblSuma_Sub_Cuenta_Debe = 0;
            double dblSuma_Sub_Cuenta_Haber = 0;
            double dblSuma_Saldo_Sub_Cuenta_Deudor = 0;
            double dblSuma_Saldo_Sub_Cuenta_Acreedor = 0;

            double dblTotal_General_Debe = 0;
            double dblTotal_General_Haber = 0;
            double dblTotal_General_Saldo_Deudor = 0;
            double dblTotal_General_Saldo_Acreedor = 0;

            ///bool blnCuentas = false;

            DateTime datFecha = DateTime.Now;

            string strFecha = datFecha.ToString("dd/MM/yyyy");

            string strEmpresa = this.cboEmpresas.SelectedText;

            intLinea++;

            if (dtSUMAS_Y_SALDOS.Rows.Count != 0)
            {

                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

                xlApp.Visible = false;

                Workbook wb = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                Worksheet ws = (Worksheet)wb.Worksheets[1];

                #region Titulos

                    intLinea++;

                    Range aRange = ws.get_Range("a" + intLinea, "a" + intLinea);
                    Object[] args = new Object[1];
                    args[0] = strEmpresa;
                    aRange.Cells.Font.Size = 12;
                    aRange.Cells.Font.Bold = true;
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                    aRange = ws.get_Range("e" + intLinea, "e" + intLinea);
                    args[0] = "Fecha: " + strFecha;
                    aRange.Merge(true);
                    aRange.Cells.Font.Size = 12;
                    aRange.Cells.Font.Bold = true;
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                    intLinea++;
                    intLinea++;

                    aRange = ws.get_Range("b" + intLinea, "c" + intLinea);
                    args[0] = "Balance Sumas y Saldos";
                    aRange.Merge(true);
                    aRange.Cells.Font.Size = 20;
                    aRange.Cells.Font.Bold = true;
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                #endregion

                intLinea++;
                intLinea++;

                intPrimer_Linea = intLinea;

                //ENCABEZADOS COLUMNAS

                aRange = ws.get_Range("a" + intLinea, "a" + intLinea);
                args[0] = "Cuenta";
                aRange.Cells.Font.Bold = true;
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                //aRange.Columns.AutoFit();

                ws.get_Range("b" + intLinea + ":b" + intLinea, Type.Missing).Merge(Type.Missing);
                aRange = ws.get_Range("b" + intLinea, "b" + intLinea);
                aRange.Merge(true);
                args[0] = "Descripción";
                aRange.Cells.Font.Bold = true;
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                //aRange.Columns.AutoFit();

                ws.get_Range("c" + intLinea + ":c" + intLinea, Type.Missing).Merge(Type.Missing);
                aRange = ws.get_Range("c" + intLinea, "c" + intLinea);
                aRange.Merge(true);
                args[0] = "Débitos";
                aRange.Cells.Font.Bold = true;
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                aRange.Columns.AutoFit();

                ws.get_Range("d" + intLinea + ":d" + intLinea, Type.Missing).Merge(Type.Missing);
                aRange = ws.get_Range("d" + intLinea, "d" + intLinea);
                aRange.Merge(true);
                args[0] = "Créditos";
                aRange.Cells.Font.Bold = true;
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                aRange.Columns.AutoFit();

                ws.get_Range("e" + intLinea + ":e" + intLinea, Type.Missing).Merge(Type.Missing);
                aRange = ws.get_Range("e" + intLinea, "e" + intLinea);
                aRange.Merge(true);
                args[0] = "Saldo deudor";
                aRange.Cells.Font.Bold = true;
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                //aRange.Columns.AutoFit();

                ws.get_Range("f" + intLinea + ":f" + intLinea, Type.Missing).Merge(Type.Missing);
                aRange = ws.get_Range("f" + intLinea, "f" + intLinea);
                aRange.Merge(true);
                args[0] = "Saldo acreedor";
                aRange.Cells.Font.Bold = true;
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                //aRange.Columns.AutoFit();

                foreach (DataRow dr in dtSUMAS_Y_SALDOS.Rows)
                {

                    if (blnPrimera_Vez)
                    {
                        strCuenta = dr["Cuenta"].ToString().Substring(0, 1);
                        strPrimer_Digito_Cuenta = strCuenta.Substring(0, 1);
                        blnPrimera_Vez = false;
                    }

                    intLinea++;

                    if (strCuenta == dr["Cuenta"].ToString().Substring(0,1) )
                    {

                        aRange = ws.get_Range("a" + intLinea, "a" + intLinea);
                        aRange.Cells.NumberFormat = "@";
                        args[0] = dr["Cuenta"].ToString();
                        aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                        aRange.Columns.AutoFit();

                        aRange = ws.get_Range("b" + intLinea, "b" + intLinea);
                        //aRange.Cells.NumberFormat = "@";
                        args[0] = dr["Descripcion"].ToString();
                        aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                        aRange.Columns.AutoFit();

                        aRange = ws.get_Range("c" + intLinea, "c" + intLinea);
                        aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                        args[0] = dr["Debitos"];
                        aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                        aRange = ws.get_Range("d" + intLinea, "d" + intLinea);
                        aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                        args[0] = dr["Creditos"];
                        aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                        aRange = ws.get_Range("e" + intLinea, "e" + intLinea);
                        aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                        args[0] = dr["Saldo_Deudor"];
                        aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                        aRange = ws.get_Range("f" + intLinea, "f" + intLinea);
                        aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                        args[0] = dr["Saldo_Acreedor"];
                        aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                    }
                    else
                    {

                        intLinea++;
                        //intLinea++;

                        aRange = ws.get_Range("b" + intLinea, "b" + intLinea);
                        args[0] = "Sub Total SubCuenta ";
                        aRange.Cells.Font.Bold = true;
                        aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                        aRange.Columns.AutoFit();

                        aRange = ws.get_Range("c" + intLinea, "c" + intLinea);
                        aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                        args[0] = dblSuma_Sub_Cuenta_Debe;
                        aRange.Cells.Font.Bold = true;
                        aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                        aRange = ws.get_Range("d" + intLinea, "d" + intLinea);
                        aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                        args[0] = dblSuma_Sub_Cuenta_Haber;
                        aRange.Cells.Font.Bold = true;
                        aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                        aRange = ws.get_Range("e" + intLinea, "e" + intLinea);
                        aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                        args[0] = dblSuma_Saldo_Sub_Cuenta_Deudor;
                        aRange.Cells.Font.Bold = true;
                        aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                        aRange = ws.get_Range("f" + intLinea, "f" + intLinea);
                        aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                        args[0] = dblSuma_Saldo_Sub_Cuenta_Acreedor;
                        aRange.Cells.Font.Bold = true;
                        aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                        strCuenta = dr["Cuenta"].ToString().Substring(0, 1);

                        dblSuma_Cuenta_Debe += dblSuma_Sub_Cuenta_Debe;
                        dblSuma_Cuenta_Haber += dblSuma_Sub_Cuenta_Haber;
                        dblSuma_Saldo_Cuenta_Deudor += dblSuma_Saldo_Sub_Cuenta_Deudor;
                        dblSuma_Saldo_Cuenta_Acreedor += dblSuma_Saldo_Sub_Cuenta_Acreedor;

                        dblSuma_Sub_Cuenta_Debe = 0;
                        dblSuma_Sub_Cuenta_Haber = 0;
                        dblSuma_Saldo_Sub_Cuenta_Deudor = 0;
                        dblSuma_Saldo_Sub_Cuenta_Acreedor = 0;

                        //Sub Total Cuenta
                        #region Total_Por_Cuenta
                        //if (strPrimer_Digito_Cuenta != strCuenta.Substring(0,1))
                        //{
                        //    //blnCuentas = true;

                        //    intLinea++;

                        //    aRange = ws.get_Range("b" + intLinea, "b" + intLinea);
                        //    args[0] = "Sub Total Cuenta ";
                        //    aRange.Cells.Font.Bold = true;
                        //    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                        //    aRange.Columns.AutoFit();

                        //    aRange = ws.get_Range("c" + intLinea, "c" + intLinea);
                        //    aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                        //    args[0] = dblSuma_Cuenta_Debe;
                        //    aRange.Cells.Font.Bold = true;
                        //    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                        //    aRange = ws.get_Range("d" + intLinea, "d" + intLinea);
                        //    aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                        //    args[0] = dblSuma_Cuenta_Haber;
                        //    aRange.Cells.Font.Bold = true;
                        //    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                        //    aRange = ws.get_Range("e" + intLinea, "e" + intLinea);
                        //    aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                        //    args[0] = dblSuma_Saldo_Cuenta_Deudor;
                        //    aRange.Cells.Font.Bold = true;
                        //    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                        //    aRange = ws.get_Range("f" + intLinea, "f" + intLinea);
                        //    aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                        //    args[0] = dblSuma_Saldo_Cuenta_Acreedor;
                        //    aRange.Cells.Font.Bold = true;
                        //    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                        //    dblSuma_Cuenta_Debe = 0;
                        //    dblSuma_Cuenta_Haber = 0;
                        //    dblSuma_Saldo_Cuenta_Deudor = 0;
                        //    dblSuma_Saldo_Cuenta_Acreedor = 0;

                        //    strPrimer_Digito_Cuenta = strCuenta.Substring(0, 1);

                        //}
                        //else
                        //{
                        //    blnCuentas = true;
                        //}
                        #endregion

                        intLinea++;
                        intLinea++;

                        aRange = ws.get_Range("a" + intLinea, "a" + intLinea);
                        aRange.Cells.NumberFormat = "@";
                        args[0] = dr["Cuenta"].ToString();
                        aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                        aRange.Columns.AutoFit();

                        aRange = ws.get_Range("b" + intLinea, "b" + intLinea);
                        args[0] = dr["Descripcion"].ToString();
                        aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                        aRange.Columns.AutoFit();

                        aRange = ws.get_Range("c" + intLinea, "c" + intLinea);
                        aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                        args[0] = dr["Debitos"];
                        aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                        aRange = ws.get_Range("d" + intLinea, "d" + intLinea);
                        aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                        args[0] = dr["Creditos"];
                        aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                        aRange = ws.get_Range("e" + intLinea, "e" + intLinea);
                        aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                        args[0] = dr["Saldo_Deudor"];
                        aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                        aRange = ws.get_Range("f" + intLinea, "f" + intLinea);
                        aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                        args[0] = dr["Saldo_Acreedor"];
                        aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                    }

                    dblSuma_Sub_Cuenta_Debe += Convert.ToDouble(dr["Debitos"]);
                    dblSuma_Sub_Cuenta_Haber += Convert.ToDouble(dr["Creditos"]);
                    dblSuma_Saldo_Sub_Cuenta_Deudor += Convert.ToDouble(dr["Saldo_Deudor"]);
                    dblSuma_Saldo_Sub_Cuenta_Acreedor += Convert.ToDouble(dr["Saldo_Acreedor"]);

                    dblTotal_General_Debe += Convert.ToDouble(dr["Debitos"]);
                    dblTotal_General_Haber += Convert.ToDouble(dr["Creditos"]);
                    dblTotal_General_Saldo_Deudor += Convert.ToDouble(dr["Saldo_Deudor"]);
                    dblTotal_General_Saldo_Acreedor += Convert.ToDouble(dr["Saldo_Acreedor"]);

                }

                #region Total_Ultima_Cuenta

                    intLinea++;
                    intLinea++;

                    aRange = ws.get_Range("b" + intLinea, "b" + intLinea);
                    args[0] = "Sub Total SubCuenta ";
                    aRange.Cells.Font.Bold = true;
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                    aRange.Columns.AutoFit();

                    aRange = ws.get_Range("c" + intLinea, "c" + intLinea);
                    aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                    args[0] = dblSuma_Sub_Cuenta_Debe;
                    aRange.Cells.Font.Bold = true;
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                    aRange = ws.get_Range("d" + intLinea, "d" + intLinea);
                    aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                    args[0] = dblSuma_Sub_Cuenta_Haber;
                    aRange.Cells.Font.Bold = true;
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                    aRange = ws.get_Range("e" + intLinea, "e" + intLinea);
                    aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                    args[0] = dblSuma_Saldo_Sub_Cuenta_Deudor;
                    aRange.Cells.Font.Bold = true;
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                    aRange = ws.get_Range("f" + intLinea, "f" + intLinea);
                    aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                    args[0] = dblSuma_Saldo_Sub_Cuenta_Acreedor;
                    aRange.Cells.Font.Bold = true;
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                #endregion

                #region Total_Final_Cuenta

                //if (blnCuentas)
                //{

                //    intLinea++;
                //    intLinea++;

                //    aRange = ws.get_Range("b" + intLinea, "b" + intLinea);
                //    args[0] = "Sub Total Cuenta ";
                //    aRange.Cells.Font.Bold = true;
                //    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                //    aRange.Columns.AutoFit();

                //    aRange = ws.get_Range("c" + intLinea, "c" + intLinea);
                //    aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                //    args[0] = dblSuma_Cuenta_Debe; ;
                //    aRange.Cells.Font.Bold = true;
                //    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                //    aRange = ws.get_Range("d" + intLinea, "d" + intLinea);
                //    aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                //    args[0] = dblSuma_Cuenta_Haber;
                //    aRange.Cells.Font.Bold = true;
                //    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                //    aRange = ws.get_Range("e" + intLinea, "e" + intLinea);
                //    aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                //    args[0] = dblSuma_Saldo_Cuenta_Deudor;
                //    aRange.Cells.Font.Bold = true;
                //    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                //    aRange = ws.get_Range("f" + intLinea, "f" + intLinea);
                //    aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                //    args[0] = dblSuma_Saldo_Cuenta_Acreedor;
                //    aRange.Cells.Font.Bold = true;
                //    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                //}

                #endregion

                #region Total_General
                    intLinea++;
                    intLinea++;

                    aRange = ws.get_Range("b" + intLinea, "b" + intLinea);
                    args[0] = "Totales ";
                    aRange.Cells.Font.Bold = true;
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                    aRange.Columns.AutoFit();

                    aRange = ws.get_Range("c" + intLinea, "c" + intLinea);
                    aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                    args[0] = dblTotal_General_Debe;
                    aRange.Cells.Font.Bold = true;
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                    aRange = ws.get_Range("d" + intLinea, "d" + intLinea);
                    aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                    args[0] = dblTotal_General_Haber;
                    aRange.Cells.Font.Bold = true;
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                    aRange = ws.get_Range("e" + intLinea, "e" + intLinea);
                    aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                    args[0] = dblTotal_General_Saldo_Deudor;
                    aRange.Cells.Font.Bold = true;
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                    aRange = ws.get_Range("f" + intLinea, "f" + intLinea);
                    aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                    args[0] = dblTotal_General_Saldo_Acreedor;
                    aRange.Cells.Font.Bold = true;
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                #endregion

                #region Totales_Por_Activos

                    strSql = string.Empty;
                    strSql = "exec Carga_TOTALES_Balance_Sumas_y_Saldos @codEmpresa = " + intEmpresa + ", @FechaDesde = '" + this.datFecha_Desde.Value.ToString("yyyyMMdd") + "', @FechaHasta = '" + this.datFecha_Hasta.Value.ToString("yyyyMMdd") + "' ";

                    System.Data.DataTable dtTOTALES_GENERALES_POR_ACTIVOS = new System.Data.DataTable();
                    dtTOTALES_GENERALES_POR_ACTIVOS = Entidades.GetDataTable_New(strSql);

                    intLinea++;
                    intLinea++;

                    foreach (DataRow dr in dtTOTALES_GENERALES_POR_ACTIVOS.Rows)
                    {

                        aRange = ws.get_Range("b" + intLinea, "b" + intLinea);
                        args[0] = dr["Label"].ToString();
                        aRange.Cells.Font.Bold = true;
                        aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                        //aRange.Columns.AutoFit();

                        aRange = ws.get_Range("c" + intLinea, "c" + intLinea);
                        args[0] = dr["Debitos"];
                        //aRange.Cells.Font.Bold = true;
                        aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                        aRange = ws.get_Range("d" + intLinea, "d" + intLinea);
                        args[0] = dr["Creditos"];
                        //aRange.Cells.Font.Bold = true;
                        aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                        if (Convert.ToDouble(dr["Saldo"]) < 0)
                        {
                            aRange = ws.get_Range("e" + intLinea, "e" + intLinea);
                            args[0] = Convert.ToDouble(dr["Saldo"]) * -1;
                            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                            aRange = ws.get_Range("f" + intLinea, "f" + intLinea);
                            args[0] = 0;
                            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                        }
                        else
                        {
                            aRange = ws.get_Range("e" + intLinea, "e" + intLinea);
                            args[0] = 0;
                            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                            aRange = ws.get_Range("f" + intLinea, "f" + intLinea);
                            args[0] = Convert.ToDouble(dr["Saldo"]);
                            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                        }

                        intLinea++;

                    }

                #endregion

                //Cambia el tamaño de la letra para todo el listado, desde A1 hasta la columna f y última línea del listado
                aRange = ws.get_Range("a" + intPrimer_Linea, "f" + intLinea);
                aRange.Cells.Font.Size = 9;
                aRange.Columns.AutoFit();

                xlApp.Visible = true;

                //xlApp.Workbooks.Close();
                //xlApp.Quit();

                aRange = null;
                wb = null;
                ws = null;
                xlApp = null;

            }

            this.btnEmite_Listado.Enabled = true;

            Cursor.Current = Cursors.Default;

            MessageBox.Show("Proceso finalizado", "Sr. Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
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


        private void lstCuentas_Seleccionadas_DragEnter(object sender, DragEventArgs e)
        {

            //if (lb_item != null)
            //{

            //    this.lstCuentas_Seleccionadas.DisplayMember = "Descripcion";
            //    this.lstCuentas_Seleccionadas.ValueMember = "IdCuenta";

            //    lstCuentas_Seleccionadas.Items.Add(lb_item);

            //    _dsCuentas_Disponibles.Tables["Cuentas_Disponibles"].Rows.RemoveAt(lstCuentas_Disponibles.SelectedIndex);

            //    lb_item = null;
            //}

        }

        private void lstCuentas_Disponibles_DragLeave(object sender, EventArgs e)
        {
            System.Windows.Forms.ListBox lb = sender as System.Windows.Forms.ListBox;

            lb_item = lb.SelectedItem;

            this.lstCuentas_Disponibles.Refresh();
        }


        private void lstCuentas_Disponibles_MouseDown(object sender, MouseEventArgs e)
        {

            //Int16 intCantidad_Seleccionados = 0;

            //var lst = this.lstCuentas_Disponibles.SelectedItems.Cast<DataRowView>();

            //foreach (var item in lst)
            //{
            //    intCantidad_Seleccionados++;
            //}

            //if (intCantidad_Seleccionados > 1)
            //{
            //    this.cmdPasa_Uno.Enabled = false;
            //    this.cmdPasa_Todo.Enabled = true;
            //}
            //else
            //{
            //    if (intCantidad_Seleccionados == 1)
            //    {
            //        this.cmdPasa_Uno.Enabled = true;
            //        //this.cmdPasa_Todo.Enabled = false;
            //    }
            //    else
            //    {

            //        if (intCantidad_Seleccionados == 0)
            //        {
            //            this.cmdPasa_Uno.Enabled = false;
            //            this.cmdPasa_Todo.Enabled = false;
            //        }

            //    }

            //}

        }

        private void Guarda_Cuentas_Seleccionadas()
        {

            string strSql = string.Empty;

            strSql = "Delete CuentasTMP_Listados ";

            int intResultado = Entidades.Ejecuta_Consulta(strSql);

            for (int intI = 0; intI < this.lstCuentas_Seleccionadas.Items.Count; intI++)
            {
                strSql = string.Empty;
                strSql = "Insert Into CuentasTMP_Listados (NroCuentaTMP) Values ('" + this.lstCuentas_Seleccionadas.Items[intI] + "')";

                intResultado = Entidades.Ejecuta_Consulta(strSql);
            }

        }

        private void lstCuentas_Seleccionadas_MouseDown(object sender, MouseEventArgs e)
        {

            //Int16 intCantidad_Seleccionados = 0;

            //var lst = this.lstCuentas_Seleccionadas.SelectedItems.Cast<DataRowView>();

            //foreach (var item in lst)
            //{
            //    intCantidad_Seleccionados++;
            //}

            //if (intCantidad_Seleccionados > 1)
            //{
            //    this.cmdDevuelve_Uno.Enabled = false;
            //    this.cmdDevuelve_Todo.Enabled = true;
            //}
            //else
            //{
            //    if (intCantidad_Seleccionados == 1)
            //    {
            //        this.cmdDevuelve_Uno.Enabled = true;
            //        this.cmdDevuelve_Todo.Enabled = false;
            //    }
            //    else
            //    {

            //        if (intCantidad_Seleccionados == 0)
            //        {
            //            this.cmdDevuelve_Uno.Enabled = false;
            //            this.cmdDevuelve_Todo.Enabled = false;
            //        }

            //    }

            //}

        }

        private void CboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Datos_Generales();
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

            this.datFecha_Desde.DataBindings.Clear();
            this.datFecha_Hasta.DataBindings.Clear();

            this.datFecha_Desde.DataBindings.Add(new Binding("Value", bindingSource_DG, ((System.Data.DataTable)bindingSource_DG.DataSource).Columns["Fecha_Inicio_Ejercicio"].ColumnName));
            this.datFecha_Hasta.DataBindings.Add(new Binding("Value", bindingSource_DG, ((System.Data.DataTable)bindingSource_DG.DataSource).Columns["Fecha_Cierre_Ejercicio"].ColumnName));

            //Setea las Fechas de Inicio y Fin del Ejercicio de la Companía.
            this.datFecha_Desde.DataBindings.Add(new Binding("MinDate", bindingSource_DG, ((System.Data.DataTable)bindingSource_DG.DataSource).Columns["Fecha_Inicio_Ejercicio"].ColumnName));
            this.datFecha_Hasta.DataBindings.Add(new Binding("MaxDate", bindingSource_DG, ((System.Data.DataTable)bindingSource_DG.DataSource).Columns["Fecha_Cierre_Ejercicio"].ColumnName));

        }

        private void ChkLiberaFechas_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkLiberaFechas.Checked == true)
            {
                this.datFecha_Desde.DataBindings.Clear();
                this.datFecha_Hasta.DataBindings.Clear();

                this.datFecha_Desde.MinDate = new DateTime(1900, 1, 1);
                this.datFecha_Desde.MaxDate = new DateTime(2100, 12, 31);

                this.datFecha_Hasta.MinDate = new DateTime(1900, 1, 1);
                this.datFecha_Hasta.MaxDate = new DateTime(2100, 12, 31);

                DateTime datFecha_Inicial = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime datFecha_Final = datFecha_Inicial.AddMonths(1).AddDays(-1);

                this.datFecha_Desde.Value = datFecha_Inicial;
                this.datFecha_Hasta.Value = datFecha_Final;

            }
            else
            {
                Carga_Datos_Generales();
            }
        }
    }

}
