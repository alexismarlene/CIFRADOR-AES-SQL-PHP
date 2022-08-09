using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace EncriptacionAES.Clases
{
    public class BLL
    {
        public static int insertar_datos(string nombre, string telefono, string cargo, string numerointerno, string Archivo, string Password)
        {
            string cadena = "insert into ClientesPrueba(Nombre,Telefono,Cargo,NumeroInterno,Archivo,Password) values ('" + nombre + "','" + telefono + "','" + cargo + "','" + numerointerno + "','" + Archivo + "','" + Password + "')";
            return DAL.ExeNonquery(cadena);

        }

        public static DataTable Ver_Datos()
        {
            string cadena = "Select IdEmpleado, Nombre,Telefono,Cargo,NumeroInterno,LTRIM(RTRIM(Substring(Archivo,15,50))) Archivo, Archivo ArchivoCompleto, Password from ClientesPrueba";
            return DAL.DatosTabla(cadena);

        }
    }
}