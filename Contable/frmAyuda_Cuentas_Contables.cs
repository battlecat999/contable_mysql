using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Contable
{
    public partial class frmAyuda_Cuentas_Contables : Form
    {
        private System.Data.DataSet _dsCuentas = new System.Data.DataSet("Cuentas");
        private string _strID_Cuenta = string.Empty;
        private string _strDesc = string.Empty;

        public frmAyuda_Cuentas_Contables()
        {
            InitializeComponent();
        }

        private void Inicia()
        {
            Configura_Grilla();

            Carga_Grilla();

            this.txtID_Cuenta.Text = string.Empty;
            this.txtDescripcion.Text = string.Empty;
            this.txtDescripcion.Focus();

            _strID_Cuenta = string.Empty;
            _strDesc = string.Empty;
        }

        private void Configura_Grilla()
        {

            DataGridViewColumn colItem = new DataGridViewTextBoxColumn();

            grdAyuda_Cuentas.AutoGenerateColumns = false;

            DataGridViewColumn colID_Cuenta = new DataGridViewTextBoxColumn();
            DataGridViewColumn colDescripcion = new DataGridViewTextBoxColumn();

            grdAyuda_Cuentas.Columns.Add(colID_Cuenta);
            grdAyuda_Cuentas.Columns.Add(colDescripcion);

            grdAyuda_Cuentas.Columns[0].Name = "ID_Cuenta";
            grdAyuda_Cuentas.Columns[1].Name = "Descripcion";

            grdAyuda_Cuentas.Columns[0].HeaderText = "Cuenta";
            grdAyuda_Cuentas.Columns[1].HeaderText = "Descripción";
            //grdAsientos.Columns[6].HeaderText = "Estado";

            grdAyuda_Cuentas.Columns[0].DataPropertyName = "IdCuenta";
            grdAyuda_Cuentas.Columns[1].DataPropertyName = "Descripcion";

            grdAyuda_Cuentas.Columns[0].ReadOnly = true;
            grdAyuda_Cuentas.Columns[1].ReadOnly = true;

            grdAyuda_Cuentas.Columns["ID_Cuenta"].Width = 100;
            grdAyuda_Cuentas.Columns["Descripcion"].Width = 300;

            //grdAyuda_Cuentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //SELECTION MODE
            grdAyuda_Cuentas.MultiSelect = false;

        }

        private void Carga_Grilla()
        {

            //System.Data.DataSet _dsCuentas = new System.Data.DataSet("Cuentas");

            string strConsulta = string.Empty;

            strConsulta = " exec Carga_Plan_Cuentas ";

            _dsCuentas = Entidades.GetDataSet(strConsulta);

            this.grdAyuda_Cuentas.AutoGenerateColumns = false;
            this.grdAyuda_Cuentas.DataSource = _dsCuentas;
            this.grdAyuda_Cuentas.DataMember = "Table";

        }

        private void TxtID_Cuenta_KeyDown(object sender, KeyEventArgs e)
        {

            TextBox txtID = (TextBox)sender;
            //TextBox txtDesc = (TextBox)sender;

            _strID_Cuenta = txtID.Text;
            //string strDesc = txtDesc.Text;

            if (e.KeyCode == Keys.Enter)
            {
                Aplica_Filtros_En_Grilla();
            }

        }

        private void Aplica_Filtros_En_Grilla()
        {

            string strConsulta = string.Empty;

            if (_strID_Cuenta.Trim() != string.Empty || _strDesc.Trim() != string.Empty)
            {

                DataView dv;

                if (_strID_Cuenta.Trim() != string.Empty && _strDesc.Trim() != string.Empty)
                {
                    strConsulta = "IdCuenta Like '" + _strID_Cuenta + "%' and Descripcion Like '%" + _strDesc + "%' ";
                }
                else
                {
                    if (_strID_Cuenta.Trim() != string.Empty && _strDesc.Trim() == string.Empty)
                    {
                        strConsulta = "IdCuenta Like '" + _strID_Cuenta + "%'";
                    }
                    else
                    {
                        if (_strID_Cuenta.Trim() == string.Empty && _strDesc.Trim() != string.Empty)
                        {
                            strConsulta = "Descripcion Like '%" + _strDesc + "%'";
                        }
                    }
                }

                dv = new DataView(_dsCuentas.Tables[0], strConsulta, "IdCuenta Asc", DataViewRowState.CurrentRows);
                grdAyuda_Cuentas.DataSource = dv;
            }
            else
            {
                grdAyuda_Cuentas.DataSource = _dsCuentas.Tables[0];
            }

        }

        private void FrmAyuda_Cuentas_Contables_Load(object sender, EventArgs e)
        {
            Inicia();
        }

        private void TxtDescripcion_KeyDown(object sender, KeyEventArgs e)
        {

            //TextBox txtID = (TextBox)sender;
            TextBox txtDesc = (TextBox)sender;

            //string strID_Cuenta = txtID.Text;
            _strDesc = txtDesc.Text;

            if (e.KeyCode == Keys.Enter)
            {
                Aplica_Filtros_En_Grilla();
            }

        }

        private void GrdAyuda_Cuentas_DoubleClick(object sender, EventArgs e)
        {
            Seleccionar();
        }

        private void Seleccionar()
        {

            string strID_Cuenta = string.Empty;

            strID_Cuenta = grdAyuda_Cuentas.Rows[grdAyuda_Cuentas.CurrentRow.Index].Cells["ID_Cuenta"].Value.ToString();

            frmAsientos fAsientos = new frmAsientos();

            fAsientos.Cuenta_Contable_Ayuda = strID_Cuenta;

            this.Close();

        }

        private void GrdAyuda_Cuentas_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                Seleccionar();
            }

        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
