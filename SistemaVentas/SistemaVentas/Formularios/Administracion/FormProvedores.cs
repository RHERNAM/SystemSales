using SistemaVentas.Formularios.Sistemas.FormSoport;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace SistemaVentas.Formularios.Administracion
{
    public partial class FormProvedores : Form
    {
        
        public static RadPageView TabControlFormularios;
        public FormProvedores()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            radPageView1.Visible = true;
            //GuardaTabPage(radPageView1);
            CargarPagina.image = Properties.Resources.carrito_de_la_compra__1_;
            CargarPagina.AbreFormulario(radPageView1, new FormArticulos());
          

        }


        private void button2_Click(object sender, EventArgs e)
        {
            radPageView1.Visible = true;
            //GuardaTabPage(radPageView1);
            CargarPagina.image = Properties.Resources.busqueda__1_;
            CargarPagina.AbreFormulario(radPageView1, new FormEmpleados());
          
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            radPageView1.Visible = true;
            //GuardaTabPage(radPageView1);
            CargarPagina.image = Properties.Resources.cruz_entrecruzada;
            CargarPagina.AbreFormulario(radPageView1, new FormSalidas());
                       
        }

        public static void GuardaTabPage(RadPageView tbTemporal)
        {
           //TabControlFormularios = tbTemporal;

        }
        //public static RadPageView RegresaTabPageLiquidacion()
        //{
        //    //return TabControlFormularios;
        //}

        private void button4_Click(object sender, EventArgs e)
        {
              //CargarPagina.AbreFormulario(radPageView1, new FormEmpleados(), TabPageLiquidacion);
        }

        private void PtbxCerrar_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //CargarPagina.CerrarTabPagesAct(radPageView1);
            CargarPagina.CerrarTabPages(radPageView1);
            if (radPageView1.Pages.Count==0)
            {
                radPageView1.Visible = false;
            }
            else
            {
                radPageView1.Visible = true;
            }

        }



        private void FormProvedores_Load(object sender, EventArgs e)
        {

        }

      

        private void radPageView1_PageRemoved(object sender, RadPageViewEventArgs e)
        {
            if (radPageView1.Pages.Count == 0)
            {
                radPageView1.Visible = false;
            }
            else
            {
                radPageView1.Visible = true;
            }
        }

     
    }
}
