using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gimnasio.Utilidades 
{
    class clsUsuario//Clase usuario
    {
        public static bool existeSesion=false;//Creamos un booleano para la sesión
        public static string usuario = "";//Un String para el usuario
        public static int idUsuario=0;//Un entero para el id de usuario
        public static string nombre="";//Un String publico para el nombre
        public static string error = "";//Un String publico para el error

        public static bool login(string usuario_,string password_) //Evento booleano para iniciar sesión con parametros de usuario y password
        {
            bool exito = false;//Una variable booleana inicializada en falso
            error = "";//Al error lo inicializamos con nada
            try// Un try para las excepciones
            {
                dsGimnasioTableAdapters.usuarioTableAdapter ta = new dsGimnasioTableAdapters.usuarioTableAdapter();// Del origen de datos dfGimnasioTableAdapters seleccionamos el usuariotableadapter y lo instanciamos con el nombre ta
                dsGimnasio.usuarioDataTable dt = ta.login(usuario_, password_);//A nuestro DataTable en usuarioDataTable le asignamos el nombre dt, con el ta mandamos a llamar al login con los paramtros usuario y pawwword
                dsGimnasio.usuarioRow dr = null;// Del origen de datos dsGimnasio  de la fila usuario le asignamos la variable dr con valor nulo
                if (dt.Rows.Count > 0)//De nuestra tabla usuario sea mayor a 0 
                {
                    dr = (dsGimnasio.usuarioRow)dt.Rows[0];// Valida que el usuario exista para la sesión
                    nombre = dr.Nombre;
                    idUsuario = dr.idUsuario;
                    usuario = dr.Usuario;
                    existeSesion = true;

                    exito = true;
                }
                else// Si no 
                {
                    error = "Usuario o password incorrecto";// Nos manda un mensaje de error
                }

            }catch(Exception ex)// Una excepción del tipo ex
            {
                error = "ERROR SISTEMA "+ex.Message;// Nos manda error de sistema como mensaje
            }

            return exito;// Retorna exito 
        }
        // Un metodo public booleano que sirve para el momento de dar click en cerrar sesión salga la ventana de iniciar seión
        public static bool salir()
        {
            bool exito = false;
            error = "";
            try
            {
                    nombre ="";
                idUsuario =0;
                usuario = "";
                existeSesion = false;

                exito = true;

            }
            catch (Exception ex)
            {
                error = "ERROR SISTEMA " + ex.Message;
            }

            return exito;
        }

    }
}
