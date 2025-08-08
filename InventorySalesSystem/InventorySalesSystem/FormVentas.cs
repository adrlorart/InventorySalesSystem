using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace InventorySalesSystem
{
    public partial class FormVentas : Form
    {
        private List<(int idProducto, string nombre, decimal precio, int cantidad)> listaVenta = new List<(int idProducto, string nombre, decimal precio, int cantidad)>(); private decimal totalVenta = 0;
        public FormVentas()
        {
            InitializeComponent();
        }

        private void FormVentas_Load(object sender, EventArgs e)
        {
            CargarClientes();
            CargarProductos();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbBoxProducto.SelectedItem is KeyValuePair<int, string> seleccionado &&
                    int.TryParse(txtCantidad.Text, out int cantidad) && cantidad > 0)
                {
                    int idProducto = seleccionado.Key;
                    string nombre = seleccionado.Value;
                    decimal precioUnitario = ProductoDAL.ObtenerPrecio(idProducto);
                    int existencias = ProductoDAL.ObtenerExistencias(idProducto);

                    if (cantidad > existencias)
                    {
                        MessageBox.Show("No hay suficientes existencias.");
                        return;
                    }

                    listaVenta.Add((idProducto, nombre, precioUnitario, cantidad));
                    totalVenta += precioUnitario * cantidad;

                    dgvVenta.Rows.Add(nombre, precioUnitario, cantidad, precioUnitario * cantidad);
                    label5Total.Text = $"Total: ${totalVenta:F2}";
                }
                else
                {
                    MessageBox.Show("Selecciona un producto válido y una cantidad mayor que 0.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar producto: " + ex.Message);
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (listaVenta.Count == 0)
                {
                    MessageBox.Show("Agrega productos a la venta primero.");
                    return;
                }

                if (cbBoxCliente.SelectedValue == null)
                {
                    MessageBox.Show("Selecciona un cliente.");
                    return;
                }

                foreach (var item in listaVenta)
                {
                    VentasDAL.ValidarExistencias(item.idProducto, item.cantidad);
                }

                int idCliente = Convert.ToInt32(cbBoxCliente.SelectedValue);
                int idVenta = VentasDAL.AgregarVenta(idCliente, totalVenta, null);

                foreach (var item in listaVenta)
                {
                    VentasDAL.AgregarDetalleVenta(idVenta, item.idProducto, item.cantidad, item.precio);
                    VentasDAL.ActualizarExistencias(item.idProducto, item.cantidad);
                }

                MessageBox.Show("Venta registrada exitosamente.");

                // Limpiar todo
                listaVenta.Clear();
                dgvVenta.Rows.Clear();
                totalVenta = 0;
                label5Total.Text = "Total: $0.00";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar la venta: " + ex.Message);
            }
        }

        // Método para cargar los productos en el ComboBox
        private void CargarProductos()
        {
            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();
                string query = "SELECT id_producto, nombre FROM Productos";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Dictionary<int, string> productos = new Dictionary<int, string>();

                    while (reader.Read())
                    {
                        productos.Add(reader.GetInt32(0), reader.GetString(1));
                    }

                    cbBoxProducto.DataSource = new BindingSource(productos, null);
                    cbBoxProducto.DisplayMember = "Value";
                    cbBoxProducto.ValueMember = "Key";
                }
            }
        }

        // Método para cargar los clientes en el ComboBox
        private void CargarClientes()
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString;
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                string query = "SELECT id_cliente, nombre FROM Clientes";
                SqlCommand cmd = new SqlCommand(query, conexion);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cbBoxCliente.DataSource = dt;
                cbBoxCliente.DisplayMember = "nombre";
                cbBoxCliente.ValueMember = "id_cliente";
            }
        }
        //Eventos no utilizados
        private void cbBoxProducto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void cbBoxCliente_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dgvVenta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
