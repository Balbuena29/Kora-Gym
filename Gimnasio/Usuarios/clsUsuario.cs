using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gimnasio.Usuarios 
{
    class clsUsuario : Utilidades.clsModulo 
    {
        public dsGimnasio.usuarioRow datos; //Lalamos al origen de datos como publico de la tabla usuario en la fila y le llamamos dato
        public string Usuario=""; // Variable publica de tipo cadena llamada usuario inicializada con nada
        public string Password=""; // Variable publica de tipo cadena llamada password inicializada con nada
        public string Nombre=""; // Variable publica de tipo cadena llamada nombre inicializada con nada

        public override bool getDatos(System.Windows.Forms.DataGridView dgv) // Realiza el refresh de los datos
        {
            clear(); // Metodo que limpia atributos que deben limpiarse
            bool exito = false; // Variable de tipo booleano inicializado en falso
            try // Para el manejo de excepciones
            {
                dsGimnasioTableAdapters.vwusuariosTableAdapter taUsuarios = new dsGimnasioTableAdapters.vwusuariosTableAdapter(); //Instanciamos del origen de datos la vista de usuario con el nombre taUsuarios
                dsGimnasio.vwusuariosDataTable dtUsuarios = taUsuarios.GetData();// Del DataTable lo llamamos dtUsuarios y le asignamos lo del ta.Usuarios
                dgv.DataSource = dtUsuarios;// Obtiene el origen de datos se asignalo del dtUsuarios
                exito = true;// Cambiamos la varible booleana de falso a verdadero
            }
            catch (Exception ex)// Una excepción del tipo ex
            {
                error.Add(ex.Message);// Mensaje de error
            }
            
            return exito;
        }

        // Metodo que llena los datos de un solo registro
        public override bool getDatos(int id)
        {
            clear();// Metodo que limpia atributos
            bool exito = false; //
            try//
            {
                 dsGimnasioTableAdapters.usuarioTableAdapter taUsuarios = new dsGimnasioTableAdapters.usuarioTableAdapter(); //Instanciamos del origen de datos la vista de usuario con el nombre taUsuarios
                dsGimnasio.usuarioDataTable dtUsuarios = taUsuarios.GetDataByIdUsuario(id);

                if (dtUsuarios.Rows.Count > 0)// Si el numero de filas es mayor a 0
                {
                    datos = (dsGimnasio.usuarioRow)dtUsuarios.Rows[0]; //Se asigna lo de la fila a datos
                    exito = true;// cambiamos la variable booleana a verdadera
                }

                
            }
            catch (Exception ex)// Una excepción del tipo ex
            {
                error.Add(ex.Message);// Nos manda un error
            }

            return exito;// Retornamos exito
        }

        // Metodo que agrega un registro
        public override bool add()
        {
            clear();// Metodo que limpia atributos
            bool exito = false;// Variable booleana inicializada en falso
            try// Manejo de excepciones
            {
                dsGimnasioTableAdapters.usuarioTableAdapter taUsuarios = new dsGimnasioTableAdapters.usuarioTableAdapter();// Instanciamos del origen de datos la vista de usuario con el nombre taUsuarios
                taUsuarios.add(Usuario, Nombre, Password);// A la variable taUsuarios le mandamos parametros de usuario, nombre y password

                exito = true;// Cambiamos el valor de la variable booleana a verdadero
            }
            catch (Exception ex)// Excepcion del tipo ex
            {
                error.Add(ex.Message);// Nos manda un error
            }

            return exito;
        }

        // Metodo que cambia el estado del registro
        public override bool changeState(int newState, int id)
        {
            clear();// Metodo que limpia atributos
            bool exito = false;// Variable booleana inicializada en falso
            try// Manejo de excepciones
            {
                dsGimnasioTableAdapters.usuarioTableAdapter taUsuarios = new dsGimnasioTableAdapters.usuarioTableAdapter();// Instanciamos del origen de datos la vista de usuario con el nombre taUsuarios
                taUsuarios.cambiaEstado(newState, id);// A la variable taUsuarios le mandamos un parametro de nuevo estado y su id

                exito = true;// cambiamos el valor de la variable a verdadero
            }
            catch (Exception ex)// Excepción del tipo ex
            {
                error.Add(ex.Message);// Nos manda un error
            }

            return exito;
        }

        //Metodo que edita un registro
        public override bool edit(int id)
        {
            clear();// Metodo que limpia atributos
            bool exito = false;// Variable booleana inicializada en falso
            try// Manejo de excepciones
            {
                dsGimnasioTableAdapters.usuarioTableAdapter taUsuarios = new dsGimnasioTableAdapters.usuarioTableAdapter();// Instanciamos del origen de datos la vista de usuario con el nombre taUsuarios
                taUsuarios.edit(Usuario, Nombre,Password,id);// A la variable taUsuarios le mandamos como parametro el usuario nombre password e id

                exito = true;// Cambiamos el valor de la variable a verdadero
            }
            catch (Exception ex)// Excepción del tipo ex
            {
                error.Add(ex.Message);// Nos manda un error
            }

            return exito;
        }

        // Metodo utilizado para la busqueda 
        public override bool search(System.Windows.Forms.DataGridView dgv, int campo, string valor)
        {
            throw new NotImplementedException();// Inicializa una nueva instancia
        }
    }
}
