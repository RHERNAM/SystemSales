using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccesDLL;
using Excel;
using SistemaVentas.Clases.SQL;

namespace SistemaVentas.Formularios.Administracion
{
    public partial class FormImportacion : Form
    {
        private DataSet dataSet;
        public FormImportacion()
        {
            InitializeComponent();
            ListaEntidades();
        }

        private void BtnSeccionarArchivo_Click(object sender, EventArgs e)
        {
            var resultado = openFileDialog1.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
                BtnCargarArchivos.Enabled = true;
            }
        }

        private void ListaEntidades()
        {
            CmbDestino.DataSource = Consulta.EntidadesLista();
            CmbDestino.DisplayMember = "Nombre";
            CmbDestino.ValueMember = "Id_Entidades";
        }

        private void BtnCargarArchivos_Click(object sender, EventArgs e)
        {
            var fila = new FileInfo(textBox1.Text);
            using (var stream = new FileStream(textBox1.Text, FileMode.Open))
            {
                IExcelDataReader lecturaexcel = null;

                if (fila.Extension == ".xls")
                {
                    lecturaexcel = ExcelReaderFactory.CreateBinaryReader(stream);
                    BtnImportar.Enabled = true;
                }
                else if (fila.Extension == ".xlsx")
                {
                    lecturaexcel = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    BtnImportar.Enabled = true;
                }

                if (lecturaexcel == null)
                {
                    return;
                }
                lecturaexcel.IsFirstRowAsColumnNames = ChkFilaNombres.Checked;
                dataSet = lecturaexcel.AsDataSet();

                var tablaNombres = ObtenerNombreTablas(dataSet.Tables);
                CmbHojasExcel.DataSource = tablaNombres;

                if (tablaNombres.Count > 0)
                {
                    CmbHojasExcel.SelectedIndex = 0;
                }

            }
        }

        private void SeleccionarTabla()
        {
            var TablaNomnbre = CmbHojasExcel.SelectedItem.ToString();
            int NumTable = CmbHojasExcel.SelectedIndex;

            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = dataSet;
            dataGridView1.DataMember = TablaNomnbre;
        }


        private IList<string> ObtenerNombreTablas(DataTableCollection tablas)
        {
            var ListaTabla = new List<string>();
            foreach (var tabla in tablas)
            {
                ListaTabla.Add(tabla.ToString());
            }

            return ListaTabla;
        }
       
        private void ImportarSLQ()
        {
           string mensaje= Consulta.Immportar(dataSet, CmbHojasExcel.SelectedIndex, CmbDestino.Text);
           Soporte.MsgInformacion(mensaje);
        }

        private void CmbHojasExcel_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionarTabla();
        }

        private void BtnImportar_Click(object sender, EventArgs e)
        {
            ImportarSLQ();
           
        }

        private void PtbxCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
