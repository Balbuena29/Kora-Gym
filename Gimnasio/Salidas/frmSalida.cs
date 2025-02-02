﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gimnasio.Salidas 
{
    public partial class frmSalida : Form 
    {

        Productos.clsProducto oProducto = new Productos.clsProducto();// Instanciamos producto de la clase producto
        clsSalida oEntrada = new clsSalida();// Instanciamos la clase salida
        decimal TOTAL = 0; 

        
        public struct Producto// Estructura para productos
        {
           public int idProducto;
            public int cantidad;
            public string nombre;
        }

        public frmSalida()// Formuario
        {
            InitializeComponent();// Componentes del formulario
        }

        private void frmEntrada_Load(object sender, EventArgs e)//Load del formulario
        {
            Productos.clsProducto.getProductosEnCbo(cboProducto);// Mandamos a llamar un metodo de autocompletar
        }

        //Metodo que muestra etiquetas de precio y costo
        private void cboProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idProducto = 0;
            try
            {
                idProducto = int.Parse(cboProducto.SelectedValue.ToString());
                oProducto.getDatos(idProducto);

                lblCosto.Text = oProducto.datos.Costo.ToString();
                lblPrecio.Text = oProducto.datos.Precio.ToString();

            }
            catch
            {
            }
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {

        }

        //Metodo de caja de texto que sirve para agregar producto
        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
        {

            //Al dar enter se agrega el producto
            if (e.KeyCode == Keys.Enter)
            {
                if (!ExpresionesRegulares.RegEX.isNumber(txtCantidad.Text))
                {
                    MessageBox.Show("Solo debes capturar numeros en cantidad");
                    txtCantidad.Text = "";
                    txtCantidad.Focus();
                    return;
                }

                if (oProducto.datos==null)
                {
                    MessageBox.Show("Debes seleccionar un producto");
                    cboProducto.Text = "";
                    cboProducto.Focus();
                    return;
                }

                int cantidad=int.Parse(txtCantidad.Text.ToString());
                decimal total = cantidad * oProducto.datos.Precio;


               


                //Aqui se agrega al datagridview
                dgvLista.Rows.Add(new object[] { oProducto.datos.idProducto.ToString(),cantidad.ToString(),
                                                oProducto.datos.Nombre,oProducto.datos.Precio.ToString(),total.ToString(),"Eliminar"});

                calcularTotal();
              
                txtCantidad.Text = "1";
                cboProducto.Text = "";
                lblCosto.Text = "";
                lblPrecio.Text = "";
                cboProducto.Focus();
                oProducto = new Productos.clsProducto();
                    

            }
        }

        // Metodo para eliminar producto cuando se selecciona un dato de el datagriview
        private void dgvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //AquÍ solo sigues si se presiono la columna del boton
            if (e.RowIndex < 0 || e.ColumnIndex != dgvLista.Columns["Operaciones"].Index)
                return;

            //Se obtiene el valor del productio
            int idProducto = int.Parse(dgvLista.Rows[e.RowIndex].Cells[0].Value.ToString());

            if (MessageBox.Show("Estas seguro de eliminar el producto de esta entrada", "Confirm delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                dgvLista.Rows.RemoveAt(e.RowIndex);
                calcularTotal();
            }

        }

        // Metodo que calcula el total de ganancia 
        private void calcularTotal()
        {
            TOTAL = 0;
            foreach (DataGridViewRow dr in dgvLista.Rows)
            {
                decimal totalD=decimal.Parse(dr.Cells[4].Value.ToString());
                TOTAL += totalD; 
            }
            lblTotal.Text = "$ " + TOTAL.ToString();

        }

        //Metodo con evento de botón donde se guardan las salidas
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //Validaciones
                //Verificar si hay existencias
                if (!verificaExistenciasProductos())
                {
                    return;
                }
                //Verificar si hay detalle
                if(dgvLista.Rows.Count<=0){
                    MessageBox.Show("Deben existir productos en la lista de la salida");
                    return;
                }

                //Asignacion de datos
                oEntrada.Total = TOTAL;
                oEntrada.idUsuarioLog = Utilidades.clsUsuario.idUsuario;
                //Llenar detalle
                foreach (DataGridViewRow dr in dgvLista.Rows)
                {
                    int cant = int.Parse(dr.Cells[1].Value.ToString());
                    for (int i = 0; i < cant; i++)
                    {
                        clsDetalleSalida de= new clsDetalleSalida();
                        de.CostoUnitario = decimal.Parse(dr.Cells[3].Value.ToString());
                        de.idProducto = int.Parse(dr.Cells[0].Value.ToString());
                        oEntrada.lDetalle.Add(de);
                    }
                }
                //Se guarda
                if (oEntrada.add())
                {
                    MessageBox.Show("Registro agregado con exito"); 
                    this.Close(); //
                }
                else
                    MessageBox.Show(oProducto.getError()); 

            }
            catch (Exception EX) 
            {
                MessageBox.Show("Ocurrio un error de sistema " + EX.Message); 
            }
        }


        /// <summary>
        /// metodo que verifica si hay existencias en inventario
        /// </summary>
        /// <returns></returns>
        private bool verificaExistenciasProductos()
        {
            bool exito = false;
            List<Producto> lProducto = new List<Producto>();
            //recorre el gridview y agrupamos
            foreach (DataGridViewRow dr in dgvLista.Rows)
            {
                Producto p= new Producto();
                p.idProducto=int.Parse(dr.Cells[0].Value.ToString());
                p.cantidad = int.Parse(dr.Cells[1].Value.ToString());
                p.nombre = dr.Cells[2].Value.ToString();
                int pos=buscaProducto(lProducto,p.idProducto);//si existe aqui cachamos la posicion
                if ( pos> -1)
                {
                    Producto p2 =lProducto[pos];
                    p2.cantidad += p.cantidad;
                    lProducto[pos] = p2;
                }
                else
                {
                    lProducto.Add(p);
                }

            }

            //buscamos el producto y su existencia
          //  MessageBox.Show(lProducto.Count.ToString());
            bool band = false;
            foreach (Producto p in lProducto) 
            {
                if (!clsSalida.existenciaProducto(p.cantidad, p.idProducto)) 
                {
                    MessageBox.Show("El producto "+p.nombre+" no tiene existencia suficiente para ser vendida, agrega una nueva entrada para poder realizar esta operación");
                    band = true;
                    break;
                }
            }
            //si no hay incidencia de que no se ajuste un producto no se guarda
            if (!band)
            {
                exito = true;
            }


            return exito;
        }
        // Metodo que busva un producto con foreach
        private int buscaProducto(List<Producto> lProducto,int idproducto)
        {
            int pos = -1; 
            int i=0; 
            foreach (Producto p in lProducto) 
            {
                if (idproducto == p.idProducto) 
                    return i; 
                i++;
            }
            return pos;
        }
    }
}
