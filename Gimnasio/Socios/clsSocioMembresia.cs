using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gimnasio.Socios 
{
    class clsSocioMembresia : Utilidades.clsModulo 
    {
        dsGimnasio.sociomembresiaRow datos; // Del origen de datos 
        public int idUsuarioLog=0; // variable entera publica
        public int idSocio=0,idMembresia=0;// variable entera publica
        public decimal Precio=0;// variable decimal 
        public DateTime fechaInicioMembresia;// variabe del tipo datetime

        // Metodo que realizara el refresh de datos
        public override bool getDatos(System.Windows.Forms.DataGridView dgv)
        {
            clear();// Metodo que limpia atributos
            bool exito = false;// Variable booleana 
            try
            {
                dsGimnasioTableAdapters.vwsociomembresiasTableAdapter ta = new dsGimnasioTableAdapters.vwsociomembresiasTableAdapter();
                dsGimnasio.vwsociomembresiasDataTable dt = ta.GetData();
                dgv.DataSource = dt;
                exito = true;
            }
            catch (Exception ex)
            {
                error.Add(ex.Message);
            }

            return exito;
        }

        // Metodo que muestra datos de una cuadricula
        public bool getDatos(System.Windows.Forms.DataGridView dgv,int idSocio) 
        {
            clear();
            bool exito = false;
            try
            {
                dsGimnasioTableAdapters.vwsociomembresiasTableAdapter ta = new dsGimnasioTableAdapters.vwsociomembresiasTableAdapter();//
                dsGimnasio.vwsociomembresiasDataTable dt = ta.GetDataByIdSocio(idSocio);
                dgv.DataSource = dt;
                exito = true;
            }
            catch (Exception ex)
            {
                error.Add(ex.Message);
            }

            return exito;// Retornamos
        }

        // Metodo que llena los datos
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
                dsGimnasioTableAdapters.sociomembresiaTableAdapter ta = new dsGimnasioTableAdapters.sociomembresiaTableAdapter();
                ta.add(idUsuarioLog,idSocio,idMembresia,Precio,fechaInicioMembresia);

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
            clear();
            bool exito = false;
            try
            {
                dsGimnasioTableAdapters.sociomembresiaTableAdapter ta = new dsGimnasioTableAdapters.sociomembresiaTableAdapter();
                ta.cambiaEstado(newState, id);

                exito = true;
            }
            catch (Exception ex)
            {
                error.Add(ex.Message);
            }

            return exito;
        }

        // Metodo que edita un registro
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
