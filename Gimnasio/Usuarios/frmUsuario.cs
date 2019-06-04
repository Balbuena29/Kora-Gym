using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gimnasio.Usuarios 
{
    public partial class frmUsuario : Form 
    {
        public int idUsuario = 0; // Creamos una variable publica entera y la inicializamos en 0
        clsUsuario oUsuario = new clsUsuario(); // Instanciamos la clase usuario con el nombre oUsuario 

        public frmUsuario() // Clase formulario
        {
            InitializeComponent(); // Componentes del formulario 
        }

        // Load del formulario que se ejecutan el iniciar el formulario
        private void frmUsuario_Load(object sender, EventArgs e) 
        {
            
            if (idUsuario > 0) // Si el idUsuario es mayor a 0
            {
                cargaDatos(); // Mandamos llamar al metodo cargaDatos
            }
        }

        // Metodo que sire para crear y modificar un usuario
        private void cargaDatos() 
        {
            if (oUsuario.getDatos(idUsuario)) 
            {
                txtUsuario.Text = oUsuario.datos.Usuario; 
                txtNombre.Text = oUsuario.datos.Nombre; 
            }
            else // Si no
            {
                MessageBox.Show("Ocurrio un problema al cargar los datos "+oUsuario.getError());// Si no carga mnos manda error
                this.Close();// se cierra la ventana
            }
        }

        // Metodo con evento para el botón de guardar 
        private void btnGuardar_Click(object sender, EventArgs e) 
        {
            if (idUsuario <= 0)
            {
                agrega();// Mandamos llamar al metodo agrega
            }
            else// Si no
            {
                modifica();// Mandamos llamar al metodo moficia
            }
        }

        // Metodo que sirve para un usuario
        private void agrega()
        {
            //El trim sirve para borrar espacios en blanco al inicio y al final de cada texto
            oUsuario.Usuario = txtUsuario.Text.Trim();
            oUsuario.Nombre = txtNombre.Text.Trim();
            oUsuario.Password = txtPassword.Text.Trim();

            if (oUsuario.Usuario.Equals("") || oUsuario.Password.Equals(""))
            {
                MessageBox.Show("Usuario y password son obligatorios");
                return;
            }

            if (oUsuario.add())
            {
                MessageBox.Show("Registro agregado con exito");
                this.Close();
            }
            else
                MessageBox.Show(oUsuario.getError());

        }

        //Metodo que sirve para modificar un usuario ya existente
        private void modifica()
        {
            //El trim sirve para borrar espacios en blanco al inicio y al final de cada texto
            oUsuario.Usuario = txtUsuario.Text.Trim();
            oUsuario.Nombre = txtNombre.Text.Trim();
            oUsuario.Password = txtPassword.Text.Trim();

            if (oUsuario.Usuario.Equals("") || oUsuario.Password.Equals(""))
            {
                MessageBox.Show("Usuario y password son obligatorios");
                return;
            }
            
            if (oUsuario.edit(idUsuario))
            {
                MessageBox.Show("Registro modificado con exito");
                this.Close();
            }
            else
                MessageBox.Show(oUsuario.getError());
        }

        // Metodo con evento keypress, que transfiere el foco con un enter
        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {

                txtPassword.Focus();
            }
        }

        // Metodo con evento keypress, que transfiere el foco con un enter
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {

                txtNombre.Focus();
            }
        }

        // Metodo con evento keypress, que transfiere el foco con un enter
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {

                btnGuardar.Focus();// El foco va al botón
                if (idUsuario <= 0)// Si el usuario es meno o igual a 0
                {
                    agrega();// Mandamos a llamar el metodo agrega
                }
                else// Si no 
                {
                    modifica();// Mandamos a llamar al metodo modifica
                }
            }
        }
    }
}
