using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient; //Paquete para poder usar la base de datos
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using System.Diagnostics;

namespace Gimnasio.Socios
{
    public partial class frmSocios : Gimnasio.Utilidades.frmPadre //public partial es algo nuevo del framework, con el nombre frmSocios 
    {
        clsSocio oSocio = new clsSocio(); //Se instancia la clase socio en el formulario actual con el nombre "oSocio"




        public frmSocios()
        {
            InitializeComponent(); 
        }

        private void frmSocios_Load(object sender, EventArgs e) //Es el load que viene por defecto en el formulario, donde se inician eventos ya creados
        {
            addEventos(); //Mandamos a llamar el evento "addEventos", que se ejecuta al inicio del programa
            refrescaLista(); //Mandamos a llamar el evento "refrescaLista"
            interfaz(); //Mandamoa a llamar al evento "interfaz"
        }

        //Evento con funciones de interfaz
        private void interfaz() 
        {
            try //Excepciones
            {
                lblTitle.Text = "Socios"; //Se le da el nombre a la etiquea titulo del formulario socios
                dgvLista.Columns[dgvLista.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; //Indica si la columna va a ajustar automáticamente su ancho y cómo va a determinar su ancho preferido

            }
            catch (Exception ex) //Cachamos la excepción
            {
                MessageBox.Show("Error de sistema " + ex.Message); //Mensaje de error que muestra el sistema 
            }

        }
        private void addEventos() //Evento con funciones de tabla
        {
            cmdNuevo.Click += new EventHandler(nuevo);
            cmdModificar.Click += new EventHandler(modificar);
            cmdDesabilitar.Click += new EventHandler(desabilita);
            cmdAbilitar.Click += new EventHandler(abilita);
            cmdEliminar.Click += new EventHandler(elimina);
            button1.Click += new EventHandler(membresia);
        }

        private void refrescaLista() //Evento con funciones de refresco
        {
            if (!oSocio.getDatos(dgvLista)) //Si no se obtienen datos del datagriedview dgvLista 
            {
                MessageBox.Show(oSocio.getError()); //Nos genera un messagebox con un error
            }


        }

        //Eventos de botones
        //Metodo que sirve para el botón "Nuevo"
        private void nuevo(object sender, EventArgs e) 
        {
            frmSocio frmUs = new frmSocio(); //Se instancia el frmSocio con el nombre frmUs 
            frmUs.ShowDialog(); //Se muestra en un Dialog
            refrescaLista(); // Mandamos a llamar al evento refrescaLista
        }

        private void modificar(object sender, EventArgs e) //Netodo que sirve para el botón "Modificar"
        {
            int id = Utilidades.OperacionesFormulario.getId(dgvLista); //Variable de id de tipo entero, que le asignamos lo que tenemos en utilidades  de nuestro DataGridView
            if (id > 0) //Si el id es mayor a 0 (No existen id´s 0 en el programa, o negativas)
            {

                frmSocio frm = new frmSocio(); //Instanciamos el frmSocio con el nombre frm 
                frm.id = id; //Le asignamos el id a l id creado aquí
                frm.ShowDialog(); //Mostramos el formulario 
                refrescaLista(); //Llamamos al evento refrescarLista
            }
            else //En caso de no seleccionar alguien
            {
                MessageBox.Show("Debe existir una fila seleccionada"); //No seleccionamos ninguna fila antes de dar click al botón y nos manda error
            }
        }

        //Metodo que sirve para deshabilitar un socio
        private void desabilita(object sender, EventArgs e) 
        {
            int id = Utilidades.OperacionesFormulario.getId(dgvLista); //Variable de id de tipo entero, que le asignamos lo que tenemos en utilidades de nuestro DatGridView
            if (id > 0) //Si el id es mayor a 0 (No existen id´s 0 en el programa, o negativas)
            {
                if (oSocio.changeState(2, id)) //De oSocio cambiamos el valor del id a 2, esto significa deshabilitado
                {
                    refrescaLista(); //Mandamos a llamar al evento refresaLista
                }
                else // Si no 
                {
                    MessageBox.Show("Ocurrio un error " + oSocio.getError()); // En caso de no existir nos manda el mensaje de error
                }

            }
            else // Si no 
            {
                MessageBox.Show("Debe existir una fila seleccionada"); // Nos manda un MessageBox con el error que veemos
            }
        }

        // Metodo que sirve para habilitar un usuario deshabilitado
        private void abilita(object sender, EventArgs e) 
        {
            int id = Utilidades.OperacionesFormulario.getId(dgvLista); //Variable de id de tipo entero, que le asignamos lo que tenemos en utilidades de nuestro datagridView
            if (id > 0) // Si el id es mayor a 0 (No existen id´s 0 en el programa, o negativas)
            {
                if (oSocio.changeState(1, id)) // Se cambia el estado del socio por 1, esto significa habilitado
                {
                    refrescaLista(); // Mandamos a llamar el metodo refrescaLista
                }
                else // Si no 
                {
                    MessageBox.Show("Ocurrio un error " + oSocio.getError()); // Se nos abre un MessageBox con el error sucitado de la lista de errores
                }

            }
            else // Si no es ninguna de las anteriores
            {
                MessageBox.Show("Debe existir una fila seleccionada"); // Se nos abre un MessageBox con el error que veemos
            }
        }
        // Metodo que sirve para eliminar un socio
        private void elimina(object sender, EventArgs e) 
        {
            int id = Utilidades.OperacionesFormulario.getId(dgvLista); // Variable de id de tipo entero, que le asignamos lo que tenemos en utilidades de nuestro datagridView
            if (id > 0) // Si el id es mayor a 0 (No existen id´s 0 en el programa, o negativas)
            {
                if (MessageBox.Show("Estas seguro de eliminar el registro seleccionado", "Confirm delete", MessageBoxButtons.YesNo) == DialogResult.Yes) // Al dar click al botón nos pregunta si queremos borrar en caso de ser si se hace lo que viene abajo
                {
                    if (oSocio.changeState(3, id)) // Si el estado del socio cambia a 3, significa que esta eliminado
                    {
                        refrescaLista(); // Mandamos a llamar al Metodo refrescaLista
                    }
                    else // Si no
                    {
                        MessageBox.Show("Ocurrio un error " + oSocio.getError()); // Se nos abre un MessageBox con el error que mandamos llamar de la lista de errores
                    }
                }

            }
            else // Si NO
            {
                MessageBox.Show("Debe existir una fila seleccionada"); // Se nos abre un MessageBox con el error que veemos
            }
        }

        /// <summary>
        /// abre formulario de membresias de socios
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// // Metodo que asigna membresia a socio
        private void membresia(object sender, EventArgs e) 
        {
            int id = Utilidades.OperacionesFormulario.getId(dgvLista); // Variable de id de tipo entero, que le asignamos lo que tenemos en utilidades de nuestro datagridView
            if (id > 0) // Si el id es mayor a 0 (No existen id´s 0 en el programa, o negativas)
            {
                FrmMembresia frmM = new FrmMembresia(); // Instanciamos el FrmMembresia como frm
                frmM.id = id; // El id se lo asignamos al id creado
                frmM.ShowDialog(); // Abrimos el frmM
                refrescaLista(); // Mandamos a llamar al metdo refrescaLista
            }
            else // Si no
            {
                MessageBox.Show("Debe existir una fila seleccionada"); // Se nos abre un MessageBox con el error que veemos
            }
        }







        private void spContenedor_Panel1_Paint(object sender, PaintEventArgs e) // Es el panel 
        {

        }

        private void button2_Click(object sender, EventArgs e) // Metodo con evento de botón que no quise borrar
        {

        }

        private void textBox4_KeyUp(object sender, KeyEventArgs e) // Metodo con evento de textBox que sirve para cuando tecleamos una letra lo haga automaticamente
        {
            (dgvLista.DataSource as DataTable).DefaultView.RowFilter = string.Format($"Nombre LIKE '{textBox4.Text}%'"); // Del dgvLista en el DataTable filtramos las filas de acuerdo al textBox4, de acuerdo al nombre del socio
            //((DataTable)dgvLista.DataSource).DefaultView .RowFilter = "Nombre=" + textBox4.Text;
        }

        private void textBox4_MouseLeave(object sender, EventArgs e) // Metodo con evento de textBox  que sirve para ejecutar algo cuando pasamos el mouse sobre el textBox
        {
            
                if (textBox4.Text == "" && textBox4.Font.Italic == false) // Si el textBox4 no tiene nada y el textBox4 no tiene fuente italic
            {
                    textBox4.Text = "Buscar Socio..."; // Le asignamos el texto "Buscar Socio..."
                textBox4.ForeColor = Color.DarkGray; // Le asignamos el color al texto gris oscuro
                textBox4.Font = new System.Drawing.Font(textBox4.Font, FontStyle.Regular); // Le asgnamos el tamaño de la fuente al textBox4, regular
            }
            else // Si no 
            {
                    textBox4.ForeColor = SystemColors.WindowText; // El color del texto del TextBox sera el del sistema
                textBox4.Font = new System.Drawing.Font(textBox4.Font, FontStyle.Regular); // Le asgnamos el tamaño de la fuente al textBox4, regular
            }
            }

        private void textBox4_MouseEnter(object sender, EventArgs e) // Metodo con evento de textBox cuando el puntero esta dentro del TextBox
        {
            textBox4.Text = ""; // Le damos el valor del texto a nuestro textBox como "" que es vacio, para dar efecto 
        }

        private void textBox4_TextChanged(object sender, EventArgs e) // Metodo con evento de textBox que no quisimos borrar
        {

        }

     

        private void spContenedor_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click_1(object sender, EventArgs e)
        {

            if (dgvLista.RowCount == 0)
            {
                MessageBox.Show("No Hay Datos Para Realizar Un Reporte");
            }
            else
                { //ESCOJEA RUTA DONDE GUARDAREMOS EL PDF
                    SaveFileDialog save = new SaveFileDialog();
            save.Filter = "PDF Files(*.pdf)|*.pdf|All Files(*.*)|*.*";
            if (save.ShowDialog() == DialogResult.OK)
                    {
                        string filename = save.FileName;
            Document doc = new Document(PageSize.A3, 9, 9,9, 9);
            Chunk encab = new Chunk("REPORTE", FontFactory.GetFont("COURIER", 18));
                    try
                    {
                        FileStream file = new FileStream(filename,FileMode.OpenOrCreate);
                        PdfWriter writer = PdfWriter.GetInstance(doc, file);
                        writer.ViewerPreferences = PdfWriter.PageModeUseThumbs;
                        writer.ViewerPreferences = PdfWriter.PageLayoutOneColumn;
                        doc.Open();
                    doc.Add(new Paragraph(encab));
                    GenerarDocumentos(doc);
                    Process.Start(filename);
                    doc.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        // Función que genera el documento Pdf
        public void GenerarDocumentos(Document document)
        {
            // se crea un objeto PdfTable con el numero de columnas del dataGridView
            PdfPTable datatable = new PdfPTable(dgvLista.ColumnCount);
                    // asignamos algunas propiedades para el diseño del pdf
                    datatable.DefaultCell.Padding = 1;
                    float[] headerwidths = GetTamañoColumnas(dgvLista);
                    datatable.SetWidths(headerwidths);
                    datatable.WidthPercentage = 100;
                    datatable.DefaultCell.BorderWidth = 1;
                    //DEFINIMOS EL COLOR DE LAS CELDAS EN EL PDF
datatable.DefaultCell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
                    // DEFINIMOS EL COLOR DE LOS BORDES
datatable.DefaultCell.BorderColor = iTextSharp.text.BaseColor.BLACK;
                    // LA FUENTE DE NUESTRO TEXTO
iTextSharp.text.Font fuente = new iTextSharp.text.Font
(iTextSharp.text.Font.FontFamily.HELVETICA);
                    Phrase objP = new Phrase("A", fuente);
                    datatable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    //SE GENERA EL ENCABEZADO DE LA TABLA EN EL PDF
for (int i = 0; i < dgvLista.ColumnCount; i++)
            {
                objP = new Phrase(dgvLista.Columns[i].HeaderText, fuente);
                    datatable.HorizontalAlignment = Element.ALIGN_CENTER;
                    datatable.AddCell(objP);
            }
            datatable.HeaderRows = 2;
                    datatable.DefaultCell.BorderWidth = 1;
                    // SE GENERA EL CUERPO DEL PDF
for (int i = 0; i < dgvLista.RowCount; i++)
            {
                for (int j = 0; j < dgvLista.ColumnCount; j++)
                {
                    objP = new Phrase(dgvLista[j, i].Value.ToString(),fuente);
                    datatable.AddCell(objP);
                }
                datatable.CompleteRow();
            }
            document.Add(datatable);
        }
        //Función que obtiene los tamaños de Ias columnas del datagridview
        public float[] GetTamañoColumnas(DataGridView dg)
        {
            //Tomamos el numero de columnas
            float[] values = new float[dg.ColumnCount];
              for (int i = 0; i < dg.ColumnCount; i++)
            {
                //Tomamos el ancho de cada columna
                values[i] = (float) dg.Columns[i].Width;
            }
            return values;
                    }

        private void cmdNuevo_Click(object sender, EventArgs e)
        {

        }
    }
}

