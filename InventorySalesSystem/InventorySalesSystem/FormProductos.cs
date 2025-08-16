using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySalesSystem
{
    public partial class FormProductos : Form
    {
        public FormProductos()
        {
            InitializeComponent();
            CargarProductos();
        }
        private void CargarProductos()
        {
            dgvProductos.DataSource = ProductoDAL.ObtenerProductos();

        }

        //congiguración botón añadir producto --Eventos--
        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtCategoria.Text) ||
                string.IsNullOrWhiteSpace(txtDescrip.Text) ||
                string.IsNullOrWhiteSpace(txtPrecio_unit.Text) ||
                string.IsNullOrWhiteSpace(txtExist.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar que ningun string tenga números
            if (txtNombre.Text.Any(char.IsDigit) || txtCategoria.Text.Any(char.IsDigit))
            {
                MessageBox.Show("Los elementos marcados con * solo permiten texto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar que precio y existencias tenga solo números
            if (!txtPrecio_unit.Text.All(char.IsDigit) || !txtExist.Text.All(char.IsDigit))
            {
                MessageBox.Show("Los campos de Precio unitario y Existencias solo deben contener numeros.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar duplicados por nombre
            if (ProductoDAL.ExisteProducto(txtNombre.Text.Trim()))
            {
                MessageBox.Show("Este producto ya está registrado.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar y convertir precio unitario
            if (!decimal.TryParse(txtPrecio_unit.Text.Trim(), out decimal precio_unit))
            {
                MessageBox.Show("Ingresa un precio válido (ej. 10.50).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validar y convertir existencias
            if (!int.TryParse(txtExist.Text.Trim(), out int exist))
            {
                MessageBox.Show("Ingresa una cantidad válida de existencias (solo números enteros).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ProductoDAL.AgregarProducto(
            txtNombre.Text,
            txtCategoria.Text,
            txtDescrip.Text,
            precio_unit,
            exist);

            MessageBox.Show("Producto agregado exitosamente.");
            LimpiarCampos();
            CargarProductos();
        }

        //config boton actualizar
        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtId_producto.Text))
            {

                
                // Validaciones
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtCategoria.Text) ||
                    string.IsNullOrWhiteSpace(txtDescrip.Text) ||
                    string.IsNullOrWhiteSpace(txtPrecio_unit.Text) ||
                    string.IsNullOrWhiteSpace(txtExist.Text))
                {
                    MessageBox.Show("Todos los campos son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Verificar que ningun string tenga números
                if (txtNombre.Text.Any(char.IsDigit) || txtCategoria.Text.Any(char.IsDigit))
                {
                    MessageBox.Show("Los elementos marcados con * solo permiten texto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Verificar que precio y existencias tenga solo números
                if (!txtPrecio_unit.Text.All(char.IsDigit) || !txtExist.Text.All(char.IsDigit))
                {
                    MessageBox.Show("Los campos de Precio unitario y Existencias solo deben contener numeros.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Verificar duplicados por nombre
                /*if (ProductoDAL.ExisteProducto(txtNombre.Text.Trim()))
                {
                    MessageBox.Show("Este producto ya está registrado.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }*/

                // Validar y convertir precio unitario
                if (!decimal.TryParse(txtPrecio_unit.Text.Trim(), out decimal precio_unit))
                {
                    MessageBox.Show("Ingresa un precio válido (ej. 10.50).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validar y convertir existencias
                if (!int.TryParse(txtExist.Text.Trim(), out int exist))
                {
                    MessageBox.Show("Ingresa una cantidad válida de existencias (solo números enteros).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ProductoDAL.ActualizarProducto(
                    int.Parse(txtId_producto.Text),
                    txtNombre.Text,
                    txtCategoria.Text,
                    txtDescrip.Text,
                    precio_unit,
                    exist
                );

                MessageBox.Show("Producto actualizado exitosamente.");
                LimpiarCampos();
                CargarProductos();
            }
            else
            {
                MessageBox.Show("Selecciona un producto para actualizar.");
            }
        }



        //congiguracion boton eliminar producto
        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            // Confirmación
            var confirmResult = MessageBox.Show("¿Estás seguro de que deseas eliminar este producto?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.No)
                return;//aqui termina el mensaje de confirmacion

            if (!string.IsNullOrEmpty(txtId_producto.Text))
            {
                ProductoDAL.EliminarProducto(int.Parse(txtId_producto.Text));
                MessageBox.Show("Produto eliminado exitosamente.");
                LimpiarCampos();
                CargarProductos();

            }
            else
            {
                MessageBox.Show("Selecciona un producto para eliminar.");
            }
        }

        //dvgProductos
        private void DgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvProductos.Rows[e.RowIndex];
                txtId_producto.Text = row.Cells["id_producto"].Value.ToString();
                txtNombre.Text = row.Cells["nombre"].Value.ToString();
                txtCategoria.Text = row.Cells["categoria"].Value.ToString();
                txtDescrip.Text = row.Cells["descrip"].Value.ToString();
                txtPrecio_unit.Text = row.Cells["precio_unit"].Value.ToString();
                txtExist.Text = row.Cells["exist"].Value.ToString();
            }
        }

        //limpiar campos
        private void LimpiarCampos()
        {
            txtId_producto.Text = "";
            txtNombre.Text = "";
            txtCategoria.Text = "";
            txtDescrip.Text = "";
            txtPrecio_unit.Text = "";
            txtExist.Text = "";
        }

        

        private void txtId_producto_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void FormProductos_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        
    }
    
}
