using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySalesSystem
{
    internal class ConexionBD
    {
        private static string cadenaConexion = "Server=ADRI_POMPU;Database=InventorySalesDB;Trusted_Connection=True;";

        public static SqlConnection ObtenerConexion()
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();
            return conexion;
        }
    }
}
