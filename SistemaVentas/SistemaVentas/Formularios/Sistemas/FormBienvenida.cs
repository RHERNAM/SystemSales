using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaVentas.Clases.Entidates;

namespace SistemaVentas.Formularios.Sistemas
{
    public partial class FormBienvenida : Form
    {

        List<Color> colors = new List<Color>();
        int colorcurrent = 0;
        int color = 0;
        int LX, LY;
        public FormBienvenida()
        {
            InitializeComponent();
            LblCorreo.ForeColor = Color.White;
            LblUsuario.ForeColor = Color.White;
            label1.ForeColor = Color.White;
            label2.ForeColor = Color.White;
            colors.Add(Color.FromArgb(42, 123, 155));
            colors.Add(Color.FromArgb(0, 186, 173));
            colors.Add(Color.FromArgb(87, 199, 133));
            colors.Add(Color.FromArgb(173, 212, 92));
            colors.Add(Color.FromArgb(237, 221, 83));
            colors.Add(Color.FromArgb(255, 195, 0));
            colors.Add(Color.FromArgb(255, 141, 26));
            colors.Add(Color.FromArgb(255, 87, 51));
            colors.Add(Color.FromArgb(199, 0, 57));
        }


        protected override CreateParams CreateParams
        {
            get
            { CreateParams cp = base.CreateParams; cp.ExStyle |= 0x02000000;
                //Turn on WS_EX_COMPOSITED 
                    return cp;

            }
        }

         

        private void FormBienvenida_Load(object sender, EventArgs e)
        {
            MaximazarPrincipal();
            CargarDatosUser();
            this.Opacity = 0.0;
            circularProgressBar1.Value = 0;
            circularProgressBar1.Minimum = 0;
            circularProgressBar1.Maximum = 100;
            timer1.Start();

        }


        #region Cargar Datos Usuario

        private void CargarDatosUser()
        {
            LblUsuario.Text = UsuarioCache.Nombre + " " + UsuarioCache.Apellido_Paterno;
            LblCorreo.Text = UsuarioCache.Email;
            byte[] imagenbufer = UsuarioCache.Imagen;
            MemoryStream ms = new MemoryStream(imagenbufer);
            OvlImagen.BackgroundImage = Image.FromStream(ms);
            OvlImagen.BackgroundImageLayout = ImageLayout.Stretch;
        }
        #endregion

        #region Maximizar Formulario Login

        public void MaximazarPrincipal()
        {
            LX = Location.X;
            LY = Location.Y;
            Size = Screen.PrimaryScreen.WorkingArea.Size;
            Location = Screen.PrimaryScreen.WorkingArea.Location;

        }

        #endregion

        #region Timer 1
       
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1) this.Opacity += 0.05;
            circularProgressBar1.Value += 1;
            circularProgressBar1.Text = circularProgressBar1.Value.ToString();
            if (circularProgressBar1.Value == 100)
            {
                timer1.Stop();
                timer2.Start();

            }
        }

        #endregion

        #region Timer 2
       
        private void timer2_Tick(object sender, EventArgs e)
        {
            this.Opacity -= 0.1;
            if (this.Opacity == 0)
            {
                timer2.Stop();

                this.Close();
            }
        }

        #endregion

        #region Timer Color
        
        private void TmrColor_Tick(object sender, EventArgs e)
        {
            TmrColor.Enabled = false;
            if (colorcurrent < colors.Count - 1)
            {
                this.BackColor = Bunifu.Framework.UI.BunifuColorTransition.getColorScale(color, colors[colorcurrent], colors[colorcurrent + 1]);
                if (color < 40)
                {
                    color++;
                }
                else
                {
                    color = 0;
                    colorcurrent++;

                }
                TmrColor.Enabled = true;
            }
        }
        #endregion

    }
}
