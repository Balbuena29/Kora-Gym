using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Gimnasio.Membresias 
{
    public partial class frmMembresias : Gimnasio.Utilidades.frmPadre 
    {
        clsMembresia oMembresia = new clsMembresia(); // Instanciamos la clase Membresía

        public frmMembresias() // Formulario
        {
            InitializeComponent(); // Componentes iniciales
        }

        private void frmMembresias_Load(object sender, EventArgs e) // Load que carga metodos
        {
            addEventos(); 
            refrescaLista(); 
            interfaz(); 
        }

        // Metodo con componentes de interfaz
        private void interfaz()
        {
            try 
            {
                lblTitle.Text = "Membresias"; 
                dgvLista.Columns[0].Visible = false; 
                dgvLista.Columns[dgvLista.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; 

            }
            catch(Exception ex){ 
                MessageBox.Show("Error de sistema "+ex.Message); 
            }
            
        }
        // Metdo con eventos de botones principales
        private void addEventos()
        {
            cmdNuevo.Click += new EventHandler(nuevo);
            cmdModificar.Click += new EventHandler(modificar);
            cmdDesabilitar.Click += new EventHandler(desabilita);
            cmdAbilitar.Click += new EventHandler(abilita);
            cmdEliminar.Click += new EventHandler(elimina);
        }

        // Metodo que refresca los dato de la lista
        private void refrescaLista()
        {
            if (!oMembresia.getDatos(dgvLista))
            {
                MessageBox.Show(oMembresia.getError());
            }


        }

        // Metodo que sirve para crear una nueva membresía
        private void nuevo(object sender, EventArgs e)
        {
            frmMembresia frmUs = new frmMembresia(); // Instanciamos
            frmUs.ShowDialog(); 
            refrescaLista(); 
        }

        // Metodo que nos permite modificar una membresía ya existente
        private void modificar(object sender, EventArgs e) 
        {
            int id = Utilidades.OperacionesFormulario.getId(dgvLista); 
            if (id > 0) 
            {

                frmMembresia frm = new frmMembresia();
                frm.idMembresia = id;
                frm.ShowDialog();
                refrescaLista();
            }
            else
            {
                MessageBox.Show("Debe existir una fila seleccionada");
            }
        }

        // Metodo que nos permite deshabilitar una membresía existente
        private void desabilita(object sender, EventArgs e)
        {
            int id = Utilidades.OperacionesFormulario.getId(dgvLista);
            if (id > 0)
            {
                if (id == 1)
                {
                    MessageBox.Show("No puedes realizar esta acción en la membresia visita");
                    return;
                }
                if (oMembresia.changeState(2, id))
                {
                    refrescaLista();
                }
                else
                {
                    MessageBox.Show("Ocurrio un error " + oMembresia.getError());
                }

            }
            else
            {
                MessageBox.Show("Debe existir una fila seleccionada");
            }
        }

        // Metodo que nos permite habilitar una membresía dehsabilitada
        private void abilita(object sender, EventArgs e)
        {
            int id = Utilidades.OperacionesFormulario.getId(dgvLista);
            if (id > 0)
            {
                if (id == 1)
                {
                    MessageBox.Show("No puedes realizar esta acción en la membresia visita");
                    return;
                }
                if (oMembresia.changeState(1, id))
                {
                    refrescaLista();
                }
                else
                {
                    MessageBox.Show("Ocurrio un error " + oMembresia.getError());
                }

            }
            else
            {
                MessageBox.Show("Debe existir una fila seleccionada");
            }
        }

        // Metodo que nos permite eliminar definitivamente una membresía
        private void elimina(object sender, EventArgs e)
        {
            int id = Utilidades.OperacionesFormulario.getId(dgvLista);
            if (id > 0)
            {
                if (id == 1)
                {
                    MessageBox.Show("No puedes eliminar la membresia visita");
                    return;
                }
                if (MessageBox.Show("Estas seguro de eliminar el registro seleccionado", "Confirm delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (oMembresia.changeState(3, id))
                    {
                        refrescaLista();
                    }
                    else
                    {
                        MessageBox.Show("Ocurrio un error " + oMembresia.getError());
                    }
                }

            }
            else
            {
                MessageBox.Show("Debe existir una fila seleccionada");
            }
        }

       
    }
}
