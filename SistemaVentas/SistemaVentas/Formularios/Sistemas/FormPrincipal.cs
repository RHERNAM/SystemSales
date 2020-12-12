using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;// Importacion de servicio para poder mover el formulario
using SistemaVentas.Clases.SQL;
using SistemaVentas.Formularios.Administracion;
using SistemaVentas.Formularios.Ventas;
using SistemaVentas.Clases.Entidates;
using System.IO;
using System.Threading;
using AccesDLL;
using SistemaVentas.Formularios.Sistemas;
using SistemaVentas.Clases.SQL.Transacciones;
using SistemaVentas.Clases.Validaciones;
using SistemaVentas.Formularios.Configuraciones;
using SistemaVentas.Formularios.Sistemas.FormSoport;

namespace SistemaVentas
{
    public partial class FormPrincipal : Form
    {
        private int R = 255;
        private int G = 0;
        private int B = 0;
        private int Tiempo;
        //#region Reduce Parpadeo 

        ///*Reduce el Parpadeo de controles al abrir formularios */
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;
        //        return cp;
        //    }
        //}

        //#endregion

        int LX, LY;
       
        private DataTable DtRegistros;


        #region Animacion de Windows       
        [DllImport("User32")]
        static extern bool AnimateWindow(IntPtr hWnd, int time, AnimateWindowFlags animateWindowsFlags);
        enum AnimateWindowFlags : int
        {
            AW_HOR_POSITIVE = 0x00000001,
            AW_HOR_NEGATIVE = 0x00000002,
            AW_VER_POSITIVE = 0x00000004,
            AW_VER_NEGATIVE = 0x00000008,
            AW_CENTER = 0x00000010,
            AW_HIDE = 0x00010000,
            AW_ACTIVATE = 0x00020000,
            AW_SLIDE = 0x00040000,
            AW_BLEND = 0x00080000

        }
        #endregion

        #region Mover Formulario

        //Codigo para Mover formulario
        [DllImport("User32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("User32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wsmg, int wparam, int lparam);

        #endregion

        #region Redimencionar Tamaño de Formulario en Tiempo de ejecucion

        //OPCION 2 CON PANELES
        //METODO PARA REDIMENCIONAR/CAMBIAR TAMAÑO A FORMULARIO  TIEMPO DE EJECUCION ----------------------------------------------------------
        private int tolerance = 15;
        private const int WM_NCHITTEST = 132;
        private const int HTBOTTOMRIGHT = 17;
        private Rectangle sizeGripRectangle;

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    var hitPoint = this.PointToClient(new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16));
                    if (sizeGripRectangle.Contains(hitPoint))
                        m.Result = new IntPtr(HTBOTTOMRIGHT);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        //----------------DIBUJAR RECTANGULO / EXCLUIR ESQUINA PANEL 
        protected override void OnSizeChanged(EventArgs e)
        {
            BtnRestaurar.Visible = false;
            BtnMaximizar.Visible = true;
            base.OnSizeChanged(e);
            var region = new Region(new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));

            sizeGripRectangle = new Rectangle(this.ClientRectangle.Width - tolerance, this.ClientRectangle.Height - tolerance, tolerance, tolerance);

            region.Exclude(sizeGripRectangle);
            this.PanelPrincipal.Region = region;
            this.Invalidate();
        }
        //----------------COLOR Y GRIP DE RECTANGULO INFERIOR
        protected override void OnPaint(PaintEventArgs e)
        {

            SolidBrush blueBrush = new SolidBrush(Color.FromArgb(255, 128, 0));
            //olidBrush blueBrush = new SolidBrush(Color.Orange);
            //SolidBrush blueBrush = new SolidBrush(Color.LightCoral);
            e.Graphics.FillRectangle(blueBrush, sizeGripRectangle);

            base.OnPaint(e);
            ControlPaint.DrawSizeGrip(e.Graphics, Color.Transparent, sizeGripRectangle);
        }

        #endregion

        #region Mover Formulario por Panel Titulo       

