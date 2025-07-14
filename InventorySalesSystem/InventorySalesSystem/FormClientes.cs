using System;
using System.Linq;
using System.Windows.Forms;

namespace InventorySalesSystem
{
    public partial class FormClientes : Form
    {
        public FormClientes()
        {
            InitializeComponent();
            CargarClientes();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void CargarClientes()
        {
            dgvClientes.DataSource = ClienteDAL.ObtenerClientes();
        }
        //config boton guardar
        private void BtnGuardar_Click(object sender, EventArgs e)
        {

            // Validación
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MessageBox.Show("Los campos Nombre, Apellido y Teléfono son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar que nombre y apellido no tengan números
            if (txtNombre.Text.Any(char.IsDigit) || txtApellido.Text.Any(char.IsDigit))
            {
                MessageBox.Show("El nombre y el apellido no deben contener números.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar que el teléfono tenga solo números
            if (!txtTelefono.Text.All(char.IsDigit))
            {
                MessageBox.Show("El campo Teléfono debe contener solo números.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            ClienteDAL.AgregarCliente(
            txtNombre.Text,
            txtApellido.Text,
            txtEmail.Text,
            txtTelefono.Text,
            txtDireccion.Text);

            MessageBox.Show("Cliente agregado exitosamente.");
            LimpiarCampos();
            CargarClientes();
        }
        //config boton actualizar
        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtId.Text))
            {

                // Validación
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtApellido.Text) ||
                    string.IsNullOrWhiteSpace(txtTelefono.Text))
                {
                    MessageBox.Show("Los campos Nombre, Apellido y Teléfono son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Verificar que nombre y apellido no tengan números
                if (txtNombre.Text.Any(char.IsDigit) || txtApellido.Text.Any(char.IsDigit))
                {
                    MessageBox.Show("El nombre y el apellido no deben contener números.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Verificar que el teléfono tenga solo números
                if (!txtTelefono.Text.All(char.IsDigit))
                {
                    MessageBox.Show("El campo Teléfono debe contener solo números.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }



                ClienteDAL.ActualizarCliente(
                    int.Parse(txtId.Text),
                    txtNombre.Text,
                    txtApellido.Text,
                    txtEmail.Text,
                    txtTelefono.Text,
                    txtDireccion.Text
                );

                MessageBox.Show("Cliente actualizado exitosamente.");
                LimpiarCampos();
                CargarClientes();
            }
            else
            {
                MessageBox.Show("Selecciona un cliente para actualizar.");
            }
        }

        //config boton eliminar

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtId.Text))
            {
                ClienteDAL.EliminarCliente(int.Parse(txtId.Text));
                MessageBox.Show("Cliente eliminado exitosamente.");
                LimpiarCampos();
                CargarClientes();
            }
            else
            {
                MessageBox.Show("Selecciona un cliente para eliminar.");
            }
        }

        //config boton limpiar
        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        //dvgCLientes
        private void DgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvClientes.Rows[e.RowIndex];
                txtId.Text = row.Cells["id_cliente"].Value.ToString();
                txtNombre.Text = row.Cells["nombre"].Value.ToString();
                txtApellido.Text = row.Cells["apellido"].Value.ToString();
                txtEmail.Text = row.Cells["email"].Value.ToString();
                txtTelefono.Text = row.Cells["telefono"].Value.ToString();
                txtDireccion.Text = row.Cells["direccion"].Value.ToString();
            }
        }


        private void label5_Click(object sender, EventArgs e)
        {

        }
        //id
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //limpiar campos
        private void LimpiarCampos()
        {
            txtId.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEmail.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";
        }
        //validación de caudro de texto para nombre
        /*private void TxtNombre_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //aplicable para todos?
            // Validación
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MessageBox.Show("Los campos Nombre, Apellido y Teléfono son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar que nombre y apellido no tengan números
            if (txtNombre.Text.Any(char.IsDigit) || txtApellido.Text.Any(char.IsDigit))
            {
                MessageBox.Show("El nombre y el apellido no deben contener números.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar que el teléfono tenga solo números
            if (!txtTelefono.Text.All(char.IsDigit))
            {
                MessageBox.Show("El campo Teléfono debe contener solo números.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            /*if (String.IsNullOrWhiteSpace(txtNombre.Text))
            {
                e.Cancel   = true;
                txtNombre.Focus();
                errorProvider1.SetError(txtNombre, "Introduce tu nombre");


            }
            else
            {
                e.Cancel= false;
                errorProvider1.SetError(txtNombre, null);
            }
        }*/
    }
}
