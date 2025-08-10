using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace InventorySalesSystem
{
    internal class ClienteDAL
    {
        // Método para Insertar
        public static void AgregarCliente(string nombre, string apellido, string email, string telefono, string direccion)
        {
            // Código SQL INSERT
            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = "INSERT INTO Clientes (nombre, apellido, email, telefono, direccion) VALUES (@nombre, @apellido, @email, @telefono, @direccion)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@apellido", apellido);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@telefono", telefono);
                cmd.Parameters.AddWithValue("@direccion", direccion);
                cmd.ExecuteNonQuery();
            }
        }

        // Método para Consultar
        public static DataTable ObtenerClientes()
        {
            // Código SQL SELECT
            DataTable tabla = new DataTable();
            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = "SELECT id_cliente, nombre, apellido, email, telefono, direccion FROM Clientes";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(tabla);
            }
            return tabla;
        }

        // Método para Actualizar
        public static void ActualizarCliente(int id_cliente, string nombre, string apellido, string email, string telefono, string direccion)
        {
            // Código SQL UPDATE
            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = "UPDATE Clientes SET nombre=@nombre, apellido=@apellido, email=@email, telefono=@telefono, direccion=@direccion WHERE id_cliente=@id_cliente";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@apellido", apellido);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@telefono", telefono);
                cmd.Parameters.AddWithValue("@direccion", direccion);
                cmd.Parameters.AddWithValue("@id_cliente", id_cliente);
                cmd.ExecuteNonQuery();
            }
        }

        // Método para Eliminar
        public static void EliminarCliente(int id_cliente)
        {
            // Código SQL DELETE
            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = "DELETE FROM Clientes WHERE id_cliente=@id_cliente";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id_cliente", id_cliente);
                cmd.ExecuteNonQuery();
            }
        }

        //Metodo para evitar duplicados
        //en base a nombre y apellido
        public static bool ExisteClienteNyA(string nombre, string apellido)
        {
            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Clientes WHERE nombre = @nombre AND apellido = @apellido";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@apellido", apellido);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
        //en base a telefono
        public static bool ExisteClienteTel(string telefono)
        {
            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Clientes WHERE telefono = @telefono";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@telefono", telefono);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

    }
}
