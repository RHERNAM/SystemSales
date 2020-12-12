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

namespace SistemaVentas.Formularios.Configuraciones
{
    public partial class FormEstados : Form
    {
        private string Resultado;
        public FormEstados()
        {
            InitializeComponent();
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormEstados_Load(object sender, EventArgs e)
        {
            ConsultaEstados();
            ConsultaPais();
        }

        private void ConsultaPais()
        {
            CmbxPais.DataSource = Consulta.PaisConsulta();
            CmbxPais.DisplayMember = "Nombre";
            CmbxPais.ValueMember = "IdPais";


        }

        private void ConsultaEstados()
        {
            radGridView1.DataSource = Consulta.Estados();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            Resultado = ClsGuardar.EstadoInsertar(textBox1.Text, textBox2.Text, Convert.ToInt32(CmbxPais.SelectedValue));

            if (Resultado=="Insertado")
            {
                Limpiar();
                ConsultaEstados();
                Soporte.MsgInformacion("El Registro se ha guardado correctamente");
             
            }
            else
            {
                Soporte.MsgError(Resultado);
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Resultado = Eliminar.Estados(Convert.ToInt32(radGridView1.CurrentRow.Cells["IdEstado"].Value));
            if (Resultado=="Eliminado")
            {
                Limpiar();
                ConsultaEstados();
                Soporte.MsgInformacion("El registro se ha eliminado correctamente");
             
                
            }
            else
            {
                Soporte.MsgError(Resultado);
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        private void Limpiar()
        {
            textBox1.Clear();
            textBox2.Clear();
        }
    }
}