        private void PanelTitulo_MouseMove(object sender, MouseEventArgs e)
        {
            Soporte.CapturaLanzamiento();
            Soporte.EnviarCordenadas(Handle, 0x112, 0x112, 0);
            //ReleaseCapture();
            //SendMessage(Handle, 0x112, 0xf012, 0);
        }
        #endregion
        public FormPrincipal()
        {  
            InitializeComponent();
            //Thread thread = new Thread(new ThreadStart(forminiciar));
            //thread.Start();
            //Thread.Sleep(13000);
            //thread.Abort();
        }

        private void forminiciar()
        {
            Application.Run(new FormBienvenida());

        }


        #region Consultar Aperura Caja

        private int AperturasCaja()
        {
            DtRegistros = Consulta.CajaAperturas(UsuarioCache.Id_Usuario);
            int aperturas = DtRegistros.Rows.Count;

            if (DtRegistros.Rows.Count>0)
            {
                MemoriaCache.IdApertura = Convert.ToInt32(DtRegistros.Rows[0]["Id_AperturaCaja"]);
                MemoriaCache.IdCaja = Convert.ToInt32(DtRegistros.Rows[0]["Id_Caja"]);
                MemoriaCache.IdEstadoCaja = Convert.ToInt32(DtRegistros.Rows[0]["Id_Estado"]);
            }
           
            return aperturas;
        }

      

        #endregion

