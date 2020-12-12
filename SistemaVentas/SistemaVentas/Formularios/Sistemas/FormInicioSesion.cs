using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SistemaVentas.Clases.SQL;
namespace SistemaVentas.Formularios.Sistemas
{
    public partial class FormInicioSesion : Form
    {
    
        Color MuestracolorRGB = Color.FromArgb(25, 128, 0);
        Color MuestraColor = Color.DarkGray;

        Color EscribecolorRGB = Color.FromArgb(25, 128, 0);
        Color EscribeColor = Color.Black;

       // DataTable DtRegistros;

        public FormInicioSesion()
        {
            InitializeComponent();
        }

        
        private void BtnAcceder_Click(object sender, EventArgs e)
        {

            
                
            
        }

        

        private void PtboxCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PtboxCerrar_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnTeclado_Click(object sender, EventArgs e)
        {
            
        }

        private void TxtUsuario_Enter(object sender, EventArgs e)
        {
            //ClsSoporte.EventoEnterTxtPerColor(TxtUsuario, "USUARIO", "", EscribeColor);
          //Color Incluido
            
        }

        
        private void TxtUsuario_Leave(object sender, EventArgs e)
        {
          
        }

        private void TxtContraseña_Enter(object sender, EventArgs e)
        {
            //Soporte.EventoEnterTxtPassword(TxtContraseña, "CONTRASEÑA", "");//Color Incluido en 
        }

        private void TxtContraseña_Leave(object sender, EventArgs e)
        {
            //Color Incluido
        }



        private void PctbxOjo_Click(object sender, EventArgs e)
        {
           
           
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
