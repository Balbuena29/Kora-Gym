using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gimnasio.Entradas 
{
    class clsEntrada : Utilidades.clsModulo 
    {

        public List<clsDetalleEntrada> lDetalleEmtrada = new List<clsDetalleEntrada>(); // Instanciamos
        public dsGimnasio.entradaRow datos;// Instanciamos
        public int idUsuarioLog = 0;
        public decimal Total = 0;

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

        public bool getDatosDetalle(System.Windows.Forms.DataGridView dgv)// Metodo que obtiene los datos del datagridview de entrada
        {
            clear();
            bool exito = false;
            try
            {

                dsGimnasioTableAdapters.vwdetalleentradaviewTableAdapter ta = new dsGimnasioTableAdapters.vwdetalleentradaviewTableAdapter();
                dsGimnasio.vwdetalleentradaviewDataTable dt = ta.GetDataById(datos.idEntrada);
                dgv.DataSource = dt;
                exito = true;
            }
            catch (Exception ex)
            {
                error.Add(ex.Message);
            }

            return exito;
        }

        // Metodo que llena los datos de solo un registro
        public override bool getDatos(int id)
        {
            clear();
            bool exito = false;
            try
            {
                dsGimnasioTableAdapters.entradaTableAdapter ta = new dsGimnasioTableAdapters.entradaTableAdapter();
                dsGimnasio.entradaDataTable dt = ta.GetDataById(id);

                if (dt.Rows.Count > 0)
                {
                    datos = (dsGimnasio.entradaRow)dt.Rows[0];
                    exito = true;
                }


            }
            catch (Exception ex)
            {
                error.Add(ex.Message);
            }

            return exito;
        }

        // Metodo que agrega un registro
        public override bool add()
        {
              clear();
            bool exito = false;
            try
            {
                    dsGimnasioTableAdapters.entradaTableAdapter ta = new dsGimnasioTableAdapters.entradaTableAdapter();//
                ta.add(idUsuarioLog,Total);

                int? idEntrada=0;//Utilizada pa ultimo id insertado de entrada
                    //Dar de alta entradas
                    try
                    {
                        foreach (clsDetalleEntrada de in lDetalleEmtrada)
                    {
                            dsGimnasioTableAdapters.QueriesTableAdapter query = new dsGimnasioTableAdapters.QueriesTableAdapter();
                        idEntrada = query.getLastIdEntrada();

                        de.idEntrada = idEntrada.Value;
                        de.add();
                    }
                    }
                    catch (Exception ex)
                {
                        this.changeState(3, idEntrada.Value);//Se elimina la ultima entrada
                        error.Add(ex.Message);
                }

                    exito = true;
            }
                catch (Exception ex)
            {
                    error.Add(ex.Message);
            }

                return exito;
        }

        public override bool changeState(int newState, int id)// Metodo que cambia el estado del registro
        {
            clear();
            bool exito = false;
            try
            {
                dsGimnasioTableAdapters.entradaTableAdapter ta = new dsGimnasioTableAdapters.entradaTableAdapter();
                ta.cambiaEstado(newState, id);

                exito = true;
            }
            catch (Exception ex)
            {
                error.Add(ex.Message);
            }

            return exito;
        }

        /// <summary>
        /// elimina utilizando delete
        /// </summary>
        /// <param name="id">id del registro</param>
        /// <returns></returns>
        /// // Metodo que elimina un registro
        public bool remove(int id)
        {
            clear();
            bool exito = false;
            try
            {
                dsGimnasioTableAdapters.entradaTableAdapter ta = new dsGimnasioTableAdapters.entradaTableAdapter();
                ta.remove(id);

                exito = true;
            }
            catch (Exception ex)
            {
                error.Add(ex.Message);
            }

            return exito;
        }

        /// <summary>
        /// verifica si es posible eliminar una entrada
        /// </summary>
        /// <param name="id">id de registro</param>
        /// <returns></returns>
        /// //Metodo que verifica si es posible eliminar una entrada
        public bool posibleEliminar(int id)
        {
            bool exito = false;
            dsGimnasioTableAdapters.QueriesTableAdapter query = new dsGimnasioTableAdapters.QueriesTableAdapter();
            long? res = (long)query.getNumEntradasUtilizadas(id);
            if (res == 0)
            {
                exito = true;
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
