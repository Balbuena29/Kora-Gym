using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Gimnasio.Entradas 
{
    public partial class frmEntradas : Gimnasio.Utilidades.frmPadre 
    {
        clsEntrada oEntrada = new clsEntrada(); // Instanciamos la clase entrada
        public frmEntradas() // Formulario
        {
            InitializeComponent(); // Componentes

        }

        private void frmEntradas_Load(object sender, EventArgs e) //Load con metodos que se ejecutan
        { 
            addEventos(); 
            refrescaLista(); 
            interfaz(); 


            // AlternarColorFila(dgvLista);
        }

        // Metodo que sirve para  la interfaz
        private void interfaz() 
        {
            try 
            {
                lblTitle.Text = "Entradas"; 
                dgvLista.Columns[0].Visible = false; 
                dgvLista.Columns[dgvLista.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; 

            }
            catch (Exception ex) 
            {
                MessageBox.Show("Error de sistema " + ex.Message); 
            }

        }

        // Metodo que agrega eventos
        private void addEventos() 
        {
            cmdNuevo.Click += new EventHandler(nuevo); 
            cmdModificar.Click += new EventHandler(mostrar);
            cmdEliminar.Click += new EventHandler(elimina);
        }

        // Metodo refrescador
        private void refrescaLista()
        {
            if (!oEntrada.getDatos(dgvLista)) 
            {
                MessageBox.Show(oEntrada.getError()); 
            }


        }

        // Metodo que agrega nueva entrada
        private void nuevo(object sender, EventArgs e) 
        {
            frmEntrada frmUs = new frmEntrada();// Instanciamos
            frmUs.ShowDialog();
            refrescaLista();
        }

        // Metodo que elimina entrada
        private void elimina(object sender, EventArgs e)
        {
            int id = Utilidades.OperacionesFormulario.getId(dgvLista);
            if (id > 0)
            {
                //Comprobar si es posible eliminarlo
                if (!oEntrada.posibleEliminar(id))
                {
                    MessageBox.Show("Es imposible eliminar esta entrada ya que productos de ella ya han sido vendidos, solo se puede eliminar una entrada al momento de ser creada, SE CUIDADOSO AL CREARLAS");
                    return;
                }

                if (MessageBox.Show("Estas seguro de eliminar el registro seleccionado", "Confirm delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (oEntrada.remove(id)) 
                    {
                        refrescaLista(); 
                    }
                    else 
                    {
                        MessageBox.Show("Ocurrio un error " + oEntrada.getError()); 
                    }
                }

            }
            else 
            {
                MessageBox.Show("Debe existir una fila seleccionada"); 
            }
        }

        // Metodo que muestra entrada
        private void mostrar(object sender, EventArgs e) 
        {
            int id = Utilidades.OperacionesFormulario.getId(dgvLista); 
            if (id > 0) 
            {
                frmViewEntrada frmUs = new frmViewEntrada(); // Instanciamos
                frmUs.id = id; 
                frmUs.ShowDialog(); 



            }
            else 
            {
                MessageBox.Show("Debe existir una fila seleccionada"); 
            }
        }


        private void spContenedor_Panel1_Paint(object sender, PaintEventArgs e) //
        {

        }

        // Metodo de caja de texto que filtra datos del datagriview
        private void textBox1_KeyUp(object sender, KeyEventArgs e) 
        {
            (dgvLista.DataSource as DataTable).DefaultView.RowFilter = string.Format($"Estado LIKE '{textBox1.Text}%'"); 
        }

        // Metodo cuando el mouse entra a la caja de texto se pone en blanco
        private void textBox1_MouseEnter(object sender, EventArgs e) 
        {
            textBox1.Text = ""; 
        }

        // Metodo cuando el mouse sale de la caja de texto aparece lo de abajo
        private void textBox1_MouseLeave(object sender, EventArgs e) 
        {
            if (textBox1.Text == "" && textBox1.Font.Italic == false) 
            {
                textBox1.Text = "Buscar Entrada..."; 
                textBox1.ForeColor = Color.DarkGray; 
                textBox1.Font = new Font(textBox1.Font, FontStyle.Regular); 
            }
            else 
            {
                textBox1.ForeColor = SystemColors.WindowText; 
                textBox1.Font = new Font(textBox1.Font, FontStyle.Regular); 
            }
        }
   
    }
}
