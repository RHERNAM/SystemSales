using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SistemaVentas.Clases.SQL;
using SistemaVentas.Clases.Entidates;
using System.IO;
using AccesDLL;
using SistemaVentas.Clases.SQL.Transacciones;
using SistemaVentas.Clases.Validaciones;
using SCL;
using SistemaVentas.Formularios.Sistemas.FormSoport.Recuperacion;
using System.Diagnostics;

namespace SistemaVentas.Formularios.Sistemas
{
    public partial class FormLogin : Form
    {
        int LX, LY;
        DataTable DtRegitros;
        public string Nombre_Usuario;
        private int Nomina, Pin, IdLog;     
        private string NombreEmpleado;
        private string Contraseña, Email, Palabra_Seguridad, NombreCompleto, miMensaje;
        string Ip, Mac, NombrePC, SerialPC, Acceso;
        public FormLogin()
        {
            InitializeComponent();
        }


        #region Reduce Parpadeo 

        /*Reduce el Parpadeo de controles al abrir formularios */
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        #endregion

        private void FormLogin_Load(object sender, EventArgs e)
        {            
            MaximazarPrincipal();                   
            DatosPC();
            DibujarUsuario();
            ConsultaEquipoCaja();

        }


        private void button1_Click_1(object sender, EventArgs e)
        {
           
        }

        #region Consultar Equipo Caja
        private void ConsultaEquipoCaja()
        {
            DtRegitros = Consulta.CajaEquipo(NombrePC,Ip,SerialPC);
            if (DtRegitros.Rows.Count>0)
            {
                MemoriaCache.Empresa = DtRegitros.Rows[0]["Empresa"].ToString();
                MemoriaCache.Sucursal = DtRegitros.Rows[0]["Sucursal"].ToString();
                MemoriaCache.IdEmpresa =Convert.ToInt32( DtRegitros.Rows[0]["IdEmpresa"]);
                MemoriaCache.IdSucursal =Convert.ToInt32( DtRegitros.Rows[0]["IdSucursal"]);
            }
            else
            {
                MemoriaCache.Empresa = "";
                MemoriaCache.Sucursal = "";
                MemoriaCache.IdEmpresa = 0; ;
                MemoriaCache.IdSucursal =0;

            }
        }

        #endregion

        #region Obtener Datos de la PC

        private void DatosPC()
        {
            Ip = Soporte.ObtenerInformacionComputo(Soporte.DescripcionCompu.Ip);
            Mac = Soporte.ObtenerInformacionComputo(Soporte.DescripcionCompu.MacAdres);
            NombrePC = Soporte.ObtenerInformacionComputo(Soporte.DescripcionCompu.NombrePC);
            SerialPC = Soporte.Serial_Computadora();

            MemoriaCache.Ip = Soporte.ObtenerInformacionComputo(Soporte.DescripcionCompu.Ip);
            MemoriaCache.Mac = Soporte.ObtenerInformacionComputo(Soporte.DescripcionCompu.MacAdres);
            MemoriaCache.NombrePC = Soporte.ObtenerInformacionComputo(Soporte.DescripcionCompu.NombrePC);
            MemoriaCache.SerialPC = Soporte.Serial_Computadora();


        }

        #endregion

        #region Guardar Log de Acceso
        private void GuadarParaLog()
        {

            IdLog = BaseProp.IdLog = Consulta.NumLog();
            Nomina = UsuarioCache.Nomina;
        }

        private void InsertarLog()
        {
            Acceso = "Formulario Principal";
            ClsGuardar.LogAcceso(Nomina, NombrePC, SerialPC, Ip, Mac, Acceso);
        }

        #endregion

