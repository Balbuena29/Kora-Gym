using CrystalDecisions.CrystalReports.Engine; 
using CrystalDecisions.Shared; // Reportes
using MySql.Data.MySqlClient; // Datos de la base de datos
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web.Services;
using System.IO;

namespace Gimnasio.Reportes 
{
    public partial class frmReportes : Form 
    {
        Productos.clsProducto oProducto = new Productos.clsProducto(); // Instanciamos productos de la clase producto
        Membresias.clsMembresia oMembresia = new Membresias.clsMembresia(); // Instanciamos membresias de la clase membresias
        Socios.clsSocio oSocio = new Socios.clsSocio(); // Instanciamos socios de la clase socios

        public frmReportes() // Formulario
        {
            InitializeComponent(); // Componentes iniciales
        }

        // Load que carga metodos
        private void frmReportes_Load(object sender, EventArgs e) 
        {
            refrescaListaInventario(); 
            refrescaListaMembresias(); 
            refrescaListaSocios();
            refrescaListaRegistro();
            refrescaListaVisitas();
            refrescaListaVentaProductos();

            interfaz();
        }



        //Metodo utilizado para exportar socios con membresia a reporte a excel
        public void ExportarExcelMem()
        {
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            app.Visible = false;
            worksheet = workbook.Sheets["Hoja1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "Usuarios";
            // Cabeceras
            for (int i = 0; i < dgvListaMembresias.Columns.Count + 1; i++)
            {
                if (i > 0  && i < dgvListaMembresias.Columns.Count)
                {
                    worksheet.Cells[i+1] = dgvListaMembresias.Columns[i].HeaderText;
                }
            }
            // Valores
            for (int i = 0; i < dgvListaMembresias.Rows.Count ; i++)
            {
                for (int j = 0; j < dgvListaMembresias.Columns.Count; j++)
                {
                    if (j > 0 && j < dgvListaMembresias.Columns.Count )
                    {
                        worksheet.Cells[i + 2, j + 1] = dgvListaMembresias.Rows[i].Cells[j].Value.ToString();
                    }
                }
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivos de Excel|*.xlsx";
            saveFileDialog.Title = "Guardar archivo";
            saveFileDialog.FileName = "NombredeArchivoDefault";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                Console.WriteLine("Ruta en: " + saveFileDialog.FileName);
                workbook.SaveAs(saveFileDialog.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                app.Quit();
            }

        }

        //Metodo utilizado para exportar inventario a reporte a excel
        public void ExportarExcelInv()
        {
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            app.Visible = false;
            worksheet = workbook.Sheets["Hoja1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "Usuarios";
            // Cabeceras
            for (int i = 0; i < dgvListaInventario.Columns.Count ; i++)
            {
                if (i > 0 && i < dgvListaInventario.Columns.Count)
                {
                    worksheet.Cells[ i +1] = dgvListaInventario.Columns[i ].HeaderText;
                }
            }
            // Valores
            for (int i = 0; i < dgvListaInventario.Rows.Count; i++)
            {
                for (int j = 0; j < dgvListaInventario.Columns.Count; j++)
                {
                    if (j > 0 && j < dgvListaInventario.Columns.Count)
                    {
                        worksheet.Cells[i +2, j+1 ] = dgvListaInventario.Rows[i].Cells[j].Value.ToString();
                    }
                }
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivos de Excel|*.xlsx";
            saveFileDialog.Title = "Guardar archivo";
            saveFileDialog.FileName = "NombredeArchivoDefault";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                Console.WriteLine("Ruta en: " + saveFileDialog.FileName);
                workbook.SaveAs(saveFileDialog.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                app.Quit();
            }

        }

        //Metodo utilizado para exportar registro de reporte a excel
        public void ExportarExcelReg()
        {
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            app.Visible = false;
            worksheet = workbook.Sheets["Hoja1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "Usuarios";
            // Cabeceras
            for (int i = 0; i < dgvListaInventario.Columns.Count; i++)
            {
                if (i > 0 && i < dgvListaInventario.Columns.Count)
                {
                    worksheet.Cells[i + 1] = dgvListaInventario.Columns[i].HeaderText;
                }
            }
            // Valores
            for (int i = 0; i < dgvListaInventario.Rows.Count; i++)
            {
                for (int j = 0; j < dgvListaInventario.Columns.Count; j++)
                {
                    if (j > 0 && j < dgvListaInventario.Columns.Count)
                    {
                        worksheet.Cells[i + 2, j + 1] = dgvListaInventario.Rows[i].Cells[j].Value.ToString();
                    }
                }
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivos de Excel|*.xlsx";
            saveFileDialog.Title = "Guardar archivo";
            saveFileDialog.FileName = "NombredeArchivoDefault";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                Console.WriteLine("Ruta en: " + saveFileDialog.FileName);
                workbook.SaveAs(saveFileDialog.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                app.Quit();
            }

        }



        //Metodo utilizado para exportar reporte de socios a excel
        public void ExportarExcelSoc()
        {
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            app.Visible = false;
            worksheet = workbook.Sheets["Hoja1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "Usuarios";
            // Cabeceras
            for (int i = 0; i < dgvListaSocios.Columns.Count; i++)
            {
                if (i > 0 && i < dgvListaSocios.Columns.Count)
                {
                    worksheet.Cells[i + 1] = dgvListaSocios.Columns[i].HeaderText;
                }
            }
            // Valores
            for (int i = 0; i < dgvListaSocios.Rows.Count; i++)
            {
                for (int j = 0; j < dgvListaSocios.Columns.Count; j++)
                {
                    if (j > 0 && j < dgvListaSocios.Columns.Count)
                    {
                        worksheet.Cells[i + 2, j + 1] = dgvListaSocios.Rows[i].Cells[j].Value.ToString();
                    }
                }
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivos de Excel|*.xlsx";
            saveFileDialog.Title = "Guardar archivo";
            saveFileDialog.FileName = "NombredeArchivoDefault";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                Console.WriteLine("Ruta en: " + saveFileDialog.FileName);
                workbook.SaveAs(saveFileDialog.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                app.Quit();
            }

        }

        //Metodo utilizado para exportar reporte de venta de productos a excel
        public void ExportarExcelVenP()
        {
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            app.Visible = false;
            worksheet = workbook.Sheets["Hoja1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "Usuarios";
            // Cabeceras
            for (int i = 0; i < dgvListaVentaProductos.Columns.Count; i++)
            {
                if (i > 0 && i < dgvListaVentaProductos.Columns.Count)
                {
                    worksheet.Cells[i + 1] = dgvListaVentaProductos.Columns[i].HeaderText;
                }
            }
            // Valores
            for (int i = 0; i < dgvListaVentaProductos.Rows.Count; i++)
            {
                for (int j = 0; j < dgvListaVentaProductos.Columns.Count; j++)
                {
                    if (j > 0 && j < dgvListaVentaProductos.Columns.Count)
                    {
                        worksheet.Cells[i + 2, j + 1] = dgvListaVentaProductos.Rows[i].Cells[j].Value.ToString();
                    }
                }
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivos de Excel|*.xlsx";
            saveFileDialog.Title = "Guardar archivo";
            saveFileDialog.FileName = "NombredeArchivoDefault";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                Console.WriteLine("Ruta en: " + saveFileDialog.FileName);
                workbook.SaveAs(saveFileDialog.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                app.Quit();
            }

        }

        //Metodo utilizado para exportar reporte de vista a excel
        public void ExportarExcelVis()
        {
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            app.Visible = false;
            worksheet = workbook.Sheets["Hoja1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "Usuarios";
            // Cabeceras
            for (int i = 0; i < dgvListaVisitas.Columns.Count; i++)
            {
                if (i > 0 && i < dgvListaVisitas.Columns.Count)
                {
                    worksheet.Cells[i + 1] = dgvListaVisitas.Columns[i].HeaderText;
                }
            }
            // Valores
            for (int i = 0; i < dgvListaVisitas.Rows.Count; i++)
            {
                for (int j = 0; j < dgvListaVisitas.Columns.Count; j++)
                {
                    if (j > 0 && j < dgvListaVisitas.Columns.Count)
                    {
                        worksheet.Cells[i + 2, j + 1] = dgvListaVisitas.Rows[i].Cells[j].Value.ToString();
                    }
                }
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivos de Excel|*.xlsx";
            saveFileDialog.Title = "Guardar archivo";
            saveFileDialog.FileName = "NombredeArchivoDefault";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                Console.WriteLine("Ruta en: " + saveFileDialog.FileName);
                workbook.SaveAs(saveFileDialog.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                app.Quit();
            }

        }

        // Metodo que refresca la lista inventario
        private void refrescaListaInventario()
        {
            if (!oProducto.getDatosRptInventario(dgvListaInventario))
            {
                MessageBox.Show(oProducto.getError());
            }


        }
        // Metodo que refresca la lista membresias
        private void refrescaListaMembresias()
        {
            DateTime fecha1 = dtpFecha1Membresia.Value;
            DateTime fecha2 = dtpFecha2Membresia.Value;

            if (!oMembresia.getDatosRptMembresias(dgvListaMembresias,fecha1,fecha2))
            {
                MessageBox.Show(oMembresia.getError());
            }

            
            lblTotalMembresia.Text = "$ " + getTotalMembresia().ToString();//Sacar el total
        }

        private void refrescaListaSocios() // Metodo que refresca la lista socios
        {
            if (!oSocio.getDatosRptSocios(dgvListaSocios))
            {
                MessageBox.Show(oSocio.getError());
            }


        }

        // Metodo que refresca la lista registro
        private void refrescaListaRegistro()
        {
            DateTime fecha = dtpFecha1Registro.Value;


            if (!oSocio.getDatosRptRegistro(dgvListaRegistro, fecha))
            {
                MessageBox.Show(oSocio.getError());
            }

        }

        // Metodo que refresca la lista Visitas
        private void refrescaListaVisitas()
        {
            DateTime fecha = dtpFecha1Visitas.Value;


            if (!oSocio.getDatosRptVisitas(dgvListaVisitas, fecha))
            {
                MessageBox.Show(oSocio.getError());
            }

          
            lblTotalVisitas.Text = "$ " + getTotalVisitas().ToString();  //Sacar el total
        }

        //Metodo que refresca la lista venta productos
        private void refrescaListaVentaProductos()
        {
            DateTime fecha1 = dtpFecha1VtaProductos.Value;
            DateTime fecha2 = dtpFecha2VtaProductos.Value;

            if (!oProducto.getDatosRptVentaProductos(dgvListaVentaProductos, fecha1, fecha2)) 
            {
                MessageBox.Show(oProducto.getError());
            }

           
            lblTotalVentaProductos.Text = "$ " + getTotalVentaProductos().ToString(); //Sacar el total
        }



        // Metodo con funciones de interfaz
        private void interfaz()
        {
            this.SuspendLayout();

            try
            {
                dgvListaInventario.Columns[0].Visible = false;
                dgvListaInventario.Columns[dgvListaInventario.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de sistema " + ex.Message);
            }

            try
            {
                dgvListaMembresias.Columns[0].Visible = false;
                dgvListaMembresias.Columns[dgvListaInventario.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dtpFecha1Membresia.Value = DateTime.Now.Date;
                dtpFecha2Membresia.Value = DateTime.Now.Date;

                dtpFecha1VtaProductos.Value = DateTime.Now.Date;
                dtpFecha2VtaProductos.Value = DateTime.Now.Date;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de sistema " + ex.Message);
            }

            try
            {
                dgvListaVentaProductos.Columns[0].Visible = false;
                dgvListaVentaProductos.Columns[dgvListaInventario.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dtpFecha1VtaProductos.Value = DateTime.Now.Date;
                dtpFecha2VtaProductos.Value = DateTime.Now.Date;

                dtpFecha1Registro.Value = DateTime.Now.Date;
                dtpFecha1Visitas.Value = DateTime.Now.Date;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de sistema " + ex.Message);
            }

            this.ResumeLayout();
        }


        // Metodo que obtiene el total de la membresía
        private decimal getTotalMembresia()
        {
            decimal total = 0;

            foreach (DataGridViewRow dr in dgvListaMembresias.Rows)
            {
                total+=decimal.Parse(dr.Cells[dr.Cells.Count-1].Value.ToString());
            }
            return total;
        }

        private decimal getTotalVisitas()// Metodo que obtiene el total de visitas
        {
            decimal total = 0;

            foreach (DataGridViewRow dr in dgvListaVisitas.Rows)
            {
                total += decimal.Parse(dr.Cells[dr.Cells.Count - 1].Value.ToString());
            }
            return total;
        }

        private decimal getTotalVentaProductos()//Metodo que ibtiene el total de venta de productos
        {
            decimal total = 0;

            foreach (DataGridViewRow dr in dgvListaVentaProductos.Rows)
            {
                total += decimal.Parse(dr.Cells[dr.Cells.Count - 1].Value.ToString());
            }
            return total;
        }


        //Metodo que manda llamar a otros metodos en un solo enter
        private void frmReportes_Enter(object sender, EventArgs e)
        {
            refrescaListaInventario();
            refrescaListaMembresias();
            refrescaListaSocios();
            refrescaListaRegistro();
            refrescaListaVisitas();
            refrescaListaVentaProductos();
        }

        // Metodo con evento de botón que ejecuta un metodo de refrescar lista de membresias
        private void button1_Click(object sender, EventArgs e)
        {
            refrescaListaMembresias();
        }

        // Metodo con evento de botón que ejecuta un metodo de refrescar lista de registro
        private void button2_Click(object sender, EventArgs e)
        
        {
            refrescaListaRegistro();
        }

        // Metodo con evento de botón que ejecuta un metodo de refrescar lista de visitas
        private void button4_Click(object sender, EventArgs e)
        {
            refrescaListaVisitas();
        }

        // Metodo con evento de botón que ejecuta un metodo de exportar venta de productos a excel
        private void button3_Click(object sender, EventArgs e)
        {
            refrescaListaVentaProductos();
        }

        // Metodo con evento de botón que ejecuta un metodo de exportar inventario a excel
        private void btnExInv_Click(object sender, EventArgs e)
        {
            ExportarExcelInv();
        }

        private void dgvListaInventario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        // Metodo con evento de botón que ejecuta un metodo de exportar reporte de membresia a excel
        private void btnExMem_Click(object sender, EventArgs e)
        {
            ExportarExcelMem();

        }

        //Función que obtiene los tamaños de las columnas del datagridview 
        public float[] GetTamañoColumnas(DataGridView dg)
        {
            //Tomamos el numero de columnas 
            float[] values = new float[dg.ColumnCount];
            for (int i = 0; i < dg.ColumnCount; i++)
            {
                //Tomamos el ancho de cada columna 
                values[i] = (float)dg.Columns[i].Width;
            }
            return values;
        }

        private void spSocios_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        // Metodo con evento de botón que ejecuta un metodo de exportar registro a excel
        private void btnExReg_Click(object sender, EventArgs e)
        {
            ExportarExcelReg();
        }

        // Metodo con evento de botón que ejecuta un metodo de exportar socios a excel
        private void btnExSoc_Click(object sender, EventArgs e)
        {
            ExportarExcelSoc();
        }

        // Metodo con evento de botón que ejecuta un metodo de exportar venta de productos a excel
        private void btnExVenP_Click(object sender, EventArgs e)
        {
            ExportarExcelVenP();
        }

        private void btnExVis_Click(object sender, EventArgs e)
        {

        }
    }
}
