using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Common;

namespace Contable
{
    public partial class frmPlanCuentas : Form
    {

        Boolean _Alta;
        System.Data.DataSet _dsPlan_Cuentas = new System.Data.DataSet("Plan_Cuentas");
        BindingSource _bindingSource = new BindingSource();

        public frmPlanCuentas()
        {
            InitializeComponent();

            funciones_varias fv = new funciones_varias();
            fv.PrepararForm(this);
        }

        private void Carga_Combo_Plan_Cuentas()
        {

            System.Data.DataSet dsPlan_Cuentas = new System.Data.DataSet("Plan_Cuentas");

            string strConsulta = string.Empty;

            //Byte intEmpresa = (Byte)this.cboPlan_Cuentas.SelectedValue;

            strConsulta = "exec Carga_Plan_Cuentas ";

            dsPlan_Cuentas = Entidades.GetDataSet(strConsulta);

            _dsPlan_Cuentas = dsPlan_Cuentas; 
            _bindingSource.DataSource = null;
            _bindingSource.DataSource = _dsPlan_Cuentas.Tables["Table"];

            this.cboPlan_Cuentas.DataSource = dsPlan_Cuentas.Tables["Table"];

            this.cboPlan_Cuentas.DisplayMember = "idCuenta";
            this.cboPlan_Cuentas.ValueMember = "idCuenta";

            this.cboPlan_Cuentas.Text = string.Empty;
            this.cboPlan_Cuentas.SelectedIndex = -1;

        }

        

        private void CboPlan_Cuentas_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.cboPlan_Cuentas.SelectedIndex == -1)
            {
                return; 
            }

            DataRow[] returnedRows;
            returnedRows = _dsPlan_Cuentas.Tables["Table"].Select("idCuenta = " + this.cboPlan_Cuentas.SelectedValue.ToString() );
            DataRow dr1;
            dr1 = returnedRows[0];

            this.txtDescripcion.Text = dr1["Descripcion"].ToString();

            //this.cboEstados.SelectedValue = dr1["Estado"].ToString();

            this.txtNivel.Value = Convert.ToInt32 ( dr1["Nivel"]);

            this.chkAcepta_Movimientos.Checked = false;
            if (dr1["Acepta_Movimiento"].ToString() == "1")
            {
                this.chkAcepta_Movimientos.Checked = true; 
            }

