using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace Gimnasio.Socios
{
    public partial class frmSocio : Form  //public partial es algo nuevo del framework, con el nombre frmSocio 
    {
    
        public int id = 0; // variable publica entero inicializada en 0
        clsSocio oSocio = new clsSocio(); // Instanciamos la clase socio como oSocio

        public frmSocio() 
        {
            InitializeComponent(); 
        }

        // Load del formulario
        private void frmSocio_Load(object sender, EventArgs e) 
        {
            if (id > 0) // Si existe id
            {
                cargaDatos(); // Llamamos al metodo cargar datos
            }

        }

        // Metodo para cargar datos
        private void cargaDatos() 
        {
            if (oSocio.getDatos(id)) // Del socio se manda a llamar su id
            {
                //Validaciones
                txtNombre.Text = oSocio.datos.Nombre; 
                txtPaterno.Text = oSocio.datos.Paterno; 
                txtMaterno.Text = oSocio.datos.Materno; 
                txtTelefono.Text = oSocio.datos.Telefono; 
                txtObservaciones.Text = oSocio.datos.Observaciones; 

                if (oSocio.datos.foto!=null) // Si hay foto 
                {
                    MemoryStream stream = new MemoryStream(oSocio.datos.foto); // Inicializamos una variable  de acuerdo con la matriz de bytes
                    Bitmap image = new Bitmap(stream); // Una nueva instancia de la cadena Bitmap a partir del flujo especificado
                    pbFoto.Image = image; // Al picture box le asignamos la variable image
                }

            }
            else // Si no
            {
                MessageBox.Show("Ocurrio un problema al cargar los datos " + oSocio.getError()); // Mensaje de error
                this.Close(); // Cerramos la ventana
            }
        }

        /// <summary>
        /// aqui abrimos la captura de imagen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbFoto_Click(object sender, EventArgs e) //
        {
            frmFoto frmF = new frmFoto(); // Instanciamos el frmFoto como frmF
            frmF.pbFotoSocio = pbFoto; // Asignamos la foto
            frmF.Show(); // Mostramos el formulario
            frmF.WindowState = FormWindowState.Maximized; // El formulario incia maximizado
        }
         
        // Metodo con evento click para elejir entre agregar socio y guardar socio
        private void btnGuardar_Click(object sender, EventArgs e) 
        {
            if (id <= 0) // Si el id del socio es mayor a 0
            {
                agrega(); // Mandamos a llamar al metodo agrega
            }
            else // Si no 
            {
                modifica(); // Mandamos a llamar al metodo modifica
            }
        }

        // Metodo que sirve para agregar socio en el botón
        private void agrega() 
        {
            try // Manejo de excepciones
            {
                //validaciones
                if (txtNombre.Text.Trim().Equals("") || txtPaterno.Text.Trim().Equals("") || txtMaterno.Text.Equals("")) // Comparación en if
                {
                    MessageBox.Show("Nombre, Apellido Paterno y Apellido Materno son obligatorios"); // Mensaje de error
                    return; // Retornamos
                }
              

                //Asignacion de datos
                oSocio.Nombre = txtNombre.Text.Trim(); 
                oSocio.Paterno = txtPaterno.Text.Trim(); 
                oSocio.Materno = txtMaterno.Text.Trim(); 
                oSocio.Telefono = txtTelefono.Text.Trim(); 
                oSocio.Observaciones = txtObservaciones.Text.Trim(); 
                if (pbFoto.Image!=null) // Si existe imagen
                    oSocio.foto = Utilidades.OperacionesFormulario.conviertePicBoxImageToByte(pbFoto); 
                oSocio.idUsuarioLog = Utilidades.clsUsuario.idUsuario; 
                if (oSocio.add()) // Si el socio fue agregado
                {
                    MessageBox.Show("Registro agregado con exito"); // Mensaje de exito
                    this.Close(); // Cerramos la ventana
                }
                else // Si no
                    MessageBox.Show(oSocio.getError()); // Nos manda un error

            }
            catch (Exception EX) // Excepción del tipo ex
            {
                MessageBox.Show("Ocurrio un error de sistema " + EX.Message); // Nos manda un error
            }
        }

        // Metodo que sirve para modificar un usuario 
        private void modifica() 
        {

            try 
            {
                //Validaciones
                if (txtNombre.Text.Trim().Equals("") || txtPaterno.Text.Trim().Equals("") || txtMaterno.Text.Equals("")) // Comparación en if
                {
                    MessageBox.Show("Nombre, Apellido Paterno y Apellido Materno son obligatorios"); // Mensaje de adevertencia
                    return; // Retornamos
                }

                //Asignacion de datos
                // El trim sirve para no dejar espacios en blanco al inicio y al final de la caja de texto
                oSocio.Nombre = txtNombre.Text.Trim(); 
                oSocio.Paterno = txtPaterno.Text.Trim(); 
                oSocio.Materno = txtMaterno.Text.Trim(); 
                oSocio.Telefono = txtTelefono.Text.Trim(); 
                oSocio.Observaciones = txtObservaciones.Text.Trim(); 
                if (pbFoto.Image != null) //
                    oSocio.foto = Utilidades.OperacionesFormulario.conviertePicBoxImageToByte(pbFoto); 

                oSocio.idUsuarioLog = Utilidades.clsUsuario.idUsuario; //Compara usuasios
                if (oSocio.edit(id)) // Condición
                {
                    MessageBox.Show("Registro modificado con exito"); // Mensahe de exito
                    this.Close(); // Cierra la ventana
                }
                else // Si no 
                    MessageBox.Show(oSocio.getError()); // Nos manda un error

            }
            catch (Exception EX) // ExcepciÓn del tipo ex
            {
                MessageBox.Show("Ocurrio un error de sistema " + EX.Message); // Mensaje de error
            }
        }

        // Metodo con evento de keypress que manda el foco al sigueinte dando enter
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e) 
        {
            if (e.KeyChar == (char)13) 
            {

                txtPaterno.Focus(); // Pasa el foco a txtPaterno
            }
        }

        // Metodo con evento de keypress que manda el foco al siguiente dando enter
        private void txtPaterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) 
            {

                txtMaterno.Focus(); // Pasa el foco a txtMaterno
            }
        }

        // Metodo con evento de keypress que manda el foco al siguiente dando enter
        private void txtMaterno_KeyPress(object sender, KeyPressEventArgs e) 
        {
            if (e.KeyChar == (char)13) 
            {

                txtTelefono.Focus(); // Pasa el foco a txtTelefono
            }
        }

        // Metodo con evento de keypress que manda el foco al siguiente dando enter
        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e) 
        {
            if (e.KeyChar == (char)13) 
            {

                txtObservaciones.Focus(); // Pasa el foco a txtObservaciones
            }
        }

        // Metodo con evento de keypress que manda el foco al siguiente dando enter
        private void txtObservaciones_KeyPress(object sender, KeyPressEventArgs e) 
        {
            if (e.KeyChar == (char)13) 
            {

                btnGuardar.Focus(); // Al dar enter el foco hace el if
                if (id <= 0) 
                {
                    agrega(); // Mandamos a llamar al metodo agrega
                }
                else
                {
                    modifica(); // Mandamos a llamar al metodo modifica
                }
            }
        }
    }
}
