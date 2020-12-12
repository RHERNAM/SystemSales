using SistemaVentas.Clases.SQL;
using System;
using System.Collections;
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
    public partial class FormEstadisticas : Form
    {

        private DataTable DtRegistros;
        public FormEstadisticas()
        {
            InitializeComponent();
        }

        private void FormEstadisticas_Load(object sender, EventArgs e)
        {
            mostra();
            ProdPreferidos();
            DeshBoard();
        }


        ArrayList Categorias = new ArrayList();
        ArrayList CantidadProducto = new ArrayList();

        ArrayList ProductosPref = new ArrayList();
        ArrayList CantidProdcPref = new ArrayList();

        private void mostra()
        {
            DtRegistros = Consulta.GrafCategoria();          

            for (int i = 0; i < DtRegistros.Rows.Count; i++)
            {
                Categorias.Add(DtRegistros.Rows[i]["Descripcion"]);
                CantidadProducto.Add(DtRegistros.Rows[i]["Cantidad"]);

                ChartProdPresentacion.Series[0].Points.DataBindXY(Categorias, CantidadProducto);
            }
            
        }

        private void ProdPreferidos()
        {
            DtRegistros = Consulta.GrafProductos();

            for (int i = 0; i < DtRegistros.Rows.Count; i++)
            {
                ProductosPref.Add(DtRegistros.Rows[i]["Productos"]);
                CantidProdcPref.Add(DtRegistros.Rows[i]["Cantidad_Salidas"]);

                chartProdPreferidos.Series[0].Points.DataBindXY(ProductosPref, CantidProdcPref);
            }
        }

        private void DeshBoard()
        {
            DtRegistros = Consulta.Deshboard();

            if (DtRegistros.Rows.Count>0)
            {
                LblVentas.Text ="$ " + DtRegistros.Rows[0]["TotalVentas"].ToString();
                LblCantClienetes.Text = "N° " + DtRegistros.Rows[0]["CantClientes"].ToString();
                LblCantEmpleados.Text = "N° " + DtRegistros.Rows[0]["CantEmpleados"].ToString();
                LblCantProductos.Text = "N° " + DtRegistros.Rows[0]["Total_Articulos"].ToString();
                LblCantProveedores.Text = "N° " + DtRegistros.Rows[0]["CantProveedores"].ToString();              
                LblCantPresentaciones.Text= "N° " + DtRegistros.Rows[0]["CantPresntaciones"].ToString();
            }
        }

        private void LblCantEmpleados_Click(object sender, EventArgs e)
        {

        }

        private void LblCantProveedores_Click(object sender, EventArgs e)
        {

        }

        private void PtbxCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            FormPrincipal formPrincipal = new FormPrincipal();
            formPrincipal.BtnRegEmpleados.PerformClick();
           
        }
    }
}
