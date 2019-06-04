using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gimnasio.Socios 
{
    class clsSocio : Utilidades.clsModulo 
    {
        public dsGimnasio.socioRow datos;// Del origen de datos de la fia socio lo asignamos datos
        public string Nombre = "";// Variable del tipo cadena inicializada
        public string Materno = "";// Variable del tipo cadena inicializada
        public string Paterno = "";// Variable del tipo cadena inicializada
        public string Observaciones = "";// Variable del tipo cadena inicializada
        public string Telefono = "";// Variable del tipo cadena inicializada
        public byte[] foto = null;// Variable del tipo byte para la foto con valor nulo
        public int idUsuarioLog = 0;// Variable del tipo entero  inicializada

        public override bool getDatos(System.Windows.Forms.DataGridView dgv)// Metodo que obtiene datos del DataGridView
        {
            clear();// Limpia atributos
            bool exito = false;
            try
            {
                dsGimnasioTableAdapters.vwsociosTableAdapter ta = new dsGimnasioTableAdapters.vwsociosTableAdapter();
                dsGimnasio.vwsociosDataTable dt = ta.GetData();
                dgv.DataSource = dt;// Asignamos data source  a dt
                exito = true;
            }
            catch (Exception ex)// Excepción del tipo ex
            {
                error.Add(ex.Message);// mensaje de error
            }

            return exito;// retornamos
        }

        public override bool getDatos(int id)// Metodo que llena datos
        {
            clear();
            bool exito = false;
            try//
            {
                dsGimnasioTableAdapters.socioTableAdapter ta = new dsGimnasioTableAdapters.socioTableAdapter();
                dsGimnasio.socioDataTable dt = ta.GetDataById(id);

                if (dt.Rows.Count > 0)
                {
                    datos = (dsGimnasio.socioRow)dt.Rows[0];
                    exito = true;
                }


            }
            catch (Exception ex)
            {
                error.Add(ex.Message);
            }

            return exito;
        }

        public override bool add()// Metodo que agrega un registro
        {
            clear();
            bool exito = false;
            try
            {
                dsGimnasioTableAdapters.socioTableAdapter ta = new dsGimnasioTableAdapters.socioTableAdapter();
                ta.add(Nombre, Paterno, Materno, Telefono, Observaciones, idUsuarioLog, foto);

                exito = true;
            }
            catch (Exception ex)
            {
                error.Add(ex.Message);
            }

            return exito;
        }

        public override bool changeState(int newState, int id)// Metodo que cambia el estado de un registro
        {
            clear();
            bool exito = false;
            try
            {
                dsGimnasioTableAdapters.socioTableAdapter ta = new dsGimnasioTableAdapters.socioTableAdapter();
                ta.cambiaEstado(newState, id);

                exito = true;
            }
            catch (Exception ex)
            {
                error.Add(ex.Message);
            }

            return exito;
        }

        public override bool edit(int id)// Metodo que edita un registro
        {
            clear();
            bool exito = false;
            try
            {
                dsGimnasioTableAdapters.socioTableAdapter ta = new dsGimnasioTableAdapters.socioTableAdapter();
                ta.edit(Nombre, Paterno, Materno, Telefono, Observaciones, foto, id);

                exito = true;
            }
            catch (Exception ex)
            {
                error.Add(ex.Message);
            }

            return exito;
        }

        // Metodo que sirve para la busqueda
        public override bool search(System.Windows.Forms.DataGridView dgv, int campo, string valor)
        {
            throw new NotImplementedException();//
        }

        //Reportes

        // Metodo que obtiene los datos del reportesocios
        public bool getDatosRptSocios(System.Windows.Forms.DataGridView dgv)
        {
            clear();
            bool exito = false;
            try
            {
                dsGimnasioTableAdapters.rptsociosTableAdapter ta = new dsGimnasioTableAdapters.rptsociosTableAdapter();
                dsGimnasio.rptsociosDataTable dt = ta.GetData();
                dgv.DataSource = dt;
                exito = true;
            }
            catch (Exception ex)
            {
                error.Add(ex.Message);
            }

            return exito;
        }

        // Metodo que obtiene los datos del reporte registro
        public bool getDatosRptRegistro(System.Windows.Forms.DataGridView dgv,DateTime fecha)
        {
            clear();
            bool exito = false;
            try
            {
                dsGimnasioTableAdapters.rptvisitasTableAdapter ta = new dsGimnasioTableAdapters.rptvisitasTableAdapter();
                dsGimnasio.rptvisitasDataTable dt = ta.GetDataByFecha(fecha);
                dgv.DataSource = dt;
                exito = true;
            }
            catch (Exception ex)
            {
                error.Add(ex.Message);
            }

            return exito;
        }

        // Metodo que obtiene los datos del reporte visitas
        public bool getDatosRptVisitas(System.Windows.Forms.DataGridView dgv, DateTime fecha)
        {
            clear();
            bool exito = false;
            try
            {
                dsGimnasioTableAdapters.rptvisitasTableAdapter ta = new dsGimnasioTableAdapters.rptvisitasTableAdapter();
                dsGimnasio.rptvisitasDataTable dt = ta.GetDataVisitasByFecha(fecha);
                dgv.DataSource = dt;
                exito = true;
            }
            catch (Exception ex)
            {
                error.Add(ex.Message);
            }

            return exito;
        }
     

    }

}
