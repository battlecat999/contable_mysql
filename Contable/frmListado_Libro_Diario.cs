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

using System.Runtime.InteropServices;

namespace Contable
{
    public partial class frmListado_Libro_Diario : Form
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
        public frmListado_Libro_Diario()
        {
            InitializeComponent();
            funciones_varias fv = new funciones_varias();
            fv.PrepararForm(this);
        }

        private void Emite_Reporte_2()
        {

            ReportDocument cryRpt = new ReportDocument();

            string filePath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "rptLibroDiario.rpt");
            cryRpt.Load(filePath);

            Byte intEmpresa = (Byte)this.cboEmpresas.SelectedValue;

            string strFecha_Desde = string.Empty;
            string strFecha_Hasta = string.Empty;
            string strAsiento_Desde = string.Empty;
            string strAsiento_Hasta = string.Empty;

            if (this.chkSeleccionar_Entre_Fechas.Checked)
            {
                strFecha_Desde = this.datFecha_Desde.Value.ToString("yyyyMMdd");
                strFecha_Hasta = this.datFecha_Hasta.Value.ToString("yyyyMMdd");
            }

            if (this.cboAsiento_Desde.SelectedIndex != -1)
            {
                strAsiento_Desde = this.cboAsiento_Desde.SelectedValue.ToString();
                strAsiento_Hasta = this.cboAsiento_Hasta.SelectedValue.ToString();
            }

            //report.Load(Server.MapPath("ReportsFolder") + "\\YOURCRYSTALREPORT.rpt"); 
            CrystalDecisions.Shared.ParameterValues Empresa = new ParameterValues();
            CrystalDecisions.Shared.ParameterValues Fecha_Desde = new ParameterValues();
            CrystalDecisions.Shared.ParameterValues Fecha_Hasta = new ParameterValues();
            CrystalDecisions.Shared.ParameterValues Asiento_Desde = new ParameterValues();
            CrystalDecisions.Shared.ParameterValues Asiento_Hasta = new ParameterValues();
            CrystalDecisions.Shared.ParameterValues Orden = new ParameterValues();

            ParameterDiscreteValue rptParameterDiscreteValue = new ParameterDiscreteValue();

            ParameterDiscreteValue Empresa_val = new ParameterDiscreteValue();
            Empresa_val.Value = intEmpresa;
            Empresa.Add(Empresa_val);

            ParameterDiscreteValue Fecha_Desde_val = new ParameterDiscreteValue();
            Fecha_Desde_val.Value = strFecha_Desde;
            Fecha_Desde.Add(Fecha_Desde_val);

            ParameterDiscreteValue Fecha_Hasta_val = new ParameterDiscreteValue();
            Fecha_Hasta_val.Value = strFecha_Hasta;
            Fecha_Hasta.Add(Fecha_Hasta_val);

            ParameterDiscreteValue Asiento_Desde_val = new ParameterDiscreteValue();
            //Asiento_Desde_val.Value = strAsiento_Desde;
            Asiento_Desde_val.Value = DBNull.Value;
            Asiento_Desde.Add(Asiento_Desde_val);

            ParameterDiscreteValue Asiento_Hasta_val = new ParameterDiscreteValue();
            //Asiento_Hasta_val.Value = strAsiento_Hasta;
            Asiento_Hasta_val.Value = DBNull.Value;
            Asiento_Hasta.Add(Asiento_Hasta_val);

            ParameterDiscreteValue Orden_val = new ParameterDiscreteValue();
            Orden_val.Value = 1;
            Orden.Add(Orden_val);

            cryRpt.DataDefinition.ParameterFields["@intEmpresa"].ApplyCurrentValues(Empresa);
            cryRpt.DataDefinition.ParameterFields["@strFecha_Desde"].ApplyCurrentValues(Fecha_Desde);
            cryRpt.DataDefinition.ParameterFields["@strFecha_Hasta"].ApplyCurrentValues(Fecha_Hasta);
            cryRpt.DataDefinition.ParameterFields["@intAsiento_Desde"].ApplyCurrentValues(Asiento_Desde);
            cryRpt.DataDefinition.ParameterFields["@intAsiento_Hasta"].ApplyCurrentValues(Asiento_Hasta);
            cryRpt.DataDefinition.ParameterFields["@intOrden_Por_Asiento"].ApplyCurrentValues(Orden);

