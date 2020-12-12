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
    public partial class FormMunicipios : Form
    {
        private DataTable DtRegistros;

        private string Resultado;
        public FormMunicipios()
        {
            InitializeComponent();
        }

        private void FormMunicipios_Load(object sender, EventArgs e)
        {
            ConsultaMunicipio();
            ListaCombos();
        }
        private void ListaCombos()
        {           
            CmbxPais.DataSource = Consulta.PaisListCombo();
            CmbxPais.DisplayMember = "Nombre";
            CmbxPais.ValueMember = "IdPais";
        }

        private void ConsultaMunicipio()
        {
            radGridView1.DataSource = Consulta.MunicipioConsulta();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32( CmbxEstado.SelectedValue)==0)
            {
                Soporte.MsgError("Seleccione un estado");
            }
            else
            {
                Resultado = ClsGuardar.MunicipioInsertar(TxtMunicipio.Text, TxtDescripcion.Text, Convert.ToInt32(CmbxEstado.SelectedValue));

                if (Resultado == "Insertado")
                {
                    ConsultaMunicipio();
                    Limpiar();
                    Soporte.MsgInformacion("El Municipio se ha regustrado correctamente");
                }
                else
                {
                    Soporte.MsgError(Resultado);
                }
            }
            

        }

      

        private void CmbxPais_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (CmbxPais.SelectedValue.ToString() != null)
            {

                int pais = Convert.ToInt32(CmbxPais.SelectedValue);
                DtRegistros= Consulta.Estados(pais);
                if (DtRegistros.Rows.Count>0)
                {
                    CmbxEstado.DataSource = DtRegistros;
                    CmbxEstado.DisplayMember = "Nombre";
                    CmbxEstado.ValueMember = "IdEstado";
                }
                else
                {
                    CmbxEstado.DataSource = null;
                    CmbxEstado.Text = "";
                }
               
            }
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            CmbxPais.SelectedValue = 0;
            CmbxEstado.DataSource = null;
            TxtDescripcion.Clear();
            TxtMunicipio.Clear();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Resultado = Eliminar.Municipio(Convert.ToInt32(radGridView1.CurrentRow.Cells["IdMunicipio"].Value));
            if (Resultado == "Eliminado")
            {
                Limpiar();
                ConsultaMunicipio();
                Soporte.MsgInformacion("El registro se ha eliminado correctamente");


            }
            else
            {
                Soporte.MsgError(Resultado);
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            BtnCancelar.BackColor = Color.FromArgb(118, 185, 25);
            BtnCancelar.ForeColor = Color.White;
            BtnGuardar.BackColor = Color.FromArgb(118, 185, 25);
            BtnGuardar.ForeColor = Color.White;
            BtnLimpiar.BackColor = Color.FromArgb(118, 185, 25);
            BtnLimpiar.ForeColor = Color.White;
        }

        private void BtnLimpiar_MouseMove(object sender, MouseEventArgs e)
        {
            BtnLimpiar.ForeColor = Color.FromArgb(89, 146, 9);
        }

        private void BtnCancelar_MouseMove(object sender, MouseEventArgs e)
        {
          BtnCancelar.ForeColor = Color.FromArgb(89, 146, 9);
        }

        private void BtnGuardar_MouseMove(object sender, MouseEventArgs e)
        {
            BtnGuardar.ForeColor = Color.FromArgb(89, 146, 9);
        }
    }
}