        #region Dibujar Usuario       
        private void DibujarUsuario()
        {
            //DtRegitros = Consulta.ConsultaUsuarios();
            DtRegitros = Consulta.LogSesionesRecientes(NombrePC);

            for (int i = 0; i < DtRegitros.Rows.Count; i++)
            {

                Label label = new Label();
                Panel panel = new Panel();
                PictureBox pictureBox = new PictureBox();

                label.Text = DtRegitros.Rows[i]["Usuario_Nombre"].ToString();
                label.Name = DtRegitros.Rows[i]["Id_Usuario"].ToString();
                label.Size = new Size(204, 57);
                label.Font = new Font("Microsoft Sans Serif", 10);
                label.BackColor = Color.FromArgb(244, 247, 252);
                label.ForeColor = Color.Black;
                label.Dock = DockStyle.Bottom;
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Cursor = Cursors.Hand;

                panel.Size = new Size(204, 183);
                panel.BorderStyle = BorderStyle.None;
                panel.BackColor = Color.FromArgb(215, 228, 242);             

                pictureBox.Size = new Size(204, 126);
                pictureBox.Dock = DockStyle.Top;                
                pictureBox.BackgroundImage = null;
                byte[] Btimagen = (byte[])DtRegitros.Rows[i]["Foto"];
                MemoryStream ms = new MemoryStream(Btimagen);
                pictureBox.Image = Image.FromStream(ms);
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                //pictureBox.BackColor = Color.FromArgb(0, 192, 192);
                pictureBox.Tag = DtRegitros.Rows[i]["Usuario_Nombre"].ToString();
                pictureBox.Cursor = Cursors.Hand;

                panel.Controls.Add(label);
                panel.Controls.Add(pictureBox);
                label.BringToFront();
                FlpanelContenedorSesiones.Controls.Add(panel);
               
              

                //label.Click += new EventHandler(MiEventoLabel); esto no pasa la imagen y se opta por usar el PictorBox
               pictureBox.Click += new EventHandler(MiEventoImagen);

            }
        }

        #endregion

        #region Evento Imagen     
        private void MiEventoImagen(object sender, EventArgs e)
        {
          

            Nombre_Usuario = ((PictureBox)sender).Tag.ToString();
            PtboxUsuario.Image =((PictureBox)sender).Image;
            LblNombreCompleto.Text = Nombre_Usuario;
            OcultarPanelesPrincipal();         
            PanelContenedor.Visible = false;
            PanelDerecha.Visible = false;
            PanelTareas.Visible = false;
            PanelTitulo.Visible = false;
            Panelizquierda.Visible = true;
            Panelizquierda.Dock = DockStyle.Fill;
            TxtPassword.Focus();


        }

        #endregion       

        #region Botones Abrir Formularios


        private void BtnRegistroUsuarios_Click(object sender, EventArgs e)
        {
            PanelTitulo.Visible = false;
            PanelDerecha.Visible = false;
            Panelizquierda.Visible = false;
            PanelContenedor.Visible = false;
            PanelTareas.Dock = DockStyle.Fill;
            PanelRegisTitulo.Visible = false;
            PanelRegisContenedor.Visible = true;

        }

        private void BtnIniciarUsuario_Click(object sender, EventArgs e)
        {

            PanelTitulo.Visible = false;
            Panelizquierda.Visible = false;
            PanelContenedor.Visible = false;
            PanelContenedor.Visible = false;
            PanelTareas.Visible = false;
            PanelDerecha.Visible = true;
            PanelDerecha.Dock = DockStyle.Fill;

            TxtUsuario.Text = "USUARIO";
            TxtUsuario.ForeColor = Color.Gray;

            TxtContraseña.Text = "CONTRASEÑA";
            TxtContraseña.UseSystemPasswordChar = false;
            TxtContraseña.ForeColor = Color.Gray;

            PanelTeclado2.Visible = false;
            LbMostrarPasword.Visible = false;
            PctbxOjo.Image = Properties.Resources.ojo;
            TxtUsuario.Focus();
        }

        #endregion

        #region Ocultar Paneles Principal

        private void OcultarPanelesPrincipal()
        {
            PanelDerecha.Visible = false;          
            PanelTareas.Visible = false;
            PanelTitulo.Visible = false;

        }

        #endregion

        #region Mostrar Paneles Principal
      
        private void MostrarPanelesPrincipal()
        {
            PanelDerecha.Visible = true;         
            PanelTareas.Visible = true;
            PanelTitulo.Visible = true;
        }

        #endregion

        #region Maximizar Formulario Login
       
        public void MaximazarPrincipal()
        {
            LX = Location.X;
            LY = Location.Y;
            Size = Screen.PrimaryScreen.WorkingArea.Size;
            Location = Screen.PrimaryScreen.WorkingArea.Location;
            
        }

        #endregion

        #region Metodo Abrir Formularios Hijos

        Form formulario;       

        private void AbrirFormulario<MiFormulario>() where MiFormulario : Form, new()
        {
            OcultarPanelesPrincipal();
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
                formulario.Show();
                formulario.FormClosed += FormularioHijo_FormClosed;

            }
            else
            {
                formulario.BringToFront();
            }
        }

