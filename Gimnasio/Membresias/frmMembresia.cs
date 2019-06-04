using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gimnasio.Membresias //
{
    public partial class frmMembresia : Form 
    {
        public int idMembresia = 0;// Entero instanciado en 0
        clsMembresia oMembresia = new clsMembresia();// Intanciamos la clase membresía
        public frmMembresia() // Formulario
        {
            InitializeComponent();// Componentes que se inicializan
        }

        private void frmMembresia_Load(object sender, EventArgs e)// Load con metodos que se cargaran junto a el
        {
            cboMeses.SelectedIndex = 0;

            if (idMembresia > 0)
            {
                cargaDatos();
            }
        }

        // Metodo que carga los datos de la membresía
        private void cargaDatos()
        {
            if (oMembresia.getDatos(idMembresia))
            {

                txtNombre.Text = oMembresia.datos.Nombre;
                txtPrecio.Text = oMembresia.datos.Precio.ToString();
                cboMeses.Text = oMembresia.datos.meses.ToString();
                dpInicio.Text = oMembresia.datos.horaInicio.ToString();
                dpFinal.Text = oMembresia.datos.horaFinal.ToString();
            }
            else 
            {
                MessageBox.Show("Ocurrio un problema al cargar los datos " + oMembresia.getError());
                this.Close();
            }
        }

        // Metodo con evento de botón que agrega o modifica dependiendo de la condición
        private void btnGuardarMem_Click(object sender, EventArgs e)
        {
            if (idMembresia <= 0)
            {
                agrega();
            }
            else
            {
                modifica();
            }
        }

        //Metodo que sirve para agregar una nueva membresía
        private void agrega() 
        {
            try
            {
                //Validaciones
                if (txtNombre.Text.Trim().Equals("") || txtPrecio.Text.Trim().Equals("") || cboMeses.Text.Equals(""))
                {
                    MessageBox.Show("Nombre, Precio y Meses son obligatorios");
                    return;
                }
                if (!ExpresionesRegulares.RegEX.isDecimal(txtPrecio.Text.Trim()))
                {
                    MessageBox.Show("El precio debe ser un numero valido, no se permiten letras ni caracteres que no sean numeros");
                    return;
                }

                //Asignacion de datos
                oMembresia.Nombre = txtNombre.Text.Trim();
                oMembresia.Precio = decimal.Parse(txtPrecio.Text.Trim());
                oMembresia.meses = int.Parse(cboMeses.Text.Trim());
                oMembresia.horaInicio = dpInicio.Value.TimeOfDay;
                oMembresia.horaFinal = dpFinal.Value.TimeOfDay;
                oMembresia.idUsuarioLog = Utilidades.clsUsuario.idUsuario;
                if (oMembresia.add())
                {
                    MessageBox.Show("Registro agregado con exito");
                    this.Close();
                }
                else
                    MessageBox.Show(oMembresia.getError());

            }
              catch (Exception EX)
            {
                  MessageBox.Show("Ocurrio un error de sistema " + EX.Message);
            }
        }

        // Metodo que sirve para modificar una membresía existente
        private void modifica()
        {

            try
            {
                //Validaciones
                if (txtNombre.Text.Trim().Equals("") || txtPrecio.Text.Trim().Equals("") || cboMeses.Text.Equals(""))
                {
                    MessageBox.Show("Nombre, Precio y Meses son obligatorios");
                    return;
                }
                if (!ExpresionesRegulares.RegEX.isDecimal(txtPrecio.Text.Trim()))
                {
                    MessageBox.Show("El precio debe ser un numero valido, no se permiten letras ni caracteres que no sean numeros");
                    return;
                }

                //Asignacion de datos
                oMembresia.Nombre = txtNombre.Text.Trim();
                oMembresia.Precio = decimal.Parse(txtPrecio.Text.Trim());
                oMembresia.meses = int.Parse(cboMeses.Text.Trim());
                oMembresia.horaInicio= dpInicio.Value.TimeOfDay;
                oMembresia.horaFinal = dpFinal.Value.TimeOfDay;
                oMembresia.idUsuarioLog = Utilidades.clsUsuario.idUsuario;
                if (oMembresia.edit(idMembresia))
                {
                    MessageBox.Show("Registro modificado con exito");
                    this.Close();
                }
                else
                    MessageBox.Show(oMembresia.getError());

            }
            catch (Exception EX)
            {
                MessageBox.Show("Ocurrio un error de sistema "+EX.Message);
            }
        }

        // Metodo que manda el foco a la siguiente caja de texto
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {

                txtPrecio.Focus();
            }
        }
    }
}
