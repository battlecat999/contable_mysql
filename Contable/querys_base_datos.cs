using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using k_loger;


namespace Contable
{
    class querys_base_datos
    {
        

        private DataTable  leer;

        public DataTable iniciarSesion(string user, string pass)
        {
            //MD5 o = new MD5();
            //pass = o.GetMd5Hash(pass);
            DataSet dsUsuarios = new DataSet("Usuarios") ;
            //SqlCommand cmmd = new SqlCommand("SP_IniciarSesion", Conexion.AbrirConexion());
            //cmmd.CommandType = System.Data.CommandType.StoredProcedure;
            //cmmd.Parameters.AddWithValue("@_userName", user);
            //cmmd.Parameters.AddWithValue("@_passName", pass);

            string sql = string.Concat("select * from Usuarios where idUsuario='",user,"' AND Password='",pass,"'");
            dsUsuarios = Entidades.GetDataSet(sql);
            leer = dsUsuarios.Tables[0];
            //Conexion.CerrarConexion();
            return leer;
            

        }
    }
}
