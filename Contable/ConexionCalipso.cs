using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Odbc;

namespace Contable
{

    public class ConexionCalipso
    {
        private OdbcConnection cadenaODBC=new OdbcConnection("DSN=seipacpg");

        public DataTable getTablas_Calipso(string strConsulta)
        {
            DataSet ds = new DataSet();
            OdbcDataAdapter da = new OdbcDataAdapter();
            DataTable dt = new DataTable();
            da.SelectCommand = new OdbcCommand(strConsulta, cadenaODBC);
            da.Fill(ds, "tTabla");
            dt = ds.Tables[0];
            cadenaODBC.Close();
            return dt;
            
         }
        public bool AbrirCalipso()
        {
            bool OK;
            cadenaODBC.Close();
            string str = string.Empty;
            try
            {
                OdbcCommand cmmd = new OdbcCommand() ;
                cmmd.Connection = cadenaODBC;
                cmmd.CommandTimeout = 1200;

                OK = true;  
            }
            catch (Exception ex)
            {
                OK = false;
                str = ex.ToString();
            }
            finally
            {

            }
            return OK;
        }





    }
}
