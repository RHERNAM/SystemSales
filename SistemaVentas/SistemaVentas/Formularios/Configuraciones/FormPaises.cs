using AccesDLL;
using SistemaVentas.Clases.SQL;
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
using Telerik.WinControls.Styles;

namespace SistemaVentas.Formularios.Configuraciones
{
    public partial class FormPaises : Form
    {
        private string Resultado;
        public FormPaises()
        {
            InitializeComponent();
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            Resultado = ClsGuardar.PaisGuardar(textBox1.Text, textBox2.Text);
            if (Resultado == "Insertado")
            {
                Soporte.MsgInformacion("El Registro se ha Guardado correctamente.");
                ConsultaPais();
            }
            else
            {
                Soporte.MsgError(Resultado);
            }
           
           
        }

        private void FormPaises_Load(object sender, EventArgs e)
        {
            ConsultaPais();
        }


        private void ConsultaPais()
        {
            radGridView1.DataSource = Consulta.PaisConsulta();
            radGridView1.BestFitColumns();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Resultado= Eliminar.Pais(Convert.ToInt32(radGridView1.CurrentRow.Cells["IdPais"].Value));
            if (Resultado=="Eliminado")
            {
                Soporte.MsgInformacion("Se ha eliminado correctamente el nombre del Pais");
                ConsultaPais();
            }
            else
            {
                Soporte.MsgError(Resultado);
            }

        }
    }
}
