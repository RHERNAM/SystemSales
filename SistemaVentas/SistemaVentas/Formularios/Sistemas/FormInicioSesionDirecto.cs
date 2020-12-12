using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaVentas.Clases;

namespace SistemaVentas.Formularios.Sistemas
{
    public partial class FormInicioSesionDirecto : Form
    {
        public FormInicioSesionDirecto()
        {
            InitializeComponent();
        }

        

        private void PtboxCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnTeclado_Click(object sender, EventArgs e)
        {
            //ClsSoporte.OcultarMostraPanel(PanelTeclado,PanelTeclado);
           // ClsSoporte.OcultarPorBotonTexto(BtnTeclado, "Teclado en Pantalla", "Ocultar Teclado");
        }

        private void FormInicioSesionDirecto_Load(object sender, EventArgs e)
        {
            
        }

        private void PtboxUsuario_Click(object sender, EventArgs e)
        {

        }
    }
}
