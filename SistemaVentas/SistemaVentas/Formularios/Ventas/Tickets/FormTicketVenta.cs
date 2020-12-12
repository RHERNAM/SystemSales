using CrystalDecisions.CrystalReports.Engine;
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

namespace SistemaVentas.Formularios.Ventas.Tickets
{
    public partial class FormTicketVenta : Form
    {
        public FormTicketVenta()
        {
            InitializeComponent();
        }

        public void VistaPrevia(ReportDocument report)
        {
            crystalReportViewer1.ReportSource = report;
           // Repositorios.ImprimirDirecto(report);
        }
    }
}
