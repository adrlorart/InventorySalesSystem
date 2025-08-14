using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static InventorySalesSystem.Entidades;

namespace InventorySalesSystem
{
    public partial class FormVentas : Form
    {
        private List<(int id_producto, string nombre, decimal precio, int cantidad)> listaVenta = new List<(int id_producto, string nombre, decimal precio, int cantidad)>(); private decimal totalVenta = 0;
        public FormVentas()
        {
            InitializeComponent();
        }

        private void FormVentas_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
            CargarClientes();
            CargarProductos();
        }

               

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbBoxProducto.SelectedValue != null && int.TryParse(txtCantidad.Text, out int cantidad) && cantidad > 0)
                {
                    int id_producto = Convert.ToInt32(cbBoxProducto.SelectedValue);
                    string nombre = cbBoxProducto.Text;
                    decimal precio_unit = ProductoDAL.ObtenerPrecio(id_producto);
                    int exist = ProductoDAL.ObtenerExistencias(id_producto);

                    if (cantidad > exist)
                    {
                        MessageBox.Show("No hay suficientes existencias.");
                        return;
                    }
                    dgvVenta.Rows.Add(nombre, precio_unit, cantidad, precio_unit * cantidad);
                    listaVenta.Add((id_producto, nombre, precio_unit, cantidad));
                    totalVenta += precio_unit * cantidad;
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
                    VentasDAL.ValidarExistencias(item.id_producto, item.cantidad);
                }

                int id_cliente = Convert.ToInt32(cbBoxCliente.SelectedValue);
                int id_venta = VentasDAL.AgregarVenta(id_cliente, totalVenta, null);

                foreach (var item in listaVenta)
                {
                    VentasDAL.AgregarDetalleVenta(id_venta, item.id_producto, item.cantidad, item.precio);
                    VentasDAL.ActualizarExistencias(item.id_producto, item.cantidad);
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


        private void CargarClientes()
        {
            cbBoxCliente.DataSource = ClienteDAL.ObtenerClientes();
            cbBoxCliente.DisplayMember = "nombre";
            cbBoxCliente.ValueMember = "id_cliente";
        }

        private void CargarProductos()
        {
            cbBoxProducto.DataSource = ProductoDAL.ObtenerProductos();
            cbBoxProducto.DisplayMember = "nombre";
            cbBoxProducto.ValueMember = "id_producto";
        }

        private void ConfigurarGrid()
        {
            dgvVenta.Columns.Clear();
            dgvVenta.AutoGenerateColumns = false;

            dgvVenta.Columns.Add("producto", "Producto");
            dgvVenta.Columns.Add("precio_unit", "Precio unitario");
            dgvVenta.Columns.Add("cantidad", "Cantidad");
            dgvVenta.Columns.Add("total", "Total");
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
