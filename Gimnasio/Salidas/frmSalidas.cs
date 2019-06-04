using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Gimnasio.Salidas 
{
    public partial class frmSalidas : Gimnasio.Utilidades.frmPadre 
    {
        clsSalida oSalida = new clsSalida(); // Instanciamos la clase salida
        public frmSalidas() 
        {
            InitializeComponent(); // Componentes que se inicializan
        }

        private void frmSalidas_Load(object sender, EventArgs e) //Load del formulario
        {
            addEventos(); // Mandamos a llamar al metodo addEventos
            refrescaLista(); // Mandamos a llamar al metodo refrescaLista
            interfaz(); // Mandamos a llamar al metodo interfaz
        }


        // Metodo con funciones de interfaz
        private void interfaz() 
        {
            try 
            {
                lblTitle.Text = "Salidas"; 
                dgvLista.Columns[0].Visible = false; 
                dgvLista.Columns[dgvLista.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; 

            }
            catch (Exception ex) 
            {
                MessageBox.Show("Error de sistema " + ex.Message); 
            }

        }
        // Metodo que contiene los eventos que utilizamos siempre
        private void addEventos()
        {
            cmdNuevo.Click += new EventHandler(nuevo); 
            cmdModificar.Click += new EventHandler(mostrar); 
            cmdEliminar.Click += new EventHandler(elimina);
        }

        // Metodo que refresca la lista
        private void refrescaLista() 
        {
            if (!oSalida.getDatos(dgvLista)) 
            {
                MessageBox.Show(oSalida.getError()); 
            }


        }



        // Metodo que abre el formulario salida
        private void nuevo(object sender, EventArgs e) 
        {
            frmSalida frmUs = new frmSalida(); 
            frmUs.ShowDialog(); 
            refrescaLista(); 
        }

        // Metodo que elimina una salida
        private void elimina(object sender, EventArgs e)
        {
            int id = Utilidades.OperacionesFormulario.getId(dgvLista); 
            if (id > 0) 
            {
                if (MessageBox.Show("Estas seguro de eliminar el registro seleccionado", "Confirm delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (oSalida.remove(id))
                    {
                        refrescaLista();
                    }
                    else 
                    {
                        MessageBox.Show("Ocurrio un error " + oSalida.getError()); 
                    }
                }

            }
            else 
            {
                MessageBox.Show("Debe existir una fila seleccionada"); 
            }
        }

        // Metodo que muestra una salida registrada
        private void mostrar(object sender, EventArgs e)
        {
            int id = Utilidades.OperacionesFormulario.getId(dgvLista); 
            if (id > 0) 
            {
                frmViewSalida frmUs = new frmViewSalida(); 
                frmUs.id = id; 
                frmUs.ShowDialog(); 
            }
            else 
            {
                MessageBox.Show("Debe existir una fila seleccionada"); 
            }
        }

        // Metodo con evento de mouse que asigna al buscador un valor en nada
        private void txtBuscarSalida_MouseEnter(object sender, EventArgs e) 
        {
            txtBuscarSalida.Text = ""; 
        }
        // Metodo con evento de mouse que al salir de la caja de texto  pone "Buscar salida"
        private void txtBuscarSalida_MouseLeave(object sender, EventArgs e) 
        {
            if (txtBuscarSalida.Text == "" && txtBuscarSalida.Font.Italic == false) 
            {
                txtBuscarSalida.Text = "Buscar Salida..."; 
                txtBuscarSalida.ForeColor = Color.DarkGray; 
                txtBuscarSalida.Font = new Font(txtBuscarSalida.Font, FontStyle.Regular); 
            }
            else 
            {
                txtBuscarSalida.ForeColor = SystemColors.WindowText; 
                txtBuscarSalida.Font = new Font(txtBuscarSalida.Font, FontStyle.Regular); 
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e) // Metodo que sirve para  buscar un dato filtrado del datagriview
        {
            (dgvLista.DataSource as DataTable).DefaultView.RowFilter = string.Format($"Estado LIKE '{txtBuscarSalida.Text}%'"); 
        }
    }
}
