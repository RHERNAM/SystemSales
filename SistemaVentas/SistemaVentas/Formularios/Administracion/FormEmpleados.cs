using SistemaVentas.Clases.SQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccesDLL;
using SistemaVentas.Clases.SQL.Transacciones;
using SistemaVentas.Clases.Entidates;
using SistemaVentas.Formularios.Configuraciones;

namespace SistemaVentas.Formularios.Administracion
{
    public partial class FormEmpleados : Form
    {
        #region Variables

        DataTable DtRegistros;
        private int Nomina, Puesto, Codigo_Postal, IdEmpleado, IdEstatus, IdArea, IdEpresa, IdSucursal, IdColonia, IdMunicipio, IdEstado, IdPais;
        private string Nombre, Apellido_Paterno, Apellido_Materno, CURP, RFC, Telefono, Celular, Email, Calle, Numero_Interno, Numero_Exterior, Entre_Calles, Colonia, Municipio, Estado, Pais;
        private bool EsNuevo= false, EsEditar= false;   
        private byte[] Foto;
        DateTime Fecha_Nacimiento;

        #endregion

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

        #region Inicio Formulario

        public FormEmpleados()
        {
            InitializeComponent();
        }      

        private void FormEmpleados_Load(object sender, EventArgs e)
        {
            MostrarRegistros();
            //ListaPuestos();
            ListaAreas();
            ListaEstatus();
            HabilitarCajasText(false);
            HabilitarBotones();
            DtpFechaNacimiento.MaxDate = DateTime.Now.AddYears(-18);
        }

       

        #endregion

        #region Consultas

        private void MostrarRegistros()
        {
            DtRegistros = Consulta.ConsultaEmpleados();
            RadGVEmpleados.DataSource = null;
            RadGVEmpleados.DataSource = DtRegistros;
            RadGVEmpleados.BestFitColumns();
            Soporte.ContarRegistros(RadGVEmpleados, LblTotal, "Total Registros: ");
            OcultarColumnas();
        }

        private void ListaAreas()
        {
            CmbAreas.DisplayMember = "Area";
            CmbAreas.ValueMember = "Id_Area";
            CmbAreas.DataSource = Consulta.AreaComboLista();

            CmbEmpresa.DisplayMember = "Nombre";
            CmbEmpresa.ValueMember = "Id_Empresa";
            CmbEmpresa.DataSource = Consulta.EmpresaCombobox();

            CmbxPais.DisplayMember = "Nombre";
            CmbxPais.ValueMember = "IdPais";
            CmbxPais.DataSource = Consulta.PaisListCombo();


        }

        #region Lista Puestos

        private void ListaPuestos(int idarea)
        {
            CmbPuesto.DataSource = Consulta.dtConsultaPuestos(idarea);
            CmbPuesto.DisplayMember = "Nombre";
            CmbPuesto.ValueMember = "Id_Puesto";
        }

        private void ListaSucursal(int IdEmpresa)
        {
            CmbSucursal.DataSource = Consulta.SucursalListCombo(IdEmpresa);
            CmbSucursal.DisplayMember = "Nombre";
            CmbSucursal.ValueMember = "Id_Negocio";
        }

        #endregion

        #region Lista Estatus        
        private void ListaEstatus()
        {
            CmbEstatus.DataSource = Consulta.EstatusEmple();
            CmbEstatus.DisplayMember = "Nombre";
            CmbEstatus.ValueMember = "Id_Estatus";
        }
        #endregion

        #endregion

        #region Metodos