        #endregion

        #region Evento Cerrar Formularios Hijos      

        private void FormularioHijo_FormClosed(object sender, FormClosedEventArgs e)
        {
            MostrarPanelesPrincipal();
        }

        #endregion
              

        private void PtboxCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            RestableceAccesoDirecto();

        }

        #region Regresar a Login Principal    

        private void RestableceAccesoDirecto()
        {
            TxtPassword.Text = "";
            BtnTeclado.Text = "Teclado en Pantalla";
            PanelTeclado.Visible = false;
            LblMostrarContra.Visible = false;
            PctboxBotonVer.Image = Properties.Resources.ojo;
            TxtPassword.UseSystemPasswordChar = true;          
            MostrarPanelesPrincipal();
            PanelContenedor.Dock = DockStyle.Fill;
            PanelContenedor.Visible = true;
            PanelDerecha.Visible = false;
            Panelizquierda.Visible = false;
            Panelizquierda.Dock = DockStyle.Left;
            TxtPassword.Focus();
        }

        #endregion

        #region Mostar u Ocultar Label  Contraseña
      
        private void OperacionBoton(Label label, PictureBox pictureBox, TextBox textBox)
        {
            if (label.Visible == false)
            {
                ValidaLabel(label);
                label.Visible = true;
                pictureBox.Image = Properties.Resources.OjoOcultar;
                textBox.UseSystemPasswordChar = false;
            }
            else
            {
                label.Visible = false;
                pictureBox.Image = Properties.Resources.ojo;
                textBox.UseSystemPasswordChar = true;
            }
        }

        #endregion

        #region Validar Label Mostrar u Ocultar
       
        private void ValidaLabel(Label label)
        {            
            if (label.Visible == true)
                label.Visible = false;
            
        }

        #endregion

        #region Consulta, Valida y Obtencion de Datos Usuario

        private bool ValidarUsuario()
        {
            DtRegitros = Consulta.Login(Nombre_Usuario, Encryption.Encrypt( TxtPassword.Text));
            if (DtRegitros.Rows.Count > 0)
            {
                UsuarioCache.Id_Usuario=Convert.ToInt32(DtRegitros.Rows[0]["Id_Usuario"].ToString());
                UsuarioCache.Nombre = DtRegitros.Rows[0]["Nombre"].ToString();
                UsuarioCache.Apellido_Paterno = DtRegitros.Rows[0]["Apellido_Paterno"].ToString();
                UsuarioCache.Apellido_Materno = DtRegitros.Rows[0]["Apellido_Materno"].ToString();
                UsuarioCache.Contraseña = DtRegitros.Rows[0]["Contraseña"].ToString();
                UsuarioCache.Usuario_Nombre = DtRegitros.Rows[0]["Usuario_Name"].ToString();
                UsuarioCache.PuestoUsuario = DtRegitros.Rows[0]["Puesto"].ToString();
                UsuarioCache.Puesto = Convert.ToInt32(DtRegitros.Rows[0]["Id_Puesto"].ToString());
                UsuarioCache.Nomina = Convert.ToInt32(DtRegitros.Rows[0]["Nomina"].ToString());
                UsuarioCache.Email = DtRegitros.Rows[0]["Correo"].ToString();
                byte[] imagen=(byte[])(DtRegitros.Rows[0]["Foto"]);
                UsuarioCache.Imagen =imagen;
                UsuarioCache.IdEmpresa = Convert.ToInt32(DtRegitros.Rows[0]["Id_Empresa"]);
                UsuarioCache.IdEmpresa = Convert.ToInt32(DtRegitros.Rows[0]["Id_Sucursal"]);
                UsuarioCache.Empresa = DtRegitros.Rows[0]["Empresa"].ToString();
                UsuarioCache.Sucursal = DtRegitros.Rows[0]["Sucursal"].ToString();

                RevisarPermiso();
                return true;
            }
            return false;
        }

        #endregion

        #region Validacion Permisos

        private void RevisarPermiso()
        {
            DtRegitros = Consulta.AutenticarPermisos(UsuarioCache.Nomina, UsuarioCache.Contraseña);
            if (DtRegitros.Rows.Count>0)
            {
                Repositorios.PermisoUsuario = DtRegitros.Rows[0]["PERMISO"].ToString().Split(',');
            }
        }

        #endregion

      

        #region Evento Changed TxtPasword Inicio Login
       
        private void TxtPassword_TextChanged(object sender, EventArgs e)
        {           
           
            if (ValidarUsuario())
            {
                FormPrincipal formPrincipal = new FormPrincipal();
                FormBienvenida formBienvenida = new FormBienvenida();
                this.Hide();
                GuadarParaLog();
                InsertarLog();
                formBienvenida.ShowDialog();
                formPrincipal.Show();
              
                formPrincipal.FormClosed += CerrarFormPrincipal;

            }
        }
        #endregion

        #region Evento Cerrar Formulario Principal

        private void CerrarFormPrincipal(object sender, FormClosedEventArgs e)
        {
            RestableceAccesoDirecto();
            ClsGuardar.SalirSistema(IdLog, Nomina);
            LimpiarPanelContenedorUsuarios();
            DibujarUsuario();
            this.Show();
        }

        #endregion

        private void PctboxBotonVer_Click(object sender, EventArgs e)
        {
            OperacionBoton(LblMostrarContra,PctboxBotonVer,TxtPassword);
        }

        private void BtnTeclado_Click(object sender, EventArgs e)
        {
            OperacionBotonTeclado(BtnTeclado, PanelTeclado);
        }

        #region Ocultar u Mostrar Panel Teclado
        
        private void ValidarBotonText(Button button)
        {
            if(button.Text == "Teclado en Pantalla")
            {
                button.Text = "Ocultar Teclado";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
          
        }

     

        private bool ValidarTxtUserContra()
        {
            if (TxtUsuario.Text == "USUARIO" && TxtUsuario.TextLength < 4)
            {
                Soporte.MsgError("Ingrese un Nombre de Usuario para continuar.");
                return false;
            }
            if (TxtContraseña.Text == "CONTRASEÑA")
            {
                Soporte.MsgError("Ingrese Su contraseña de Usuario");
                return false;
            }
            return true;
        }
        private void BtnAcceder_Click(object sender, EventArgs e)
        {
            if (ValidarTxtUserContra())
            {
                if (Consulta.ValidarUsuario(TxtUsuario.Text, Encryption.Encrypt( TxtContraseña.Text) ))
                {
                    FormPrincipal formPrincipal = new FormPrincipal();
                    FormBienvenida formBienvenida = new FormBienvenida();
                    this.Hide();
                    GuadarParaLog();
                    InsertarLog();
                    formBienvenida.ShowDialog();
                    formPrincipal.Show();//Mostramos el Form Principal                    
                    formPrincipal.FormClosed += CerrarFormPrincipal;

                }
                else
                {

                    
                    Soporte.MsgInformacion("Usuario o Contraseña es incorrecto intente nuevamente.");
                    TxtContraseña.Text = "CONTRASEÑA";
                    TxtContraseña.ForeColor = Color.FromArgb(216, 27, 96);
                    TxtContraseña.UseSystemPasswordChar = false;
                    BtnTeclado.Text = "Teclado en Pantalla";
                    PanelTeclado.Visible = false;
                    LbMostrarPasword.Visible = false;
                    PctbxOjo.Image = Properties.Resources.ojo;
                    TxtUsuario.Focus();
                }
            }
        }
        private void FormPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            RestablecerControles();
        }

        private void RestablecerControles()
        {
            MostrarPanelesPrincipal();
            TxtContraseña.Text = "CONTRASEÑA";
            TxtContraseña.ForeColor = Color.FromArgb(216, 27, 96);
            TxtContraseña.UseSystemPasswordChar = false;
            BtnTeclado.Text = "Teclado en Pantalla";
            PanelTeclado.Visible = false;
            LbMostrarPasword.Visible = false;
            PctbxOjo.Image = Properties.Resources.ojo;
            TxtUsuario.Focus();
            



        }

        private void BtnTeclado2_Click(object sender, EventArgs e)
        {
            OperacionBotonTeclado(BtnTeclado2, PanelTeclado2);
        }

        private void PctbxOjo_Click(object sender, EventArgs e)
        {
            if (TxtContraseña.Text != "CONTRASEÑA")
            {
                OperacionBoton(LbMostrarPasword,PctbxOjo,TxtContraseña);
            }
        }

        private void ValidarLabel(Label label)
        {
            if (label.Visible == true)
                label.Visible = false;
        }

        

        private void TxtUsuario_Enter(object sender, EventArgs e)
        {
            Soporte.EventoEnterTxt(TxtUsuario, "USUARIO", "");
        }

        

        private void TxtUsuario_Leave(object sender, EventArgs e)
        {
            Soporte.EventoLeaveTxt(TxtUsuario, "USUARIO", "");
        }

        private void TxtContraseña_Enter(object sender, EventArgs e)
        {
            Soporte.EventoEnterTxtPassword(TxtContraseña, "CONTRASEÑA", "");
        }
        private void TxtContraseña_Leave(object sender, EventArgs e)
        {
            Soporte.EventoLeaveTxtPassword(TxtContraseña, "CONTRASEÑA", "");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            PanelTitulo.Visible = true;
            PanelTareas.Visible = true;
            PanelContenedor.Visible = true;
            PanelContenedor.Visible = true;
            PanelDerecha.Visible = false;
            PanelDerecha.Dock = DockStyle.Right;

        }

        private void PctBoxSalirRegistro_Click(object sender, EventArgs e)
        {

        }

        private void PctBoxSalirRegistro_Click_1(object sender, EventArgs e)
        {
            PanelTitulo.Visible = true;
            //PanelDerecha.Visible = false;
            //Panelizquierda.Visible = false;
            PanelContenedor.Visible = true;
            PanelTareas.Dock = DockStyle.Bottom;
            PanelRegisTitulo.Visible = true;
            PanelRegisContenedor.Visible = false;
            LimpiarDatosEmpleado();
            Restablecer_Cajas();
        }

        private void LblSalirRegistro_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void TxtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            Soporte.EventoPresKeyTxt(sender, e, TxtContraseña);
            
        }

        private void TxtContraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                BtnAcceder.PerformClick();
            }
        }

        #region Eventos Enter y Leave en Cajas de Texto
       
        private void TxtNomina_Enter(object sender, EventArgs e)
        {
            Soporte.EventoEnterTxt(TxtNomina, "Nomina...", "");
        }

        private void TxtNomina_Leave(object sender, EventArgs e)
        {
            Soporte.EventoLeaveTxt(TxtNomina,"Nomina...","" );
        }

        private void TxtUser_Name_Enter(object sender, EventArgs e)
        {
            Soporte.EventoEnterTxt(TxtUser_Name, "User Name...", "");
        }

        private void TxtUser_Name_Leave(object sender, EventArgs e)
        {
            Soporte.EventoLeaveTxt(TxtUser_Name, "User Name...", "");
        }

        private void txtContraseñaName_Enter(object sender, EventArgs e)
        {
            Soporte.EventoEnterTxtPassword(txtContraseñaName, "Contraseña...", "");
        }

        private void txtContraseñaName_Leave(object sender, EventArgs e)
        {
            Soporte.EventoLeaveTxtPassword(txtContraseñaName, "Contraseña...", "");
        }

        private void TxtContraConfirmar_Enter(object sender, EventArgs e)
        {
            Soporte.EventoEnterTxtPassword(TxtContraConfirmar, "Confirmar Contraseña...", "");
        }

        private void TxtContraConfirmar_Leave(object sender, EventArgs e)
        {
            Soporte.EventoLeaveTxtPassword(TxtContraConfirmar, "Confirmar Contraseña...", "");
        }

        private void TxtCorreon_Enter(object sender, EventArgs e)
        {
            Soporte.EventoEnterTxt(TxtCorreon, "email@contacto.com", "");

        }

        private void TxtCorreon_Leave(object sender, EventArgs e)
        {
            Soporte.EventoLeaveTxt(TxtCorreon, "email@contacto.com", "");
        }

        private void TxtPin_Enter(object sender, EventArgs e)
        {
            Soporte.EventoEnterTxtPassword(TxtPin, "PIN", "");
        }

        private void TxtPin_Leave(object sender, EventArgs e)
        {
            Soporte.EventoLeaveTxtPassword(TxtPin, "PIN", "");
        }

        private void TxtPalabraSeguridad_Enter(object sender, EventArgs e)
        {
            Soporte.EventoEnterTxt(TxtPalabraSeguridad, "Palabra de Seguridad...", "");
        }

        private void TxtPalabraSeguridad_Leave(object sender, EventArgs e)
        {
            Soporte.EventoLeaveTxt(TxtPalabraSeguridad, "Palabra de Seguridad...", "");
        }

        #endregion

        #region Evento KeyPress Validando Numeros
        
        private void TxtNomina_KeyPress(object sender, KeyPressEventArgs e)
        {

            Soporte.ValidarNumeros(TxtNomina, e);

            if (e.KeyChar == (Char)Keys.Enter)
            {
                BtnBuscarNomina.PerformClick();
            }
          
            
           
        }

        private void TxtPin_KeyPress(object sender, KeyPressEventArgs e)
        {
            Soporte.ValidarNumeros(TxtPin, e);
            Soporte.EventoPresKeyTxt(sender, e, TxtPalabraSeguridad);
        }

        #endregion


        private void ObtenerDatosEmpleado()
        {
            NombreEmpleado = DtRegitros.Rows[0]["Nombre"].ToString();
            NombreCompleto = DtRegitros.Rows[0]["Nombre"].ToString() + ' ' + DtRegitros.Rows[0]["Apellido_Paterno"].ToString() + ' ' + DtRegitros.Rows[0]["Apellido_Materno"].ToString();
            Lblnombre.Text = NombreCompleto;
            LblCorreo.Text = DtRegitros.Rows[0]["Email"].ToString();
            LblPuesto.Text = DtRegitros.Rows[0]["Puesto"].ToString();
            Lbltelefono.Text = "Tel: " + DtRegitros.Rows[0]["Telefono"].ToString();
            byte[] imagen = (byte[])DtRegitros.Rows[0]["Foto"];
            MemoryStream memoryStream = new MemoryStream(imagen);
            PtbxFotoPerfil.Image = Image.FromStream(memoryStream);
            PtbxFotoPerfil.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void BuscarNomina_UserName(int Nomina)
        {
          
            DtRegitros = Consulta.NominaUsuarioBuscar(Nomina);
            if (DtRegitros.Rows.Count > 0)
            {
                ObtenerDatosUserName();
                HabilitarCajas(false);
                BtnRegistrar.Enabled = false;

                ActivarPasswordChat();
                ActivarColorGray();
                miMensaje = ", estos son tus datos de perfil espero que sean los correctos de lo contrario consulta al Administrador";
                LblMensajeBienvedido.Text = "Hola! " + NombreEmpleado + miMensaje;
                LblMensajeBienvedido.Visible = true;


            }
            else
            {

                HabilitarCajas(true);
                TxtUser_Name.Text = "User Name...";
                txtContraseñaName.Text = "Contraseña...";
                TxtContraConfirmar.Text = "Confirmar Contraseña...";
                TxtCorreon.Text = "email@contacto.com";
                TxtPin.Text = "PIN";
                TxtPalabraSeguridad.Text = "Palabra de Seguridad...";
                BtnRegistrar.Enabled = true;
                DesactivarPassworChat();
                ActivarColorGray();
                miMensaje = ", se han encontrado tus datos de Perfil, continua registrando tus Datos de Usuario para completar tu informacion.";
                LblMensajeBienvedido.Text = "Hola!, " + NombreEmpleado + miMensaje;
                LblMensajeBienvedido.Visible = true;
                TxtUser_Name.Focus();
            }
        }
        private void ObtenerDatosUserName()
        {
            txtContraseñaName.Text =Encryption.Decrypt( DtRegitros.Rows[0]["Contraseña"].ToString());
            TxtContraConfirmar.Text = DtRegitros.Rows[0]["Contraseña"].ToString();
            TxtUser_Name.Text = DtRegitros.Rows[0]["Usuario_Name"].ToString();
            TxtCorreon.Text = DtRegitros.Rows[0]["Correo"].ToString();
            TxtPalabraSeguridad.Text = DtRegitros.Rows[0]["Palabra_Seguridad"].ToString();
            TxtPin.Text = DtRegitros.Rows[0]["Pin"].ToString();

            PtbxOjoContraseñaUsuario.Visible = true;
            PtbxOjoContraseñaUsuario.Image = Properties.Resources.ojo;
            label13.Visible = false;
        }

        private void BuscarNominaEmpleado(int iNomina)
        {

            DtRegitros = Consulta.NominaEmpleadoBuscar(iNomina);
            if (DtRegitros.Rows.Count > 0)
            {
                ObtenerDatosEmpleado(); //Busca los Datos del Empleados
                BuscarNomina_UserName(iNomina);//Buscar los datos de Usuario como la contraseña y Nombre de Usuario

            }
            else
            {

                LimpiarDatosEmpleado();
                Restablecer_Cajas();
                HabilitarCajas(false);
                LblMensajeBienvedido.Visible = false;
                Soporte.MsgInformacion("No se encontraron datos de la nomina que se ingreso, favor de revisar que sean los datos correctos o en caso consultar al Administrador.");
            }
        }
        private void LimpiarDatosEmpleado()
        {
            TxtNomina.Focus();
            TxtNomina.SelectAll();
            Lblnombre.Text = "Nombre Completo";
            LblCorreo.Text = "correo@empresa.com.mx";
            Lbltelefono.Text = "Tel: 00000000";
            LblPuesto.Text = "Puesto Asignado";

            PtbxFotoPerfil.SizeMode = PictureBoxSizeMode.Zoom;
            PtbxFotoPerfil.Image = Properties.Resources.LogoRHAquaTrazo;
        }
        private void Restablecer_Cajas()
        {
            TxtNomina.Text = "Nomina...";
            TxtUser_Name.Text = "User Name...";
            txtContraseñaName.Text = "Contraseña...";
            TxtContraConfirmar.Text = "Confirmar Contraseña...";
            TxtCorreon.Text = "email@contacto.com";
            TxtPin.Text = "PIN";
            TxtPalabraSeguridad.Text = "Palabra de Seguridad...";

            PtbxOjoContraseñaUsuario.Visible = false;
            PtbxOjoContraseñaUsuario.Image = Properties.Resources.ojo;            
            label13.Visible = false;

            ActivarColorGray();
            DesactivarPassworChat();

        }

        private void ActivarColorGray()
        {
            TxtPin.ForeColor = Color.Gray;
            TxtPalabraSeguridad.ForeColor = Color.Gray;
            TxtCorreon.ForeColor = Color.Gray;
            TxtContraConfirmar.ForeColor = Color.Gray;
            txtContraseñaName.ForeColor = Color.Gray;
            TxtUser_Name.ForeColor = Color.Gray;

        }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                AsignarValoresUserName();
                Guardar_UserName();
                LimpiarDatosEmpleado();
                Restablecer_Cajas();
                HabilitarCajas(false);
                LimpiarPanelContenedorUsuarios();
                DibujarUsuario();
                
            }
        }
        private void Guardar_UserName()
        {
            
            ClsGuardar.RegistrarUsuario(Nomina, Nombre_Usuario, Contraseña,
                Email, Pin, Palabra_Seguridad);
            Soporte.MsgInformacion("Se ha registrado correctamente, cierre el sistema he ingrese con su Usuario.");


        }
        private void LimpiarPanelContenedorUsuarios()
        {
            FlpanelContenedorSesiones.Controls.Clear();
        }

        private void AsignarValoresUserName()
        {
            Nomina = Convert.ToInt32(TxtNomina.Text);
            Nombre_Usuario = TxtUser_Name.Text;
            Contraseña =Encryption.Encrypt(TxtContraConfirmar.Text);
            Email = TxtCorreon.Text;
            Pin = Convert.ToInt32(TxtPin.Text);
            Palabra_Seguridad = TxtPalabraSeguridad.Text;
        }

        private void LkblRecuperar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormRecuperarContraseña formRecuperar = new FormRecuperarContraseña();
            formRecuperar.ShowDialog();
        }

        private void LkblRecuperarContra_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormRecuperarContraseña formRecuperar = new FormRecuperarContraseña();
            formRecuperar.ShowDialog();
        }

        private void PtbxOjoContraseñaUsuario_Click(object sender, EventArgs e)
        {
            OperacionBoton(label13, PtbxOjoContraseñaUsuario, txtContraseñaName);
            
        }

        private void BtnFacebook_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.facebook.com/Springbok11/");
        }

        private void BtnYoutube_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.youtube.com/channel/UC0y7RYpny27U_K6nTGQZZBA?view_as=subscriber");
        }

        private void BtnMapa_Click(object sender, EventArgs e)
        {
            Process.Start("https://goo.gl/maps/8XYMztSog2tFK3ei6");
        }

        private void BtnWatsApp_Click(object sender, EventArgs e)
        {
            Process.Start("https://web.whatsapp.com/");
        }

        private void TxtUser_Name_KeyPress(object sender, KeyPressEventArgs e)
        {
            Soporte.EventoPresKeyTxt(sender, e, txtContraseñaName);
        }

        private void txtContraseñaName_KeyPress(object sender, KeyPressEventArgs e)
        {
            Soporte.EventoPresKeyTxt(sender, e, TxtContraConfirmar);
        }

        private void TxtContraConfirmar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Soporte.EventoPresKeyTxt(sender, e, TxtCorreon);
        }

        private void TxtCorreon_KeyPress(object sender, KeyPressEventArgs e)
        {
            Soporte.EventoPresKeyTxt(sender, e, TxtPin);
        }

        private void TxtPalabraSeguridad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                BtnBuscarNomina.PerformClick();
            }
        }

       

        private void DesactivarPassworChat()
        {
            txtContraseñaName.UseSystemPasswordChar = false;
            TxtContraConfirmar.UseSystemPasswordChar = false;
            TxtPin.UseSystemPasswordChar = false;
            TxtPalabraSeguridad.UseSystemPasswordChar = false;
        }
        private void ActivarPasswordChat()
        {
            txtContraseñaName.UseSystemPasswordChar = true;
            TxtContraConfirmar.UseSystemPasswordChar = true;
            TxtPin.UseSystemPasswordChar = true;
            TxtPalabraSeguridad.UseSystemPasswordChar = true;
        }

        private void HabilitarCajas(bool valor)
        {
            TxtUser_Name.ReadOnly = !valor;
            txtContraseñaName.ReadOnly = !valor;
            TxtContraConfirmar.ReadOnly = !valor;
            TxtCorreon.ReadOnly = !valor;
            TxtPin.ReadOnly = !valor;
            TxtPalabraSeguridad.ReadOnly = !valor;

        }

        private void BtnBuscarNomina_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtNomina.Text) || TxtNomina.Text == "Nomina..." || TxtNomina.Text == "0")
            {
                Soporte.MsgInformacion("Ingrese el numero de nomina para poder continuar.");
                
            }
            else
            {
                BuscarNominaEmpleado(Convert.ToInt32(TxtNomina.Text));
            }
        }

        private void OperacionBotonTeclado(Button button, Panel panel)
        {
            if (button.Text == "Ocultar Teclado")
            {
                ValidarBotonText(button);
                button.Text = "Teclado en Pantalla";
                panel.Visible = false;

            }
            else
            {
                button.Text = "Ocultar Teclado";
                panel.Visible = true;
            }

        }

        #endregion

        #region Validar Campos Usuario
        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(TxtNomina.Text) || TxtNomina.Text == "Nomina...")
            {
                Soporte.MsgInformacion("Ingrese Numero de nomina para buscar sus Datos");
                TxtNomina.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(TxtUser_Name.Text) || TxtUser_Name.Text == "User Name...")
            {
                Soporte.MsgInformacion("Ingrese un Nombre de Usuario para continuar");
                TxtUser_Name.Focus();
                return false;
            }


            if (string.IsNullOrEmpty(txtContraseñaName.Text) || txtContraseñaName.Text == "Contraseña...")
            {
                Soporte.MsgInformacion("Falta ingresar la Contraseña.");
                txtContraseñaName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(TxtContraConfirmar.Text) || TxtContraConfirmar.Text == "Confirmar Contraseña...")
            {
                Soporte.MsgInformacion("Confirme la contraseña para poder continuar.");
                TxtContraConfirmar.Focus();
                TxtContraConfirmar.SelectAll();
            }

            if (txtContraseñaName.Text != TxtContraConfirmar.Text)
            {
                Soporte.MsgInformacion("Las Contraseñas no coinciden favor de revisar");
                TxtContraConfirmar.Focus();
                TxtContraConfirmar.SelectAll();
                return false;
            }

            if (string.IsNullOrEmpty(TxtCorreon.Text))
            {
                TxtCorreon.Text = "email@contacto.com";
            }

            if (Soporte.ValidarMail(TxtCorreon.Text) == false)
            {
                Soporte.MsgInformacion("Correo no valido favor de revisar");
                TxtCorreon.Focus();
                TxtCorreon.SelectAll();
                return false;
            }
            if (string.IsNullOrEmpty(TxtPin.Text) || TxtPin.Text == "PIN")
            {
                TxtPin.Text = "0";
            }
            if (string.IsNullOrEmpty(TxtPalabraSeguridad.Text) || TxtPalabraSeguridad.Text == "Palabra de Seguridad...")
            {
                TxtPalabraSeguridad.Text = "Sin Registro de Seguridad";
            }

            return true;
        }
        #endregion


    }
}
