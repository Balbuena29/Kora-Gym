using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gimnasio.Salidas 
{
    public partial class frmViewSalida: Form 
    {
        public int id = 0; // Variable id del tipo entero publica
        clsSalida oSalida = new clsSalida(); // Instanciamos la clase salida
        public frmViewSalida() // Formulario
        {
            InitializeComponent(); // Componentes iniciales
        }

        private void panel1_Paint(object sender, PaintEventArgs e) 
        {

        }

        private void frmViewEntrada_Load(object sender, EventArgs e) // Load del formulario
        {
            cargaDatos(); // Mandamos llamar al evento cargaDatos
            refrescaLista(); // Mandamos llamar al evento refrescaLista
            interfaz(); // Mandamos llamar al evento interfaz
        }

        // Metodo que contiene la interfaz
        private void interfaz() 
        {
            try 
            {
                dgvLista.Columns[0].Visible = false; 
                dgvLista.Columns[dgvLista.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; 

            }
            catch (Exception ex) 
            {
                MessageBox.Show("Error de sistema " + ex.Message); 
            }

        }

        // Metodo refrescador
        private void refrescaLista() 
        {
            if (!oSalida.getDatosDetalle(dgvLista)) 
            {
                MessageBox.Show(oSalida.getError()); 
            }


        }

        // Metodo que carga los datos de los label y su id
        private void cargaDatos() 
        {
            if (oSalida.getDatos(id)) 
            {
                
                lblTotal.Text = oSalida.datos.total.ToString(); 
                lblFecha.Text = oSalida.datos.fechaCreacion.ToShortDateString(); 

            }
            else 
            {
                MessageBox.Show("Ocurrio un problema al cargar los datos " + oSalida.getError()); 
                this.Close(); 
            }
        }
      

    }
}
