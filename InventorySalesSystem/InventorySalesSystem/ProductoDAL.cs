using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySalesSystem
{
    internal class ProductoDAL
    {
        // Método para Agregar un nuevo producto
        public static void AgregarProducto(string nombre, string categoria, string descrip, decimal precio_unit, int exist)
        {
            // Código SQL INSERT
            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                string query = "INSERT INTO Productos (nombre, categoria, descrip, precio_unit, exist) VALUES (@nombre, @categoria, @descrip, @precio_unit, @exist)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@categoria", categoria);
                cmd.Parameters.AddWithValue("@descrip", descrip);
                cmd.Parameters.AddWithValue("@precio_unit", precio_unit);
                cmd.Parameters.AddWithValue("@exist", exist);
                cmd.ExecuteNonQuery();
            }
        }

        // Método para Consultar
        public static DataTable ObtenerProductos()
        {
            // Código SQL SELECT
            DataTable tabla = new DataTable();
            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                string query = "SELECT id_producto, nombre, categoria, descrip, precio_unit, exist FROM Productos";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(tabla);
            }
            return tabla;
        }

        // Método para Actualizar Producto
        public static void ActualizarProducto(int id_producto, string nombre, string categoria, string descrip, decimal precio_unit, int exist)
        {
            // Código SQL UPDATE
            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                string query = "UPDATE Productos SET nombre=@nombre, categoria=@categoria, descrip=@descrip, precio_unit=@precio_unit, exist=@exist WHERE id_producto=@id_producto";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@categoria", categoria);
                cmd.Parameters.AddWithValue("@descrip", descrip);
                cmd.Parameters.AddWithValue("@precio_unit", precio_unit);
                cmd.Parameters.AddWithValue("@exist", exist);
                cmd.Parameters.AddWithValue("@id_producto", id_producto);
                cmd.ExecuteNonQuery();
            }
        }

        //Metodo para eliminar un producto existente
        public static void EliminarProducto(int id_producto)
        {
            // Código SQL DELETE
            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                string query = "DELETE FROM Productos WHERE id_producto=@id_prducto";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id_producto", id_producto);
                cmd.ExecuteNonQuery();
            }
        }

        //Metodo para evitar duplicados
        public static bool ExisteProducto(string nombre)
        {
            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {

                string query = "SELECT COUNT(*) FROM Productos WHERE nombre = @nombre";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nombre", nombre);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        // Método para obtener el precio de un producto por su ID
        public static decimal ObtenerPrecio(int id_producto)
        {
            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = "SELECT precio_unit FROM Productos WHERE id_producto = @id_producto";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_producto", id_producto);
                    return Convert.ToDecimal(cmd.ExecuteScalar());
                }
            }
        }

        public static int ObtenerExistencias(int id_producto)
        {
            int existencias = 0;
            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                string query = "SELECT Existencias FROM Productos WHERE id_producto = @id_productoo";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id_producto", id_producto);

                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    existencias = Convert.ToInt32(result);
                }
            }
            return existencias;
        }


    }
}
