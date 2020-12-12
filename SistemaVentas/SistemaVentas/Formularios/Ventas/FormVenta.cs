using SistemaVentas.Clases.Entidates;
using SistemaVentas.Clases.SQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVentas.Formularios.Ventas
{
    public partial class FormVenta : Form
    {
        private ListaVenta listaVenta;
        List<ListaVenta> listaVentas= new List<ListaVenta>();
        private DataTable DtRegistros;
        private int iId_Articulo;
        private string sDescripcion;
        private string sCodigo;
        private string sNombreArt;
        private string sMarca;
        private int iId_Inventario;
        private decimal dPrecio;
        private decimal dTotalArticulos;
        private decimal dTotalVenta;
        private decimal dIva;
        private decimal dSubTotal;
        private decimal dCantidad;
        private decimal dTotalCantidad;
        private decimal Total;
        private decimal Precio;

        public FormVenta()
        {
            InitializeComponent();
        }

        private void PtbxCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormVenta_Load(object sender, EventArgs e)
        {

          
            

        }

        #region Metodos        

        #region Carga de Articulo para venta y Texboxt        

        private void BuscarArticulo(string Codigo)
        {


            DtRegistros = Consulta.CodigoBuscarTblArticulo(Codigo);
            if (DtRegistros.Rows.Count > 0)
            {
                iId_Articulo = Convert.ToInt32(DtRegistros.Rows[0]["ID_ARTICULO"].ToString());
                sDescripcion = DtRegistros.Rows[0]["Descripcion"].ToString();
                sCodigo = DtRegistros.Rows[0]["Codigo"].ToString();
                sNombreArt = DtRegistros.Rows[0]["Nombre"].ToString();
                sMarca = DtRegistros.Rows[0]["Nombre"].ToString();
                TxtDescripcion.Text = sDescripcion;

                if (DtRegistros.Rows[0]["Foto"] != DBNull.Value)
                {

                    byte[] imagenBuffer = (byte[])DtRegistros.Rows[0]["Foto"];
                    MemoryStream ms = new MemoryStream(imagenBuffer);
                    PctBoxImagen.Image = Image.FromStream(ms);
                    PctBoxImagen.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {

                    PctBoxImagen.SizeMode = PictureBoxSizeMode.Zoom;
                    PctBoxImagen.Image = Properties.Resources.LogoRHNegro;
                }
                #region Pasar datos a Texboxt

                //TxtIdArticulo.Text = (DtRegistros.Rows[0]["ID_ARTICULO"].ToString());
                //TxtCodigo.Text = (DtRegistros.Rows[0]["Codigo"].ToString());
                //TxtNombre.Text = (DtRegistros.Rows[0]["Nombre"].ToString());
                TxtDescripcion.Text = (DtRegistros.Rows[0]["Descripcion"].ToString());
                //TxtPresentacion.Text = (DtRegistros.Rows[0]["Presentacion"].ToString());
                //TxtCategoria.Text = (DtRegistros.Rows[0]["Categoria"].ToString());
                //TxtContenido.Text = (DtRegistros.Rows[0]["Contenido_Neto"].ToString());

                #endregion


                DtRegistros = Consulta.InventarioBuscaCodArt(Codigo);
                if (DtRegistros.Rows.Count > 0)
                {
                    iId_Inventario = int.Parse(DtRegistros.Rows[0]["Id_Inventario"].ToString());
                    dPrecio = decimal.Parse(DtRegistros.Rows[0]["Precio_Venta"].ToString());

                }
                else
                {
                    dPrecio = 0;
                }

                AgregarLista();
                dTotalArticulos = 0;
                dTotalVenta = 0;
                foreach (DataGridViewRow Sumar in dataGridView1.Rows)
                {
                    dTotalVenta += Convert.ToDecimal(Sumar.Cells["Total"].Value);
                    dTotalArticulos += Convert.ToInt32(Sumar.Cells["Cantidad"].Value);
                }


                TxtTotalArticulos.Text = Convert.ToString(dTotalArticulos);
                TxtTotalPagar.Text = Convert.ToString(dTotalVenta);
                TxtTotalVenta.Text = Convert.ToString(dTotalVenta);
                dSubTotal = dTotalVenta / Convert.ToDecimal(1.16);
                dIva = dTotalVenta - dSubTotal;
                TxtSubtotal.Text = Convert.ToString(string.Format("{0:#,#0.00}", dSubTotal));
                TxtiVA.Text = Convert.ToString(string.Format("{0:#,#0.00}", dIva));

                #region Personalizar DataGirdView
                dataGridView1.Columns["Id_Articulo"].Visible = false;
                dataGridView1.Columns["Id_Inventario"].Visible = false;
                dataGridView1.Columns["Descripcion"].Width = 400;
                dataGridView1.Columns["Precio"].DefaultCellStyle.Format = "C2";
                dataGridView1.Columns["Total"].DefaultCellStyle.Format = "C2";
                dataGridView1.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns["Precio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns["Cantidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                #endregion

                TxtBuscarCodigo.Text = "";
                TxtBuscarCodigo.ForeColor = Color.Black;
                TxtBuscarCodigo.Focus();
            }
           


        }

        #endregion

        #region Agregar a Lista y Cargar al DataGridview
    
    
        public void AgregarLista()
        {
            if (ValidarArticulo())
            {

            }
            else
            {
                dCantidad = 1;
                Total = Precio * dCantidad;
               // listaVenta = new ListaVenta(sCodigo, iId_Inventario, iId_Articulo, sDescripcion, dCantidad, Precio, Total,);
                listaVentas.Add(listaVenta);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = listaVentas;
            }
        }
        #endregion

        #region Validacion de Articulo si esta en el DataGridView

        private bool ValidarArticulo()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string EvaluarDescripcion = dataGridView1.Rows[i].Cells["Descripcion"].Value.ToString();
                    if (sDescripcion == EvaluarDescripcion)
                    {
                        dCantidad = int.Parse(dataGridView1.Rows[i].Cells["Cantidad"].Value.ToString());
                        dTotalCantidad = dCantidad + 1;
                        Total = Precio * dTotalCantidad;
                        dataGridView1.Rows[i].Cells["Cantidad"].Value = dTotalCantidad;
                        dataGridView1.Rows[i].Cells["Total"].Value = Total;
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion

        #endregion


        #region Eventos


        private void TxtBuscarCodigo_TextChanged(object sender, EventArgs e)
        {
            if (TxtBuscarCodigo.Text != "" && TxtBuscarCodigo.Text != "Ingrese Codigo...")
            {

                BuscarArticulo(TxtBuscarCodigo.Text);

            }
        }

        #endregion
    }
}
