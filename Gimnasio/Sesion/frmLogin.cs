using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Gimnasio.Sesion 
{
    public partial class frmLogin : Form 
    {
        Rectangle sizeGripRectangle;// Almacena una posición del tamaño de un rectangulo
        const int GRIP_SIZE = 15;// Se declara un campo constante entero de 15

        private void AdaptGripRectangle()//Metodo donde se asigna el tamaño al rectangulo
        {
            sizeGripRectangle.X = this.Width - GRIP_SIZE;
            sizeGripRectangle.Y = this.Height - GRIP_SIZE;
        }

        public frmLogin()
        {
            InitializeComponent(); // Componentes que se inicializan con el programa
            sizeGripRectangle.Width = sizeGripRectangle.Height = GRIP_SIZE; //Se establece el alto y ancho
            AdaptGripRectangle(); 
            this.Paint += (o, ea) => { 
                ControlPaint.DrawSizeGrip(ea.Graphics, //Dibuja controles
             this.BackColor,
            sizeGripRectangle);
            };


        }







        private void frmLogin_Load(object sender, EventArgs e) //Load del formulario
        {
           

        }
        //Metodo con evento de botón que valida usuario y contraseña
        private void btnEntrar_Click(object sender, EventArgs e) 
        {
            
            string usuario = txtUsuario.Text.Trim(); // Caja de texto no acepta valores de espacio al principio y al final
            string password = txtPassword.Text.Trim(); // Caja de texto no acepta valores de espacio al principio y al final
            //Validaciones
            if (usuario.Equals("") || password.Equals("")) 
            {

                MessageBox.Show("Usuario y password son obligatorios"); 
                return; 

            }
            //Proceso
            if (Utilidades.clsUsuario.login(usuario, password)) 
            {
                this.Close(); //Cerramos
            }
            else 
            {
                MessageBox.Show(Utilidades.clsUsuario.error); 
            }

        }

        private void button2_Click(object sender, EventArgs e) 
        {
            
            
        }

        private void pictureBox1_Click(object sender, EventArgs e) 
        {

        }

        // Metodo con evento de botón que transfiere el foto al dar enter
        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e) 
        {
            if (e.KeyChar == (char)13)
            {
                txtPassword.Focus(); 
            }
        }

        // Metodo con evento de botón que transfiere el foto al dar enter
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e) 
        {
            if (e.KeyChar == (char)13) 
            {
                btnEntrar.Focus(); //Se transfiere el foco al botón 1
                string usuario = txtUsuario.Text.Trim(); // Caja de texto no acepta valores de espacio al principio y al final
                string password = txtPassword.Text.Trim(); // Caja de texto no acepta valores de espacio al principio y al final
                //Validaciones
                if (usuario.Equals("") || password.Equals("")) 
                {

                    MessageBox.Show("Usuario y password son obligatorios"); 
                    return; 

                }
                //Proceso
                if (Utilidades.clsUsuario.login(usuario, password)) 
                {
                    this.Close(); 
                }
                else
                {
                    MessageBox.Show(Utilidades.clsUsuario.error); 
                }
            }
        }

      
    }
}
