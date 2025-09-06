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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Button1_Click(object sender, EventArgs e)
        {

            string connectionString = @"Server=localhost;Database=InventorySalesDB;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MessageBox.Show("¡Conexión exitosa!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        }
        private void Button2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormClientes());
            
            

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormProductos());


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)//LabelMensajeInicio
        {

        }

        private void btnVenta_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormVentas());
        }

        private void panelMenu_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void OpenChildForm(Form childForm)
        {
            OpenChildForm(childForm, ActiveMdiChild);
        }

        private void OpenChildForm(Form childForm, Form activeMdiChild)
        {
            // Cerrar el formulario hijo actual si existe
            if (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Close();
            }

            activeMdiChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktop.Controls.Add(childForm);
            this.panelDesktop.Tag = childForm;
            // Mostrar el formulario hijo
            childForm.BringToFront();
            childForm.Show();
            lblHome.Text = childForm.Text;
        }

        private void panelDesktop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblHome_Click(object sender, EventArgs e)
        {

        }

        private Form activeForm = null;
        private void button4_Click(object sender, EventArgs e)
        {
            // Cierra el formulario hijo que esté activo en el panel
            if (this.panelDesktop.Controls.Count > 0)
            {
                // Obtiene el primer (y único) control en el panel (el formulario hijo)
                Form childForm = this.panelDesktop.Controls[0] as Form;
                if (childForm != null)
                {
                    childForm.Close();
                }
            }
            lblHome.Text = "INICIO"; // texto de pantalla de inicio

        }
    }
}
