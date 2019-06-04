using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gimnasio.Entradas 
{
    class clsDetalleEntrada :Utilidades.clsModulo 
    {
        public dsGimnasio.detalleentradaRow datos; // Instanciamos

        public decimal CostoUnitario=0; 
        public int idProducto = 0, idEntrada;

        // Metodo que realizara el refresh de los datos
        public override bool getDatos(System.Windows.Forms.DataGridView dgv) 
        {
            clear(); 
            bool exito = false; 
            try 
            {

                dsGimnasioTableAdapters.vwentradasTableAdapter ta = new dsGimnasioTableAdapters.vwentradasTableAdapter(); 
                dsGimnasio.vwentradasDataTable dt = ta.GetData();
                dgv.DataSource = dt;
                exito = true;
            }
            catch (Exception ex)
            {
                error.Add(ex.Message);
            }

            return exito;
        }

        // Metodo que llena los datos de un solo registro
        public override bool getDatos(int id)
        {
            throw new NotImplementedException();
        }

        // Metodo que agrega un registro
        public override bool add()
        {
            clear();
            bool exito = false;
            try
            {
                dsGimnasioTableAdapters.detalleentradaTableAdapter ta = new dsGimnasioTableAdapters.detalleentradaTableAdapter();
                ta.add(idProducto,CostoUnitario,idEntrada);

                exito = true;
            }
            catch (Exception ex)
            {
                error.Add(ex.Message);
            }

            return exito;
        }

        // Metodo que cambia el estado del registro
        public override bool changeState(int newState, int id)
        {
            throw new NotImplementedException();
        }

        // Metodo que sirve para editar un registro
        public override bool edit(int id)
        {
            throw new NotImplementedException();
        }

        // Metodo que sirve para la busqueda
        public override bool search(System.Windows.Forms.DataGridView dgv, int campo, string valor)
        {
            throw new NotImplementedException();
        }
    }
}
