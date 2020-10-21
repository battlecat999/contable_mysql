using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace Contable
{
    class ValidarUsuario
    {
        //Encapusalmiento variables
        private querys_base_datos objDato = new querys_base_datos ();//instancia a la capa datos de empleados
                                                //variables
        private string _Usuario;
        private string _Pass;
        //todas las demas....
        // METODOS GET Y SET -->para el manejo de variables.
        public string Usuario
        {
            //set { _Usuario = value; }
            //get { return _Usuario; }
            set
            {
                if (value == "USUARIO") { _Usuario = "Ingrese su Usuario."; }
                else { _Usuario = value; }
            }
            get { return _Usuario; }
        }
        public string Password
        {
            set
            {
                if (value == "CONTRASEÑA") { _Pass = "Ingrese su Contraseña"; }
                else
                {
                    _Pass = value;
                }
            }
            get { return _Pass; }
        }
        //CONSTRUCTOR
        //public DNUsers(){ }
        public DataTable iniciarSesion()
        {
            DataTable Loguear;
            Loguear = objDato.iniciarSesion(Usuario, Password);//el nombre del get y set

            return Loguear;

        }
    }
}
