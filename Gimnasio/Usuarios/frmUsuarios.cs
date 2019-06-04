using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Gimnasio.Usuarios
{
    public partial class frmUsuarios : Gimnasio.Utilidades.frmPadre
    {
        clsUsuario oUsuario = new clsUsuario();// Instanciamos la clase usuario con el nombre oUsuario

        public frmUsuarios()// Clase del formulario
        {
            InitializeComponent();// Componentes del formulario
        }

        // Load del formulario donde se inician eventos al ejecutarlo
        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            
            addEventos();//Mandamos a llamarl al evebnto addeventos
            refrescaLista();//Mandamos a llamar al evento refrescaLista
            interfaz();//Mandamos a llamar al evento interfza
        }

        // Metodo interfaz que sirve para el tamaño de las columnas y ajustael texto a ellas
        private void interfaz() 
        {
            lblTitle.Text = "Usuarios"; 

            dgvLista.Columns[0].Visible = false;

            if (dgvLista.Rows.Count > 0)
            {
              
                dgvLista.Columns[dgvLista.Columns.Count-1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            }
        }
        //Metodo con eventos para los botones del formulario cuando se les de click
        private void addEventos()
        {
            cmdNuevo.Click += new EventHandler(nuevo);
            cmdModificar.Click += new EventHandler(modificar);
            cmdDesabilitar.Click += new EventHandler(desabilita);
            cmdAbilitar.Click += new EventHandler(abilita);
            cmdEliminar.Click+=new EventHandler(elimina);
        }
        // Metodo para refrescar la lista
        private void refrescaLista()
        {
            if (!oUsuario.getDatos(dgvLista))
            {
                MessageBox.Show(oUsuario.getError());
            }
           
     
        }


        // Metodo para el botón nuevo 
        private void nuevo(object sender, EventArgs e)
        {
            frmUsuario frmUs = new frmUsuario();// Instanciamos el frmUsuario con el nombre frmUs
            frmUs.ShowDialog();// Lo mandamos a llamar para que lo abra
            refrescaLista();// Refrescamos
        }

        // Metodo para el botón modificar un usuario 
        private void modificar(object sender, EventArgs e)
        {
            // Creamos una variable id a la cual le asignamos lo que tenemos en utilidades del dvfLista
            int id = Utilidades.OperacionesFormulario.getId(dgvLista);
            if (id > 0)// Si el id es mayor a 0
            {

                frmUsuario frmUs = new frmUsuario();// Instanciamos el formulario frmUsuario como frmUs
                frmUs.idUsuario = id;//del frmUs asignamos lo del id a idUsuario
                frmUs.ShowDialog();//Y mostramos un dialog
                refrescaLista();//refrescamos 
            }
            else//Si no 
            {
                MessageBox.Show("Debe existir una fila seleccionada");//Significa que no se selecciononiguna fila antes de dar click a modificar
            }
        }

        // Metodo para el botón deshabiitar un usuario
        private void desabilita(object sender, EventArgs e)
        {
            int id = Utilidades.OperacionesFormulario.getId(dgvLista);// Creamos una variable id a la cual le asignamos lo que tenemos en utilidades del dvfLista
            if (id > 0)// Si el id es mayor a 0
            {
                if (oUsuario.changeState(2, id))// Si el usuario es de estado "2" lo que en la base de datos significa deshabilitado
                {
                    refrescaLista();//Mandamos a llamar el evento refrescaLista
                }
                else// SI no
                {
                    MessageBox.Show("Ocurrio un error "+oUsuario.getError());// Nos manda un error
                }
               
            }
            else//Si no
            {
                MessageBox.Show("Debe existir una fila seleccionada");// No se selecciono fila a deshabilitar
            }
        }

        // Metodo que sirve para habilitar un usuario deshabilitado
        private void abilita(object sender, EventArgs e)
        {
            int id = Utilidades.OperacionesFormulario.getId(dgvLista);
            if (id > 0)
            {
                if (oUsuario.changeState(1, id))// Si el usuario cambia su estado a 1, que en la base de datos significa activo
                {
                    refrescaLista();// Refrescamos con el evento refrescaLista
                }
                else//Si no
                {
                    MessageBox.Show("Ocurrio un error " + oUsuario.getError());//Nos manda error
                }

            }
            else//Si no 
            {
                MessageBox.Show("Debe existir una fila seleccionada");//No se selecciono ninguna fila a habilitar
            }
        }

        //Metodo que sirve para el botón eliminar usuario
        private void elimina(object sender, EventArgs e)
        {
            int id = Utilidades.OperacionesFormulario.getId(dgvLista);
            if (id > 0)
            {
                if (MessageBox.Show("Estas seguro de eliminar el registro seleccionado", "Confirm delete", MessageBoxButtons.YesNo) == DialogResult.Yes)// Nos pregunta si queremos eliminar usuario al seleccionar y dar eliminar
                {
                    if (oUsuario.changeState(3, id))// Si el estado del usuario es 3, en la base de datos significa eliminado
                    {
                        refrescaLista();// Refrescamos con el evento refrescaLista
                    }
                    else//Si no
                    {
                        MessageBox.Show("Ocurrio un error " + oUsuario.getError());// Nos manda un error
                    }
                }

            }
            else//Si no 
            {
                MessageBox.Show("Debe existir una fila seleccionada");// No se seleccino ninguna fila a eliminar
            }
        }


        // Metodo con evento de botón que sirve para asignar valor de nada cuando el mouse entre a la caja de texto
        private void txtBuscarUsuario_MouseEnter(object sender, EventArgs e)
        {
            txtBuscarUsuario.Text = "";
        }

        // Metodo con evento de botón que sirve para asignar texto cuando se salga de la caja de texto
        private void txtBuscarUsuario__MouseLeave(object sender, EventArgs e)
        {
            if (txtBuscarUsuario.Text == "" && txtBuscarUsuario.Font.Italic == false)
            {
                txtBuscarUsuario.Text = "Buscar Usuario...";
                txtBuscarUsuario.ForeColor = Color.DarkGray;
                txtBuscarUsuario.Font = new Font(txtBuscarUsuario.Font, FontStyle.Regular);
            }
            else
            {
                txtBuscarUsuario.ForeColor = SystemColors.WindowText;
                txtBuscarUsuario.Font = new Font(txtBuscarUsuario.Font, FontStyle.Regular);
            }
        }

        // Metodo con evento de botón que sirve para filtrar datos por nombre desde el datagridview
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            (dgvLista.DataSource as DataTable).DefaultView.RowFilter = string.Format($"Nombre LIKE '{txtBuscarUsuario.Text}%'");//
        }

        private void spContenedor_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtBuscarUsuario_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