        private void OcultarColumnas()
        {
            RadGVEmpleados.Columns["Id_Usuario"].IsVisible = false;
            RadGVEmpleados.Columns["Foto"].IsVisible = false;
            RadGVEmpleados.Columns["IdPais"].IsVisible = false;
            RadGVEmpleados.Columns["IdEstado"].IsVisible = false;
            RadGVEmpleados.Columns["IdMunicipio"].IsVisible = false;
            RadGVEmpleados.Columns["IdColonia"].IsVisible = false;
            RadGVEmpleados.Columns["IdPuesto"].IsVisible = false;
            RadGVEmpleados.Columns["IdEstatus"].IsVisible = false;
            RadGVEmpleados.Columns["Id_Area"].IsVisible = false;
            RadGVEmpleados.Columns["Id_Empresa"].IsVisible = false;
            RadGVEmpleados.Columns["Id_Sucursal"].IsVisible = false;
        }

        private void BtnEnviarRegistro_Click(object sender, EventArgs e)
        {

        }

        private void BtnMunicipio_Click(object sender, EventArgs e)
        {
            FormMunicipios formMunicipios = new FormMunicipios();
            formMunicipios.WindowState = FormWindowState.Normal;
            formMunicipios.ShowDialog();
        }

        private void BtnEstado_Click(object sender, EventArgs e)
        {
            FormEstados formEstados = new FormEstados();
            formEstados.WindowState = FormWindowState.Normal;
            formEstados.ShowDialog();
        }

        private void BtnPais_Click(object sender, EventArgs e)
        {
            FormPaises formPaises = new FormPaises();
            formPaises.WindowState = FormWindowState.Normal;
            formPaises.ShowDialog();
        }

        private void CmbxPais_SelectedValueChanged(object sender, EventArgs e)
        {
            if (CmbxPais.SelectedValue != null)
            {

                int pais = Convert.ToInt32(CmbxPais.SelectedValue);
                DtRegistros = Consulta.Estados(pais);
                if (DtRegistros.Rows.Count > 0)
                {

                    if (pais == 0)
                    {
                        CmbxEstado.Enabled = false;
                        if (CmbxPais.DataSource != null)
                        {
                            CmbxEstado.SelectedValue = 0;
                        }

                    }
                    else
                    {
                        if (CmbxPais.Enabled)
                        {
                            CmbxEstado.Enabled = true;
                        }
                        else
                        {
                            CmbxEstado.Enabled = false;
                        }

                        CmbxEstado.DisplayMember = "Nombre";
                        CmbxEstado.ValueMember = "IdEstado";
                        CmbxEstado.DataSource = DtRegistros;
                    }

                }

                CmbxMunicipio.SelectedValue = 0;
                CmbxMunicipio.Enabled = false;
                CmbColonias.SelectedValue = 0;
                CmbColonias.Enabled = false;
                TxtCodigoPostal.Clear();

            }
        }

        private void CmbxEstado_SelectedValueChanged(object sender, EventArgs e)
        {
            if (CmbxEstado.SelectedValue != null)
            {

                int Estado = Convert.ToInt32(CmbxEstado.SelectedValue);
                DtRegistros = Consulta.MunicipioListCombo(Estado);
                if (DtRegistros.Rows.Count > 0)
                {
                    if (Estado == 0)
                    {
                        if (CmbxMunicipio.DataSource != null)
                        {
                            CmbxMunicipio.SelectedIndex = 0;
                        }
                        CmbxMunicipio.Enabled = false;
                    }
                    else
                    {
                        if (CmbxEstado.Enabled)
                        {
                            CmbxMunicipio.Enabled = true;
                        }
                        else
                        {
                            CmbxMunicipio.Enabled = false;
                        }

                        CmbxMunicipio.DisplayMember = "Nombre";
                        CmbxMunicipio.ValueMember = "IdMunicipio";
                        CmbxMunicipio.DataSource = DtRegistros;

                    }

                }

                CmbColonias.SelectedValue = 0;
                CmbColonias.Enabled = false;
                TxtCodigoPostal.Clear();

            }
        }

