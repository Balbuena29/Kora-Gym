using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace Gimnasio.Configuracion 
{
    public partial class frmConfiguracion : Form 
    {
        clsConfiguracion oConfiguracion = new clsConfiguracion(); // Instanciamos la clase configuración

        public frmConfiguracion() // Formulario
        {
            InitializeComponent(); // Componentes iniciales
        }

        private void frmConfiguracion_Load(object sender, EventArgs e) // Load con metodo
        {
            cargaDatos(); 
        }

        // Metodo cargar datos de gimnasio
        private void cargaDatos()
        {
            if (clsConfiguracion.getDatos()) 
            {

                txtNombre.Text = clsConfiguracion.datos.NombreGimnacio; 
                txtDomicilio.Text = clsConfiguracion.datos.Domicilio;
                txtTelefono.Text = clsConfiguracion.datos.Telefono;
                txtRFC.Text = clsConfiguracion.datos.RFC;
                txtMensaje.Text = clsConfiguracion.datos.Mensaje;
                txtAlerta.Text = clsConfiguracion.datos.mensajeVencimiento.ToString();
                if (clsConfiguracion.datos.Logo != null)
                {
                    MemoryStream stream = new MemoryStream(clsConfiguracion.datos.Logo);
                    Bitmap image = new Bitmap(stream);
                    pbLogo.Image = image;
                }

                

            }
            else
            {
                MessageBox.Show("Ocurrio un problema al cargar los datos " + clsConfiguracion.Error);
                this.Close();
            }
        }

        // Metodo con graficos de ventana
        private void frmConfiguracion_SizeChanged(object sender, EventArgs e)
        {
            Utilidades.clsGrafico.centraX(this, groupBox1);
        }

        // Metodo que sirve para agregar una imagen del gimnasio
        private void pbLogo_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Archivos jpg (*.jpg)|*.jpg|Archivos png (*.png)|*.png|Archivos gif (*.gif)|*.gif";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            pbLogo.Image = Image.FromStream(myStream);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error del Sistema: " + ex.Message);
                }
            }
        }
        // Metodo que sirve para programar a los cuantos días se mandara la alerta de vencimiento
        private void btnGuardarConf_Click(object sender, EventArgs e)
        {
            //Validaciones
            if (!txtAlerta.Text.Trim().ToString().Equals(""))
            {
                if (!ExpresionesRegulares.RegEX.isNumber(txtAlerta.Text.Trim().ToString()))
                {
                    MessageBox.Show("El mensaje de alerta debe ser un numero, no letras ni caracteres extraños");
                    return;
                }
            }
            else
            {
                txtAlerta.Text = "0";
            }

            if (!txtRFC.Text.Trim().Equals(""))
            {
                if (!ExpresionesRegulares.RegEX.isRFC2(txtRFC.Text.Trim()))
                {
                    MessageBox.Show("Si introduces un valor en rfc debe ser un rfc valido, en caso de que no requieras de este valor, dejalo en blanco");
                    return;
                }
            }


            
                oConfiguracion.NombreGimnacio = txtNombre.Text.Trim();
            oConfiguracion.Domicilio = txtDomicilio.Text.Trim();
            oConfiguracion.Telefono = txtTelefono.Text.Trim();
            if (pbLogo.Image!=null)
                oConfiguracion.Logo = Utilidades.OperacionesFormulario.conviertePicBoxImageToByte(pbLogo);
            oConfiguracion.idUsuarioLog = Utilidades.clsUsuario.idUsuario;
            oConfiguracion.mensajeVencimiento = int.Parse(txtAlerta.Text.Trim().ToString());
            oConfiguracion.RFC = txtRFC.Text.Trim().ToString();
            oConfiguracion.Mensaje = txtMensaje.Text.Trim().ToString();

            if (oConfiguracion.edit(0))
            {
                    MessageBox.Show("Datos Guardados");
            }
            else
            {
                    MessageBox.Show(oConfiguracion.getError());
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

       

        }

        // Metodo que al dar enter manda el foco a la siguiente caja de texto
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {

                txtDomicilio.Focus();
            }
        }

        // Metodo que al dar enter manda el foco a la siguiente caja de texto
        private void txtDomicilio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {

                txtTelefono.Focus();
            }
        }

        // Metodo que al dar enter manda el foco a la siguiente caja de texto
        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {

                txtRFC.Focus();
            }
        }

        // Metodo que al dar enter manda el foco a la siguiente caja de texto
        private void txtRFC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {

                txtMensaje.Focus();
            }
        }

        // Metodo que al dar enter manda el foco a la siguiente caja de texto
        private void txtMensaje_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {

                txtAlerta.Focus();
            }
        }

        // Metodo que al dar enter manda el foco aL SIGUIENTE BOTÓN
        private void txtAlerta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {

                btnGuardarConf.Focus();
                //Validaciones
                if (!txtAlerta.Text.Trim().ToString().Equals(""))
                {
                    if (!ExpresionesRegulares.RegEX.isNumber(txtAlerta.Text.Trim().ToString()))
                    {
                        MessageBox.Show("El mensaje de alerta debe ser un numero, no letras ni caracteres extraños");
                        return;
                    }
                }
                else
                {
                    txtAlerta.Text = "0";
                }

                if (!txtRFC.Text.Trim().Equals(""))
                {
                    if (!ExpresionesRegulares.RegEX.isRFC2(txtRFC.Text.Trim()))
                    {
                        MessageBox.Show("Si introduces un valor en rfc debe ser un rfc valido, en caso de que no requieras de este valor, dejalo en blanco");
                        return;
                    }
                }



                oConfiguracion.NombreGimnacio = txtNombre.Text.Trim();
                oConfiguracion.Domicilio = txtDomicilio.Text.Trim();
                oConfiguracion.Telefono = txtTelefono.Text.Trim();
                if (pbLogo.Image != null)
                    oConfiguracion.Logo = Utilidades.OperacionesFormulario.conviertePicBoxImageToByte(pbLogo);
                oConfiguracion.idUsuarioLog = Utilidades.clsUsuario.idUsuario;
                oConfiguracion.mensajeVencimiento = int.Parse(txtAlerta.Text.Trim().ToString());
                oConfiguracion.RFC = txtRFC.Text.Trim().ToString();
                oConfiguracion.Mensaje = txtMensaje.Text.Trim().ToString();

                if (oConfiguracion.edit(0))
                {
                    MessageBox.Show("Datos Guardados");
                }
                else
                {
                    MessageBox.Show(oConfiguracion.getError());
                }
            }
        }
    }
}
