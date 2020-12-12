using AccesDLL;
using SistemaVentas.Clases.Entidates;
using SistemaVentas.Clases.SQL;
using SistemaVentas.Formularios.Sistemas.FormSoport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVentas.Formularios.Administracion
{
    public partial class FormInventario : Form
    {
        #region Variables
        private DataTable DtRegistros;
        DataTable dataTable = new DataTable();
        #endregion


        public FormInventario()
        {
            InitializeComponent();
        }

          
      
        private void FormInventario_Load(object sender, EventArgs e)
        {
            RegistroInvenatrio();
            RegistroKardex();
        }

        #region Metodos

        #region Inventario        
        private void RegistroInvenatrio()
        {
            DtRegistros = Consulta.InventarioLista();
            RadgvInventario.DataSource = DtRegistros;
            RadgvInventario.BestFitColumns();
        }
        #endregion

      


        #endregion

        #region Eventos 

        private void PtboxCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }



        #endregion


        #region Pagina Movimiento

        #region Metodos 

        #region Kardex

        #region Consulta KARDEX

        private void RegistroKardex()
        {
           
            dataTable = Consulta.KardexRegistros();          
            dataGridView1.DataSource = dataTable;
            Ayuda.Multilinea(ref dataGridView1);
        }

        #endregion

        #region Buscar Articulo en Kardex
        private void BuscarArticuloKerdex(string sCodigo)
        {
            try
            {
                DtRegistros = Kardex.ConsultaKardexArticupoCodigo(sCodigo);
                if (DtRegistros.Rows.Count > 0)
                {
                    dataGridView1.DataSource = DtRegistros;
                    Ayuda.Multilinea(ref dataGridView1);
                }
                else
                {
                    DgvAutoCompletar.Visible = false;
                    TxtBuscarArticulo.Text = "Escriba lo que desea buscar...";
                    TxtBuscarArticulo.ForeColor = Color.Gray;
                    dataGridView1.DataSource = dataTable;
                    Soporte.MsgError("No se encontro ningun producto.");
                }
            }
            catch (Exception ex )
            {

                Soporte.MsgError(ex.Message);
            }
           
           
        }

        #endregion

        #region Buscar Articulo Para Autocompletado

        private void OcultarColumasDataGrid()
        {
            DgvAutoCompletar.Columns["Id_Articulo"].Visible = false;
            DgvAutoCompletar.Columns["Codigo"].Visible = false;
            DgvAutoCompletar.Columns["Nombre"].Visible = false;
            DgvAutoCompletar.Columns["Marca"].Visible = false;
            DgvAutoCompletar.Columns["Presentacion"].Visible = false;
            DgvAutoCompletar.Columns["Unidad_Medida"].Visible = false;
            DgvAutoCompletar.Columns["Contenido_Neto"].Visible = false;
            DgvAutoCompletar.Columns["Fecha_Registro"].Visible = false;
            DgvAutoCompletar.Columns["Fecha_Actualizacio"].Visible = false;
            DgvAutoCompletar.Columns["Fabricante"].Visible = false;
            DgvAutoCompletar.Columns["Cantidad_Minimo"].Visible = false;
            DgvAutoCompletar.Columns["Cantidad_Maximo"].Visible = false;
            DgvAutoCompletar.Columns["Unidad_Venta"].Visible = false;
            DgvAutoCompletar.Columns["Foto"].Visible = false;
            DgvAutoCompletar.Columns["Estatus"].Visible = false;
        }

        private void ValidarTextoCodigoABuscar(string Codigo)
        {
            if (TxtBuscarArticulo.Text != "" && TxtBuscarArticulo.Text != "Escriba lo que desea buscar...")
            {

                DtRegistros = Consulta.ArticuloBuscarXdescripcion(Codigo);
                DgvAutoCompletar.DataSource = DtRegistros;
                if (DgvAutoCompletar.Rows.Count > 0)
                {
                    OcultarColumasDataGrid();
                    DgvAutoCompletar.Visible = true;
                    
                }
                else
                {
                    DgvAutoCompletar.Visible = false;
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #region Eventos
        private void TxtBuscarArticulo_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtBuscarArticulo.Text) && TxtBuscarArticulo.Text!= "Escriba lo que desea buscar...")
            {
                
                ValidarTextoCodigoABuscar(TxtBuscarArticulo.Text);

            }
            else
            {
                DgvAutoCompletar.Visible = false;
                dataGridView1.DataSource = dataTable;
            }
        }

        private void PtbxOcultarPnlizquierda_Click(object sender, EventArgs e)
        {
            PnlKardexAcumulado.Visible = false;
            //PnlKardex.Visible = true;
        }

        private void radPageViewPage1_Paint(object sender, PaintEventArgs e)
        {

        }




        #endregion

        #endregion

        private void TxtBuscarArticulo_Enter(object sender, EventArgs e)
        {
            Soporte.EventoEnterTxt(TxtBuscarArticulo, "Escriba lo que desea buscar...", "");
            
        }

        private void TxtBuscarArticulo_Leave(object sender, EventArgs e)
        {
            Soporte.EventoLeaveTxt(TxtBuscarArticulo, "Escriba lo que desea buscar...", "");
        }

        private void DgvAutoCompletar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            BuscarArticuloKerdex(Convert.ToString(DgvAutoCompletar.CurrentRow.Cells["Codigo"].Value));
            DgvAutoCompletar.Visible = false;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
