using AccesDLL;
using SistemaVentas.Clases.Entidates;
using SistemaVentas.Clases.SQL.Transacciones;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVentas.Formularios.Ventas
{
    public partial class FormCierreCaja : Form
    {
        public FormCierreCaja()
        {
            InitializeComponent();
        }

        private void PtbxCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnFinalizarTurno_Click(object sender, EventArgs e)
        {
            ClsGuardar.CajaCerrarTurno(UsuarioCache.Id_Usuario, MemoriaCache.IdCaja, MemoriaCache.IdApertura);
            Close();

        }
    }
}
