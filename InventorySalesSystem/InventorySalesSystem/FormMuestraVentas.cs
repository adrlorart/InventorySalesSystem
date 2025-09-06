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

namespace InventorySalesSystem
{
    public partial class FormMuestraVentas : Form
    {
        public FormMuestraVentas()
        {
            InitializeComponent();
        }

        private void FormVentasRegistradas_Load(object sender, EventArgs e)
        {
            // Cargar todas las ventas al iniciar
            CargarVentas();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarVentas(dateTimePickerDesde.Value, dateTimePickerHasta.Value);
        }

        private void CargarVentas(DateTime? desde = null, DateTime? hasta = null)
        {
            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                string query = @"
                    SELECT v.id_venta, c.nombre + ' ' + c.apellido AS Cliente, 
                           v.fecha, v.total
                    FROM Ventas v
                    INNER JOIN Clientes c ON v.id_cliente = c.id_cliente
                    WHERE 1=1";

                if (desde.HasValue && hasta.HasValue)
                {
                    query += " AND v.fecha BETWEEN @desde AND @hasta";
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (desde.HasValue && hasta.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@desde", desde.Value.Date);
                        cmd.Parameters.AddWithValue("@hasta", hasta.Value.Date.AddDays(1).AddSeconds(-1));
                    }

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable tabla = new DataTable();
                    da.Fill(tabla);
                    dgvMuestraVentas.DataSource = tabla;
                }
            }
        }

    }
}