            //get connection string from web.config

            CrystalDecisions.CrystalReports.Engine.Table myTable;
            CrystalDecisions.Shared.ConnectionInfo conn = new ConnectionInfo();
            CrystalDecisions.Shared.TableLogOnInfo myLog;

            string strServer = ConfigurationManager.AppSettings["ServerName"];
            string strDBName = ConfigurationManager.AppSettings["DatabaseName"];
            string strUID = ConfigurationManager.AppSettings["UserID"];
            string strPassword = ConfigurationManager.AppSettings["Password"];

            conn.ServerName = strServer;
            conn.DatabaseName = strDBName;
            conn.UserID = strUID;
            conn.Password = strPassword;
            
            //TableLogOnInfo logOnInfo; 

            for (int i = 0; i < cryRpt.Database.Tables.Count; i++)
            {
                myTable = cryRpt.Database.Tables[i];
                //logOnInfo = myTable.LogOnInfo;
                //conn.Type = logOnInfo.ConnectionInfo.Type; 
                myLog = myTable.LogOnInfo;
                myLog.ConnectionInfo = conn;
                myTable.ApplyLogOnInfo(myLog);
                myTable.Location = myLog.TableName;
            }

            //crtLibro_Diario.Visible = true;
            //crtLibro_Diario.DisplayGroupTree = false;
            //crtLibro_Diario.HasPageNavigationButtons = true;
            //crtLibro_Diario.HasCrystalLogo = false;
            //crtLibro_Diario.HasDrillUpButton = false;
            //crtLibro_Diario.HasSearchButton = false;
            //crtLibro_Diario.HasViewList = false;
            //crtLibro_Diario.HasToggleGroupTreeButton = false;
            //crtLibro_Diario.HasZoomFactorList = false;
            //crtLibro_Diario.ToolbarStyle.Width = new Unit("750px");

