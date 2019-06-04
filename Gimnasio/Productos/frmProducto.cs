using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gimnasio.Productos 
{
    public partial class frmProducto : Form 
    {
        public int id = 0; // Variable id entera publica
        clsProducto oProducto = new clsProducto(); // Instanciamos la clase producto 

        public frmProducto() // Formulario
        {
            InitializeComponent(); // Inicializamos los componentes
        }

        private void frmProducto_Load(object sender, EventArgs e) // Load del formulario con metodo de cargaDatos
        {
            if (id > 0) 
            {
                cargaDatos(); 
            }
        }

        // Metodo que carga los datos de los productos 
        private void cargaDatos() 
        {
            if (oProducto.getDatos(id)) 
            {

                txtNombre.Text = oProducto.datos.Nombre; 
                txtPrecio.Text = oProducto.datos.Precio.ToString(); 
                txtCosto.Text = oProducto.datos.Costo.ToString(); 
                txtDescripcion.Text = oProducto.datos.Descripcion.ToString(); 

            }
            else 
            {
                MessageBox.Show("Ocurrio un problema al cargar los datos " + oProducto.getError()); 
                this.Close(); 
            }
        }

        // Metodo para el botón 1 de agregar si no es modificar
        private void btnGuardarP_Click(object sender, EventArgs e) 
        {
            if (id <= 0) 
            {
                agrega(); 
            }
            else 
            {
                modifica(); 
            }
        }

        // Metodo para agregar un producto
        private void agrega() 
        {
            try 
            {
                //Validaciones
                if (txtNombre.Text.Trim().Equals("") || txtPrecio.Text.Trim().Equals("") || txtCosto.Text.Equals(""))
                {
                    MessageBox.Show("Nombre, Precio y Costo son obligatorios"); 
                    return; 
                }
                if (!ExpresionesRegulares.RegEX.isDecimal(txtPrecio.Text.Trim())) 
                {
                    MessageBox.Show("El precio debe ser un numero valido, no se permiten letras ni caracteres que no sean numeros"); 
                    return; 
                }
                if (!ExpresionesRegulares.RegEX.isDecimal(txtCosto.Text.Trim())) 
                {
                    MessageBox.Show("El costo debe ser un numero valido, no se permiten letras ni caracteres que no sean numeros"); 
                    return; //
                }
                if (decimal.Parse(txtPrecio.Text.ToString()) <= decimal.Parse(txtCosto.Text.ToString())) 
                {
                    MessageBox.Show("El costo debe ser menor que el precio al publico"); 
                    return; 
                }

                //Asignacion de datos
                oProducto.Nombre = txtNombre.Text.Trim();
                oProducto.Precio = decimal.Parse(txtPrecio.Text.Trim());
                oProducto.Costo = decimal.Parse(txtCosto.Text.Trim());
                oProducto.Descripcion = txtDescripcion.Text.Trim();
                oProducto.idUsuarioLog = Utilidades.clsUsuario.idUsuario;
                if (oProducto.add())
                {
                    MessageBox.Show("Registro agregado con exito"); 
                    this.Close(); 
                }
                else 
                    MessageBox.Show(oProducto.getError()); 

            }
            catch (Exception EX)
            {
                MessageBox.Show("Ocurrio un error de sistema " + EX.Message); 
            }
        }

        // Metodo que modifica un producto
        private void modifica() 
        {

            try
            {
                //Validaciones
                if (txtNombre.Text.Trim().Equals("") || txtPrecio.Text.Trim().Equals("") || txtCosto.Text.Equals("")) 
                {
                    MessageBox.Show("Nombre, Precio y Costo son obligatorios");
                    return;
                }
                if (!ExpresionesRegulares.RegEX.isDecimal(txtPrecio.Text.Trim())) 
                {
                    MessageBox.Show("El precio debe ser un numero valido, no se permiten letras ni caracteres que no sean numeros");
                    return;
                }
                if (!ExpresionesRegulares.RegEX.isDecimal(txtCosto.Text.Trim())) 
                {
                    MessageBox.Show("El costo debe ser un numero valido, no se permiten letras ni caracteres que no sean numeros");
                    return;
                }
                if (decimal.Parse(txtPrecio.Text.ToString()) <= decimal.Parse(txtCosto.Text.ToString())) 
                {
                    MessageBox.Show("El costo debe ser menor que el precio al publico");
                    return;
                }

                //Asignacion de datos
                oProducto.Nombre = txtNombre.Text.Trim();
                oProducto.Precio = decimal.Parse(txtPrecio.Text.Trim());
                oProducto.Costo = decimal.Parse(txtCosto.Text.Trim());
                oProducto.Descripcion = txtDescripcion.Text.Trim();
                oProducto.idUsuarioLog = Utilidades.clsUsuario.idUsuario;
                if (oProducto.edit(id))
                {
                    MessageBox.Show("Registro modificado con exito"); 
                    this.Close(); 
                }
                else
                    MessageBox.Show(oProducto.getError()); 

            }
            catch (Exception EX) 
            {
                MessageBox.Show("Ocurrio un error de sistema " + EX.Message); 
            }
        }
        // Metodo con evento de caja de texto para pasar el foco a la siguiente caja de texto al dar enter
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) 
            {

                txtDescripcion.Focus(); 
            }
        }
        // Metodo con evento de caja de texto para pasar el foco a la siguiente caja de texto al dar enter
        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e) 
        
            {
            if (e.KeyChar == (char)13) 
            {

                txtCosto.Focus(); 
            }
        }
        // Metodo con evento de caja de texto para pasar el foco a la siguiente caja de texto al dar enter
        private void txtCosto_KeyPress(object sender, KeyPressEventArgs e) 
       
            {
            if (e.KeyChar == (char)13) 
            {

                txtPrecio.Focus(); 
            }
        }
        // Metodo con evento de caja de texto para pasar el foco a la siguiente caja de texto al dar enter
        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e) 
        
            {
            if (e.KeyChar == (char)13) 
            {

                txtPrecio.Focus(); 
                if (id <= 0) 
                {
                    agrega(); 
                }
                else 
                
                    modifica(); 
                }
            }
        }
    
}