        private void CmbxMunicipio_SelectedValueChanged(object sender, EventArgs e)
        {
            if (CmbxMunicipio.SelectedValue != null)
            {

                int IdMunicipio = Convert.ToInt32(CmbxMunicipio.SelectedValue);
                DtRegistros = Consulta.ColoniasPorMunicipio(IdMunicipio);
                if (DtRegistros.Rows.Count > 0)
                {
                    if (IdMunicipio == 0)
                    {
                        if (CmbColonias.DataSource != null)
                        {
                            CmbColonias.SelectedIndex = 0;
                        }

                        CmbColonias.Enabled = false;
                    }
                    else
                    {
                        if (CmbxMunicipio.Enabled)
                        {
                            CmbColonias.Enabled = true;
                        }
                        else
                        {
                            CmbColonias.Enabled = false;
                        }

                        CmbColonias.DisplayMember = "Colonia";
                        CmbColonias.ValueMember = "IdColonia";
                        CmbColonias.DataSource = DtRegistros;
                    }

                }


            }
        }

        private void CmbColonias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DtRegistros.Rows.Count > 0)
            {
                try
                {
                    TxtCodigoPostal.Text = DtRegistros.Rows[CmbColonias.SelectedIndex]["CodigoPostal"].ToString();
                }
                catch (Exception)
                {

                    return;
                }



            }
        }

        private void BtnColonia_Click(object sender, EventArgs e)
        {
            FormColonias formColonias = new FormColonias();
            formColonias.WindowState = FormWindowState.Normal;
            formColonias.ShowDialog();
        }

        private void CmbAreas_SelectedValueChanged(object sender, EventArgs e)
        {
            if (CmbAreas.SelectedValue != null)
            {

                int area = Convert.ToInt32(CmbAreas.SelectedValue);                           

                if (area == 0)
                {
                    CmbPuesto.Enabled = false;
                    if (CmbAreas.DataSource != null)
                    {
                        CmbPuesto.SelectedValue = 0;
                    }

                }
                else
                {
                    if (CmbAreas.Enabled)
                    {
                        CmbPuesto.Enabled = true;
                    }
                    else
                    {
                        CmbPuesto.Enabled = false;
                    }  
                   
                    ListaPuestos(area);
                }
                              

            }
        }

        private void CmbEmpresa_SelectedValueChanged(object sender, EventArgs e)
        {
            if (CmbEmpresa.SelectedValue != null)
            {

                int IdEmpresa = Convert.ToInt32(CmbEmpresa.SelectedValue);

                if (IdEmpresa == 0)
                {
                    CmbSucursal.Enabled = false;
                    if (CmbEmpresa.DataSource != null)
                    {
                        CmbSucursal.SelectedValue = 0;
                    }

                }
                else
                {
                    if (CmbEmpresa.Enabled)
                    {
                        CmbSucursal.Enabled = true;
                    }
                    else
                    {
                        CmbSucursal.Enabled = false;
                    }
                   

                    ListaSucursal(IdEmpresa);
                }


            }
        }


        #region Habilitar Botones
        private void HabilitarBotones()
        {
            if (EsNuevo || EsEditar)
            {
                HabilitarCajasText(true);
                BtnNuevo.Enabled = false;
                BtnGuardar.Enabled = true;
                BtnActulizar.Enabled = false;
                BtnCancelar.Enabled = true;
                BtnAbrirImagen.Enabled = true;
                BtnBorrarImagen.Enabled = true;


            }
            else
            {
                HabilitarCajasText(false);
                BtnNuevo.Enabled = true;
                BtnGuardar.Enabled = false;
                BtnActulizar.Enabled = true;
                BtnCancelar.Enabled = false;
                BtnAbrirImagen.Enabled = false;
                BtnBorrarImagen.Enabled = false;
            }

        }
        #endregion

        #region Habilitar Cajas de Texto
        private void HabilitarCajasText(bool valor)
        {
            TxtId_Usuario.ReadOnly = !valor;
            TxtNombres.ReadOnly = !valor;
            TxtApPaterno.ReadOnly = !valor;
            TxtApMaterno.ReadOnly = !valor;
            TxtCURP.ReadOnly = !valor;
            TxtEmail.ReadOnly = !valor;
            TxtCalle.ReadOnly = !valor;
            TxtCelular.ReadOnly = !valor;
            TxtCodigoPostal.ReadOnly = !valor;
            TxtColonia.ReadOnly = !valor;
          
            TxtEntreCalles.ReadOnly = !valor;
            TxtEstado.ReadOnly = !valor;
            TxtMunicipio.ReadOnly = !valor;
           // TxtNomina.ReadOnly = !valor;
            TxtNumExterno.ReadOnly = !valor;
            TxtNumInterno.ReadOnly = !valor;
            TxtPais.ReadOnly = !valor;
            TxtRFC.ReadOnly = !valor;
            TxtTelefono.ReadOnly = !valor;
      
            DtpFechaNacimiento.Enabled = valor;
          
            CmbEstatus.Enabled = valor;
            CmbEmpresa.Enabled = valor;
            CmbAreas.Enabled = valor;
            CmbxPais.Enabled = valor;

            if (EsEditar)
            {
                CmbColonias.Enabled = valor;
                CmbxMunicipio.Enabled = valor;
                CmbxEstado.Enabled = valor;
                CmbSucursal.Enabled = valor;
                CmbPuesto.Enabled = valor;
            }

        }

        #endregion

        #region Vaciar Cajas de Texto
        private void LimpiaCajasTextoBox()
        {
            //CmbPuesto.SelectedIndex=0;
            //CmbEstatus.SelectedIndex = 0;
            TxtNomina.Clear();
            TxtId_Usuario.Clear();            
            TxtNombres.Clear();
            TxtApMaterno.Clear();
            TxtApPaterno.Clear();
            TxtRFC.Clear();
            TxtCURP.Clear();
            TxtTelefono.Clear();
            TxtCelular.Clear();
            TxtEmail.Clear();
          
            TxtCalle.Clear();
            TxtNumInterno.Clear();
            TxtNumExterno.Clear();
            TxtEntreCalles.Clear();
            TxtColonia.Clear();
            TxtCodigoPostal.Clear();
            TxtEstado.Clear();
            TxtMunicipio.Clear();
            TxtPais.Clear();
            DtpFechaNacimiento.MaxDate = DateTime.Now.AddYears(-18);
            PtbxFotoPerfil.Image = Properties.Resources.LgoEsferaBlancTrasp;

            CmbxPais.SelectedValue = 0;
            CmbAreas.SelectedValue = 0;
            CmbEstatus.SelectedValue = 0;
            CmbEmpresa.SelectedValue = 0;

        }

        #endregion

        #region Nomina Empleado
        private void NominaEmpleado()
        {
            BaseProp.Nomina = Consulta.NominaEmpleado();
            //TxtNomina.Text = Convert.ToString(BaseProp.Nomina);
            Nomina = BaseProp.Nomina;
        }
        #endregion

        #region Guardar Empleado

        private void GuardarEmpleado()
        {

            ClsGuardar.GuardaEmpleado(
            Nomina, Nombre, Apellido_Paterno, Apellido_Materno, CURP, RFC, Fecha_Nacimiento,
            Telefono, Celular, Email, Puesto, Calle, Numero_Interno, Numero_Exterior, Entre_Calles,
            IdColonia, Codigo_Postal, IdMunicipio, IdEstado, IdPais, Foto, IdEstatus, IdArea,  IdEpresa, IdSucursal);
        }

        #endregion

        #region Validar Campos

        private bool ValidarCampos()
        {

            //if (string.IsNullOrEmpty(TxtNomina.Text))
            //{
            //    Soporte.MsgInformacion("Ingrese el Numero de Nomina");
            //    return false;
            //}

            if (string.IsNullOrEmpty(TxtNombres.Text))
            {
                Soporte.MsgInformacion("Ingrese un Nombre del Empleado");
                return false;
            }
            if (string.IsNullOrEmpty(TxtApPaterno.Text))
            {
                Soporte.MsgInformacion("Ingrese el Apellido Paterno del Empleado");
                return false;
            }
            if (string.IsNullOrEmpty(TxtApMaterno.Text))
            {
                Soporte.MsgInformacion("Ingrese el Apellido Materno del Empleado");
                return false;
            }
            if (string.IsNullOrEmpty(TxtCURP.Text))
            {

                TxtCURP.Text = "Sin Registro";
            }
            if (string.IsNullOrEmpty(TxtRFC.Text))
            {
                TxtRFC.Text = "Sin Registro";
            }
            if (string.IsNullOrEmpty(TxtTelefono.Text))
            {
                TxtTelefono.Text = "0";
            }
            if (string.IsNullOrEmpty(TxtCelular.Text))
            {
                TxtCelular.Text = "0";
            }
            if (string.IsNullOrEmpty(TxtEmail.Text))
            {

                TxtEmail.Text = "correo@gamail.com";
            }

            if (CmbAreas.SelectedIndex == 0)
            {
                Soporte.MsgInformacion("Seleccione un Area");
                return false;
            }

            if (string.IsNullOrEmpty(CmbPuesto.Text))
            {
                Soporte.MsgInformacion("Seleccione un Puesto para el Empleado");
                return false;
            }

            if (CmbPuesto.SelectedIndex==0)
            {
                Soporte.MsgInformacion("Seleccione un Puesto para el Empleado");
                return false;
            }

            if (string.IsNullOrEmpty(CmbEstatus.Text))
            {
                Soporte.MsgInformacion("Seleccione el estatus del Empleado");
                return false;
            }

            if (CmbEstatus.SelectedIndex==0)
            {
                Soporte.MsgInformacion("Seleccione el estatus del Empleado");
                return false;
            }

            if (string.IsNullOrEmpty(TxtCalle.Text))
            {
                TxtCalle.Text = "Registro Pendiente";
            }

            if (string.IsNullOrEmpty(TxtNumInterno.Text))
            {
                TxtNumInterno.Text = "0";
            }
            if (string.IsNullOrEmpty(TxtNumExterno.Text))
            {
                TxtNumExterno.Text = "0";
            }

            if (string.IsNullOrEmpty(TxtEntreCalles.Text))
            {
                TxtEntreCalles.Text = "Registro Pendiente";
            }


            if (CmbxPais.SelectedIndex == 0)
            {
                Soporte.MsgError("Seleccione el Pais.");
                return false;
            }

            if (CmbxEstado.SelectedIndex == 0)
            {
                Soporte.MsgError("Seleccione un Estado");
                return false;
            }
            if (CmbxMunicipio.SelectedIndex == 0)
            {
                Soporte.MsgError("Seleccione un Municipio");
                return false;
            }

            if (CmbColonias.SelectedIndex == 0)
            {
                Soporte.MsgError("Seleccione una Colonia");
                return false;
            }

            if (string.IsNullOrEmpty(TxtCodigoPostal.Text))
            {
                TxtCodigoPostal.Text = "0";
            }

          
           
          
            if (Soporte.ValidarMail(TxtEmail.Text) == false)
            {
                Soporte.MsgInformacion("Correo no valido");
                TxtEmail.Focus();
                TxtEmail.SelectAll();
                return false;
            }

            if (CmbEmpresa.SelectedIndex == 0)
            {
                Soporte.MsgError("Seleccione una Empresa");
                return false;
            }

            if (CmbSucursal.SelectedIndex == 0)
            {
                Soporte.MsgError("Seleccione una Sucursal.");
                return false;
            }


            return true;

        }

        #endregion

        #region Asignar Valores        
        private void AsignarValores()
        {

            if (string.IsNullOrEmpty(TxtId_Usuario.Text))
            {
                IdEmpleado = 0;
            }
            else
            {
                IdEmpleado = Convert.ToInt32(TxtId_Usuario.Text);
            }
            if (EsEditar)
            {
                Nomina = Convert.ToInt32(TxtNomina.Text);
            }
          
           
            Nombre = TxtNombres.Text;
            Apellido_Paterno = TxtApPaterno.Text;
            Apellido_Materno = TxtApMaterno.Text;
            CURP = TxtCURP.Text;
            RFC = TxtRFC.Text;
            Fecha_Nacimiento = DtpFechaNacimiento.Value;
            Telefono = TxtTelefono.Text;
            Celular = TxtCelular.Text;
            Email = TxtEmail.Text;
            Puesto = Convert.ToInt32(CmbPuesto.SelectedValue);
            Calle = TxtCalle.Text;
            Numero_Interno = TxtNumExterno.Text;
            Numero_Exterior = TxtNumInterno.Text;
            Entre_Calles = TxtEntreCalles.Text;
            //Colonia = TxtColonia.Text;
            Codigo_Postal = Convert.ToInt32(TxtCodigoPostal.Text);
            //Municipio = TxtMunicipio.Text;
            //Estado = TxtEstado.Text;
            //Pais = TxtPais.Text;

            IdColonia = Convert.ToInt32(CmbColonias.SelectedValue);
            IdMunicipio = Convert.ToInt32(CmbxMunicipio.SelectedValue);
            IdEstado = Convert.ToInt32(CmbxEstado.SelectedValue);
            IdPais = Convert.ToInt32(CmbxPais.SelectedValue);

            MemoryStream memoria = new MemoryStream();
            PtbxFotoPerfil.Image.Save(memoria, System.Drawing.Imaging.ImageFormat.Png);
            byte[] imagen = memoria.GetBuffer();
            Foto = imagen;
            IdEstatus = Convert.ToInt32(CmbEstatus.SelectedValue);
            IdArea=Convert.ToInt32(CmbAreas.SelectedValue);
            IdEpresa = Convert.ToInt32(CmbEmpresa.SelectedValue);
            IdSucursal = Convert.ToInt32(CmbSucursal.SelectedValue);
        }
        #endregion

        #region Enviar Registros

        private void ObtenerEnviarRegistro()
        {
            TxtId_Usuario.Text = RadGVEmpleados.CurrentRow.Cells["Id_Usuario"].Value.ToString();
            TxtNomina.Text = RadGVEmpleados.CurrentRow.Cells["Nomina"].Value.ToString();
            TxtNombres.Text = RadGVEmpleados.CurrentRow.Cells["Nombre"].Value.ToString();
            TxtApPaterno.Text = RadGVEmpleados.CurrentRow.Cells["Apellido_Paterno"].Value.ToString();
            TxtApMaterno.Text = RadGVEmpleados.CurrentRow.Cells["Apellido_Materno"].Value.ToString();
            TxtCURP.Text = RadGVEmpleados.CurrentRow.Cells["CURP"].Value.ToString();
            TxtRFC.Text = RadGVEmpleados.CurrentRow.Cells["RFC"].Value.ToString();
            DtpFechaNacimiento.Value = Convert.ToDateTime(RadGVEmpleados.CurrentRow.Cells["Fecha_Nacimiento"].Value);
            TxtTelefono.Text = RadGVEmpleados.CurrentRow.Cells["Telefono"].Value.ToString();
            TxtCelular.Text = RadGVEmpleados.CurrentRow.Cells["Celular"].Value.ToString();
            TxtEmail.Text = RadGVEmpleados.CurrentRow.Cells["Email"].Value.ToString();    
            CmbAreas.SelectedValue= Convert.ToInt32(RadGVEmpleados.CurrentRow.Cells["Id_Area"].Value);
            CmbPuesto.SelectedValue = RadGVEmpleados.CurrentRow.Cells["IdPuesto"].Value.ToString();
            //CmbPuesto.Text = RadGVEmpleados.CurrentRow.Cells["Puesto"].Value.ToString();
            CmbEstatus.SelectedValue=Convert.ToInt32( RadGVEmpleados.CurrentRow.Cells["IdEstatus"].Value);
            TxtCalle.Text = RadGVEmpleados.CurrentRow.Cells["Calle"].Value.ToString();
            TxtNumInterno.Text = RadGVEmpleados.CurrentRow.Cells["Numero_Interno"].Value.ToString();
            TxtNumExterno.Text = RadGVEmpleados.CurrentRow.Cells["Numero_Exterior"].Value.ToString();
            TxtEntreCalles.Text = RadGVEmpleados.CurrentRow.Cells["Entre_Calles"].Value.ToString();
            //TxtColonia.Text = RadGVEmpleados.CurrentRow.Cells["Colonia"].Value.ToString();
            //TxtCodigoPostal.Text = RadGVEmpleados.CurrentRow.Cells["Codigo_Postal"].Value.ToString();
            //TxtMunicipio.Text = RadGVEmpleados.CurrentRow.Cells["Municipio"].Value.ToString();
            //TxtEstado.Text = RadGVEmpleados.CurrentRow.Cells["Estado"].Value.ToString();
            //TxtPais.Text = RadGVEmpleados.CurrentRow.Cells["Pais"].Value.ToString();             
            CmbEmpresa.SelectedValue= Convert.ToInt32(RadGVEmpleados.CurrentRow.Cells["Id_Empresa"].Value);
            CmbSucursal.SelectedValue= Convert.ToInt32(RadGVEmpleados.CurrentRow.Cells["Id_Sucursal"].Value);
            CmbxPais.SelectedValue =Convert.ToInt32(RadGVEmpleados.CurrentRow.Cells["IdPais"].Value);
            CmbxEstado.SelectedValue =Convert.ToInt32( RadGVEmpleados.CurrentRow.Cells["IdEstado"].Value);
            CmbxMunicipio.SelectedValue=Convert.ToInt32( RadGVEmpleados.CurrentRow.Cells["IdMunicipio"].Value);
            CmbColonias.SelectedValue=Convert.ToInt32( RadGVEmpleados.CurrentRow.Cells["IdColonia"].Value);

            byte[] imagenBufers = (byte[])RadGVEmpleados.CurrentRow.Cells["Foto"].Value;
            MemoryStream ms = new MemoryStream(imagenBufers);
            this.PtbxFotoPerfil.Image = Image.FromStream(ms);
            this.PtbxFotoPerfil.SizeMode = PictureBoxSizeMode.Zoom;

            radPageView1.SelectedPage = RadpgvRegistroEmpleado;
            TxtId_Usuario.Visible = true;
            Lbl_IdUsuario.Visible = true;            
            BtnActulizar.Text = "Editar";

        }
        #endregion

        #region Actualizar

        private void ActualizarEmpleados()
        {
            ClsGuardar.ActualizarEmpleado(
           IdEmpleado, Nomina, Nombre, Apellido_Paterno, Apellido_Materno, CURP, RFC, Fecha_Nacimiento,
           Telefono, Celular, Email, Puesto, Calle, Numero_Interno, Numero_Exterior, Entre_Calles,
           IdColonia, Codigo_Postal, IdMunicipio, IdEstado, IdPais, Foto, IdEstatus,IdArea,IdEpresa,IdSucursal);
        }

        #endregion

        #region Operacion Boton Cancelar

        private void RestablecerControles()
        {
            EsNuevo = false;
            EsEditar = false;
            HabilitarBotones();
            HabilitarCajasText(false);
            LimpiaCajasTextoBox();
            TxtId_Usuario.Visible = false;
            Lbl_IdUsuario.Visible = false;
            BtnActulizar.Text = "Actualizar";
           
        }
        #endregion

        #endregion

        #region Eventos Controles

        private void RadGVEmpleados_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            RestablecerControles();
            ObtenerEnviarRegistro();
        }
               
        private void ChkFiltro_CheckedChanged(object sender, EventArgs e)
        {
            if (RadGVEmpleados.Rows.Count > 5)
            {
                ChkFiltro.Checked = true;
                Soporte.FiltroTipoExcelRadGridView(RadGVEmpleados);
            }
            else
            {
                ChkFiltro.Checked = false;
            }

        }

        #endregion

        #region Operacion Botones

        #region Boton Guardar
        
        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                Cursor = Cursors.WaitCursor;
                AsignarValores();
                if (EsNuevo)
                {
                    NominaEmpleado();
                    GuardarEmpleado();
                    Soporte.MsgInformacion("El Registro se ha Guardado Correctamenet");
                    MostrarRegistros();
                    radPageView1.SelectedPage = RadpgvListaEmppleado;
                    
                }
                else
                {
                    ActualizarEmpleados();
                    Soporte.MsgInformacion("Los Datos se han Actualizado correctamente");
                    MostrarRegistros();
                    Soporte.AlertaNotifiacion(RadAlerta, "Actualizado", "El registro se ha Actualizado correctamente", Properties.Resources.guardar);
                    radPageView1.SelectedPage = RadpgvListaEmppleado;
                }

                RestablecerControles();
                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Boton Nuevo        
        private void BtnNuevo_Click(object sender, EventArgs e)
        {
           
            EsNuevo = true;
            EsEditar = false;
            HabilitarBotones();
            HabilitarCajasText(true);
            LimpiaCajasTextoBox();
            TxtNombres.Focus();
            TxtId_Usuario.Visible = false;
            Lbl_IdUsuario.Visible = false;
            //TxtNomina.Enabled = true;
            BtnActulizar.Text = "Actualizar";
        }

        #endregion

        #region Buton Actualizar
        
        private void BtnActulizar_Click(object sender, EventArgs e)
        {
            if (!TxtId_Usuario.Text.Equals(""))
            {
                EsEditar = true;
                HabilitarBotones();
                HabilitarCajasText(true);
            }
            else
            {
                Soporte.MsgInformacion("Seleccione un regitro de la lista de Usuarios para poder editarlo", "Seleccionar Registro");
            }
        }

        #endregion

        #region Boton Cancelar
        
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            RestablecerControles();
           
        }

        #endregion

        #region Boton Eliminar

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult Opcion;
            Opcion = MessageBox.Show("Ha selecionado Eliminar el Registro, desea Continuar?", "Eliminando Registro", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (Opcion == DialogResult.OK)
            {
                Nomina = Convert.ToInt32(RadGVEmpleados.CurrentRow.Cells["Nomina"].Value);
                if (ClsGuardar.EliminarEmpleado(Nomina))
                {
                    Soporte.MsgInformacion("El Usuario se ha eliminado correctamente");
                    MostrarRegistros();
                }
                else
                {
                    Soporte.MsgError("No se Puede Eliminar un Administrador, verifique el Registro.", "Registro no Eliminado");
                }

            }
        }

        #endregion

        #region Boton Cerrar Formulario

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Boton Cargar Imagen

        private void BtnAbrirImagen_Click(object sender, EventArgs e)
        {
            Soporte.ImagenCarga(PtbxFotoPerfil);
        }

        #endregion

        #region Boton Borrar Imagen
       
        private void BtnBorrarImagen_Click(object sender, EventArgs e)
        {
            
                PtbxFotoPerfil.SizeMode = PictureBoxSizeMode.Zoom;
                PtbxFotoPerfil.Image = Properties.Resources.LgoEsferaBlancTrasp;
            
            
        }

        #endregion

        #endregion






    }
}
