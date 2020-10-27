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
    public partial class frmListado_Plan_Cuentas : Form
    {
        public frmListado_Plan_Cuentas()
        {
            InitializeComponent();
        }

        private void FrmListado_Plan_Cuentas_Load(object sender, EventArgs e)
        {
            Inicia();
        }

        private void Inicia()
        {

            Carga_Combo_Cuentas_Desde();

            Carga_Combo_Cuentas_Hasta();

        }

        private void Carga_Combo_Cuentas_Desde()
        {

            System.Data.DataSet dsCuentas_Desde = new System.Data.DataSet("Cuentas_Desde");

            string strConsulta = "";

            strConsulta = "CALL `sgi_pop`.`sp_carga_plan_cuentas`('',0,0);";

            dsCuentas_Desde = Entidades.GetDataSet(strConsulta);

            cboCuentas_Desde.DataSource = dsCuentas_Desde.Tables["Table1"];

            this.cboCuentas_Desde.DisplayMember = "IdCuenta";
            this.cboCuentas_Desde.ValueMember = "IdCuenta";

            this.cboCuentas_Desde.SelectedIndex = -1;

        }

        private void Carga_Combo_Cuentas_Hasta()
        {

            System.Data.DataSet dsCuentas_Hasta = new System.Data.DataSet("Cuentas_Hasta");

            string strConsulta = "";

            strConsulta = "CALL `sgi_pop`.`sp_carga_plan_cuentas`('',0,0);";

            dsCuentas_Hasta = Entidades.GetDataSet(strConsulta);

            cboCuentas_Hasta.DataSource = dsCuentas_Hasta.Tables["Table1"];

            this.cboCuentas_Hasta.DisplayMember = "IdCuenta";
            this.cboCuentas_Hasta.ValueMember = "IdCuenta";

            this.cboCuentas_Hasta.SelectedIndex = -1;

        }

        private Boolean Valida()
        {

            MessageBoxButtons button = MessageBoxButtons.OK;
            MessageBoxIcon icon = MessageBoxIcon.Error;

            if (this.cboCuentas_Desde.SelectedIndex == -1 && this.cboCuentas_Hasta.SelectedIndex != -1)
            {
                MessageBox.Show("Debe seleccionar una Cuenta Desde", "Error", button, icon);
                this.cboCuentas_Desde.Focus();
                return false;
            }
            //else
            //{

            //    if (this.cboCuentas_Desde.SelectedIndex != -1 && this.cboCuentas_Hasta.SelectedIndex == -1)
            //    {
            //        MessageBox.Show("Debe seleccionar una Cuenta Hasta", "Error", button, icon);
            //        this.cboCuentas_Desde.Focus();
            //        return false;
            //    }

            //}

            return true; 

        }

        private void BtnEmite_Reporte_Click(object sender, EventArgs e)
        {

            if ( Valida() )
            {
                Emite_Reporte();
            }

        }

        //private void Emite_Reporte()
        //{

        //    ReportDocument cryRpt = new ReportDocument();

        //    string filePath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "rptPlan_Cuentas.rpt");

        //    //Byte intEmpresa = (Byte)this.cboEmpresas.SelectedValue;

        //    string strCuenta_Desde = string.Empty;
        //    string strCuenta_Hasta = string.Empty;

        //    if (this.cboCuentas_Desde.SelectedIndex != -1 )
        //    {
        //        strCuenta_Desde = this.cboCuentas_Desde.SelectedValue.ToString();
        //    }

        //    if (this.cboCuentas_Hasta.SelectedIndex != -1)
        //    {
        //        strCuenta_Hasta = this.cboCuentas_Hasta.SelectedValue.ToString();
        //    }

        //    #region Parametros_Reporte
        //        ParameterDiscreteValue parameterDiscreteValue = new ParameterDiscreteValue();
        //        ParameterValues currentParameterValues = new ParameterValues();
        //        ParameterField parameterField = new ParameterField();
        //        ParameterFields parameterFields = new ParameterFields();

        //        parameterField.Name = "@strID_Cuenta_Desde";
        //        parameterDiscreteValue.Value = (string)strCuenta_Desde;
        //        parameterField.CurrentValues.Add(parameterDiscreteValue);
        //        parameterFields.Add(parameterField);

        //        parameterField.Name = "@strID_Cuenta_Hasta";
        //        parameterDiscreteValue.Value = (string)strCuenta_Hasta;
        //        parameterField.CurrentValues.Add(parameterDiscreteValue);
        //        parameterFields.Add(parameterField);
        //    #endregion

        //    cryRpt.Load(@filePath, OpenReportMethod.OpenReportByDefault);

        //    frmReportes frmReporte = new frmReportes(cryRpt, parameterFields);

        //    frmReporte.ShowDialog(this);

        //}

        private void Emite_Reporte()
        {

            ReportDocument cryRpt = new ReportDocument();

            string filePath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "rptPlan_Cuentas.rpt");

            //Byte intEmpresa = (Byte)this.cboEmpresas.SelectedValue;

            #region Variables_Parametros_Reporte
            string strCuenta_Desde = string.Empty;
            string strCuenta_Hasta = string.Empty;

            if (this.cboCuentas_Desde.SelectedIndex != -1)
            {
                strCuenta_Desde = this.cboCuentas_Desde.SelectedValue.ToString();
            }

            if (this.cboCuentas_Hasta.SelectedIndex != -1)
            {
                strCuenta_Hasta = this.cboCuentas_Hasta.SelectedValue.ToString();
            }
            #endregion

            #region Parametros_Reporte
            ParameterDiscreteValue parameterDiscreteValue = new ParameterDiscreteValue();
            ParameterValues currentParameterValues = new ParameterValues();
            ParameterField parameterField = new ParameterField();
            ParameterFields parameterFields = new ParameterFields();

            parameterField.Name = "@strID_Cuenta_Desde";
            parameterDiscreteValue.Value = (string)strCuenta_Desde;
            parameterField.CurrentValues.Add(parameterDiscreteValue);
            parameterFields.Add(parameterField);

            parameterField = new ParameterField();
            parameterField.Name = "@strID_Cuenta_Hasta";
            parameterDiscreteValue = new ParameterDiscreteValue();
            parameterDiscreteValue.Value = (string)strCuenta_Hasta;
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

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
