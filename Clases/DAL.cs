using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace EncriptacionAES.Clases
{
    public class DAL
    {
        public static int ExeNonquery(string cadena)
        {
            try
            {
                SqlConnection con = new SqlConnection(clsConexionBD.Conexion);
                SqlCommand cmd = new SqlCommand(cadena, con);
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return 1;
            }
            catch (Exception ex)
            {

                return 0;
            }
            
        }
        public static DataTable DatosTabla(string cadena)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(clsConexionBD.Conexion);
            SqlDataAdapter dat = new SqlDataAdapter(cadena, con);
            dat.Fill(dt);
            return dt;
        }
        public static string PruebaConexion()
        {
            
            try
            {
                SqlConnection con = new SqlConnection(clsConexionBD.Conexion);
                SqlCommand cmd = new SqlCommand("prueba",con);
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
                cmd.Connection.Open();
                cmd.Connection.Close();
                return "La Conexión a la Base de Datos es Exitosa";
            }
            catch (Exception ex)
            {

                return "La Conexión fue fallida se requiere revisar nuevamente la Cadena de conexión";
            }
            
        }
    }
}