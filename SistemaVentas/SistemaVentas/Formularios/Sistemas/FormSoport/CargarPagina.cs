using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using SistemaVentas.Formularios.Administracion;
using System.Drawing;
using System.IO;

namespace SistemaVentas.Formularios.Sistemas.FormSoport
{
    public class CargarPagina
    {
        private static string NombrePaginaTab;
        private static int Resultado;
        public static RadPageView TabControlTemporal = new RadPageView();
      
        public static byte[] Imagen { get; set; }
        public static Image image;



        public static void CargarTabControl( RadPageView TabControlProducion)
        {
            TabControlTemporal = TabControlProducion;
        }

        public static void CargarTabPage(RadPageView TabControl, string NombreFormulario)
        {
            Resultado = ValidaTabpage(TabControl, NombreFormulario);
            if (Resultado==0)
            {
                Form myForm;
                Type tipo = Type.GetType(NombreFormulario);
                myForm = (Form)Activator.CreateInstance(tipo);

                string nombreTabPagina = Convert.ToString(myForm.Text);
                TabControl.Visible = true;
                TabControl.ViewElement.ShowItemCloseButton = true;

                RadPageViewPage radPage = new RadPageViewPage();
                radPage.Name = NombreFormulario;
                radPage.ControlAdded += new ControlEventHandler(LimpiarControles);
                radPage.Text = string.Format(nombreTabPagina);
                radPage.Tag = NombreFormulario;
                radPage.Image = image;

                myForm.TopLevel = false;
                myForm.FormBorderStyle = FormBorderStyle.None;
                myForm.Dock = DockStyle.Fill;
                myForm.WindowState = FormWindowState.Normal;
                myForm.AutoScroll = true;
                radPage.Controls.Add(myForm);
                radPage.AutoScroll = true;

                TabControl.Pages.Add(radPage);
                TabControl.SelectedPage = radPage;
                myForm.Show();
                CargarTabControl(TabControl);


            }
        }

        public static void LimpiarControles(object sender, ControlEventArgs e)
        {
            RadPageViewPage pagina = (RadPageViewPage)sender;
            Control control = e.Control;
            foreach (Control item in pagina.Controls)
            {
                if (item!=control)
                {
                    item.Dispose();
                }
            }
        }




        /// <summary>
        /// Este metodo abre los formularios dentro del RadPageView(TabPage)
        /// </summary>
        public static void AbreFormulario(RadPageView TabLiquidacion, Form MyForm )
        {
            int iExisteForm = ValidaTabpage(TabLiquidacion, MyForm.Name);

            if (iExisteForm == 0)
            {
                RadPageViewPage Pagina = new RadPageViewPage();

                Pagina.Name = MyForm.Name;
                Pagina.Text = MyForm.Text;
                Pagina.Tag = MyForm.Name;
                Pagina.Image = image;
                MyForm.TopLevel = false;
                MyForm.FormBorderStyle = FormBorderStyle.None;
                MyForm.Dock = DockStyle.Fill;
                MyForm.WindowState = FormWindowState.Normal;
                MyForm.AutoScroll = true;

                Pagina.Controls.Add(MyForm);
                Pagina.AutoScroll = true;


                TabLiquidacion.Pages.Add(Pagina);
                TabLiquidacion.SelectedPage = Pagina;
                //AnimateWindow(MyForm.Handle, 500, AnimateWindowFlags.AW_CENTER);
                MyForm.Show();
                //FormProvedores.GuardaTabPage(TabLiquidacion);
                //GuardaTabPage(TabLiquidacion,tbTemporal2);

            }
        }

     

        /// <summary>
        /// Metodo que valida si ya existe el formulario dentro del RadPageView(TabPage)
        /// </summary>
        private static int ValidaTabpage(RadPageView Tabs, string NombreFormulario)
        {
            for (int i = 0; i < Tabs.Pages.Count; i++)
            {
                if (i >= 0)
                {
                    NombrePaginaTab = Tabs.Pages[i].Name;

                    if (NombrePaginaTab == NombreFormulario)
                    {
                        Tabs.SelectedPage = Tabs.Pages[i];

                        return Resultado = 1;                        
                        
                    }
                }
            }
           
            return Resultado = 0;
            
            
        }


        public static void CerrarTabPages( RadPageView radPageView)
        {
            //RadPageView rdPageView = FormProvedores.RegresaTabPageLiquidacion();
           
            //for (int i = 0; i <= radPageView.Pages.Count - 1; ++i)
            //{
            //    radPageView.Pages.RemoveAt(i);
            //    --i;
            //}

            if (radPageView.Pages.Count>0)
            {
                radPageView.Pages.Clear();
            }

        }

        public static void CerrarTabPagesAct( RadPageView radPageView, int index)
        {
            //RadPageView rdPageView = FormProvedores.RegresaTabPageLiquidacion();

            radPageView.Pages.RemoveAt(index);
        }

      






    }
}
