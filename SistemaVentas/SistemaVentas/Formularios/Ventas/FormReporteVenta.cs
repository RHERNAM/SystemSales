using CrystalDecisions.CrystalReports.Engine;
using SistemaVentas.Clases.SQL;
using SistemaVentas.Clases.Validaciones;
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
    public partial class FormReporteVenta : Form
    {
        public FormReporteVenta()
        {
            InitializeComponent();
        }

        private void FormReporteVenta_Load(object sender, EventArgs e)
        {

        }


        public void VistaPrevia(ReportDocument report)
        {
            crystalReportViewer1.ReportSource = report;
            Repositorios.ImprimirDirecto(report);
        }
    }
}