        #region Evento Load Formulario

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            MaximazarPrincipal();
            CargarDatosUsuario();
            CargarPermiso();
            //TmrFechaHora.Enabled = true;
           
            
        }

        #endregion

        #region CARGAR PERMISOS


       private void CargarPermiso()
       {
          
            BtnBuscar.Visible = Repositorios.ValidarPermiso("1");
            BtnProductos.Visible= Repositorios.ValidarPermiso("2");
            BtnVenta.Visible= Repositorios.ValidarPermiso("3");
            BtnEmpleados.Visible = Repositorios.ValidarPermiso("4");
            BtnProveedores.Visible= Repositorios.ValidarPermiso("5");
            BtnClientes.Visible = Repositorios.ValidarPermiso("6");
            BtnEstadisticas.Visible = Repositorios.ValidarPermiso("7");

            //Submenus
            BtnArticulos.Visible = Repositorios.ValidarPermiso("8");
            BtnRegEmpleados.Visible= Repositorios.ValidarPermiso("9");
            BtnEntradas.Visible = Repositorios.ValidarPermiso("10");
            BtnDeshboard.Visible = Repositorios.ValidarPermiso("11");
            BtnPuntoVenta.Visible = Repositorios.ValidarPermiso("12");
            BtnKardexInventario.Visible = Repositorios.ValidarPermiso("13");
            BtnRegProveedor.Visible = Repositorios.ValidarPermiso("14");
            
       }
        #endregion

        #region Carga Datos de Usuario      
        private void CargarDatosUsuario()
        {
            LblUsuario1.Text = UsuarioCache.Nombre +' ' +UsuarioCache.Apellido_Paterno;
            LblUsuario2.Text = UsuarioCache.Nombre +' ' +UsuarioCache.Apellido_Paterno ;
            LblPuesto.Text = UsuarioCache.PuestoUsuario;

            /*Estos Datos son del Usuario es donde se ecnuentra registrado tanto como empresa y sucursal*/
             LblEmpresaSucursal.Text ="Empresa: "+' '+ UsuarioCache.Empresa + ' '+ "Sucursal: " + UsuarioCache.Sucursal;
            /*Estos datos son del equipo de computadora que se registra como caja*/
           // LblEmpresaSucursal.Text ="Empresa: "+' '+ MemoriaCache.Empresa + ' '+ "Sucursal: " + MemoriaCache.Sucursal;
            byte[] imagenbufer = UsuarioCache.Imagen;
            MemoryStream memoria = new MemoryStream(imagenbufer);

            PtboxUsuario.Image = Image.FromStream(memoria);
            PtboxUsuario.SizeMode = PictureBoxSizeMode.Zoom;
            //PtboxUsuario2.Image = Image.FromStream(memoria);
            //PtboxUsuario2.SizeMode = PictureBoxSizeMode.Zoom;
            OvFotoPerfil.BackgroundImage = Image.FromStream(memoria);
            OvFotoPerfil.BackgroundImageLayout = ImageLayout.Stretch;

        }


        #endregion

        #region Botones Basicos de Formulario

        private void BtnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
              

        public  void MaximazarPrincipal()
        {
            LX = Location.X;
            LY = Location.Y;
            Size = Screen.PrimaryScreen.WorkingArea.Size;
            Location = Screen.PrimaryScreen.WorkingArea.Location;
            BtnMaximizar.Visible = false;
            BtnRestaurar.Visible = true;
        }

        private void BtnMaximizar_Click(object sender, EventArgs e)
        {
            MaximazarPrincipal();
        }

        private void BtnRestaurar_Click(object sender, EventArgs e)
        {
            ////WindowState = FormWindowState.Normal;

            Location = new Point(LX, LY);
            Size = new Size(700, 500); //este restaura en la esquina de la perte superior
            BtnMaximizar.Visible = true;
            BtnRestaurar.Visible = false;

        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {           

            if (MessageBox.Show("Ha selecionado Cerrar la aplicacion, desea continuar?", "Cerrando Aplicación Sistema de Ventas", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //tus cdigos omo guardar antes de cerrar
                Application.Exit();
                ClsGuardar.SalirSistema(BaseProp.IdLog,UsuarioCache.Nomina);
            }
            else
            {
                //tus codigos
            }
        }

        #endregion

        #region Ocultar o MOstrar SubmenuPaneles
       
        private void OcultarSubmenuPaneles()
        {
            if (PnlBuscar.Visible == true)
                PnlBuscar.Visible = false;
            if (PnlCapital.Visible == true)
                PnlCapital.Visible = false;
            if (PnlEmpleados.Visible == true)
                PnlEmpleados.Visible = false;
            if (PnlEstadisticas.Visible == true)
                PnlEstadisticas.Visible = false;
            if (PnlProductos.Visible == true)
                PnlProductos.Visible = false;
            if (PnlProveedores.Visible == true)
                PnlProveedores.Visible = false;
            if (PnlVenta.Visible == true)
                PnlVenta.Visible = false;            

        }

        #endregion

        #region Operacion Mostrar SubmenuPaneles
        
        private void MostrarPanelesSubmenu(Panel panel)
        {
            if (panel.Visible==false)
            {
                OcultarSubmenuPaneles();
                panel.Visible = true;
            }
            else
            {
                panel.Visible = false;
            }
        }

        #endregion

        #region Butones Principales Menu Izquierda       

        #region Boton Productos

        private void BtnProductos_Click(object sender, EventArgs e)
        {
            MostrarPanelesSubmenu(PnlProductos);
        }

        #endregion

        #region Boton Venta
       
        private void BtnVenta_Click(object sender, EventArgs e)
        {
            MostrarPanelesSubmenu(PnlVenta);
        }

        #endregion

        #region Boton Empleados
        
        private void BtnEmpleados_Click(object sender, EventArgs e)
        {
            MostrarPanelesSubmenu(PnlEmpleados);
        }

        #endregion

        #region Buton Proveedores
       
        private void BtnProveedores_Click(object sender, EventArgs e)
        {
            MostrarPanelesSubmenu(PnlProveedores);
        }

        #endregion

        #region Buton Capital
      
        private void BtnCapital_Click(object sender, EventArgs e)
        {
            MostrarPanelesSubmenu(PnlCapital);
        }

        #endregion

        #region Buton Estadisticas
        
        private void BtnEstadisticas_Click(object sender, EventArgs e)
        {
            MostrarPanelesSubmenu(PnlEstadisticas);
        }

        #endregion

        #region Buton Buscar
        
        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            //radGridView1.DataSource = Consulta.DataTable();
            //radGridView1.BestFitColumns();

            MostrarPanelesSubmenu(PnlBuscar);

        }
        #endregion

        #endregion

        #region Metodo Abrir Formularios Hijos

        Form formulario;

        private void AbrirFormulario<MiFormulario>() where MiFormulario : Form, new()
        {
            Cursor = Cursors.WaitCursor;
            OcultarPenelMenuIzquierad();
            OcultarSubmenuPaneles();
            formulario = PanelContenedor.Controls.OfType<MiFormulario>().FirstOrDefault();
            if (formulario == null)
            {
                formulario = new MiFormulario
                {
                    TopLevel = false,
                    Dock = DockStyle.Fill,
                };
                PanelContenedor.Controls.Add(formulario);
                PanelContenedor.Tag = formulario;
                formulario.BringToFront();
                AnimateWindow(formulario.Handle, 500, AnimateWindowFlags.AW_SLIDE);                
                formulario.Show();
               

            }
            else
            {
                formulario.BringToFront();
            }
            Cursor = Cursors.Default;
        }


        private void AbrirFormulario2<MiFormulario>() where MiFormulario : Form, new()
        {
            Cursor = Cursors.WaitCursor;
            OcultarPenelMenuIzquierad();
            OcultarSubmenuPaneles();
            formulario = PanelContenedor.Controls.OfType<MiFormulario>().FirstOrDefault();
            if (formulario == null)
            {
                formulario = new MiFormulario
                {
                    TopLevel = false,
                    Dock = DockStyle.Fill,
                };
                PanelContenedor.Controls.Add(formulario);
                PanelContenedor.Tag = formulario;
                formulario.BringToFront();
                AnimateWindow(formulario.Handle, 500, AnimateWindowFlags.AW_SLIDE);
                formulario.Show();
                formulario.WindowState = FormWindowState.Maximized;
                formulario.FormClosed += FormularioCerrar_FormClosed;


            }
            else
            {
                formulario.BringToFront();
            }
            Cursor = Cursors.Default;
        }




        #endregion

        #region Ocultar Menu Principal

        private void OcultarMenuPrincipal()
        {
            PanelMenu.Visible = false;          
            LblUsuario2.Visible = true;
            PtboxUsuario.Visible = true;
            PtbxMenu.Image = Properties.Resources.MenuBlanco;

        }
        #endregion

        #region Mostrar y Ocultar Menu Principal por Separado
        //*********** MUESTRA PANEL MENU IZQUIERDA *******************************************

        private void MostrarPenelMenuIzquierad()
        {
            PanelMenu.Visible = true;
            LblUsuario2.Visible = false;
            PtboxUsuario.Visible = false;
            PtbxMenu.Image = Properties.Resources.menuCorto;
        }
        //************  OCULTA PANEL MENU IZQUIERDA   *****************************************
        private void OcultarPenelMenuIzquierad()
        {
            PanelMenu.Visible = false;
            LblUsuario2.Visible = true;
            PtboxUsuario.Visible = true;
            PtbxMenu.Image = Properties.Resources.MenuBlanco;
        }

        //*************************************  FIN  *************************************************************************
        #endregion

        #region Ocultar Menu Pricipal

        private void ValidacionPanelizquierda()
        {
            if (PanelMenu.Visible == true)
                PanelMenu.Visible = false;
        }

        private void OperacionPanelMenu(Panel OpreracionPanelMenu)
        {
            if (OpreracionPanelMenu.Visible == false)
            {
                ValidacionPanelizquierda();
                OpreracionPanelMenu.Visible = true;
                LblUsuario2.Visible = false;
                PtboxUsuario.Visible = false;
                PtbxMenu.Image = Properties.Resources.menuCorto;
            }
            else
            {
                OpreracionPanelMenu.Visible = false;
                LblUsuario2.Visible = true;
                PtboxUsuario.Visible = true;
                PtbxMenu.Image = Properties.Resources.MenuBlanco;
            }
        }
        #endregion

        private void AcionesTabControl()
        {
            radPageView1.Visible = true;
            radPageView1.BringToFront();
            PtbxCerrarTabControles.Visible = true;

        }

        private void AccionAbrirFormPanel()
        {
            PtbxCerrarTabControles.Visible = true;
        }

     

        #region Evento Mouse Move
       
        private void PanelContenedor_MouseMove(object sender, MouseEventArgs e)
        {
            OcultarPenelMenuIzquierad();
        }       

        private void menuStrip1_MouseMove(object sender, MouseEventArgs e)
        {
            Soporte.CapturaLanzamiento();
            Soporte.EnviarCordenadas(Handle, 0x112, 0xf012, 0);
         

        }

        #endregion

        #region Buton Cerrar Sesion
        
        private void BtnCerarSesion_Click(object sender, EventArgs e)
        {
            Close();
         
        }

        #endregion

        #region Botones para Abrir Formulario

        #region Buton Articulo

        private void BtnArticulos_Click(object sender, EventArgs e)
        {
            OcultarMenuPrincipal();
            OcultarSubmenuPaneles();
            //AbrirFormulario<FormArticulos>();    
            AcionesTabControl();
            Soporte.AbreFormularioRadPageView(radPageView1, new FormArticulos());
          
        }

        #endregion

        #region Buton Empleados       
        private void BtnRegEmpleados_Click(object sender, EventArgs e)
        {
            OcultarMenuPrincipal();
            OcultarSubmenuPaneles();
            //AbrirFormulario<FormEmpleados>();
            AcionesTabControl();
            Soporte.AbreFormularioRadPageView(radPageView1, new FormEmpleados());


        }
        #endregion
        private void BtnEntradas_Click(object sender, EventArgs e)
        {
            OcultarMenuPrincipal();
            OcultarSubmenuPaneles();
            //AbrirFormulario<FormEntradasSalidas>();
            AcionesTabControl();
            Soporte.AbreFormularioRadPageView(radPageView1, new FormEntradasSalidas());
           
        }

        //private void BtnRegistros_Click(object sender, EventArgs e)
        //{
        //    OcultarMenuPrincipal();
        //    OcultarSubmenuPaneles();
        //    //AbrirFormulario<FormInventario>();
        //    AcionesTabControl();
        //    Soporte.AbreFormularioRadPageView(radPageView1, new FormInventario());
            
        //}

        private void TmrFechaHora_Tick(object sender, EventArgs e)
        {
            LblFecha.Text = DateTime.Now.ToLongDateString();
            LblHora.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void BtnPuntoVenta_Click(object sender, EventArgs e)
        {
            
            if (AperturasCaja()>0)
            {
                OcultarMenuPrincipal();
                OcultarSubmenuPaneles();
                
                AbrirFormulario2<FormPuntoVenta>();
                AccionAbrirFormPanel();

            }
            else
            {
                //FormAperturaVenta formApertura = new FormAperturaVenta();
                //formApertura.Show();
                //formApertura.FormClosed += FormularioCerrar_FormClosed;
                AbrirFormulario2<FormAperturaVenta>();
                AccionAbrirFormPanel();
            }
           
        }
        private void BtnKardexInventario_Click(object sender, EventArgs e)
        {
            //AbrirFormulario<FormInventario>();
            OcultarMenuPrincipal();
            OcultarSubmenuPaneles();
            AcionesTabControl();
            Soporte.AbreFormularioRadPageView(radPageView1, new FormInventario());
          

        }
        private void FormularioCerrar_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 
            if (MemoriaCache.OpcionFormulario==1)
            {
                AbrirFormulario<FormPuntoVenta>();
                AccionAbrirFormPanel();
                
            }
            else if(MemoriaCache.OpcionFormulario==2)
            {
                AbrirFormulario<FormCierreCaja>();
                AccionAbrirFormPanel();
            }
            int totalControles = PanelContenedor.Controls.Count - 1;

            if (totalControles > 5)
            {
                if (radPageView1.Pages.Count == 0)
                {
                    radPageView1.Visible = false;
                }
                else
                {
                    radPageView1.Visible = true;
                }

                PtbxCerrarTabControles.Visible = true;
            }
            else
            {
               
                if (radPageView1.Pages.Count == 0)
                {
                   
                        radPageView1.Visible = false;
                        PtbxCerrarTabControles.Visible = false;
                   


                }
                else
                {
                    radPageView1.Visible = true;
                    PtbxCerrarTabControles.Visible = true;
                }
            }

        }

        private void importarExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarMenuPrincipal();
            OcultarSubmenuPaneles();
            //AbrirFormulario<FormImportacion>();
            AcionesTabControl();
            Soporte.AbreFormularioRadPageView(radPageView1, new FormImportacion());
            
        }
 

        private void BtnRegProveedor_Click(object sender, EventArgs e)
        {
            OcultarMenuPrincipal();
            OcultarSubmenuPaneles();
            //AbrirFormulario<FormProvedores>();
            AcionesTabControl();
            Soporte.AbreFormularioRadPageView(radPageView1, new FormProvedores());
            
        }

        private void empresaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarMenuPrincipal();
            OcultarSubmenuPaneles();
            //AbrirFormulario<FormEmpresa>();
            AcionesTabControl();
            Soporte.AbreFormularioRadPageView(radPageView1, new FormEmpresa());
            
        }

        private void paisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarMenuPrincipal();
            OcultarSubmenuPaneles();
            //AbrirFormulario<FormPaises>();
            AcionesTabControl();
            Soporte.AbreFormularioRadPageView(radPageView1, new FormPaises());
           
        }

        private void estadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarMenuPrincipal();
            OcultarSubmenuPaneles();
            //AbrirFormulario<FormEstados>();
            AcionesTabControl();
            Soporte.AbreFormularioRadPageView(radPageView1, new FormEstados());
           
        }

        private void municipiosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarMenuPrincipal();
            OcultarSubmenuPaneles();
            //AbrirFormulario<FormMunicipios>();
            AcionesTabControl();
            Soporte.AbreFormularioRadPageView(radPageView1, new FormMunicipios());
           
        }

        private void coloniasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarMenuPrincipal();
            OcultarSubmenuPaneles();
            //AbrirFormulario<FormColonias>();
            AcionesTabControl();
            Soporte.AbreFormularioRadPageView(radPageView1, new FormColonias());
           
        }

        private void sucursalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarMenuPrincipal();
            OcultarSubmenuPaneles();
            //AbrirFormulario<FormSucursales>();
            AcionesTabControl();
            Soporte.AbreFormularioRadPageView(radPageView1, new FormSucursales());
            
        }







        #endregion

        #region PictorBox Menu 

        private void PtbxMenu_Click(object sender, EventArgs e)
        {
            OperacionPanelMenu(PanelMenu);
            OcultarSubmenuPaneles();
        }

        #endregion

        #region Animacion Fuente Colores     
        private void TmrEfectoColores_Tick(object sender, EventArgs e)
        {
            {
                if (R == 255)
                {
                    B = B + 1;
                }
                if (B == 252)
                {
                    R = R - 1;
                    if (R == 5)
                    {
                        R = 6;
                        B = 252;
                    }
                }

                if (R == 6 & B == 252)
                {
                    G = G + 1; ;

                }
                if (G == 252)
                {
                    B = B - 1;
                    if (B == 5)
                    {
                        B = 6;
                    }
                }

                if (B == 6 & G == 252)
                {
                    R = R + 1;
                }

                if (R == 252 & B == 6)
                {
                    G = G - 1;
                    if (G == 5)
                    {
                        G = 6;
                    }
                }
                if (G == 6 & B == 6)
                {
                    R = 255;
                    G = 0;
                    B = 0;
                }


                //panel4.BackColor = Color.FromArgb(R, G, B);
                //panel3.BackColor = Color.FromArgb(R, G, B);
                //panel2.BackColor = Color.FromArgb(R, G, B);
                //panel1.BackColor = Color.FromArgb(R, G, B);
                label3.ForeColor = Color.FromArgb(R, G, B);
                return;

            }
        }

        private void radPageView1_SelectedPageChanged(object sender, EventArgs e)
        {

        }



        private void radPageView1_PageRemoved(object sender, Telerik.WinControls.UI.RadPageViewEventArgs e)
        {
            if (radPageView1.Pages.Count == 0)
            {
                if (PanelContenedor.Controls.Count>5)
                {
                   
                    radPageView1.Visible = false;
                }
                else
                {
                    radPageView1.Visible = false;
                    PtbxCerrarTabControles.Visible = false;
                }

                
            }
            else
            {
                radPageView1.Visible = true;
                PtbxCerrarTabControles.Visible = true ;
            }
        }

        private void PtbxCerrarTabControles_Click(object sender, EventArgs e)
        {
            Soporte.CerrarTabPagesTodos(radPageView1);
            PtbxCerrarTabControles.Visible = false;
            PanelContenedor.Controls.Clear();
            PanelContenedor.Controls.Add(LblFecha);
            PanelContenedor.Controls.Add(LblHora);
            PanelContenedor.Controls.Add(label3);
            PanelContenedor.Controls.Add(PictureBoxLogoCentro);
            PanelContenedor.Controls.Add(radPageView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OcultarMenuPrincipal();
            OcultarSubmenuPaneles();

            AbrirFormulario2<FormAperturaVenta>();
            AccionAbrirFormPanel();

        }

        private void PctbxNotificacion_Click(object sender, EventArgs e)
        {
            
        }

        private void PanelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LblPuesto_Click(object sender, EventArgs e)
        {

        }

        private void BtnDeshboard_Click(object sender, EventArgs e)
        {
            //OcultarMenuPrincipal();
            //OcultarSubmenuPaneles();         
            //AbrirFormulario<FormEstadisticas>();
            //AccionAbrirFormPanel();


            OcultarMenuPrincipal();
            OcultarSubmenuPaneles();
            AbrirFormulario<FormConfiguracionSistema>();
            AccionAbrirFormPanel();
        }

        private void PnlLineaArriba_MouseMove(object sender, MouseEventArgs e)
        {
            Soporte.CapturaLanzamiento();
            Soporte.EnviarCordenadas(Handle, 0x112, 0x112, 0);
        }

        #endregion

        #region Efecto Logo Animacion

        private void TmrLogoEfecto_Tick(object sender, EventArgs e)
        {
            #region Obsoleto           

            //#region Comentado


            //Tiempo++;

            //if (Tiempo == 5)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._1;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 10)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._2;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 15)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._3;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 20)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._4;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 25)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._5;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 30)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._6;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 35)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._7;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 40)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._8;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 45)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._9;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 50)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._10;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 55)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._11;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 60)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._12;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 65)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._13;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 70)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._14;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 75)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._15;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 80)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._16;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 85)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._17;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 90)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._18;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 95)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._19;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 100)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._20;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 105)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._21;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 110)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._22;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 115)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._23;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 120)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._24;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 125)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._25;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 130)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._26;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 135)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._27;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 140)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._28;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 145)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._29;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 150)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._30;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 155)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._31;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 160)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._32;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 165)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._33;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 170)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._34;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 175)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._35;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 180)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._36;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 185)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._37;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 190)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._38;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 195)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._39;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 200)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._40;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 210)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._41;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 215)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._42;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 220)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._43;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 225)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._44;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 230)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._45;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 235)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._46;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 240)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._47;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 245)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._48;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 250)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._49;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 255)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._50;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 260)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._51;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 265)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._52;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 270)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._53;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 275)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._54;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 280)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._55;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;

            //}
            //if (Tiempo == 285)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._56;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 290)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._57;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 295)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._58;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //}
            //if (Tiempo == 300)
            //{
            //    PictureBoxLogoCentro.Image = Properties.Resources._59;
            //    PictureBoxLogoCentro.SizeMode = PictureBoxSizeMode.Zoom;
            //    Tiempo = 0;
            //    TmrLogoEfecto.Start();
            //}

            //#endregion

            #endregion
        }


        #endregion


    }
}
