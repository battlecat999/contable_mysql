using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Configuration;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace Contable
{
    public partial class frmReportes : Form
    {

        //static TableLogOnInfo crTableLogonInfo;
        //static ConnectionInfo crConnectionInfo;
        //static Tables crTables;
        //static Database crDatabase; 

        private ReportDocument _cryReporte;
        private ParameterFields _parameterFields;
        public frmReportes(ReportDocument cryReporte, ParameterFields parameterFields)
        {
            InitializeComponent();

            this._cryReporte = cryReporte;
            this._parameterFields = parameterFields;
        }

        public frmReportes()
        {
            InitializeComponent();
        }

        private void frmReportes_Load(object sender, EventArgs e)
        {
            Inicia();
            configureCrystalReports();
        }
        private void configureCrystalReports()
        {
            try
            {
                ConnectionInfo boConnectionInfo = new ConnectionInfo();

                boConnectionInfo.ServerName = ConfigurationManager.AppSettings["ServerName"];
                boConnectionInfo.DatabaseName = ConfigurationManager.AppSettings["DatabaseName"];
                boConnectionInfo.UserID = ConfigurationManager.AppSettings["UserID"];
                boConnectionInfo.Password = ConfigurationManager.AppSettings["Password"];
                boConnectionInfo.Type = ConnectionInfoType.Query;
                boConnectionInfo.IntegratedSecurity = false;
                setDBLOGONforREPORT(boConnectionInfo);

                //ConnectionInfo myConnectionInfo = new ConnectionInfo();
                //myConnectionInfo.ServerName = "Nombre DNS";
                //myConnectionInfo.DatabaseName = "NombreBD ";
                //myConnectionInfo.UserID = "sa";
                //myConnectionInfo.Password = "contraseña";
                //myConnectionInfo.Type = ConnectionInfoType.Query; // Importante agregar este Type 
                //myConnectionInfo.IntegratedSecurity = false;
                //setDBLOGONforREPORT(myConnectionInfo);
            }
            catch (Exception ex)
            {
            }
        }

        private void setDBLOGONforREPORT(ConnectionInfo myconnectioninfo)
        {
            TableLogOnInfos mytableloginfos = new TableLogOnInfos();
            mytableloginfos = cryReportViewer.LogOnInfo;
            foreach (TableLogOnInfo myTableLogOnInfo in mytableloginfos)
                myTableLogOnInfo.ConnectionInfo = myconnectioninfo;
        }

        private void Inicia()
        {


            #region Seteo_Report_View
            cryReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            cryReportViewer.ParameterFieldInfo = this._parameterFields;
            //cryRpt.Load(@filePath);

            //cryRpt.SetDatabaseLogon(strUserID, strPassword, strServerName, strDatabaseName);
            cryReportViewer.ReportSource = _cryReporte;
            //crystalReportViewer1.PrintReport();
            cryReportViewer.Refresh();
            #endregion            

            //ReportDocument cryRpt = new ReportDocument();

            //string filePath = System.IO.Path.Combine( Application.StartupPath, "rptLibroDiario.rpt");
            //cryRpt.Load(filePath);

            //#region Parametros
            //ParameterField rptParametersField = new ParameterField();
            //ParameterFields rptParametersFields = new ParameterFields();
            //ParameterDiscreteValue rptParameterDiscreteValue = new ParameterDiscreteValue();

            //rptParametersField.Name = "@strCliente";
            //rptParameterDiscreteValue.Value = "";
            //rptParametersField.CurrentValues.Add(rptParameterDiscreteValue);

            //rptParametersFields.Add(rptParametersField);
            //#endregion

            //#region Codigo para establecer la conexion al informe

            //string strServerName = ConfigurationManager.AppSettings["ServerName"];
            //string strDatabaseName = ConfigurationManager.AppSettings["DatabaseName"];
            //string strUserID = ConfigurationManager.AppSettings["UserID"];
            //string strPassword = ConfigurationManager.AppSettings["Password"];

            //ConnectionInfo connectionInfo = new ConnectionInfo();
            //connectionInfo.ServerName = @strServerName;
            //connectionInfo.DatabaseName = strDatabaseName;
            //connectionInfo.UserID = strUserID;
            //connectionInfo.Password = strPassword;

            //foreach (Table t in cryRpt.Database.Tables)
            //{
            //    TableLogOnInfo tableLogInfo = t.LogOnInfo;
            //    t.ApplyLogOnInfo(tableLogInfo);
            //}
            //#endregion

            //cryRpt.SetDatabaseLogon("sa", "palomar");
            //cryRpt.ReportOptions.EnableSaveDataWithReport = false;

            //crystalReportViewer1.ParameterFieldInfo = rptParametersFields;
            //crystalReportViewer1.ReportSource = cryRpt;
            //crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            //crystalReportViewer1.Refresh();

        }

        public static void ReportLogin(ReportDocument crDoc, string Server, string Database, string UserID, string Password)
        {
            //crConnectionInfo = new ConnectionInfo();
            //crConnectionInfo.ServerName = Server;
            //crConnectionInfo.DatabaseName = Database;
            //crConnectionInfo.UserID = UserID;
            //crConnectionInfo.Password = Password;
            //crConnectionInfo.Type = ConnectionInfoType.Query;
            //crDatabase = crDoc.Database; crTables = crDatabase.Tables;

            //foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
            //{
            //    crTableLogonInfo = crTable.LogOnInfo;
            //    crTableLogonInfo.ConnectionInfo = crConnectionInfo;
            //    crTable.ApplyLogOnInfo(crTableLogonInfo);
            //}

        } 

        private void Genera_Reporte()
        {

            //ReportDocument cryRpt = new ReportDocument();
            //cryRpt.Load("C:\\Desarrollos\\Visual_Studio\\Marco_Schinini\\Contable\\Contable\\rptReporte.rpt");

            //TableLogOnInfos rptTableLogOnInfos = new TableLogOnInfos(); 
            //TableLogOnInfo rptTableLogOnInfo = new TableLogOnInfo(); 
            //ConnectionInfo rptConnectionInfo = new ConnectionInfo();

            //Tables rptTables;
            ////Table rptTable;

            //rptTables = cryRpt.Database.Tables;

            //string strRptLoc = "";

            //strRptLoc = "Fasani.dbo";

            //foreach (Table rptTable in rptTables)
            //{
            //    rptTableLogOnInfo = rptTable.LogOnInfo;

            //    rptConnectionInfo.ServerName = "(local)";
            //    rptConnectionInfo.DatabaseName = "fasani";
            //    rptConnectionInfo.UserID = "sa";
            //    rptConnectionInfo.Password = "palomar";
            //    //rptConnectionInfo.IntegratedSecurity = true; 

            //    rptConnectionInfo.Type = ConnectionInfoType.Query;

            //    rptTableLogOnInfo.ConnectionInfo = rptConnectionInfo;
            //    rptTable.ApplyLogOnInfo (rptTableLogOnInfo);
            //    rptTable.Location.Substring(rptTable.Location.LastIndexOf(";") + 1);
                
            //}

            //cryRpt.ReportOptions.EnableSaveDataWithReport = false; 
            //crystalReportViewer1.ReportSource = cryRpt;
            //crystalReportViewer1.Refresh();

        }


    }
}
