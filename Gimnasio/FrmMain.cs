using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Media;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Gimnasio 
{
    public partial class FrmMain : Form 

    {
        private Timer ti; // Variable del tipo timer

        public FrmMain() // Formulario
        {
            ti = new Timer(); // Instanciamos
            ti.Tick += new EventHandler(eventoTimer); 
            InitializeComponent(); 
            onePing(); 
            ti.Enabled = true; 
        }

        // Evento para el reloj
        private void eventoTimer(object ob, EventArgs evt) 
        {
            DateTime hoy = DateTime.Now; // Instanciamos
            labReloj.Text = hoy.ToString("hh:mm:ss"); // Formato de dia, fecha y hora al label
        }



        // Load con tamaño de ventana principal
        private void Form1_Load(object sender, EventArgs e)
        {
            
            this.WindowState = FormWindowState.Normal; //Hacemos que nuestro formulario inicie maximizado


          

            sinSesion();// Metodo Cerra sesion



        }

        // Metodo que produce un sonido al abrir el programa
        public void onePing()
        {
            SystemSounds.Hand.Play();
        }
        /// <summary>
        /// metodo que abre la ventana de login
        /// </summary>
        /// 
        // Metodo que saca a la parte del login y la habilita al validar 
        private void sinSesion()
        {
            panel1.Enabled = false;
            cerrarFormuarios();

            Sesion.frmLogin frmL = new Sesion.frmLogin();
            frmL.ShowDialog();

            if (Utilidades.clsUsuario.existeSesion) ;
            panel1.Enabled = true;
        }

        // Metodo con evento de botón que abre formulario usuarios
        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            if (!detectarFormularioAbierto("frmUsuarios"))
            {
                Usuarios.frmUsuarios frmU = new Usuarios.frmUsuarios();
                frmU.MdiParent = this;
                frmU.Show();
                frmU.WindowState = FormWindowState.Maximized;
            }
        }



        // Metodo que abre formulario membresias
        private void btnMembresias_Click(object sender, EventArgs e)
        {
            if (!detectarFormularioAbierto("frmMembresias"))
            {
                Membresias.frmMembresias frmM = new Membresias.frmMembresias();
                frmM.MdiParent = this;
                frmM.Show();//
                frmM.WindowState = FormWindowState.Maximized;
            }
        }

        // Metodo que valida formulario
        private bool detectarFormularioAbierto(string formulario)
        {
            bool abierto = false;

            if (Application.OpenForms[formulario] != null)
            {
                abierto = true;
                Application.OpenForms[formulario].Activate();
                Application.OpenForms[formulario].WindowState = FormWindowState.Maximized;
            }
            return abierto;
        }

        private void cerrarFormuarios()// Cierra formulario 
        {
            if (this.MdiChildren.Length > 0)
            {
                foreach (Form childForm in this.MdiChildren)
                    childForm.Close();


            }
        }

        // Metodo que abre formulario productos
        private void btnProductos_Click(object sender, EventArgs e)
        {
            if (!detectarFormularioAbierto("frmProductos"))
            {
                Productos.frmProductos frmM = new Productos.frmProductos();
                frmM.MdiParent = this;
                frmM.Show();
                frmM.WindowState = FormWindowState.Maximized;
            }
        }

        // Metodo que abre formulario socios
        private void btnSocios_Click(object sender, EventArgs e)
        {
            if (!detectarFormularioAbierto("frmSocios"))
            {
                Socios.frmSocios frmM = new Socios.frmSocios();
                frmM.MdiParent = this;
                frmM.Show();
                frmM.WindowState = FormWindowState.Maximized;
            }
        }

        // Metodo que abre formulario Registro
        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            if (!detectarFormularioAbierto("frmRegistro"))
            {
                Registro.frmRegistro frmM = new Registro.frmRegistro();
                frmM.Show();
                frmM.WindowState = FormWindowState.Maximized;
            }
        }

        // Metodo que abre formulario registro con F12
        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            //SE ABRE REGISTRO CON F12
            if (e.KeyCode == Keys.F12)
            {
                if (!detectarFormularioAbierto("frmRegistro"))
                {
                    Registro.frmRegistro frmM = new Registro.frmRegistro();
                    frmM.Show();
                    frmM.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void FrmMain_KeyPress(object sender, KeyPressEventArgs e)//
        {

        }

        private void FrmMain_KeyUp(object sender, KeyEventArgs e)//
        {

        }

        // Metodo que abre formulario Reportes
        private void btnReportes_Click(object sender, EventArgs e)
        {
            if (!detectarFormularioAbierto("frmReportes"))
            {
                Reportes.frmReportes frmM = new Reportes.frmReportes();
                frmM.MdiParent = this;
                frmM.Show();
                frmM.WindowState = FormWindowState.Maximized;
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)//
        {

        }

        // Metodo que cierra la sesión del usuario actual
        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Utilidades.clsUsuario.salir())
            {
                sinSesion();
            }
            else
            {
                MessageBox.Show(Utilidades.clsUsuario.error);
            }
        }

        //Metodo que abre el formulario Configuración
        private void btnConfiguración_Click(object sender, EventArgs e)
        {
            if (!detectarFormularioAbierto("frmConfiguracion"))
            {
                Configuracion.frmConfiguracion frmM = new Configuracion.frmConfiguracion();
                frmM.MdiParent = this;
                frmM.Show();
                frmM.WindowState = FormWindowState.Maximized;
            }
        }

        // Metodo que inicia sesión con usuario diferente
        private void iniciarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Utilidades.clsUsuario.existeSesion)
                sinSesion();
            else
                MessageBox.Show("Cierra la sesión primero");
        }

        // Metodo que abre el formulario entradas
        private void btnEntradas_Click(object sender, EventArgs e)
        {
            if (!detectarFormularioAbierto("frmEntradas"))
            {
                Entradas.frmEntradas frmM = new Entradas.frmEntradas();
                frmM.MdiParent = this;
                frmM.Show();
                frmM.WindowState = FormWindowState.Maximized;
            }
        }

        // Metodo que abre el formulario salidas
        private void btnSalidas_Click(object sender, EventArgs e)
        {
            if (!detectarFormularioAbierto("frmSalidas"))
            {
                Salidas.frmSalidas frmM = new Salidas.frmSalidas();
                frmM.MdiParent = this;
                frmM.Show();
                frmM.WindowState = FormWindowState.Maximized;
            }
        }

        private void button10_Click(object sender, EventArgs e)//
        {

        }

        private void button11_Click(object sender, EventArgs e)//
        {

        }

        // Metodo que hace el guardado de respaldo en el botón respaldo
        private void btnRespaldar_Click(object sender, EventArgs e)
        {


            SaveFileDialog sFileDialog1 = new SaveFileDialog();

            sFileDialog1.InitialDirectory = "c:\\";
            sFileDialog1.Filter = "Archivos de sql (*.sql)|*.sql";
            sFileDialog1.FilterIndex = 1;
            sFileDialog1.RestoreDirectory = true;

            if (sFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //Se guarda el respaldo
                    try
                    {
                        string constr = "server=localhost;User Id=root;Persist Security Info=True;database=gym";
                        string file = sFileDialog1.FileName;
                        MySqlBackup mb = new MySqlBackup(constr);
                        mb.ExportInfo.FileName = file;
                        mb.ExportInfo.ExportViews = false;

                        if (System.IO.File.Exists(file))
                            System.IO.File.Delete(file);
                        mb.Export();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrio un error " + ex.Message);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error del Sistema: " + ex.Message);
                }
            }

            MessageBox.Show("Información guardada con exito");
        }

        // Metodo que permite restaurar la base de datos a un punto previamente respaldado
        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Estas seguro de abrir un archivo de respaldo? se remplazara toda la información en el sistema con lo que existe en el archivo de respaldo", "Confirm delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //Se abre la ventana para seleccionar acrhivo

                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "Archivos de sql (*.sql)|*.sql";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        //Se abre el respaldo
                        try
                        {
                            string constr = "server=localhost;User Id=root;Persist Security Info=True;database=gym";
                            string file = openFileDialog1.FileName;
                            MySqlBackup mb = new MySqlBackup(constr);
                            mb.ImportInfo.FileName = file;
                            mb.ImportInfo.SetTargetDatabase("gym", ImportInformations.CharSet.utf8);
                            mb.Import();
                        } catch (Exception ex) {
                            MessageBox.Show("Ocurrio un error " + ex.Message);
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error del Sistema: " + ex.Message);
                    }
                }

                MessageBox.Show("Información restaurada con exito");

                //Se cierran formularios
                cerrarFormuarios();
                //Se cierra sesion
                if (Utilidades.clsUsuario.salir())
                {
                    sinSesion();
                }
                else
                {
                    MessageBox.Show(Utilidades.clsUsuario.error);
                }

            }
        }

        // Metodo con evento de botón que sirve para abrir el manual de usuario
        private void manualDeUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"D:\MANUAL DE USUARIO PDF.pdf");
        }

        private void button10_Click_1(object sender, EventArgs e)//
        {
            
        }

        private void button10_Click_2(object sender, EventArgs e)//
        {
        
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}

