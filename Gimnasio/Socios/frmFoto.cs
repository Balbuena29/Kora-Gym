using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace Gimnasio.Socios 
{
    public partial class frmFoto : Form 
    {
        private bool existenDispositivos = false; // variale privada booleana 
        private bool fotografiaHecha = false; // variale privada booleana 
        private FilterInfoCollection dispositivosDeVideo; // Para abrir foto
        private VideoCaptureDevice fuenteDeVideo = null; // para capturar foto
        public PictureBox pbFotoSocio = null;// Variable para asignar foto
        public frmFoto()//
        {
            InitializeComponent();// Componentes de la ventana
            BuscarDispositivos();// Metodo buscar dispositivos
        }

        private void pictureBox1_Click(object sender, EventArgs e)//
        {

        }

        // Load del formulario
        private void frmFoto_Load(object sender, EventArgs e)
        {
            if (existenDispositivos)
            {
        	    fuenteDeVideo = new VideoCaptureDevice(dispositivosDeVideo[0].MonikerString);
                fuenteDeVideo.NewFrame += new NewFrameEventHandler(MostrarImagen);
                fuenteDeVideo.Start();
            }
            else
            {
        	    MessageBox.Show("No se encuentra ningún dispositivo de vídeo en el sistema", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);//
                this.Close();
            }
        }

        /*
     *  Identifica los dispositivos disponibles
     */
        private void BuscarDispositivos() //
        {
            dispositivosDeVideo = new FilterInfoCollection(FilterCategory.VideoInputDevice); 

            if (dispositivosDeVideo.Count == 0) 
                existenDispositivos = false;
            else //
                existenDispositivos = true;
        }

        /*
         *  Muestra imagen en el PictureBox
         */
        private void MostrarImagen(object sender, NewFrameEventArgs eventArgs)//
        {
            Bitmap imagen = (Bitmap)eventArgs.Frame.Clone();//
            pbFoto.Image = imagen;

        }

        /*
         *  Deja de capturar imágenes, obteniendo la última capturada
         */
        private void Capturar()
        {
            if (fuenteDeVideo != null)
            {
                if (fuenteDeVideo.IsRunning)
                {
                    pbFotoSocio.Image = pbFoto.Image;
                    this.Close();
                }
            }
        }
        // Metodo con evento de botón para capturar foto
        private void btnFoto_Click(object sender, EventArgs e)
        {
            Capturar();
            fotografiaHecha = true;
        }

        // Cierra el ormulario foto
        private void frmFoto_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (fuenteDeVideo!=null)
                fuenteDeVideo.Stop();
        }

    }
}
