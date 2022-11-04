using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;


namespace Gestion_Ciber_Cafe_GUI
{
    public partial class Clientes : Form

    {
        int p = -1;
        Entidades.Cliente cliente = new Entidades.Cliente();
        Logica.ServicioCliente servicioCliente = new Logica.ServicioCliente();
        public Clientes()
        {
            InitializeComponent();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        void Limpiar()
        {
            txtcedula.Text = " ";
            txtnombre.Text = " ";
            txtTelefono.Text = " ";
            txtDireccion.Text = " ";
            txtCorreo.Text = " ";
        }
        void Guardar()
        {
            if (txtcedula.Text == "" || txtnombre.Text == "" || txtTelefono.Text == " " || txtDireccion.Text == " " || txtCorreo.Text == " ")
            {
                MessageBox.Show("Llene todos los campos, por favor");
            }
            else
            {
                if (p== -1)
                {
                    cliente.Cedula = int.Parse(txtcedula.Text);
                    cliente.Nombre = txtnombre.Text;
                    cliente.Telefono = txtTelefono.Text;
                    cliente.Direccion = txtDireccion.Text;
                    cliente.Correo = txtCorreo.Text;
                    var Respuesta = MessageBox.Show("Desea guardar el cliente?", "Responde...", MessageBoxButtons.YesNoCancel);
                    if (Respuesta == DialogResult.Yes)
                    {
                        var mensaje = servicioCliente.Guardar(cliente);
                        MessageBox.Show(mensaje);
                        Refres();
                    }
                }
                else
                {
                    var Respuesta = MessageBox.Show("Desea modifcar el cliente?", "Responde...", MessageBoxButtons.YesNoCancel);
                    if (Respuesta == DialogResult.Yes)
                    {
                        Editar();
                    }
                    Limpiar();
                    p = -1;
                }


            }

        }
        //void CargarTabla()
        //{
        //    foreach (var item in servicioCliente.GetAll())
        //    {
        //        dataGridView1.Rows.Add(item.Cedula, item.Nombre, item.Telefono, item.Direccion, item.Correo);
        //    }
        //}

        private void Clientes_Load(object sender, EventArgs e)
        {
            Refres();
            txtcedula.Focus();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            p = e.RowIndex;
            if (p != -1)
            {
                VerClientes(servicioCliente.GetAll()[p]);
            }
            tabControl1.SelectedIndex = 0;
        }
        void VerClientes(Entidades.Cliente cliente)
        {
            txtcedula.Text = Convert.ToString(cliente.Cedula);
            txtnombre.Text = cliente.Nombre;
            txtTelefono.Text = cliente.Telefono;
            txtDireccion.Text = cliente.Direccion;
            txtCorreo.Text = cliente.Correo;
        }
        void Refres()
        {
            dataGridView1.DataSource = servicioCliente.GetAll();
        }
        void Editar()
        {
            cliente.Cedula = int.Parse(txtcedula.Text);
            cliente.Nombre = txtnombre.Text;
            cliente.Telefono = txtTelefono.Text;
            cliente.Direccion = txtDireccion.Text;
            cliente.Correo = txtCorreo.Text;
            var mensaje = servicioCliente.Edit(cliente);
            MessageBox.Show(mensaje);
            Limpiar();
            txtcedula.Focus();
            Refres();
        }

        private void txtcedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtnombre.Focus();
            }
        }

        private void txtcedula_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Down)
            {
                txtnombre.Focus();
            }
        }

        private void txtnombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtTelefono.Focus();
            }
        }

        private void txtnombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                txtcedula.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                txtTelefono.Focus();
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtDireccion.Focus();
            }
        }

        private void txtTelefono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                txtnombre.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                txtDireccion.Focus();
            }
        }

        private void txtDireccion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                txtTelefono.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                txtCorreo.Focus();
            }
        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtCorreo.Focus();
            }
        }

        private void txtCorreo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnGuardar_Click(sender, e);
            }
        }
        private void txtCorreo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                txtDireccion.Focus();
            }
        }

        private void Clientes_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void btnimprimir_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = string.Format("{0}.pdf", DateTime.Now.ToString("ddMMyyyyHHmmss"));


            string pagimahtml_texto = Properties.Resources.pag.ToString();
            pagimahtml_texto = pagimahtml_texto.Replace("@FECHA", DateTime.Now.ToString("dd/MM/yyyy"));


            string filas = string.Empty;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["Cedula"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Nombre"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Telefono"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Direccion"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Correo"].Value.ToString() + "</td>";
                filas += "</tr>";
            }
            pagimahtml_texto = pagimahtml_texto.Replace("@FILAS", filas);


            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(saveFile.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                    PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();
                    pdfDoc.Add(new Phrase(""));

                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Properties.Resources.Stacked_Red_Boxes_35541, System.Drawing.Imaging.ImageFormat.Png);
                    img.ScaleToFit(60, 60);
                    img.Alignment = iTextSharp.text.Image.UNDERLYING;

                    img.SetAbsolutePosition(pdfDoc.LeftMargin, pdfDoc.Top - 60);
                    pdfDoc.Add(img);

                    using (StringReader streamReader = new StringReader(pagimahtml_texto))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(pdfWriter, pdfDoc, streamReader);
                    }
                    pdfDoc.Close();
                    stream.Close();
                }

            }
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            pictureBox6.BackColor = Color.Transparent;
        }

        private void pictureBox6_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox6.BackColor = Color.FromArgb(30, 30, 30);
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            pictureBox5.BackColor = Color.Transparent;
        }

        private void pictureBox5_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox5.BackColor = Color.FromArgb(30, 30, 30);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
            Limpiar();
            Refres();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Editar();
        }
        void Eliminar()
        {
            if (p != -1) 
            {
                var Respuesta = MessageBox.Show("Desea borrar el cliente seleccionado?", "Responde...", MessageBoxButtons.YesNo);
                if (Respuesta == DialogResult.OK)
                {
                    var mensaje = servicioCliente.Delete(p);
                    MessageBox.Show(mensaje);
                    Refres();
                }
               
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox1.BackColor = Color.PowderBlue;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnListaClientes_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }
    }
}
