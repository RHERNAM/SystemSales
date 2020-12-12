using SistemaVentas.Clases.Entidates;
using SistemaVentas.Clases.SQL.Transacciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SistemaVentas.Formularios.Sistemas.FormSoport
{
    public class Ayuda
    {
        public static void GuardaDetalleLog(string NomFormulario, string accion)
        {
            ClsGuardar.GuardarLogDetalle(BaseProp.IdLog, BaseProp.Modulo1 = NomFormulario, accion);
        }

		public static void Multilinea(ref DataGridView List)
		{
			//List.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
			//List.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
			List.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			List.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			List.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
			List.EnableHeadersVisualStyles = false;
			DataGridViewCellStyle styCabeceras = new DataGridViewCellStyle();
			styCabeceras.BackColor = Color.White;
			styCabeceras.ForeColor = Color.FromArgb(53, 53, 53);//Color.Black;
			styCabeceras.Font = new Font("Segoe UI", 10, FontStyle.Regular | FontStyle.Bold);
			List.ColumnHeadersDefaultCellStyle = styCabeceras;
		}
	}
}
