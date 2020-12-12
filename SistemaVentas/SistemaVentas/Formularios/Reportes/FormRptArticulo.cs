using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVentas.Formularios.Reportes
{
    public partial class FormRptArticulo : Form
    {
        public FormRptArticulo()
        {
            InitializeComponent();
        }

        private void FormRptArticulo_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'DataSetPrincipal.Sp_Consulta_Empresas' Puede moverla o quitarla según sea necesario.
            this.Sp_Consulta_EmpresasTableAdapter.Fill(this.DataSetPrincipal.Sp_Consulta_Empresas);
            // TODO: esta línea de código carga datos en la tabla 'DataSetPrincipal.Sp_Consulta_Articulos' Puede moverla o quitarla según sea necesario.
            this.Sp_Consulta_ArticulosTableAdapter.Fill(this.DataSetPrincipal.Sp_Consulta_Articulos);
           
            this.reportViewer1.RefreshReport();
        }
    }
}
