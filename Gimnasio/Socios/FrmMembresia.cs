using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gimnasio.Socios 
{
    public partial class FrmMembresia : Form 
    {
        public int id = 0; // Definimos una variable de tipo publico enrero y la inicializamos en 0
        clsSocio oSocio = new clsSocio(); // Instanciamos la clase socio como oSocio
        Membresias.clsMembresia oMembresia = new Membresias.clsMembresia(); // Del paquete membresias instanciamos la clase membresias como oMembresia
        clsSocioMembresia oSocioMembresia = new clsSocioMembresia(); // De la clsSocioMembresia instanciamos en oSocioMembresia
        public FrmMembresia() 
        {
            InitializeComponent(); // Componentes del formulario
        }

        private void FrmMembresia_Load(object sender, EventArgs e) // Load del formulario
        {
            if (oSocio.getDatos(id)) 
            {
                lblNombre.Text = oSocio.datos.Nombre + " " + oSocio.datos.Paterno + " " + oSocio.datos.Materno; 
                lblTelefono.Text = oSocio.datos.Telefono; 
                txtObservaciones.Text = oSocio.datos.Observaciones; 

                if (oSocio.datos.foto != null) 
                {
                    System.IO.MemoryStream stream = new System.IO.MemoryStream(oSocio.datos.foto); 
                    Bitmap image = new Bitmap(stream); 
                    pbFoto.Image = image; 
                }
            }
            else 
            {
                MessageBox.Show(oSocio.getError()); 
            }
            //llenado de combo
            Membresias.clsMembresia.getCboMembresias(cboMembresia); 
            if (cboMembresia.Items.Count <= 0) 
            {
                MessageBox.Show("No existen tipos de membresias agregadas al sistema, por favor ve al modulo de membresias y agregar una para poder ser asignada a los socios"); //
                this.Close(); 
            }
            

            refrescaLista(); // Llamamos al metodo refrescaLista
            interfaz(); // Llamamod al metodo interfaz
        }

        // Metodo para la interfaz
        private void interfaz()
        {
            try // Excepciones
            {
                dgvLista.Columns[0].Visible = false; // Ponemos el datagriview en falso
                dgvLista.Columns[dgvLista.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Damos tamaño automatico 

                Utilidades.OperacionesFormulario.quitaOrdenamientoGridView(dgvLista); 

            }
            catch (Exception ex) // Muestra excepción del tipo ex
            {
                MessageBox.Show("Error de sistema " + ex.Message); // Muestra mensaje de error
            }

        }

        // Metodo para seleccionar membresias
        private void cboMembresia_SelectedIndexChanged(object sender, EventArgs e) 
        {
            try // Esxcepciones
            {
                if (cboMembresia.Items.Count > 0) 
                {
                    id = int.Parse(cboMembresia.SelectedValue.ToString()); 

                    if (oMembresia.getDatos(id)) 
                    {
                        lblPrecio.Text = oMembresia.datos.Precio.ToString(); 
                        lblMeses.Text = oMembresia.datos.meses.ToString(); 
                        lblHoraInicial.Text = oMembresia.datos.horaInicio.ToString(); 
                        lblHoraFinal.Text = oMembresia.datos.horaFinal.ToString(); 
                    }
                }
                //Con esto evitamos la falla al abrir el formulario
            }catch{ } 

        }
        // Metodo refrescar 
        private void refrescaLista() 
        {
            if (!oSocioMembresia.getDatos(dgvLista, oSocio.datos.idSocio)) // Si no existe
            {

                MessageBox.Show(oSocioMembresia.getError()); // Nos muestra un error
            }
            else // Si no
            {
                if (dgvLista.Rows.Count > 0) // Si las filas son mayores a 0
                {
                 //   dtpFechaInicio.Enabled = false;
                    dtpFechaInicio.MinDate = DateTime.Parse(Utilidades.OperacionesFormulario.getValorCelda(dgvLista, 2, 0)); //Asignamos la fecha de inicio
                    dtpFechaInicio.Value = DateTime.Parse(Utilidades.OperacionesFormulario.getValorCelda(dgvLista, 2, 0));
                }
                else //Si no
                {
                   // dtpFechaInicio.Enabled = true;
                    dtpFechaInicio.MinDate = DateTime.Parse("01/01/1753");
                    dtpFechaInicio.Value = DateTime.Now;
                }
            }
        }

        // Metodo con evento de botón que sirve para agregar la membresia al soci
        private void btnAgregarM_Click(object sender, EventArgs e) 
        {
            oSocioMembresia.idMembresia = oMembresia.datos.idMembresia; 
            oSocioMembresia.idSocio = oSocio.datos.idSocio; 
            oSocioMembresia.Precio = oMembresia.datos.Precio; 
            oSocioMembresia.fechaInicioMembresia = dtpFechaInicio.Value; 
            oSocioMembresia.idUsuarioLog = Utilidades.clsUsuario.idUsuario; 

            if (oSocioMembresia.add()) 
            {
                refrescaLista(); 
            }
            else 
            {
                MessageBox.Show(oSocioMembresia.getError()); 
            }
        }

        // Metodo con evento de botón que sirve para eliminar una membresia del socio
        private void btnEliminarM_Click(object sender, EventArgs e) 
        {
            if (dgvLista.Rows.Count > 0) 
            {
                int id = int.Parse(Utilidades.OperacionesFormulario.getValorCelda(dgvLista, 0, 0)); 
                if (id > 0) 
                {
                    if (MessageBox.Show("Estas seguro de eliminar el ultimo registro agregado de membresia", "Confirm delete", MessageBoxButtons.YesNo) == DialogResult.Yes) 
                    {
                        if (oSocioMembresia.changeState(3, id)) 
                        {
                            refrescaLista(); 
                        }
                        else 
                        {
                            MessageBox.Show("Ocurrio un error " + oSocioMembresia.getError()); 
                        }
                    }

                }
                else 
                {
                    MessageBox.Show("Debe existir una fila seleccionada"); 
                }
            }
            else 
            {
                MessageBox.Show("No existen membresias para ser eliminadas"); 
            }
        }
    }
}
