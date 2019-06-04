using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gimnasio.Utilidades 
{
    class clsGrafico //Clase clsGrafico
    {
        public static void centraX(Control padre, Control hijo) //Manipula la posición y el tamaño de algunos controles
        {
            int x = 0; 
            x = (padre.Width/2)-(hijo.Width/2); 

            hijo.Location = new System.Drawing.Point(x,hijo.Location.Y); 
        }
    }
}
