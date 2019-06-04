using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gimnasio.Registro 
{
    class clsRegistro 
    {
         public dsGimnasio.vwultimamembresiadetalladaRow datos; //Instanciamos la vista de la ultima membresia
        public string error = "";  

        int clave = 0; 
        public clsRegistro(int clave_) // Metodo registro con parametro de clave
        {
             clave = clave_; 
        }

        // Metodo que busca los registros en la vista
        public bool buscaDatos()
        {
             bool exito = false; 
            try
             {
                 dsGimnasioTableAdapters.vwultimamembresiadetalladaTableAdapter ta = new dsGimnasioTableAdapters.vwultimamembresiadetalladaTableAdapter();//
                dsGimnasio.vwultimamembresiadetalladaDataTable dt = ta.GetDataByIdSocio(clave);
                if (dt.Rows.Count > 0)
                {
                     datos = (dsGimnasio.vwultimamembresiadetalladaRow)dt.Rows[0];
                    exito = true;
                }
             }
             catch { }
            return exito;
        }

        // Metodo que sirve para agregar una visita de un socio
        public bool addVisita()
        {
            
             bool exito = false;
            try
            {
                 dsGimnasioTableAdapters.visitaTableAdapter ta = new dsGimnasioTableAdapters.visitaTableAdapter();
                decimal precioVisita = 0;
                                         
                if (datos.idSocio <= 1000) precioVisita = obtenerPrecioVisita(); //Si es visita se agrega el precio de la visita

                ta.add(this.datos.idSocio, DateTime.Now,precioVisita); 

                exito = true; 
            }
             catch (Exception ex) 
            {
                 error=ex.Message; 
            }

             return exito; 
        }

        // Metodo que obtiene el precio de la visita
        private decimal obtenerPrecioVisita() 
        {
             decimal precio = 0; 

            dsGimnasioTableAdapters.membresiaTableAdapter ta = new dsGimnasioTableAdapters.membresiaTableAdapter();
            dsGimnasio.membresiaDataTable dt = ta.GetDataByIdMembresia(1);
            dsGimnasio.membresiaRow dr = (dsGimnasio.membresiaRow)dt.Rows[0];

            precio = dr.Precio;

            return precio;
        }


    }
}
