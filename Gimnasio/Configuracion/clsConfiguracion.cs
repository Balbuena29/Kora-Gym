using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gimnasio.Configuracion 
{
    class clsConfiguracion : Utilidades.clsModulo 
    {
        public static dsGimnasio.configuracionRow datos; // Se establece el origen de datos
        public  string NombreGimnacio="", Domicilio="", Telefono=""; 
        public  Byte[] Logo; 
        public  int idUsuarioLog=0; 
        public  int mensajeVencimiento=0; 
        public  string RFC = "", Mensaje = "";  
        public static string Error="";
        // Metodo que realizara el refresh de los datos
        public override bool getDatos(System.Windows.Forms.DataGridView dgv)
        {
            throw new NotImplementedException();
        }

        // Metodo que llea los datos de un solo registro
        public override bool getDatos(int id)
        {
            clear();
            bool exito = false; 
            try 
            {
                dsGimnasioTableAdapters.configuracionTableAdapter ta = new dsGimnasioTableAdapters.configuracionTableAdapter();
                dsGimnasio.configuracionDataTable dt = ta.GetDataById(id);

                if (dt.Rows.Count > 0)
                {
                    datos = (dsGimnasio.configuracionRow)dt.Rows[0];
                    exito = true;
                }


            }
            catch (Exception ex)
            {
                error.Add(ex.Message);
            }

            return exito;
        }

        // Metodo que sirve para obtener datos de el gimnasio
        public static  bool getDatos()
        {
            Error = "";
            bool exito = false;
            try
            {
                dsGimnasioTableAdapters.configuracionTableAdapter ta = new dsGimnasioTableAdapters.configuracionTableAdapter();
                dsGimnasio.configuracionDataTable dt = ta.GetDataById(1);

                if (dt.Rows.Count > 0)
                {
                    datos = (dsGimnasio.configuracionRow)dt.Rows[0];
                    exito = true;
                }


            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }

            return exito;
        }

        // Metodo que agrega un registro
        public override bool add()
        {
            throw new NotImplementedException();
        }

        // Metodo que cambia un registro
        public override bool changeState(int newState, int id)
        {
            throw new NotImplementedException();
        }

        // Metodo que edita un registro
        public override bool edit(int id)
        {
            clear();
            bool exito = false;
            try
            {
                dsGimnasioTableAdapters.configuracionTableAdapter ta = new dsGimnasioTableAdapters.configuracionTableAdapter();
                ta.edit(NombreGimnacio, Domicilio, Telefono, Logo, idUsuarioLog, mensajeVencimiento, RFC, Mensaje);

                exito = true;
            }
            catch (Exception ex)
            {
                error.Add(ex.Message);
            }

            return exito;
        }

        // Metodo utilizado para la busqueda
        public override bool search(System.Windows.Forms.DataGridView dgv, int campo, string valor)
        {
            throw new NotImplementedException();
        }
    }
}