            //crtLibro_Diario.ReportSource = cryRpt;

        }


        private void Emite_Reporte()
        {

            ReportDocument cryRpt = new ReportDocument();

            string filePath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "rptLibroDiario.rpt");
            //cryRpt.Load(filePath);

            //FieldDefinition FieldDef;

            Byte intEmpresa = (Byte)this.cboEmpresas.SelectedValue;

            #region Variables_Parametros_Reporte
            string strFecha_Desde = string.Empty;
            string strFecha_Hasta = string.Empty;
            string strAsiento_Desde = string.Empty;
            string strAsiento_Hasta = string.Empty;

            string strOrden_Por_Asiento = string.Empty;

            if (this.chkSeleccionar_Entre_Fechas.Checked)
            {
                strFecha_Desde = this.datFecha_Desde.Value.ToString("yyyyMMdd");
                strFecha_Hasta = this.datFecha_Hasta.Value.ToString("yyyyMMdd");
                strAsiento_Desde = "0";
                strAsiento_Hasta = "0";
            }

            if (this.cboAsiento_Desde.SelectedIndex != -1)
            {
                strAsiento_Desde = this.cboAsiento_Desde.SelectedValue.ToString();
                strAsiento_Hasta = this.cboAsiento_Hasta.SelectedValue.ToString();
            }
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

            if (this.chkSeleccionar_Entre_Fechas.Checked)
            {

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

            }

            parameterField = new ParameterField();
            parameterField.Name = "@intAsiento_Desde";
            parameterDiscreteValue = new ParameterDiscreteValue();
            parameterDiscreteValue.Value = (string)strAsiento_Desde;
            parameterField.CurrentValues.Add(parameterDiscreteValue);
            parameterFields.Add(parameterField);

            parameterField = new ParameterField();
            parameterField.Name = "@intAsiento_Hasta";
            parameterDiscreteValue = new ParameterDiscreteValue();
            parameterDiscreteValue.Value = (string)strAsiento_Hasta;
            parameterField.CurrentValues.Add(parameterDiscreteValue);
            parameterFields.Add(parameterField);

            //FieldDef = cryRpt.Database.Tables[0].Fields["FechaAsiento"];

            //if (this.chkOrdenar_Por_Numero_Asiento.Checked)
            //{
            //    FieldDef = cryRpt.Database.Tables[0].Fields["NroAsiento"];
            //}

            //cryRpt.DataDefinition.SortFields[0].Field = FieldDef;
            //cryRpt.DataDefinition.SortFields[0].SortDirection = CrystalDecisions.Shared.SortDirection.AscendingOrder;

            strOrden_Por_Asiento = "0";
            if (this.chkOrdenar_Por_Numero_Asiento.Checked)
            {
                strOrden_Por_Asiento = "1";
            }

            parameterField = new ParameterField();
            parameterField.Name = "@intOrden_Por_Asiento";
            parameterDiscreteValue = new ParameterDiscreteValue();
            parameterDiscreteValue.Value = (string)strOrden_Por_Asiento;
            parameterField.CurrentValues.Add(parameterDiscreteValue);
            parameterFields.Add(parameterField);

            #endregion

            #region Variables_Login_Reporte
            //string strServerName = ConfigurationManager.AppSettings["ServerName"];
            //string strDatabaseName = ConfigurationManager.AppSettings["DatabaseName"];
            //string strUserID = ConfigurationManager.AppSettings["UserID"];
            //string strPassword = ConfigurationManager.AppSettings["Password"];

            cryRpt.Load(@filePath, OpenReportMethod.OpenReportByDefault);
            //ConnectionInfo boConnectionInfo  = new ConnectionInfo();
            
            //boConnectionInfo.ServerName = ConfigurationManager.AppSettings["ServerName"];
            //boConnectionInfo.DatabaseName = ConfigurationManager.AppSettings["DatabaseName"];
            //boConnectionInfo.UserID = ConfigurationManager.AppSettings["UserID"];
            //boConnectionInfo.Password = ConfigurationManager.AppSettings["Password"];
            //boConnectionInfo.Type = ConnectionInfoType.SQL;

            //foreach (Table t in cryRpt.Database.Tables)
            //{
            //    TableLogOnInfo boTableLogOnInfo = t.LogOnInfo;
            //    boTableLogOnInfo.ConnectionInfo = boConnectionInfo;
            //    t.ApplyLogOnInfo(boTableLogOnInfo);
            //}

            #endregion

            #region Seteo_Report_View
            //crtLibro_Diario.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            //crtLibro_Diario.ParameterFieldInfo = parameterFields;
            //cryRpt.Load(@filePath, OpenReportMethod.OpenReportByDefault);

            //cryRpt.SetDatabaseLogon(boConnectionInfo.UserID.ToString(), boConnectionInfo.Password.ToString(), boConnectionInfo.ServerName.ToString(), boConnectionInfo.DatabaseName.ToString()); ;//MPS20190808

            //crtLibro_Diario.ReportSource = cryRpt;
            //crystalReportViewer1.PrintReport();
            //crtLibro_Diario.Refresh();
            #endregion

            frmReportes frmReporte = new frmReportes(cryRpt, parameterFields);
            
            //frmReporte.Show();
            frmReporte.ShowDialog(this);

        }

        private void Inicia()
        {

            DateTime datFecha_Inicial = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime datFecha_Final = datFecha_Inicial.AddMonths(1).AddDays(-1);

            this.cboEmpresas.SelectedValueChanged -= new System.EventHandler(this.cboEmpresas_SelectedValueChanged);

            Carga_Combo_Empresas();

            this.cboEmpresas.SelectedValueChanged += new System.EventHandler(this.cboEmpresas_SelectedValueChanged);

            this.chkSeleccionar_Entre_Fechas.Checked = false; 

            this.datFecha_Desde.Format = DateTimePickerFormat.Custom;
            this.datFecha_Desde.CustomFormat = " "; //a string with one whitespace

            this.datFecha_Hasta.Format = DateTimePickerFormat.Custom;
            this.datFecha_Hasta.CustomFormat = " "; //a string with one whitespace

            //this.datFecha_Desde.Value = datFecha_Inicial;
            //this.datFecha_Hasta.Value = datFecha_Final;

            this.chkOrdenar_Por_Numero_Asiento.Checked = false;

            this.lblEstado.Text = "";

            this.cboEmpresas.Focus();

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

        private void Carga_Combo_Asientos_Desde()
        {

            Byte intEmpresa = (Byte)this.cboEmpresas.SelectedValue;
            Byte intOrder_Por_Numero_Asiento = 0;

            if (this.chkOrdenar_Por_Numero_Asiento.Checked )
            {
                intOrder_Por_Numero_Asiento = 1;
            }

            System.Data.DataSet dsAsientos_Desde = new System.Data.DataSet("Asientos");

            string strConsulta = "";

            //strConsulta = "Carga_Combo_Asientos_Desde @intEmpresa = " + intEmpresa + ", @intOrden_Por_Asiento = " + intOrder_Por_Numero_Asiento;
            strConsulta=$"CALL `sgi_pop`.`sp_carga_combo_asientos_desde`({intEmpresa},{intOrder_Por_Numero_Asiento},0 );";
            dsAsientos_Desde = Entidades.GetDataSet(strConsulta);

            this.cboAsiento_Desde.DataSource = dsAsientos_Desde.Tables["Table1"];

            this.cboAsiento_Desde.DisplayMember = "IdAsiento";
            this.cboAsiento_Desde.ValueMember = "IdAsiento";

            this.cboAsiento_Desde.SelectedIndex = -1;

        }

        private void Carga_Combo_Asientos_Hasta()
        {

            Byte intEmpresa = (Byte)this.cboEmpresas.SelectedValue;
            Byte intOrder_Por_Numero_Asiento = 0;

            if (this.chkOrdenar_Por_Numero_Asiento.Checked)
            {
                intOrder_Por_Numero_Asiento = 1;
            }

            int intAsiento_Desde = 0;

            if (this.cboAsiento_Desde.SelectedIndex != -1)
            { 
                intAsiento_Desde = (int)this.cboAsiento_Desde.SelectedValue; 
            }

            System.Data.DataSet dsAsientos_Hasta = new System.Data.DataSet("Asientos");

            string strConsulta = "";

          //  strConsulta = "Carga_Combo_Asientos_Desde @intEmpresa = " + intEmpresa + ", @intOrden_Por_Asiento = " + intOrder_Por_Numero_Asiento;
            if (this.cboAsiento_Desde.SelectedIndex != -1)
            { 
               // strConsulta += ", @intIdAsiento_Desde = " + intAsiento_Desde;
                strConsulta = $"CALL `sgi_pop`.`sp_carga_combo_asientos_desde`({intEmpresa},{intOrder_Por_Numero_Asiento},{intAsiento_Desde} );";

            }
            else
            {
                strConsulta = $"CALL `sgi_pop`.`sp_carga_combo_asientos_desde`({intEmpresa},{intOrder_Por_Numero_Asiento},0);";

            }

            dsAsientos_Hasta = Entidades.GetDataSet(strConsulta);

            this.cboAsiento_Hasta.DataSource = dsAsientos_Hasta.Tables["Table1"];

            this.cboAsiento_Hasta.DisplayMember = "IdAsiento";
            this.cboAsiento_Hasta.ValueMember = "IdAsiento";

            this.cboAsiento_Hasta.SelectedIndex = -1;

        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {

            if (Validar() )
            {
                Emite_Reporte();
                //Genera_Excel();
            }

        }

        private void frmListado_Libro_Diario_Load(object sender, EventArgs e)
        {
            Inicia();
        }

        private void Genera_Excel()
        {
            string strTitulo = "";

            decimal decTotal_Debe = 0;
            decimal decTotal_Haber = 0;
            decimal decTotal_Debe_Por_Asiento = 0;
            decimal decTotal_Haber_Por_Asiento = 0;

            Int32 intLinea = 2;
            Int32 intLinea_Transporte = 0;
            Int32 intContador = 0;
            Int32 intAsiento = 0;

            bool blnPrimera_Vez = true;

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
            strTitulo = "Libro Diario ";

            Range aRange = ws.get_Range("a" + intLinea, "e" + intLinea);
            Object[] args = new Object[1];
            args[0] = strTitulo;
            aRange.Cells.Font.Size = 14;
            aRange.Cells.Font.Bold = true;
            aRange.Merge(true);
            aRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

            intLinea++;
            intLinea++;

            //ENCABEZADOS COLUMNAS
            ws.get_Range("a" + intLinea + ":a" + intLinea, Type.Missing).Merge(Type.Missing);
            aRange = ws.get_Range("a" + intLinea, "a" + intLinea);
            aRange.Cells.Font.Bold = true;
            args[0] = "N°";
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

            ws.get_Range("b" + intLinea + ":b" + intLinea, Type.Missing).Merge(Type.Missing);
            aRange = ws.get_Range("b" + intLinea, "b" + intLinea);
            aRange.EntireColumn.ColumnWidth = 15;
            aRange.Cells.Font.Bold = true;
            args[0] = "Cuenta";
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

            ws.get_Range("c" + intLinea + ":c" + intLinea, Type.Missing).Merge(Type.Missing);
            aRange = ws.get_Range("c" + intLinea, "c" + intLinea);
            aRange.Cells.Font.Bold = true;
            aRange.EntireColumn.ColumnWidth = 15;
            args[0] = "Débitos";
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
            
            ws.get_Range("d" + intLinea + ":d" + intLinea, Type.Missing).Merge(Type.Missing);
            aRange = ws.get_Range("d" + intLinea, "d" + intLinea);
            aRange.Cells.Font.Bold = true;
            aRange.EntireColumn.ColumnWidth = 15;
            args[0] = "Créditos";
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

            ws.get_Range("e" + intLinea + ":e" + intLinea, Type.Missing).Merge(Type.Missing);
            aRange = ws.get_Range("e" + intLinea, "e" + intLinea);
            aRange.EntireColumn.ColumnWidth = 50;
            aRange.Cells.Font.Bold = true;
            args[0] = "Leyenda";
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

            aRange = ws.get_Range("a" + intLinea, "e" + intLinea);
            Borders border = aRange.Borders;
            border.LineStyle = XlLineStyle.xlContinuous;
            border.Weight = 3d;

            #endregion

            string strConsulta = string.Empty;

            string strFecha_Desde = string.Empty;
            string strFecha_Hasta = string.Empty;

            Byte intEmpresa = (Byte)this.cboEmpresas.SelectedValue;

            if (this.chkSeleccionar_Entre_Fechas.Checked)
            {
                strFecha_Desde = this.datFecha_Desde.Value.ToString("yyyyMMdd");
                strFecha_Hasta = this.datFecha_Hasta.Value.ToString("yyyyMMdd");
            }

            strConsulta = "Carga_Listado_Libro_Diario @intEmpresa = " + intEmpresa;
            
            if (this.chkSeleccionar_Entre_Fechas.Checked)
            {
                strConsulta += ", @datFecha_Desde = '" + strFecha_Desde + "', @datFecha_Hasta = '" + strFecha_Hasta + "' ";
            }

            if (this.cboAsiento_Desde.SelectedIndex != -1)
            {
                strConsulta += ", @intAsiento_Desde = " + this.cboAsiento_Desde.SelectedValue.ToString();
            }

            if (this.cboAsiento_Hasta.SelectedIndex != -1)
            {
                strConsulta += ", @intAsiento_Hasta = " + this.cboAsiento_Hasta.SelectedValue.ToString();
            }

            this.lblEstado.Text = "OBTENIENDO DATOS...";
            this.Refresh();

            System.Data.DataTable dtListado = new System.Data.DataTable();
            dtListado = Entidades.GetDataTable(strConsulta, "");

            this.lblEstado.Text = "GENERANDO LISTADO...";
            this.Refresh();

            foreach (DataRow dr in dtListado.Rows)
            {

                if (intAsiento != Convert.ToInt32( dr["NroAsiento"]) )
                {

                    intAsiento = Convert.ToInt32(dr["NroAsiento"]);

                    if (!blnPrimera_Vez)
                    { 
                        intLinea++;
                        //intLinea++;

                        aRange = ws.get_Range("b" + intLinea, "b" + intLinea);
                        args[0] = "Totales ";
                        aRange.Cells.HorizontalAlignment = XlHAlign.xlHAlignRight;
                        aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                        //aRange.Columns.AutoFit();

                        aRange = ws.get_Range("c" + intLinea, "c" + intLinea);
                        aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                        args[0] = decTotal_Debe_Por_Asiento;
                        aRange.Cells.HorizontalAlignment = XlHAlign.xlHAlignRight;
                        aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                        //aRange.Columns.AutoFit();

                        aRange = ws.get_Range("d" + intLinea, "d" + intLinea);
                        aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                        args[0] = decTotal_Haber_Por_Asiento;
                        aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                        //aRange.Columns.AutoFit();

                        aRange = ws.get_Range("c" + intLinea, "d" + intLinea);
                        border = aRange.Borders;
                        border.LineStyle = XlLineStyle.xlContinuous;
                        border.Weight = 3d;
                    }

                    decTotal_Debe_Por_Asiento = 0;
                    decTotal_Haber_Por_Asiento = 0;

                    blnPrimera_Vez = false;

                    intLinea++;
                    intLinea++;

                    aRange = ws.get_Range("a" + intLinea, "b" + intLinea);
                    args[0] = "Asiento: " + dr["NroAsiento"].ToString();
                    aRange.Cells.Font.Bold = true;
                    aRange.Merge(true);
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                    aRange = ws.get_Range("e" + intLinea, "e" + intLinea);
                    aRange.Cells.NumberFormat = "dd/MM/yyyy";
                    aRange.Cells.Font.Bold = true;
                    aRange.Merge(true);
                    args[0] = "Fecha: " + dr["FechaAsiento"].ToString();
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                    intLinea++;

                    aRange = ws.get_Range("a" + intLinea, "d" + intLinea);
                    args[0] = "Leyenda del Asiento: " + dr["LeyendaAsiento"].ToString();
                    aRange.Merge(true);
                    aRange.Cells.Font.Bold = true;
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                    aRange = ws.get_Range("e" + intLinea, "e" + intLinea);
                    //aRange.Cells.NumberFormat = "dd/MM/yyyy";
                    args[0] = "Nro. de Comprobante: " + dr["NIComprobante"].ToString();
                    //aRange.Merge(true);
                    aRange.Cells.Font.Bold = true;
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                    intContador = 0;

                    intLinea++;
                    intLinea++;

                }

                intContador++;

                aRange = ws.get_Range("a" + intLinea, "a" + intLinea);
                //aRange.Cells.NumberFormat = "dd/MM/yyyy";
                //args[0] = dr["NroAsiento"];
                args[0] = intContador;
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                aRange = ws.get_Range("b" + intLinea, "b" + intLinea);
                //aRange.Cells.NumberFormat = "@";
                args[0] = dr["Cuenta"].ToString();
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                //aRange.Columns.AutoFit();

                aRange = ws.get_Range("c" + intLinea, "c" + intLinea);
                aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                args[0] = dr["Debe"];
                aRange.Cells.HorizontalAlignment = XlHAlign.xlHAlignRight;
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                //aRange.Columns.AutoFit();

                aRange = ws.get_Range("d" + intLinea, "d" + intLinea);
                aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                args[0] = dr["Haber"];
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                //aRange.Columns.AutoFit();

                aRange = ws.get_Range("e" + intLinea, "e" + intLinea);
                //aRange.Cells.NumberFormat = "@";
                args[0] = dr["LeyendaAsiento"].ToString();
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                //aRange.Columns.AutoFit();

                decTotal_Debe_Por_Asiento += Convert.ToDecimal(dr["Debe"]);
                decTotal_Haber_Por_Asiento += Convert.ToDecimal(dr["Haber"]);

                decTotal_Debe += Convert.ToDecimal(dr["Debe"]);
                decTotal_Haber += Convert.ToDecimal(dr["Haber"]);

                intLinea_Transporte++;

                if (intLinea_Transporte >= 47)
                {

                    intLinea++;
                    intLinea++;

                    aRange = ws.get_Range("b" + intLinea, "b" + intLinea);
                    args[0] = "Transporte ";
                    aRange.Cells.HorizontalAlignment = XlHAlign.xlHAlignRight;
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                    //aRange.Columns.AutoFFit();

                    aRange = ws.get_Range("c" + intLinea, "c" + intLinea);
                    aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                    args[0] = decTotal_Debe;
                    aRange.Cells.HorizontalAlignment = XlHAlign.xlHAlignRight;
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                    //aRange.Columns.AutoFit();

                    aRange = ws.get_Range("d" + intLinea, "d" + intLinea);
                    aRange.Cells.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)";
                    args[0] = decTotal_Haber;
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                    //aRange.Columns.AutoFit();

                    aRange = ws.get_Range("c" + intLinea, "d" + intLinea);
                    border = aRange.Borders;
                    border.LineStyle = XlLineStyle.xlContinuous;
                    border.Weight = 3d;

                    decTotal_Debe = 0;
                    decTotal_Haber = 0;

                    intLinea_Transporte = 0;

                    intLinea++;

                    //TÍTULO POR TRANSPORTE
                    aRange = ws.get_Range("a" + intLinea, "e" + intLinea);
                    //args = new Object[1];
                    args[0] = strTitulo;
                    aRange.Cells.Font.Size = 14;
                    aRange.Cells.Font.Bold = true;
                    aRange.Merge(true);
                    aRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                    intLinea++;
                    intLinea++;

                    //ENCABEZADOS COLUMNAS
                    ws.get_Range("a" + intLinea + ":a" + intLinea, Type.Missing).Merge(Type.Missing);
                    aRange = ws.get_Range("a" + intLinea, "a" + intLinea);
                    aRange.Cells.Font.Bold = true;
                    args[0] = "N°";
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                    ws.get_Range("b" + intLinea + ":b" + intLinea, Type.Missing).Merge(Type.Missing);
                    aRange = ws.get_Range("b" + intLinea, "b" + intLinea);
                    aRange.EntireColumn.ColumnWidth = 15;
                    aRange.Cells.Font.Bold = true;
                    args[0] = "Cuenta";
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                    ws.get_Range("c" + intLinea + ":c" + intLinea, Type.Missing).Merge(Type.Missing);
                    aRange = ws.get_Range("c" + intLinea, "c" + intLinea);
                    aRange.Cells.Font.Bold = true;
                    aRange.EntireColumn.ColumnWidth = 15;
                    args[0] = "Débitos";
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                    ws.get_Range("d" + intLinea + ":d" + intLinea, Type.Missing).Merge(Type.Missing);
                    aRange = ws.get_Range("d" + intLinea, "d" + intLinea);
                    aRange.Cells.Font.Bold = true;
                    aRange.EntireColumn.ColumnWidth = 15;
                    args[0] = "Créditos";
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                    ws.get_Range("e" + intLinea + ":e" + intLinea, Type.Missing).Merge(Type.Missing);
                    aRange = ws.get_Range("e" + intLinea, "e" + intLinea);
                    aRange.EntireColumn.ColumnWidth = 50;
                    aRange.Cells.Font.Bold = true;
                    args[0] = "Leyenda";
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

                    aRange = ws.get_Range("a" + intLinea, "e" + intLinea);
//                    Borders border = aRange.Borders;
                    border.LineStyle = XlLineStyle.xlContinuous;
                    border.Weight = 3d;

                    intLinea++;
                    //intLinea++;

                }

                intLinea++;
                //intLinea_Transporte++;

            }

            this.lblEstado.Text = "";

            Cursor.Current = Cursors.Default;

            xlApp.Visible = true;

            MessageBox.Show("Proceso finalizado", "Sr. Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void cboEmpresas_SelectedValueChanged(object sender, EventArgs e)
        {
            Carga_Datos_Generales();
            Carga_Combo_Asientos_Desde();
        }

        private void cboAsiento_Desde_SelectedValueChanged(object sender, EventArgs e)
        {
            
            this.cboAsiento_Hasta.SelectedIndex = -1;
            
            if (this.cboAsiento_Desde.SelectedIndex != -1)
            {
                Carga_Combo_Asientos_Hasta();
            }

        }

        private void chkOrdenar_Por_Numero_Asiento_CheckedChanged(object sender, EventArgs e)
        {

            this.cboAsiento_Desde.SelectedIndex = -1;
            this.cboAsiento_Hasta.SelectedIndex = -1;

            Carga_Combo_Asientos_Desde();

        }

        private void datFecha_Desde_ValueChanged(object sender, EventArgs e)
        {

            //if (this.datFecha_Desde.Value != null)
            //{
            //    this.datFecha_Desde.Format = DateTimePickerFormat.Custom;
            //    this.datFecha_Desde.CustomFormat = "dd/MM/yyyy"; //a string with one whitespace
            //}
            //else
            //{
            //    this.datFecha_Desde.Format = DateTimePickerFormat.Custom;
            //    this.datFecha_Desde.CustomFormat = " "; //a string with one whitespace
            //}

        }

        private void datFecha_Desde_KeyDown(object sender, KeyEventArgs e)
        {
            
            //if (e.KeyCode == Keys.Delete)
            //{
            //    this.datFecha_Desde.Format = DateTimePickerFormat.Custom;
            //    this.datFecha_Desde.CustomFormat = " "; //a string with one whitespace
            //    //this.datFecha_Desde.Value =
            //    this.datFecha_Desde.Text = "";
            //}
            
        }

        private void chkSeleccionar_Entre_Fechas_CheckedChanged(object sender, EventArgs e)
        {
            DateTime datFecha_Inicial = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime datFecha_Final = datFecha_Inicial.AddMonths(1).AddDays(-1);

            if (this.chkSeleccionar_Entre_Fechas.Checked)
            {
                this.datFecha_Desde.Format = DateTimePickerFormat.Custom;
                this.datFecha_Desde.CustomFormat = "dd/MM/yyyy"; //a string with one whitespace

                this.datFecha_Hasta.Format = DateTimePickerFormat.Custom;
                this.datFecha_Hasta.CustomFormat = "dd/MM/yyyy"; //a string with one whitespace

                //this.datFecha_Desde.Value = datFecha_Inicial;
                //this.datFecha_Hasta.Value = datFecha_Final;
            }
            else
            {
                this.datFecha_Desde.Format = DateTimePickerFormat.Custom;
                this.datFecha_Desde.CustomFormat = " "; //a string with one whitespace
                this.datFecha_Desde.Text = "";

                this.datFecha_Hasta.Format = DateTimePickerFormat.Custom;
                this.datFecha_Hasta.CustomFormat = " "; //a string with one whitespace
                this.datFecha_Hasta.Text = "";
                //this.datFecha_Hasta.Value = null;
            }

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        bool Validar()
        {

            if (this.cboEmpresas.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una Empresa", "Sr. Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.cboEmpresas.Focus();
                return false;
            }

            return true;

        }

        private void PCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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

           // strConsulta = "Exec Carga_Datos_Generales @intEmpresa = " + intEmpresa;
            strConsulta = $"CALL `sgi_pop`.`sp_carga_datos_generales`({intEmpresa})";
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

        private void ChkLiberaFechas_CheckedChanged(object sender, EventArgs e)
        {
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
}
