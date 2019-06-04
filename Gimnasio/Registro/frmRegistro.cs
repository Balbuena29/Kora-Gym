using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace Gimnasio.Registro
{
    public partial class frmRegistro : Form 
    {
        clsRegistro oRegistro; // Instanciamos la clase registro
        public frmRegistro()// Formulario
        {
            InitializeComponent();// Inicializamos componentes
        }

        private void frmRegistro_Load(object sender, EventArgs e)// Load con datos
        {
            Utilidades.clsGrafico.centraX(this, panelContenedor);
            Utilidades.clsGrafico.centraX(this, panelEncabezado);
            Utilidades.clsGrafico.centraX(this, panelFoot);
            cargaDatos();
        }

        // Metodo que carga los datos del gimnasio
        private void cargaDatos() 
        {
            Configuracion.clsConfiguracion.getDatos();
            lblNombreGimnacio.Text = Configuracion.clsConfiguracion.datos.NombreGimnacio.ToUpper();
            lblDomicilio.Text = Configuracion.clsConfiguracion.datos.Domicilio;
            lblTelefono.Text = Configuracion.clsConfiguracion.datos.Telefono;
            lblRFC.Text = Configuracion.clsConfiguracion.datos.RFC.ToUpper();
            lblMensaje.Text = Configuracion.clsConfiguracion.datos.Mensaje;
            if (Configuracion.clsConfiguracion.datos.Logo != null)
            {

                MemoryStream stream = new MemoryStream(Configuracion.clsConfiguracion.datos.Logo);
                Bitmap image = new Bitmap(stream);
                pbLogo.Image = image;
            }

            lblFecha.Text = DateTime.Now.ToLongDateString();
            lblHora.Text = DateTime.Now.ToLongTimeString();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        // Metodo con tamaño de panel
        private void frmRegistro_SizeChanged(object sender, EventArgs e)
        {
            Utilidades.clsGrafico.centraX(this, panelContenedor);
            Utilidades.clsGrafico.centraX(this, panelEncabezado);
            Utilidades.clsGrafico.centraX(this, panelFoot);
        }

        // Metodo con evento de caja te texto para cuando la tecla se halla hundido para ingresar la cave 
        private void txtClave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               

                if (!ExpresionesRegulares.RegEX.isNumber(txtClave.Text.ToString()))
                {
                    MessageBox.Show("La clave es numerica, debes introducir solo numeros");
                    return;
                }

                int clave = int.Parse(txtClave.Text.ToString());

                oRegistro = new clsRegistro(clave);
                if (oRegistro.buscaDatos())
                {
                    //Cargar datos
                    txtNombre.Text = oRegistro.datos.NombreSocio;
                    txtPaterno.Text = oRegistro.datos.Paterno;
                    txtMaterno.Text = oRegistro.datos.Materno;
                    lblVencimiento.Text = oRegistro.datos.Vencimiento.ToLongDateString();
                    //Cargar foto
                    if (oRegistro.datos.foto != null)
                    {
                        MemoryStream stream = new MemoryStream(oRegistro.datos.foto);
                        Bitmap image = new Bitmap(stream);
                        pbFoto.Image = image;
                    }

                    txtClave.Text = "";
                    if (DateTime.Compare(oRegistro.datos.Vencimiento, DateTime.Now) < 0)
                    {
                        lblVencimiento.ForeColor = Color.Red;
                        MessageBox.Show("Se a términado tu membresia");

                    }
                    //Si no se a vencido la registramos registro
                    else
                    {
                        //Si le quedan pocos dias se le avisa de que ya casi se le acaba su membresia
                        Configuracion.clsConfiguracion.getDatos();
                        if(Configuracion.clsConfiguracion.datos.mensajeVencimiento!=null&&Configuracion.clsConfiguracion.datos.mensajeVencimiento>0)//Si tiene habilitado el mensaje
                        if (DateTime.Compare(oRegistro.datos.Vencimiento, DateTime.Now.AddDays(Configuracion.clsConfiguracion.datos.mensajeVencimiento)) < 0)//Si ya casi se termina
                        {
                            DateTime newDate = oRegistro.datos.Vencimiento;
                                DateTime oldDate = DateTime.Now;
                                TimeSpan ts = newDate - oldDate;
                                int diasFaltantes=ts.Days;

                                MessageBox.Show("Quedan "+(diasFaltantes)+" dias de tu membresia");
                            }

                        lblVencimiento.ForeColor = Color.Black;

                        if (!oRegistro.addVisita())
                        {
                            MessageBox.Show(oRegistro.error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No existe un socio con esa clave");
                }
            }
        }

        private void txtClave_TextChanged(object sender, EventArgs e)
        {

        }

        // Metodo que asigna fecha y hora a los label mostrados
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblFecha.Text = DateTime.Now.ToLongDateString();
            lblHora.Text = DateTime.Now.ToLongTimeString();
        }

        // Metodo con evento de keypress que manda el foco a la siguiente caja de texto
        private void txtClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {

                txtNombre.Focus();
            }
        }
        // Metodo con evento de keypress que manda el foco a la siguiente caja de texto
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {

                txtPaterno.Focus();
            }
        }

        // Metodo con evento de keypress que manda el foco a la siguiente caja de texto
        private void txtPaterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {

                txtMaterno.Focus();
            }
        }

    
    }
}
