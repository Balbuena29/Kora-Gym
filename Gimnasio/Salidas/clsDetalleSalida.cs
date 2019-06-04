using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gimnasio.Salidas 
{
    class clsDetalleSalida :Utilidades.clsModulo 
    {
        public dsGimnasio.detallesalidaRow datos; //Se manda a llamar al origen de datos

        public decimal CostoUnitario=0; 
        public int idProducto = 0, idSalida;

        // Metodo que obtiene datos del datagridview
        public override bool getDatos(System.Windows.Forms.DataGridView dgv) 
        {
            clear(); 
            bool exito = false; 
            try 
            {

                dsGimnasioTableAdapters.vwsalidasTableAdapter ta = new dsGimnasioTableAdapters.vwsalidasTableAdapter(); 
                dsGimnasio.vwsalidasDataTable dt = ta.GetData(); 
                dgv.DataSource = dt; 
                exito = true; 
            }
            catch (Exception ex) 
            {
                error.Add(ex.Message); 
            }

            return exito; 
        }

        //Metodo que llena los datos de un registro
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
                dsGimnasioTableAdapters.detallesalidaTableAdapter ta = new dsGimnasioTableAdapters.detallesalidaTableAdapter(); 
                ta.add(idProducto,CostoUnitario,idSalida); 

                exito = true; 
            }
            catch (Exception ex) 
            {
                error.Add(ex.Message); 
            }

            return exito; 
        }

        //Metodo que cambia el estado del registro
        public override bool changeState(int newState, int id) 
        {
            throw new NotImplementedException(); 
        }

        //Metodo que edita un registro
        public override bool edit(int id)
        {
            throw new NotImplementedException(); 
        }

        // Metodo utilizado para la busqueda
        public override bool search(System.Windows.Forms.DataGridView dgv, int campo, string valor) 
        {
            throw new NotImplementedException(); 
        }
    }
}
