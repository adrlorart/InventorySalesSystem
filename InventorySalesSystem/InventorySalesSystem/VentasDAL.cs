using System;
using System.Data.SqlClient;

namespace InventorySalesSystem
{
    internal class VentasDAL
    {
        public static int AgregarVenta(int idCliente, decimal total, DateTime? fechaEntrega)
        {

            int idVenta = 0;

            // Código SQL INSERT
            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = @"INSERT INTO Ventas (id_cliente, fecha, total, fecha_entrega)
                               VALUES (@id_cliente, @fecha, @total, @fecha_entrega);
                               SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@id_cliente", idCliente);
                    cmd.Parameters.AddWithValue("@fecha", DateTime.Now); // Fecha actual
                    cmd.Parameters.AddWithValue("@total", total);

                    //Si hay una fecha de entrega, la usamos, si no, mandamos null
                    if (fechaEntrega.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@fecha_entrega", fechaEntrega.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@fecha_entrega", DBNull.Value);
                    }

                    idVenta = Convert.ToInt32(cmd.ExecuteScalar());

                }
            }
            return idVenta;
        }

        public static void AgregarDetalleVenta(int idVenta, int idProducto, int cantidad, decimal precioUnit)
        {
            // Código para insertar cada producto en DetalleVentas
            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = @"INSERT INTO DetalleVentas (id_venta, id_producto, cantidad, precio_unit)
                         VALUES (@id_venta, @id_producto, @cantidad, @precio_unit);";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_venta", idVenta);
                    cmd.Parameters.AddWithValue("@id_producto", idProducto);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@precio_unit", precioUnit);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void ActualizarExistencias(int idProducto, int cantidadVendida)
        {
            // Código para restar del inventario
            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = @"UPDATE Productos
                         SET exist = exist - @cantidad
                         WHERE id_producto = @id_producto;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@cantidad", cantidadVendida);
                    cmd.Parameters.AddWithValue("@id_producto", idProducto);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void ActualizarVenta(int idVenta, decimal total, DateTime? fechaEntrega)
        {
            // Código SQL UPDATE
            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = @"UPDATE Ventas
                         SET total = @total, fecha_entrega = @fecha_entrega
                         WHERE id_venta = @id_venta;";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@total", total);
                    cmd.Parameters.AddWithValue("@fecha_entrega", fechaEntrega.HasValue ? (object)fechaEntrega.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@id_venta", idVenta);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void EliminarVenta(int idVenta)
        {
            // Código SQL DELETE
            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = @"DELETE FROM Ventas WHERE id_venta = @id_venta;";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_venta", idVenta);
                    cmd.ExecuteNonQuery();
                }
            }

        }
        public static void EliminarDetalleVenta(int idVenta)
        {
            // Código SQL DELETE para eliminar los detalles de la venta
            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = @"DELETE FROM DetalleVentas WHERE id_venta = @id_venta;";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_venta", idVenta);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void EliminarDetalleVentaPorProducto(int idVenta, int idProducto)
        {
            // Código SQL DELETE para eliminar un detalle específico de la venta
            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = @"DELETE FROM DetalleVentas WHERE id_venta = @id_venta AND id_producto = @id_producto;";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_venta", idVenta);
                    cmd.Parameters.AddWithValue("@id_producto", idProducto);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void ValidarExistencias(int idProducto, int cantidad)
        {
            // Código SQL para validar existencias
            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = @"SELECT exist FROM Productos WHERE id_producto = @id_producto;";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_producto", idProducto);
                    int existencias = Convert.ToInt32(cmd.ExecuteScalar());
                    if (existencias < cantidad)
                    {
                        throw new InvalidOperationException("No hay suficientes existencias para completar la venta.");
                    }
                }
            }
        }
    }
}
