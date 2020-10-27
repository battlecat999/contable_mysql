using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Contable
{
    public class Entidades
    {

        public static string CadenaConexion
        {
            get { return ConfigurationManager.ConnectionStrings["constr_MySql"].ConnectionString; }
        }

        public static string Provider
        {
            get { return ConfigurationManager.ConnectionStrings["conexion"].ProviderName; }
        }

        public static string CadenaConexionParametros
        {
            get { return ConfigurationManager.ConnectionStrings["Parametros"].ConnectionString; }
        }

        public static DbProviderFactory DbPF
        {
            get
            {
                return DbProviderFactories.GetFactory(Provider);
            }
        }

        public static int EjecutaNonQuery(string strStoredProcedures, DbParameter[] Parametros)
        {



            int ID = 0;

            try
            {
                using (DbConnection Con = DbPF.CreateConnection())
                {
                    Con.ConnectionString = CadenaConexion;

                    using (DbCommand cmd = DbPF.CreateCommand())
                    {
                        cmd.Connection = Con;
                        cmd.CommandText = strStoredProcedures;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Clear();

                        foreach (DbParameter Param in Parametros)
                            cmd.Parameters.Add(Param);

                        Con.Open();

                        ID = cmd.ExecuteNonQuery();

                        Con.Close();

                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
            finally
            {
                //MessageBox.Show("Proceso Finalizado.");
            }

            return ID;

        }
        public static int EjecutaNonQuery(string strStoredProcedures, MySqlParameter[] Parametros)
        {


         
            MySqlConnection databaseConnection = new MySqlConnection(CadenaConexion);
            MySqlCommand commandDatabase = new MySqlCommand(strStoredProcedures, databaseConnection);
            commandDatabase.CommandType = CommandType.StoredProcedure;
            commandDatabase.CommandTimeout = 60;
            commandDatabase.Parameters.AddRange(Parametros);
            MySqlDataReader reader;

            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();

                // Actualizado satisfactoriamente

                databaseConnection.Close();
                return 1;

            }
            catch (Exception ex)
            {
                
                return 0;

            }

        }


        public static int EjecutaNonQuery(string strStoredProcedures, List<DbParameter> Parametros)
        {
            int ID = 0;
            string str = string.Empty;
            try
            {
                using (DbConnection Con = DbPF.CreateConnection())
                {
                    Con.ConnectionString = CadenaConexion;
                    using (DbCommand cmd = DbPF.CreateCommand())
                    {
                        cmd.Connection = Con;
                        cmd.CommandText = strStoredProcedures;
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (DbParameter Param in Parametros)
                            cmd.Parameters.Add(Param);

                        Con.Open();
                        ID = cmd.ExecuteNonQuery();

                    }
                }
            }

            catch (Exception ex)
            {
                str = ex.ToString();
                throw;
                
            }
            finally
            {

            }

            return ID;

        }

        public static int Ejecuta_Consulta(string strConsulta)
        {
            int ID = 0;
            string str = string.Empty;


            MySqlConnection databaseConnection = new MySqlConnection(CadenaConexion);
            MySqlCommand commandDatabase = new MySqlCommand(strConsulta, databaseConnection);
            commandDatabase.CommandType = CommandType.Text;
            commandDatabase.CommandTimeout = 60;
   
            MySqlDataReader reader;

            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();

                // Actualizado satisfactoriamente

                databaseConnection.Close();
                return 1;

            }
            catch (Exception ex)
            {

                return 0;

            }


        }

        public static DataSet GetDataSet(String strSqlConsulta)
        {
            DataSet dataSet = new DataSet();
            DataTable dt = new DataTable();


            try
            {
                using (MySqlConnection con = new MySqlConnection(CadenaConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand(strSqlConsulta, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        //cmd.CommandText = strSqlConsulta;
                        //cmd.Parameters.AddWithValue("@idUsuario", "marco");
                        //cmd.Parameters.AddWithValue("@Pass", "1234");


                        // cmd.Parameters.Add(new MySqlParameter { MySqlDbType = MySqlDbType.Int32, ParameterName = "@intEmpresa", Value = 1 });

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {

                            sda.Fill(dt);
                            dataSet.Tables.Add(dt);

                            return dataSet;

                        }
                    }
                }

            }
            catch (Exception)
            {

                return dataSet;
            }

        }

        public static OleDbDataReader GetDataReader_Access(string strConsulta)
        {

            OleDbConnection conConexion_Access;   // create connection
            OleDbCommand cmdComando_Consulta_Access;  // create command
            OleDbDataReader drData_Reader;  //Dataread for read data from database

            string strConnection_Access = CadenaConexion;

            conConexion_Access = new OleDbConnection(strConnection_Access);

            cmdComando_Consulta_Access = new OleDbCommand(strConsulta, conConexion_Access);

            conConexion_Access.Open();

            drData_Reader = cmdComando_Consulta_Access.ExecuteReader();

            //conConexion_Access.Close();

            return drData_Reader;

        }
        

        public static OleDbDataReader GetDataReader_Access(string strConsulta, string strCadena_Conexion)
        {

            OleDbConnection conConexion_Access;   // create connection
            OleDbCommand cmdComando_Consulta_Access;  // create command
            OleDbDataReader drData_Reader;  //Dataread for read data from database

            //string strConnection_Access = CadenaConexion;
            string strConnection_Access = "";
                
            if (strCadena_Conexion.Trim() == "")
            {
                strConnection_Access = CadenaConexion;
            }
            else
            {
                strConnection_Access = strCadena_Conexion;
            }
                
            conConexion_Access = new OleDbConnection(strConnection_Access);

            cmdComando_Consulta_Access = new OleDbCommand(strConsulta, conConexion_Access);

            conConexion_Access.Open();

            drData_Reader = cmdComando_Consulta_Access.ExecuteReader();

            //conConexion_Access.Close();

            return drData_Reader;

        }

        public static DataTable GetDataTable(String strConsulta)
        {

            DataSet dataSet = new DataSet();
            DataTable dt = new DataTable();


            try
            {
                using (MySqlConnection con = new MySqlConnection(CadenaConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand(strConsulta, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        //cmd.CommandText = strSqlConsulta;
                        //cmd.Parameters.AddWithValue("@idUsuario", "marco");
                        //cmd.Parameters.AddWithValue("@Pass", "1234");


                        // cmd.Parameters.Add(new MySqlParameter { MySqlDbType = MySqlDbType.Int32, ParameterName = "@intEmpresa", Value = 1 });

                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {

                            sda.Fill(dt);
                            dataSet.Tables.Add(dt);

                            return dataSet.Tables[0];

                        }
                    }
                }

            }
            catch (Exception)
            {

                return new DataTable();
            }

            //DbDataAdapter dbDataAdapter = new SqlDataAdapter();
            //DataTable dataTable = new DataTable();

            //using (DbConnection con = DbPF.CreateConnection())
            //{
            //    con.ConnectionString = CadenaConexion;

            //    using (DbCommand commando = DbPF.CreateCommand())
            //    {
            //        commando.Connection = con;

            //        con.Open();

            //        DataSet dataSet = new DataSet();

            //        dbDataAdapter.SelectCommand = commando;
            //        dbDataAdapter.SelectCommand.CommandType = CommandType.Text;
            //        dbDataAdapter.SelectCommand.CommandTimeout = 0;
            //        dbDataAdapter.SelectCommand.CommandText = strConsulta;
            //        dbDataAdapter.SelectCommand.Connection = con;

            //        dbDataAdapter.Fill(dataSet);

            //        dataTable.EndLoadData();
            //        dataSet.EnforceConstraints = false;
            //        dataSet.Tables.Add(dataTable);

            //        return dataSet.Tables[0]; 

            //    }
            //}
        }

        public static DataTable GetDataTable(String strConsulta, string strConnectionString)
        {
            DbDataAdapter dbDataAdapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();

            using (DbConnection con = DbPF.CreateConnection())
            {
                con.ConnectionString = CadenaConexion;
                //con.ConnectionString = strConnectionString;

                using (DbCommand commando = DbPF.CreateCommand())
                {
                    commando.Connection = con;
                    commando.CommandTimeout = 0;

                    con.Open();

                    DataSet dataSet = new DataSet();

                    dbDataAdapter.SelectCommand = commando;
                    dbDataAdapter.SelectCommand.CommandType = CommandType.Text;
                    dbDataAdapter.SelectCommand.CommandText = strConsulta;
                    dbDataAdapter.SelectCommand.Connection = con;

                    dbDataAdapter.Fill(dataSet);

                    dataTable.EndLoadData();
                    dataSet.EnforceConstraints = false;
                    dataSet.Tables.Add(dataTable);

                    return dataSet.Tables[0];

                }
            }
        }


        public static DataTable GetDataTable_New(String strConsulta)
        {
            DbDataAdapter dbDataAdapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();

            //string strStoredProcedure = "up_carga_OTs";

            using (DbConnection con = DbPF.CreateConnection())
            {
                //con.ConnectionString = CadenaConexion_AutoPack;
                con.ConnectionString = CadenaConexion;

                using (DbCommand commando = DbPF.CreateCommand())
                {
                    commando.Connection = con;
                    //commando.CommandText = strStoredProcedure;
                    //commando.CommandType = CommandType.StoredProcedure;

                    con.Open();

                    DataSet dataSet = new DataSet();

                    dbDataAdapter.SelectCommand = commando;
                    dbDataAdapter.SelectCommand.CommandType = CommandType.Text;
                    dbDataAdapter.SelectCommand.CommandText = strConsulta;
                    dbDataAdapter.SelectCommand.Connection = con;
                    dbDataAdapter.SelectCommand.CommandTimeout = 0;

                    dbDataAdapter.Fill(dataSet);

                    dataTable.EndLoadData();
                    dataSet.EnforceConstraints = false;
                    dataSet.Tables.Add(dataTable);

                    return dataSet.Tables[0];

                }
            }
        }


    }
}