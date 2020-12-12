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
    public partial class FormColonias : Form
    {
        private DataTable DtRegistros;
        private string Resultado;
        public FormColonias()
        {
            InitializeComponent();
        }

        private void FormColonias_Load(object sender, EventArgs e)
        {
            ListaCombos();
            ConsultaColonias();
        }

        private void ListaCombos()
        {
            CmbxPais.DataSource = Consulta.PaisListCombo();
            CmbxPais.DisplayMember = "Nombre";
            CmbxPais.ValueMember = "IdPais";
        }

        private void ConsultaColonias()
        {
            radGridView1.DataSource = Consulta.Colonias();
        }

        private void CmbxPais_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (CmbxPais.SelectedValue.ToString() != null)
            {

                int pais = Convert.ToInt32(CmbxPais.SelectedValue);
                DtRegistros = Consulta.Estados(pais);
                if (DtRegistros.Rows.Count > 0)
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
                CmbxMunicipio.DataSource = null;
            }
        }

        private void CmbxEstado_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (CmbxEstado.SelectedValue.ToString() != null)
            {

                int Estado = Convert.ToInt32(CmbxEstado.SelectedValue);
                DtRegistros = Consulta.MunicipioListCombo(Estado);
                if (DtRegistros.Rows.Count > 0)
                {
                    CmbxMunicipio.DataSource = DtRegistros;
                    CmbxMunicipio.DisplayMember = "Nombre";
                    CmbxMunicipio.ValueMember = "IdMunicipio";
                }
                else
                {
                    CmbxMunicipio.DataSource = null;
                    CmbxMunicipio.Text = "";
                }
                
            }


        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(CmbxMunicipio.SelectedValue) == 0)
            {
                Soporte.MsgError("Seleccione una Colonia");
            }
            else
            {
                Resultado = ClsGuardar.ColoniaGuardar(TxtColonia.Text, TxtDescripcion.Text, Convert.ToInt32(CmbxMunicipio.SelectedValue), Convert.ToInt32(TxtCodigoPostal.Text));

                if (Resultado == "Insertado")
                {
                    ConsultaColonias();
                    Limpiar();
                    Soporte.MsgInformacion("La Colonia se ha registrado correctamente");
                }
                else
                {
                    Soporte.MsgError(Resultado);
                }
            }

        }

        private void Limpiar()
        {
            CmbxPais.SelectedValue = 0;
            CmbxEstado.DataSource = null;
            CmbxMunicipio.DataSource = null;
            TxtCodigoPostal.Clear();
            TxtColonia.Clear();
            TxtDescripcion.Clear();
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Resultado = Eliminar.Colonias(Convert.ToInt32(radGridView1.CurrentRow.Cells["IdColonia"].Value));
            if (Resultado == "Eliminado")
            {
                Limpiar();
                ConsultaColonias();
                Soporte.MsgInformacion("El registro se ha eliminado correctamente");


            }
            else
            {
                Soporte.MsgError(Resultado);
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            BtnCancelar.BackColor = Color.FromArgb(255, 102, 0);
            BtnCancelar.ForeColor = Color.White;
            BtnGuardar.BackColor = Color.FromArgb(255, 102, 0);
            BtnGuardar.ForeColor = Color.White;
            BtnLimpiar.BackColor = Color.FromArgb(255, 102, 0);
            BtnLimpiar.ForeColor = Color.White;
        }

        private void BtnLimpiar_MouseMove(object sender, MouseEventArgs e)
        {
            BtnLimpiar.ForeColor = Color.FromArgb(255, 102, 0);
        }

        private void BtnCancelar_MouseMove(object sender, MouseEventArgs e)
        {
            BtnCancelar.ForeColor = Color.FromArgb(255, 102, 0);
        }

        private void BtnGuardar_MouseMove(object sender, MouseEventArgs e)
        {
            BtnGuardar.ForeColor = Color.FromArgb(255, 102, 0);
        }
    }
}
