﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace Gimnasio
{
    static class Program 
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() 
        {
            Application.EnableVisualStyles(); // Abre aplicación
            Application.SetCompatibleTextRenderingDefault(false); 
            Application.Run(new FrmMain()); // Abre el formulario principal
        }
    }
}
