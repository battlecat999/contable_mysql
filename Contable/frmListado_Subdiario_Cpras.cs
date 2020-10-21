using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Globalization;

using System.Configuration;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace Contable
{
    public partial class frmListado_Subdiario_Cpras : Form
    {
        public frmListado_Subdiario_Cpras()
        {
            InitializeComponent();
        }

        private void Inicia()
        {
            funciones_varias fv = new funciones_varias();
            fv.PrepararForm(this);
            DateTime datFecha_Inicial = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime datFecha_Final = datFecha_Inicial.AddMonths(1).AddDays(-1);

            this.cboEmpresas.SelectedIndexChanged -= new System.EventHandler(this.CboEmpresas_SelectedIndexChanged);

            Carga_Combo_Empresas();

            this.cboEmpresas.SelectedIndexChanged += new System.EventHandler(this.CboEmpresas_SelectedIndexChanged);


            this.datFecha_Desde.Value = datFecha_Inicial;
            this.datFecha_Hasta.Value = datFecha_Final;

            this.lblEstado.Text = "";

            this.cboEmpresas.Focus();

        }

        private void Emite_Reporte()
        {

            ReportDocument cryRpt = new ReportDocument();

            string filePath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "rptSubDiario_Compras.rpt");

            Byte intEmpresa = (Byte)this.cboEmpresas.SelectedValue;

            #region Variables_Parametros_Reporte
            string strFecha_Desde = string.Empty;
            string strFecha_Hasta = string.Empty;
            string strAsiento_Desde = string.Empty;
            string strAsiento_Hasta = string.Empty;

            string strOrden_Por_Asiento = string.Empty;

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
            string strTitulo = "";

            Cursor.Current = Cursors.WaitCursor;

            #region Excel
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            xlApp.Visible = false;

            this.lblEstado.Text = "CREANDO PLANILLA EXCEL...";

            Workbook wb = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = (Worksheet)wb.Worksheets[1];
            #endregion

            #region Encabezados_Columnas_Listado_General
            //TÍTULO
            strTitulo = "Subdiario de Compras ";

            Range aRange = ws.get_Range("e1", "e1");
            Object[] args = new Object[1];
            args[0] = strTitulo;
            aRange.Cells.Font.Size = 12;
            aRange.Cells.Font.Bold = true;
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
            aRange.Columns.AutoFit();

            //ENCABEZADOS COLUMNAS
            ws.get_Range("a3:a3", Type.Missing).Merge(Type.Missing);
            aRange = ws.get_Range("a3", "a3");
            //aRange.Merge(true);
            args[0] = "Fecha";

            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
            //aRange.Columns.AutoFit();

            ws.get_Range("b3:b3", Type.Missing).Merge(Type.Missing);
            aRange = ws.get_Range("b3", "b3");
            //aRange.Merge(true);
            args[0] = "Comprobante";
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
            aRange.Columns.AutoFit();

            ws.get_Range("c3:c3", Type.Missing).Merge(Type.Missing);
            aRange = ws.get_Range("c3", "c3");
            //aRange.Merge(true);
            aRange.EntireColumn.ColumnWidth = 50;
            args[0] = "Carpeta";
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
            aRange.Columns.AutoFit();

            ws.get_Range("d2:d2", Type.Missing).Merge(Type.Missing);
            aRange = ws.get_Range("d2", "d2");
            //aRange.Merge(true);
            //aRange.EntireColumn.ColumnWidth = 100;
            args[0] = "Proveedor";
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
            aRange.Columns.AutoFit();

            ws.get_Range("d3:d3", Type.Missing).Merge(Type.Missing);
            aRange = ws.get_Range("d3", "d3");
            //aRange.Merge(true);
            aRange.EntireColumn.ColumnWidth = 100;
            args[0] = "Razón Social";
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
            aRange.Columns.AutoFit();

            ws.get_Range("e3:e3", Type.Missing).Merge(Type.Missing);
            aRange = ws.get_Range("e3", "e3");
            aRange.Merge(true);
            args[0] = "CUIT";
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
            aRange.Columns.AutoFit();

            ws.get_Range("f3:f3", Type.Missing).Merge(Type.Missing);
            aRange = ws.get_Range("f3", "f3");
            //aRange.Merge(true);
            aRange.EntireColumn.ColumnWidth = 60;
            args[0] = "Neto";
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
            //aRange.Columns.AutoFit();

            ws.get_Range("g2:i2", Type.Missing).Merge(Type.Missing);
            aRange = ws.get_Range("g2", "i2");
            //aRange.Merge(true);
            aRange.EntireColumn.ColumnWidth = 60;
            args[0] = "IVA";
            aRange.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
            //aRange.Columns.AutoFit();

            ws.get_Range("g3:g3", Type.Missing).Merge(Type.Missing);
            aRange = ws.get_Range("g3", "g3");
            //aRange.Merge(true);
            aRange.EntireColumn.ColumnWidth = 60;
            args[0] = "21%";
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
            //aRange.Columns.AutoFit();

            ws.get_Range("h3:h3", Type.Missing).Merge(Type.Missing);
            aRange = ws.get_Range("h3", "h3");
            //aRange.Merge(true);
            aRange.EntireColumn.ColumnWidth = 60;
            args[0] = "27%";
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
            //aRange.Columns.AutoFit();

            ws.get_Range("i3:i3", Type.Missing).Merge(Type.Missing);
            aRange = ws.get_Range("i3", "i3");
            aRange.EntireColumn.ColumnWidth = 60;
            args[0] = "Aduana";
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
            //aRange.Columns.AutoFit();

            ws.get_Range("i3:i3", Type.Missing).Merge(Type.Missing);
            aRange = ws.get_Range("j3", "j3");
            aRange.EntireColumn.ColumnWidth = 60;
            args[0] = "Perc. IVA";
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
            //aRange.Columns.AutoFit();

            ws.get_Range("k3:k3", Type.Missing).Merge(Type.Missing);
            aRange = ws.get_Range("k3", "k3");
            //aRange.Merge(true);
            aRange.EntireColumn.ColumnWidth = 60;
            args[0] = "IIBB";
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
            //aRange.Columns.AutoFit();

            ws.get_Range("l3:l3", Type.Missing).Merge(Type.Missing);
            aRange = ws.get_Range("l3", "l3");
            //aRange.Merge(true);
            aRange.EntireColumn.ColumnWidth = 60;
            args[0] = "Ganancias";
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
            //aRange.Columns.AutoFit();

            ws.get_Range("m3:m3", Type.Missing).Merge(Type.Missing);
            aRange = ws.get_Range("m3", "m3");
            //aRange.Merge(true);
            aRange.EntireColumn.ColumnWidth = 60;
            args[0] = "Total";
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
            //aRange.Columns.AutoFit();

            aRange = ws.get_Range("a2", "m3");
            Borders border = aRange.Borders;
            border.LineStyle = XlLineStyle.xlContinuous;
            border.Weight = 3d;

            #endregion

            string strConsulta = string.Empty;

            string strFecha_Desde = string.Empty;
            string strFecha_Hasta = string.Empty;

            Byte intEmpresa = (Byte)this.cboEmpresas.SelectedValue;

            strFecha_Desde = this.datFecha_Desde.Value.ToString("yyyyMMdd");
            strFecha_Hasta = this.datFecha_Hasta.Value.ToString("yyyyMMdd");

            //IFormatProvider esESDateFormat = new CultureInfo("es-ES").DateTimeFormat;

            //DateTime datFecha_Desde = Convert.ToDateTime(this.datFecha_Desde.Value, esESDateFormat);
            //DateTime datFecha_Hasta = Convert.ToDateTime(this.datFecha_Hasta.Value, esESDateFormat);

            strConsulta = "Carga_Listado_Subdiario_Compras @intEmpresa = " + intEmpresa + ", @datFecha_Desde = '" + strFecha_Desde + "', @datFecha_Hasta = '" + strFecha_Hasta + "' ";

            this.lblEstado.Text = "OBTENIENDO DATOS...";
            this.Refresh();

            System.Data.DataTable dtListado = new System.Data.DataTable();
            dtListado = Entidades.GetDataTable(strConsulta, "");

            Int32 intLinea = 4;
            Int32 intUltima_Linea = 0;

            this.lblEstado.Text = "GENERANDO LISTADO...";
            this.Refresh();

            //Int32 intNumero_Carpeta = 0;
            string strNumero_Carpeta_Anterior = string.Empty;

            foreach (DataRow dr in dtListado.Rows)
            {

                if (dr["Numero"].ToString() != strNumero_Carpeta_Anterior)
                {
                    strNumero_Carpeta_Anterior = dr["Numero"].ToString();
                    intLinea++;
                }

                aRange = ws.get_Range("a" + intLinea, "a" + intLinea);
                aRange.Cells.NumberFormat = "dd/MM/yyyy";
                args[0] = dr["Fecha"];
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                //aRange.Columns.AutoFit();

                aRange = ws.get_Range("b" + intLinea, "b" + intLinea);
                //aRange.Cells.NumberFormat = "@";
                args[0] = dr["Comprobante"].ToString();
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                aRange.Columns.AutoFit();

                aRange = ws.get_Range("c" + intLinea, "c" + intLinea);
                //aRange.Cells.NumberFormat = "@";
                args[0] = dr["Numero"].ToString();
                aRange.Cells.HorizontalAlignment = XlHAlign.xlHAlignRight;
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                //aRange.Columns.AutoFit();

                aRange = ws.get_Range("d" + intLinea, "d" + intLinea);
                //aRange.Cells.NumberFormat = "@";
                args[0] = dr["Razon_Social_Proveedor"].ToString();
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                aRange.Columns.AutoFit();

                aRange = ws.get_Range("e" + intLinea, "e" + intLinea);
                //aRange.Cells.NumberFormat = "@";
                args[0] = dr["CUIT"].ToString();
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                //aRange.Columns.AutoFit();

                aRange = ws.get_Range("f" + intLinea, "f" + intLinea);
                aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                args[0] = dr["Neto"];
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                //aRange.Columns.AutoFit();

                aRange = ws.get_Range("g" + intLinea, "g" + intLinea);
                aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                args[0] = dr["IVA_21"];
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                //aRange.Columns.AutoFit();

                aRange = ws.get_Range("h" + intLinea, "h" + intLinea);
                aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                args[0] = dr["IVA_27"];
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                //aRange.Columns.AutoFit();

                aRange = ws.get_Range("i" + intLinea, "i" + intLinea);
                aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                args[0] = dr["IVA_105"];
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                //aRange.Columns.AutoFit();

                aRange = ws.get_Range("j" + intLinea, "j" + intLinea);
                aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                args[0] = 0;
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                //aRange.Columns.AutoFit();

                aRange = ws.get_Range("k" + intLinea, "k" + intLinea);
                aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                args[0] = dr["IIBB"];
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                //aRange.Columns.AutoFit();

                aRange = ws.get_Range("l" + intLinea, "l" + intLinea);
                aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                args[0] = dr["Imp_Ganancias"];
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                //aRange.Columns.AutoFit();

                aRange = ws.get_Range("m" + intLinea, "m" + intLinea);
                aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                aRange.Cells.Font.Bold = true;
                aRange.Formula = "=SUM(f" + intLinea + ":l" + intLinea + ")";
                //aRange.Columns.AutoFit();

                intLinea++;

            }

            #region Totales

            intUltima_Linea = intLinea;

            intLinea++;
            intLinea++;

            aRange = ws.get_Range("e" + intLinea , "e" + intLinea);
            aRange.EntireColumn.ColumnWidth = 60;
            args[0] = "Totales Facturas";
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

            aRange = ws.get_Range("f" + intLinea, "f" + intLinea);
            aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
            aRange.Cells.Font.Bold = true;
            aRange.Formula = "=SUM(f5:f" + intUltima_Linea + ")";
            //aRange.Columns.AutoFit();

            aRange = ws.get_Range("g" + intLinea, "g" + intLinea);
            aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
            aRange.Cells.Font.Bold = true;
            aRange.Formula = "=SUM(g5:g" + intUltima_Linea + ")";
            //aRange.Columns.AutoFit();

            aRange = ws.get_Range("h" + intLinea, "h" + intLinea);
            aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
            aRange.Cells.Font.Bold = true;
            aRange.Formula = "=SUM(h5:h" + intUltima_Linea + ")";
            //aRange.Columns.AutoFit();

            aRange = ws.get_Range("i" + intLinea, "i" + intLinea);
            aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
            aRange.Cells.Font.Bold = true;
            aRange.Formula = "=SUM(i5:i" + intUltima_Linea + ")";
            //aRange.Columns.AutoFit();

            aRange = ws.get_Range("j" + intLinea, "j" + intLinea);
            aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
            aRange.Cells.Font.Bold = true;
            aRange.Formula = "=SUM(j5:j" + intUltima_Linea + ")";
            //aRange.Columns.AutoFit();

            aRange = ws.get_Range("k" + intLinea, "k" + intLinea);
            aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
            aRange.Cells.Font.Bold = true;
            aRange.Formula = "=SUM(k5:k" + intUltima_Linea + ")";
            //aRange.Columns.AutoFit();

            aRange = ws.get_Range("l" + intLinea, "l" + intLinea);
            aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
            aRange.Cells.Font.Bold = true;
            aRange.Formula = "=SUM(l5:l" + intUltima_Linea + ")";
            //aRange.Columns.AutoFit();

            aRange = ws.get_Range("m" + intLinea, "m" + intLinea);
            aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
            aRange.Cells.Font.Bold = true;
            aRange.Formula = "=SUM(m5:m" + intUltima_Linea + ")";

            #endregion

            #region Total_Exentos

            this.lblEstado.Text = "GENERANDO TOTALES EXENTOS...";
            this.Refresh();


            Decimal decTotal_Neto = 0;
            Decimal decTotal_IVA_21 = 0;
            Decimal decTotal_IVA_27 = 0;
            Decimal decTotal_IVA_105 = 0;
            Decimal decTotal_IIBB = 0;
            Decimal decTotal_Ganancias = 0;

            System.Data.DataTable dtExentos;

            DataRow[] filtered_rows = dtListado.Select("Letra = 'C'");

            if (filtered_rows.Length > 0)
            {
                //filtered_data = filtered_rows.CopyToDataTable();
                dtExentos = dtListado.Select("Letra = 'C'").CopyToDataTable();
                //System.Data.DataTable dtExentos = dtListado.Select("Letra = 'C'").CopyToDataTable();

                decTotal_Neto = Convert.ToDecimal(dtExentos.Compute("SUM(Neto)", "Letra = 'C'"));
                decTotal_IVA_21 = Convert.ToDecimal(dtExentos.Compute("SUM(IVA_21)", "Letra = 'C'"));
                decTotal_IVA_27 = Convert.ToDecimal(dtExentos.Compute("SUM(IVA_27)", "Letra = 'C'"));
                decTotal_IVA_105 = Convert.ToDecimal(dtExentos.Compute("SUM(IVA_105)", "Letra = 'C'"));
                decTotal_IIBB = Convert.ToDecimal(dtExentos.Compute("SUM(IIBB)", "Letra = 'C'"));
                decTotal_Ganancias = Convert.ToDecimal(dtExentos.Compute("SUM(Imp_Ganancias)", "Letra = 'C'"));

                intLinea++;

            }            

            aRange = ws.get_Range("e" + intLinea, "e" + intLinea);
            aRange.EntireColumn.ColumnWidth = 60;
            args[0] = "Totales no Gravadas ó Exentas ";
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

            aRange = ws.get_Range("f" + intLinea, "f" + intLinea);
            aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
            args[0] = decTotal_Neto;
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

            aRange = ws.get_Range("g" + intLinea, "g" + intLinea);
            aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
            args[0] = decTotal_IVA_21;
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

            aRange = ws.get_Range("h" + intLinea, "h" + intLinea);
            aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
            args[0] = decTotal_IVA_27;
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

            aRange = ws.get_Range("i" + intLinea, "i" + intLinea);
            aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
            args[0] = decTotal_IVA_105;
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

            aRange = ws.get_Range("j" + intLinea, "j" + intLinea);
            aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
            args[0] = decTotal_IIBB;
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

            aRange = ws.get_Range("k" + intLinea, "k" + intLinea);
            aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
            args[0] = decTotal_Ganancias;
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

            #endregion

            intLinea++;

            #region Total_Servicios

            this.lblEstado.Text = "GENERANDO TOTALES SERVICIOS...";
            this.Refresh();

            var Servicios = dtListado.Select("Letra = 'S'");

            System.Data.DataTable dtServicios;
            //System.Data.DataTable dtServicios = dtListado.Select("Letra = 'S'").CopyToDataTable();

            decTotal_Neto = 0;
            decTotal_IVA_21 = 0;
            decTotal_IVA_27 = 0;
            decTotal_IVA_105 = 0;
            decTotal_IIBB = 0;
            decTotal_Ganancias = 0;

            if (Servicios.Count() != 0)
            {

                dtServicios = dtListado.Select("Letra = 'S'").CopyToDataTable();

                decTotal_Neto = Convert.ToDecimal(dtServicios.Compute("SUM(Neto)", "Letra = 'C'"));
                decTotal_IVA_21 = Convert.ToDecimal(dtServicios.Compute("SUM(IVA_21)", "Letra = 'C'"));
                decTotal_IVA_27 = Convert.ToDecimal(dtServicios.Compute("SUM(IVA_27)", "Letra = 'C'"));
                decTotal_IVA_105 = Convert.ToDecimal(dtServicios.Compute("SUM(IVA_105)", "Letra = 'C'"));
                decTotal_IIBB = Convert.ToDecimal(dtServicios.Compute("SUM(IIBB)", "Letra = 'C'"));
                decTotal_Ganancias = Convert.ToDecimal(dtServicios.Compute("SUM(Imp_Ganancias)", "Letra = 'C'"));

                intLinea++;
            }

            aRange = ws.get_Range("e" + intLinea, "e" + intLinea);
            aRange.EntireColumn.ColumnWidth = 60;
            args[0] = "Totales Servicios ";
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

            aRange = ws.get_Range("f" + intLinea, "f" + intLinea);
            aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
            args[0] = decTotal_Neto;
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

            aRange = ws.get_Range("g" + intLinea, "g" + intLinea);
            aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
            args[0] = decTotal_IVA_21;
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

            aRange = ws.get_Range("h" + intLinea, "h" + intLinea);
            aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
            args[0] = decTotal_IVA_27;
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

            aRange = ws.get_Range("i" + intLinea, "i" + intLinea);
            aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
            args[0] = decTotal_IVA_105;
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

            aRange = ws.get_Range("j" + intLinea, "j" + intLinea);
            aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
            args[0] = decTotal_IIBB;
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

            aRange = ws.get_Range("k" + intLinea, "k" + intLinea);
            aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
            args[0] = decTotal_Ganancias;
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

            intLinea++;

            #endregion

            #region Total_Importaciones

            this.lblEstado.Text = "GENERANDO TOTALES IMPORTACIONES...";
            this.Refresh();

            //var Importaciones = dtListado;

            decTotal_Neto = 0;
            decTotal_IVA_21 = 0;
            decTotal_IVA_27 = 0;
            decTotal_IVA_105 = 0;
            decTotal_IIBB = 0;
            decTotal_Ganancias = 0;

            if (dtListado.Rows.Count != 0)
            {

                System.Data.DataTable dtImportaciones = dtListado;

                decTotal_Neto = Convert.ToDecimal(dtImportaciones.Compute("SUM(Neto)", string.Empty));
                decTotal_IVA_21 = Convert.ToDecimal(dtImportaciones.Compute("SUM(IVA_21)", string.Empty));
                decTotal_IVA_27 = Convert.ToDecimal(dtImportaciones.Compute("SUM(IVA_27)", string.Empty));
                decTotal_IVA_105 = Convert.ToDecimal(dtImportaciones.Compute("SUM(IVA_105)", string.Empty));
                decTotal_IIBB = Convert.ToDecimal(dtImportaciones.Compute("SUM(IIBB)", string.Empty));
                decTotal_Ganancias = Convert.ToDecimal(dtImportaciones.Compute("SUM(Imp_Ganancias)", string.Empty));

                intLinea++;
            }

            aRange = ws.get_Range("e" + intLinea, "e" + intLinea);
            aRange.EntireColumn.ColumnWidth = 60;
            args[0] = "Totales Importaciones ";
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

            aRange = ws.get_Range("f" + intLinea, "f" + intLinea);
            aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
            args[0] = decTotal_Neto;
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

            aRange = ws.get_Range("g" + intLinea, "g" + intLinea);
            aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
            args[0] = decTotal_IVA_21;
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

            aRange = ws.get_Range("h" + intLinea, "h" + intLinea);
            aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
            args[0] = decTotal_IVA_27;
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

            aRange = ws.get_Range("i" + intLinea, "i" + intLinea);
            aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
            args[0] = decTotal_IVA_105;
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

            aRange = ws.get_Range("j" + intLinea, "i" + intLinea);
            aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
            args[0] = 0;
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

            aRange = ws.get_Range("k" + intLinea, "k" + intLinea);
            aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
            args[0] = decTotal_IIBB;
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

            aRange = ws.get_Range("l" + intLinea, "l" + intLinea);
            aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
            args[0] = decTotal_Ganancias;
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

            #endregion

            this.lblEstado.Text = "";

            Cursor.Current = Cursors.Default;

            //bool dialogResult = xlApp.Dialogs[XlBuiltInDialog.xlDialogPrintPreview].Show();

            xlApp.Visible = true;

            MessageBox.Show("Proceso finalizado", "Sr. Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            //Genera_Excel();

            Emite_Reporte();
        }

        private void frmListado_Subdiario_Cpras_Load(object sender, EventArgs e)
        {
            Inicia();
        }


        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Carga_Datos_Generales();
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


    }

}
