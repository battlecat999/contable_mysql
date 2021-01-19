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
using System.Runtime.InteropServices;

namespace Contable
{
    public partial class frm_EmiteMayorGeneral : Form
    {

        public object lb_item = null;
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wpara, int lparam);
        private void MoverForm_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        public frm_EmiteMayorGeneral()
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

            //this.lstCuentas_Disponibles.SelectedValueChanged -= new System.EventHandler(lstCuentas_Disponibles_SelectedValueChanged);

            Carga_Lista_Cuentas_Disponibles();

            this.cboEmpresas.SelectedIndex = -1;
            this.lstCuentas_Disponibles.SelectedIndex = -1;

            this.lstCuentas_Seleccionadas.Sorted = true; 
            this.lstCuentas_Disponibles.Sorted = true;

            //this.lstCuentas_Disponibles.SelectedValueChanged += new System.EventHandler(lstCuentas_Disponibles_SelectedValueChanged);

            //this.cmdPasa_Uno.Enabled = false;
            //this.cmdPasa_Todo.Enabled = true;

            //this.cmdDevuelve_Uno.Enabled = false; 
            //this.cmdDevuelve_Todo.Enabled = true;

            this.cboEmpresas.Focus();

        }

        private static int _intID_Empresa;
        public int Empresa
        {
            get // this makes you to access value in form2
            {
                return _intID_Empresa;
            }
            set // this makes you to change value in form2
            {
                _intID_Empresa = value;
            }
        }

        private void Carga_Lista_Cuentas_Disponibles()
        {

            System.Data.DataSet dsCuentas_Disponibles = new System.Data.DataSet("Cuentas_Disponibles");

            string strConsulta = "";

          //  strConsulta = "exec Carga_Plan_Cuentas ";
            strConsulta = "CALL `sgi_pop`.`sp_carga_plan_cuentas`('',0,0);";
            dsCuentas_Disponibles = Entidades.GetDataSet(strConsulta);

            foreach ( System.Data.DataRow dr in dsCuentas_Disponibles.Tables["Table1"].Rows)
            {
                this.lstCuentas_Disponibles.Items.Add(dr["IdCuenta"]);
            }

        }

        private void frm_Part003_Load(object sender, EventArgs e)
        {
            Inicia();

            //this.lstCuentas_Seleccionadas.AllowDrop = true;
        }

        private void lstCuentas_Disponibles_DragLeave(object sender, EventArgs e)
        {
            System.Windows.Forms.ListBox lb = sender as System.Windows.Forms.ListBox;

            lb_item = lb.SelectedItem;

            this.lstCuentas_Disponibles.Refresh();
        }


        private void lstCuentas_Disponibles_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void lstCuentas_Seleccionadas_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void lstCuentas_Seleccionadas_DragDrop(object sender, DragEventArgs e)
        {

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

            for (int intI = 0; intI < this.lstCuentas_Disponibles.Items.Count;intI++)
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

        private void Carga_Combo_Empresas()
        {

            System.Data.DataSet dsEmpresas = new System.Data.DataSet("Empresas");

            string strConsulta = "";

            // strConsulta = "select IdEmpresa, RazonSocial From Empresa Order By RazonSocial ";

            strConsulta = "CALL `sgi_pop`.`sp_empresas_select_all`();";

            dsEmpresas = Entidades.GetDataSet(strConsulta);

            cboEmpresas.DataSource = dsEmpresas.Tables["Table1"];

            this.cboEmpresas.DisplayMember = "RazonSocial";
            this.cboEmpresas.ValueMember = "IdEmpresa";

            this.cboEmpresas.SelectedIndex = -1;

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

        private void lstCuentas_Seleccionadas_MouseDown(object sender, MouseEventArgs e)
        {

        }

        //private void  Busca_Cuentas_Seleccionadas()
        //{
        //    //int intI;
        //    string[] arrCuentas_Seleccionadas = new string[_dsCuentas_Seleccionadas.Tables["Cuentas_Seleccionadas"].Rows.Count];

        //    for (int intI = 0; intI < _dsCuentas_Seleccionadas.Tables["Cuentas_Seleccionadas"].Rows.Count; intI++)
        //    {
        //        arrCuentas_Seleccionadas[intI] = _dsCuentas_Seleccionadas.Tables["Cuentas_Seleccionadas"].Rows[intI]["IdCuenta"].ToString();
        //    }

        //}

        //public List<Cuentas_Seleccionadas> Busca_Cuentas_Seleccionadas(){

        //    List<Cuentas_Seleccionadas> lstCuentas = new List<Cuentas_Seleccionadas>();

        //    return lstCuentas;

        //}
        private void btnEmite_Mayor_Click(object sender, EventArgs e)
        {

            if (Valida() )
            {
                Guarda_Cuentas_Seleccionadas();
                Emite_Reporte();
                //Genera_Excel();
            }

        }

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

                if (this.lstCuentas_Seleccionadas.Items.Count == 0 )
                {

                    MessageBox.Show("Debe seleccionar al menos una Cuenta", "Error", button, icon);
                    return false;

                }

            }

            return true;

        }

        private void Genera_Excel()
        {

            Byte intEmpresa = (Byte)this.cboEmpresas.SelectedValue;

            this.btnEmite_Mayor.Enabled = false;

            Cursor.Current = Cursors.WaitCursor;

            //List<Cuentas_Seleccionadas> lstSeleccionadas = new List<Cuentas_Seleccionadas>();
            //lstSeleccionadas = Busca_Cuentas_Seleccionadas();

            string strSql = string.Empty;

            strSql = "CALL `sgi_pop`.`sp_eliminar_cuentasTMP_listados`();";

            int intResultado = Entidades.Ejecuta_Consulta(strSql);

            for (int intI = 0; intI < this.lstCuentas_Seleccionadas.Items.Count; intI++)
            {
                strSql = string.Empty;
                strSql = "CALL `sgi_pop`.`sp_insertar_cuentasTMP_listados`('" + this.lstCuentas_Seleccionadas.Items[intI] + "')";

                intResultado = Entidades.Ejecuta_Consulta(strSql);
            }

            strSql = string.Empty;
            //strSql = "Carga_Listado_Mayor_General @codEmpresa = " + intEmpresa + ", @FechaDesde = '" + this.datFecha_Desde.Value.ToString("yyyyMMdd") + "', @FechaHasta = '" + this.datFecha_Hasta.Value.ToString("yyyyMMdd") + "' "; // +"', @strCuentas_Seleccionadas = '" + strCuentas_Seleccionadas + "'";
            strSql = $"CALL `sgi_pop`.`sp_carga_listado_mayor_general`({intEmpresa}, {this.datFecha_Desde.Value.ToString("yyyyMMdd")}, {this.datFecha_Hasta.Value.ToString("yyyyMMdd")});";
            System.Data.DataTable dtCOPIA_ASIENTOS = new System.Data.DataTable();
            dtCOPIA_ASIENTOS = Entidades.GetDataTable_New(strSql);

            Int32 intLinea = 0;

            intLinea++;

            Int32 intPrimer_Linea_Mes = 0;
            Int32 intUltima_Linea_Mes = 0;

            Double dblSaldo_Mensual_Cuenta_Debe = 0;
            Double dblSaldo_Mensual_Cuenta_Haber = 0;

            Double dblSaldo_Acumulado_Mensual_Cuenta_Debe = 0;
            Double dblSaldo_Acumulado_Mensual_Cuenta_Haber = 0;


            Double dblSaldo_Final_Cuenta_Debe = 0;
            Double dblSaldo_Final_Cuenta_Haber = 0;

            Boolean blnPrimera_Vez = true;

            string strCuenta = string.Empty;

            string strAnio_Mes = string.Empty;

            DateTime datFecha = DateTime.Now;

            string strFecha = datFecha.ToString("dd/MM/yyyy");

            string strEmpresa = this.cboEmpresas.SelectedText;

            intLinea++;

            if (dtCOPIA_ASIENTOS.Rows.Count != 0)
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
                args[0] = "Mayor General";
                aRange.Merge(true);
                aRange.Cells.Font.Size = 20;
                aRange.Cells.Font.Bold = true;
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                #endregion
                //ENCABEZADOS COLUMNAS

                aRange = ws.get_Range("a" + intLinea, "a" + intLinea);
                args[0] = "Fecha";
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                //aRange.Columns.AutoFit();

                ws.get_Range("b" + intLinea + ":b" + intLinea, Type.Missing).Merge(Type.Missing);
                aRange = ws.get_Range("b" + intLinea, "b" + intLinea);
                aRange.Merge(true);
                args[0] = "Asiento";
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                //aRange.Columns.AutoFit();

                //ws.get_Range("c1:c1", Type.Missing).Merge(Type.Missing);
                aRange = ws.get_Range("c" + intLinea, "c" + intLinea);
                aRange.Merge(true);
                args[0] = "Nro. de Comprobante";
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                aRange.Columns.AutoFit();

                //ws.get_Range("d1:d1", Type.Missing).Merge(Type.Missing);
                aRange = ws.get_Range("d" + intLinea, "d" + intLinea);
                aRange.Merge(true);
                args[0] = "Leyenda";
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                aRange.Columns.AutoFit();

                //ws.get_Range("e1:e1", Type.Missing).Merge(Type.Missing);
                aRange = ws.get_Range("e" + intLinea, "e" + intLinea);
                aRange.Merge(true);
                args[0] = "Débitos";
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                //aRange.Columns.AutoFit();

                //ws.get_Range("f1:f1", Type.Missing).Merge(Type.Missing);
                aRange = ws.get_Range("f" + intLinea, "f" + intLinea);
                aRange.Merge(true);
                args[0] = "Créditos";
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                //aRange.Columns.AutoFit();

                //ws.get_Range("g1:g1", Type.Missing).Merge(Type.Missing);
                aRange = ws.get_Range("g" + intLinea, "g" + intLinea);
                aRange.Merge(true);
                args[0] = "Saldo";
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                //aRange.Columns.AutoFit();

                intLinea++;
                intLinea++;

                foreach (DataRow dr in dtCOPIA_ASIENTOS.Rows)
                {

                    if (blnPrimera_Vez)
                    {

                        strCuenta = dr["Cuenta"].ToString();
                        strAnio_Mes = dr["Anio_Mes"].ToString();

                        intLinea++;

                        aRange = ws.get_Range("a" + intLinea, "c" + intLinea);
                        aRange.Merge(true);
                        args[0] = "Cuenta: " + dr["Cuenta"].ToString() + " " + dr["Descripcion"].ToString();
                        aRange.Cells.Font.Bold = true;
                        aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                        intLinea++;

                        aRange = ws.get_Range("a" + intLinea, "a" + intLinea);
                        aRange.Cells.NumberFormat = "dd/MM/yyyy";
                        args[0] = this.datFecha_Desde.Value;
                        aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                        aRange = ws.get_Range("c" + intLinea, "c" + intLinea);
                        args[0] = "Saldo Inicial";
                        aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                        if (Convert.ToDouble(dr["Saldo"]) >= 0)
                        {
                            aRange = ws.get_Range("e" + intLinea, "e" + intLinea);
                            dblSaldo_Final_Cuenta_Debe = Convert.ToDouble(dr["Saldo"]);
                        }
                        else
                        {
                            aRange = ws.get_Range("f" + intLinea, "f" + intLinea);
                            dblSaldo_Final_Cuenta_Haber = Convert.ToDouble(dr["Saldo"]);
                        }

                        aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                        args[0] = dr["Saldo"];
                        aRange.Cells.Font.Bold = true;
                        aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                        blnPrimera_Vez = false;

                        intLinea++;

                        intPrimer_Linea_Mes = intLinea;

                    }
                    else
                    {

                        //if (strAnio_Mes.Trim() != dr["Anio_Mes"].ToString().Trim())
                        if (strCuenta.Trim() != dr["Cuenta"].ToString().Trim())
                        {

                            //strCuenta = dr["Cuenta"].ToString().Trim();
                            intUltima_Linea_Mes = intLinea;

                            intLinea++;

                            aRange = ws.get_Range("c" + intLinea, "c" + intLinea);
                            args[0] = "Saldo Cuenta: " + strCuenta.Trim();
                            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                            aRange = ws.get_Range("e" + intLinea, "e" + intLinea);
                            aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                            args[0] = dblSaldo_Final_Cuenta_Debe;
                            //args[0] = dblSaldo_Mensual_Cuenta_Debe;
                            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                            aRange = ws.get_Range("f" + intLinea, "f" + intLinea);
                            aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                            args[0] = dblSaldo_Final_Cuenta_Haber;
                            //args[0] = dblSaldo_Mensual_Cuenta_Haber;
                            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                            //intLinea++;

                            //aRange = ws.get_Range("a" + intLinea, "a" + intLinea);
                            //args[0] = "Cuenta: " + dr["Cuenta"].ToString() + " " + dr["Descripcion"].ToString();
                            //aRange.Cells.Font.Bold = true;
                            //aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                            //intLinea++;

                            //aRange = ws.get_Range("a" + intLinea, "a" + intLinea);
                            //aRange.Cells.NumberFormat = "dd/MM/yyyy";
                            //args[0] = this.datFecha_Desde.Value;
                            //aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                            //aRange = ws.get_Range("c" + intLinea, "c" + intLinea);
                            //args[0] = "Saldo Inicial";
                            //aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                            //if (Convert.ToDouble(dr["Saldo"]) >= 0)
                            //{
                            //    aRange = ws.get_Range("e" + intLinea, "e" + intLinea);
                            //    dblSaldo_Final_Cuenta_Debe = Convert.ToDouble(dr["Saldo"]);
                            //}
                            //else
                            //{
                            //    aRange = ws.get_Range("f" + intLinea, "f" + intLinea);
                            //    dblSaldo_Final_Cuenta_Haber = Convert.ToDouble(dr["Saldo"]);
                            //}

                            //aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                            //args[0] = dr["Saldo"];
                            //aRange.Cells.Font.Bold = true;
                            //aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                            //inicio Cabecera de Cuenta
                            intLinea++;
                            intLinea++;

                            aRange = ws.get_Range("a" + intLinea, "a" + intLinea);
                            args[0] = "Cuenta: " + dr["Cuenta"].ToString() + " " + dr["Descripcion"].ToString();
                            aRange.Cells.Font.Bold = true;
                            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                            intLinea++;

                            aRange = ws.get_Range("a" + intLinea, "a" + intLinea);
                            aRange.Cells.NumberFormat = "dd/MM/yyyy";
                            args[0] = this.datFecha_Desde.Value;
                            aRange.Cells.Font.Bold = true;
                            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                            aRange = ws.get_Range("c" + intLinea, "c" + intLinea);
                            args[0] = "Saldo Inicial";
                            aRange.Cells.Font.Bold = true;
                            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                            if (Convert.ToDouble(dr["Saldo"]) >= 0)
                            {
                                aRange = ws.get_Range("e" + intLinea, "e" + intLinea);
                                dblSaldo_Final_Cuenta_Debe = Convert.ToDouble(dr["Saldo"]);
                            }
                            else
                            {
                                aRange = ws.get_Range("f" + intLinea, "f" + intLinea);
                                dblSaldo_Final_Cuenta_Haber = Convert.ToDouble(dr["Saldo"]);
                            }

                            aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                            args[0] = dr["Saldo"];
                            aRange.Cells.Font.Bold = true;
                            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                            //Fin Cabecera de Cuenta


                            strCuenta = dr["Cuenta"].ToString().Trim();
                            strAnio_Mes = dr["Anio_Mes"].ToString();

                            dblSaldo_Mensual_Cuenta_Debe = 0;
                            dblSaldo_Mensual_Cuenta_Haber = 0;

                            dblSaldo_Final_Cuenta_Debe = 0;
                            dblSaldo_Final_Cuenta_Haber = 0;

                            dblSaldo_Acumulado_Mensual_Cuenta_Debe = 0;
                            dblSaldo_Acumulado_Mensual_Cuenta_Haber = 0;

                            intLinea++;
                            intLinea++;

                        }
                        else
                        {
                            if (strAnio_Mes != dr["Anio_Mes"].ToString().Trim())
                            {

                                strAnio_Mes = dr["Anio_Mes"].ToString();

                                intUltima_Linea_Mes = intLinea;

                                intLinea++;

                                dblSaldo_Acumulado_Mensual_Cuenta_Debe += dblSaldo_Mensual_Cuenta_Debe;
                                dblSaldo_Acumulado_Mensual_Cuenta_Haber += dblSaldo_Mensual_Cuenta_Haber;

                                aRange = ws.get_Range("c" + intLinea, "c" + intLinea);
                                args[0] = "Saldo acumulado mensual ";
                                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                                aRange = ws.get_Range("e" + intLinea, "e" + intLinea);
                                aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                                //args[0] = dblSaldo_Mensual_Cuenta_Debe;
                                args[0] = dblSaldo_Acumulado_Mensual_Cuenta_Debe;
                                aRange.Cells.Font.Bold = true;
                                //aRange.Formula = "=SUM(e" + intPrimer_Linea_Mes  + ":e" + intUltima_Linea_Mes + ")";
                                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                                aRange = ws.get_Range("f" + intLinea, "f" + intLinea);
                                aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                                //args[0] = dblSaldo_Mensual_Cuenta_Haber;
                                args[0] = dblSaldo_Acumulado_Mensual_Cuenta_Haber;
                                aRange.Cells.Font.Bold = true;
                                //aRange.Formula = "=SUM(f" + intPrimer_Linea_Mes + ":f" + intUltima_Linea_Mes + ")";
                                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                                aRange = ws.get_Range("g" + intLinea, "g" + intLinea);
                                aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                                aRange.Cells.Font.Bold = true;
                                aRange.Formula = "=(e" + intLinea + "-f" + intLinea + ")";

                                dblSaldo_Mensual_Cuenta_Debe = 0;
                                dblSaldo_Mensual_Cuenta_Haber = 0;

                                intLinea++;
                                intLinea++;

                                intPrimer_Linea_Mes = intLinea;

                            }

                        }

                    }

                    aRange = ws.get_Range("a" + intLinea, "a" + intLinea);
                    aRange.Cells.NumberFormat = "dd/MM/yyyy";
                    args[0] = dr["FechaAsiento"];
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                    aRange.Columns.AutoFit();

                    aRange = ws.get_Range("b" + intLinea, "b" + intLinea);
                    //aRange.Cells.NumberFormat = "@";
                    args[0] = dr["NroAsiento"].ToString();
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                    aRange.Columns.AutoFit();

                    aRange = ws.get_Range("c" + intLinea, "c" + intLinea);
                    aRange.Cells.NumberFormat = "@";
                    args[0] = dr["NIComprobante"].ToString();
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                    //aRange.Columns.AutoFit();

                    aRange = ws.get_Range("d" + intLinea, "d" + intLinea);
                    //aRange.Cells.NumberFormat = "@";
                    args[0] = dr["LeyendaItem"].ToString();
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                    //aRange.Columns.AutoFit();

                    aRange = ws.get_Range("e" + intLinea, "e" + intLinea);
                    aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                    args[0] = dr["Debe"];
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                    aRange = ws.get_Range("f" + intLinea, "f" + intLinea);
                    aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                    args[0] = dr["Haber"];
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                    //aRange = ws.get_Range("g" + intLinea, "g" + intLinea);
                    //args[0] = dr["Cuenta"];
                    //aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                    dblSaldo_Mensual_Cuenta_Debe += Convert.ToDouble(dr["Debe"]);
                    dblSaldo_Mensual_Cuenta_Haber += Convert.ToDouble(dr["Haber"]);

                    dblSaldo_Final_Cuenta_Debe += Convert.ToDouble(dr["Debe"]);
                    dblSaldo_Final_Cuenta_Haber += Convert.ToDouble(dr["Haber"]);

                    intLinea++;

                }

                //Corte Mes por Cuenta.
                intUltima_Linea_Mes = intLinea;

                intLinea++;

                aRange = ws.get_Range("c" + intLinea, "c" + intLinea);
                args[0] = "Saldo Cuenta: " + strCuenta;
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                aRange = ws.get_Range("e" + intLinea, "e" + intLinea);
                aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                args[0] = dblSaldo_Final_Cuenta_Debe;
                aRange.Cells.Font.Bold = true;
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                aRange = ws.get_Range("f" + intLinea, "f" + intLinea);
                aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                args[0] = dblSaldo_Final_Cuenta_Haber;
                aRange.Cells.Font.Bold = true;
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                //Fin Corte Mes.

                //ws.PrintPreview();
                xlApp.Visible = true;

                //xlApp.Workbooks.Close();
                //xlApp.Quit();

                aRange = null;
                wb = null;
                ws = null;
                xlApp = null;

            }

            this.btnEmite_Mayor.Enabled = true;

            Cursor.Current = Cursors.Default;

            MessageBox.Show("Proceso finalizado", "Sr. Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void Emite_Reporte()
        {

            ReportDocument cryRpt = new ReportDocument();

            string filePath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "rptMayorGeneral.rpt");

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

        private void pMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //private void pCerrar_Click(object sender, EventArgs e)
        //{
        //    this.Close();
        //}

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Guarda_Cuentas_Seleccionadas()
        {

            string strSql = string.Empty;

            strSql = "Delete CuentasTMP_Listados ";

            int intResultado = Entidades.Ejecuta_Consulta(strSql);


            strSql = string.Empty;
            for (int intI = 0; intI < this.lstCuentas_Seleccionadas.Items.Count; intI++)
            {
                
                strSql = string.Concat(strSql,"Insert Into CuentasTMP_Listados (NroCuentaTMP) Values ('" + this.lstCuentas_Seleccionadas.Items[intI] + "');", Environment.NewLine);

                
            }
            if (strSql!=string.Empty )
            {
                intResultado = Entidades.Ejecuta_Consulta(strSql);
            }
            

        }

        private void Carga_Datos_Generales()
        {

            Byte intEmpresa = (Byte)this.cboEmpresas.SelectedValue;

            BindingSource bindingSource_DG = new BindingSource();

            System.Data.DataSet dsDatos_Generales = new System.Data.DataSet("Datos_Generales");

            string strConsulta = "";

           // strConsulta = "Exec Carga_Datos_Generales @intEmpresa = " + intEmpresa;
            strConsulta = $"CALL `sgi_pop`.`sp_carga_datos_generales`({intEmpresa});";
            dsDatos_Generales = Entidades.GetDataSet(strConsulta);

            bindingSource_DG.DataSource = dsDatos_Generales.Tables["Table1"];

            this.datFecha_Desde.DataBindings.Clear();
            this.datFecha_Hasta.DataBindings.Clear();

            this.datFecha_Desde.DataBindings.Add(new Binding("Value", bindingSource_DG, ((System.Data.DataTable)bindingSource_DG.DataSource).Columns["Fecha_Inicio_Ejercicio"].ColumnName));
            this.datFecha_Hasta.DataBindings.Add(new Binding("Value", bindingSource_DG, ((System.Data.DataTable)bindingSource_DG.DataSource).Columns["Fecha_Cierre_Ejercicio"].ColumnName));

            //Setea las Fechas de Inicio y Fin del Ejercicio de la Companía.
            this.datFecha_Desde.DataBindings.Add(new Binding("MinDate", bindingSource_DG, ((System.Data.DataTable)bindingSource_DG.DataSource).Columns["Fecha_Inicio_Ejercicio"].ColumnName));
            this.datFecha_Hasta.DataBindings.Add(new Binding("MaxDate", bindingSource_DG, ((System.Data.DataTable)bindingSource_DG.DataSource).Columns["Fecha_Cierre_Ejercicio"].ColumnName));

        }


        private void CboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Datos_Generales();
        }

        private void ChkLiberaFechas_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkLiberaFechas.Checked == true)
            {
                this.datFecha_Desde.DataBindings.Clear();
                this.datFecha_Hasta.DataBindings.Clear();

                this.datFecha_Desde.MinDate = new DateTime (1900, 1, 1); 
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
