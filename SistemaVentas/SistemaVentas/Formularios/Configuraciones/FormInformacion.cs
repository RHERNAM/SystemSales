using SistemaVentas.Clases.Entidates;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVentas.Formularios.Configuraciones
{
    public partial class FormInformacion : Form
    {
        public FormInformacion()
        {
            InitializeComponent();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormInformacion_Load(object sender, EventArgs e)
        {
            LblMensaje.Text = MemoriaCache.Mensaje;
            
        }
    }
}