            btnEliminar.Enabled = true;
            btnEditar.Enabled = true;

        }

        private void FrmPlanCuentas_Load(object sender, EventArgs e)
        {
            
            this.grpLogin.Visible = true;
            this.txtPassword.Focus();

        }

        private void Inicia()
        {
            this.cboPlan_Cuentas.SelectedValueChanged -= new System.EventHandler(this.CboPlan_Cuentas_SelectedValueChanged);

            Carga_Combo_Plan_Cuentas();

            Limpia_Controles();

            this.cboPlan_Cuentas.SelectedValueChanged += new System.EventHandler(this.CboPlan_Cuentas_SelectedValueChanged);

            _Alta = false;

            this.txtID_Plan_Cuenta.Visible = false;
            this.cboPlan_Cuentas.Visible = true; 

            this.cboPlan_Cuentas.Focus();

            //btnEliminar.Enabled = false;
            //btnEditar.Enabled = false; 

        }

        private void Limpia_Controles()
        {

            this.cboPlan_Cuentas.SelectedValueChanged -= new System.EventHandler(this.CboPlan_Cuentas_SelectedValueChanged);

            this.txtID_Plan_Cuenta.Text = string.Empty;
            this.cboPlan_Cuentas.SelectedIndex = -1;
            this.txtDescripcion.Text = string.Empty;
            this.txtNivel.Value = 0;
            this.chkAcepta_Movimientos.Checked = false;

            this.cboPlan_Cuentas.SelectedValueChanged += new System.EventHandler(this.CboPlan_Cuentas_SelectedValueChanged);

        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnGrabar_Click(object sender, EventArgs e)
        {
            if (Valida())
            { 

                if (Grabar())
                {
                    Inicia();

                    MessageBox.Show("Registro grabado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private Boolean Grabar()
        {

            int intResultado;

            int intAcepta_Movimientos;

            string strID_Cuenta; 

            intAcepta_Movimientos = 0;

            if (!_Alta)
            {
                strID_Cuenta = this.cboPlan_Cuentas.SelectedValue.ToString();                
            }
            else
            {
                strID_Cuenta = this.txtID_Plan_Cuenta.Text;
            }

            if (this.chkAcepta_Movimientos.Checked)
            {
                intAcepta_Movimientos = 1;
            }

            DbParameter[] Parametros = new DbParameter[4];

            Parametros[0] = new SqlParameter("@strId_Cuenta ", strID_Cuenta);
            Parametros[1] = new SqlParameter("@strDescripcion ", this.txtDescripcion.Text );
            Parametros[2] = new SqlParameter("@intNivel ", this.txtNivel.Value);
            Parametros[3] = new SqlParameter("@intAcepta_Movimientos ", intAcepta_Movimientos);

            if (!_Alta)
            {
                intResultado = Entidades.EjecutaNonQuery("Actualiza_Plan_Cuentas", Parametros);
            }
            else
            {
                intResultado = Entidades.EjecutaNonQuery("Alta_Plan_Cuentas", Parametros);
            }

            if (intResultado > 0)
            {
                _Alta = false;

                return true;
            }
            else
            {
                //if (intResultado == -1)
                //{
                    return false; 
                //}

            }

        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {

            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;

            Limpia_Controles();

            this.txtID_Plan_Cuenta.Visible = true;
            this.cboPlan_Cuentas.Visible = false;

            _Alta = true; 
        }

        private Boolean Valida()
        {

            if (_Alta)
            {
                if (this.txtID_Plan_Cuenta.Text == string.Empty)
                {
                    MessageBox.Show("Debe ingresar un ID de Cuenta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtID_Plan_Cuenta.Focus();
                    return false;
                }
            }

            else
            {

                if (this.cboPlan_Cuentas.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar un ID de Cuenta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.cboPlan_Cuentas.Focus();
                    return false;
                }

            }

            if (this.txtDescripcion.Text == string.Empty )
            {
                MessageBox.Show("Debe ingresar una Descripción válida", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtDescripcion.Focus();
                return false;
            }

            return true; 
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {

            //_Alta = false;

            //this.txtID_Plan_Cuenta.Visible = false;
            //this.cboPlan_Cuentas.Visible = true;

            //Limpia_Controles();

            //this.cboPlan_Cuentas.Focus();

        }

        private void Elimina_Plan_Cuenta()
        {

            int intResultado;

            string strID_Cuenta;
            strID_Cuenta = this.cboPlan_Cuentas.SelectedValue.ToString();

            DbParameter[] Parametros = new DbParameter[1];

            Parametros[0] = new SqlParameter("@strId_Cuenta ", strID_Cuenta);

            intResultado = Entidades.EjecutaNonQuery("Elimina_Plan_Cuentas", Parametros);

            if (intResultado > 0)
            {
                //_Alta = false;
            }

        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {

            if (this.cboPlan_Cuentas.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un ID de Cuenta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult dr = MessageBox.Show("Está seguro que desea eliminar el Plan de Cuenta?", "Pregunta", MessageBoxButtons.YesNo);

            switch (dr)
            {
                case DialogResult.Yes:

                    Elimina_Plan_Cuenta();

                    Inicia();

                    break;

                case DialogResult.No:
                    break;
            }

        }

        private void PMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void PCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private Boolean Valida_Login()
        {

            if (this.txtPassword.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar una Password", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtPassword.Focus();
                return false;
            }

            if (this.txtPassword.Text.Trim() != "SEIPAC")
            {
                MessageBox.Show("Password incorrecta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtPassword.Focus();
                return false;
            }

            return true;

        }

        private void BtnAceptar_Login_Click(object sender, EventArgs e)
        {

            if (Valida_Login() )
            {
                this.grpLogin.Visible = false;

                Inicia();
            }

        }

        private void BtnSalir_Login_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TxtPassword_KeyUp(object sender, KeyEventArgs e)
        {

            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                //this.SelectNextControl((Control)sender, true, true, true, true);
                BtnAceptar_Login_Click(sender, e);
            }


        }

        private void TxtID_Plan_Cuenta_KeyUp(object sender, KeyEventArgs e)
        {
            this.SelectNextControl((Control)sender, true, true, true, true);
        }

        private void CboPlan_Cuentas_KeyUp(object sender, KeyEventArgs e)
        {
            this.SelectNextControl((Control)sender, true, true, true, true);
        }

        private void TxtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            this.SelectNextControl((Control)sender, true, true, true, true);
        }

        private void TxtNivel_KeyUp(object sender, KeyEventArgs e)
        {
            this.SelectNextControl((Control)sender, true, true, true, true);
        }

        private void ChkAcepta_Movimientos_KeyUp(object sender, KeyEventArgs e)
        {
            this.SelectNextControl((Control)sender, true, true, true, true);

        }
    }

}
