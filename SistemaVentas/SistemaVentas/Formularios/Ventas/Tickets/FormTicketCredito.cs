using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVentas.Formularios.Ventas.Tickets
{
    public partial class FormTicketCredito : Form
    {
        public FormTicketCredito()
        {
            InitializeComponent();
        }

        public void VistaPrevia(ReportDocument report)
        {
            crystalReportViewer1.ReportSource = report;
            // Repositorios.ImprimirDirecto(report);
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
