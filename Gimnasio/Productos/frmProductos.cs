using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Gimnasio.Productos 
{
    public partial class frmProductos : Gimnasio.Utilidades.frmPadre 
    {
        clsProducto oProducto = new clsProducto(); // Instanciamos la clase producto 

        public frmProductos()// Formulario
        {
            InitializeComponent();// Componentes de formulario inicializados
        }

        private void frmProductos_Load(object sender, EventArgs e)// LoaD con eventos a cargar
        {
            addEventos();
            refrescaLista();
            interfaz();
        }

        // Metodo con funciones de interfaz
        private void interfaz()
        {
            try
            {
                lblTitle.Text = "Productos";
                dgvLista.Columns[0].Visible = false;
                dgvLista.Columns[dgvLista.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de sistema " + ex.Message);
            }

        }

        // Metodo con los eventos que usamos siempre
        private void addEventos()
        {
            cmdNuevo.Click += new EventHandler(nuevo);
            cmdModificar.Click += new EventHandler(modificar);
            cmdDesabilitar.Click += new EventHandler(desabilita);
            cmdAbilitar.Click += new EventHandler(abilita);
            cmdEliminar.Click += new EventHandler(elimina);
        }

        // Metodo que refresca la lista de productos
        private void refrescaLista()
        {
            if (!oProducto.getDatos(dgvLista))
            {
                MessageBox.Show(oProducto.getError());
            }


        }

        // Metodo que abre el formulario de un nuevo producto
        private void nuevo(object sender, EventArgs e)
        {
            frmProducto frmUs = new frmProducto();
            frmUs.ShowDialog();
            refrescaLista();
        }

        // Metodo que modifica un producto ya existente
        private void modificar(object sender, EventArgs e)
        {
            int id = Utilidades.OperacionesFormulario.getId(dgvLista);
            if (id > 0)
            {

                frmProducto frm = new frmProducto();
                frm.id = id;
                frm.ShowDialog();
                refrescaLista();
            }
            else
            {
                MessageBox.Show("Debe existir una fila seleccionada");
            }
        }

        // Metodo que sirve para deshabilitar un producto 
        private void desabilita(object sender, EventArgs e)
        {
            int id = Utilidades.OperacionesFormulario.getId(dgvLista);
            if (id > 0) 
            {
                if (oProducto.changeState(2, id))
                {
                    refrescaLista();
                }
                else
                {
                    MessageBox.Show("Ocurrio un error " + oProducto.getError());
                }

            }
            else
            {
                MessageBox.Show("Debe existir una fila seleccionada");
            }
        }

        // Metodo que sirve para habilitar un producto
        private void abilita(object sender, EventArgs e)
        {
            int id = Utilidades.OperacionesFormulario.getId(dgvLista);
            if (id > 0)//
            {
                if (oProducto.changeState(1, id))
                {
                    refrescaLista();
                }
                else
                {
                    MessageBox.Show("Ocurrio un error " + oProducto.getError());
                }

            }
            else
            {
                MessageBox.Show("Debe existir una fila seleccionada");
            }
        }

        // Metodo que elimina un producto
        private void elimina(object sender, EventArgs e) 
        {
            int id = Utilidades.OperacionesFormulario.getId(dgvLista); 
            if (id > 0) 
            {
                if (MessageBox.Show("Estas seguro de eliminar el registro seleccionado", "Confirm delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (oProducto.changeState(3, id))
                    {
                        refrescaLista();
                    }
                    else
                    {
                        MessageBox.Show("Ocurrio un error " + oProducto.getError());
                    }
                }

            }
            else 
            {
                MessageBox.Show("Debe existir una fila seleccionada");
            }
        }



        // Metodo con evento de caja de texto para cuando el puntero ingrese dentro el texto sea nada
        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            textBox1.Text = ""; 
        }

        // Metodo que sirve cuando el mouse este fuera de la caja de texto muestre "Buscar producto"
        private void textBox1_MouseLeave(object sender, EventArgs e) 
        {
            if (textBox1.Text == "" && textBox1.Font.Italic == false) 
            {
                textBox1.Text = "Buscar Producto..."; 
                textBox1.ForeColor = Color.DarkGray; 
                textBox1.Font = new Font(textBox1.Font, FontStyle.Regular); 
            }
            else 
            {
                textBox1.ForeColor = SystemColors.WindowText; 
                textBox1.Font = new Font(textBox1.Font, FontStyle.Regular); 
            }
        }

        //Metodo que sirve cuando se escriba dentro se desapareza el texto por default
        private void textBox1_KeyUp(object sender, KeyEventArgs e) 
        {
            (dgvLista.DataSource as DataTable).DefaultView.RowFilter = string.Format($"Nombre LIKE '{textBox1.Text}%'"); // Sirve para filtrar los datos segun Nombre
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
