using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gimnasio.Entradas 
{
    public partial class frmViewEntrada : Form 
    {
        public int id = 0; 
        clsEntrada oEntrada = new clsEntrada(); // Instanciamos la clase entrada
        public frmViewEntrada() // Formulario
        {
            InitializeComponent(); // Componentes
        }

        private void panel1_Paint(object sender, PaintEventArgs e) // Panel
        {

        }

        private void frmViewEntrada_Load(object sender, EventArgs e) // Load con metodos que se cargan
        {
            cargaDatos(); 
            refrescaLista(); 
            interfaz(); 
        }

        // Metodo utilizado en la interfaz
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
            if (!oEntrada.getDatosDetalle(dgvLista)) 
            {
                MessageBox.Show(oEntrada.getError()); 
            }


        }

        // Metodo que carga los datos 
        private void cargaDatos() 
        {
            if (oEntrada.getDatos(id)) 
            {
                lblTotal.Text = oEntrada.datos.Total.ToString(); 
                lblFecha.Text = oEntrada.datos.fechaCreacion.ToShortDateString(); 

            }
            else 
            {
                MessageBox.Show("Ocurrio un problema al cargar los datos " + oEntrada.getError()); 
                this.Close(); 
            }
        }

    }
}
