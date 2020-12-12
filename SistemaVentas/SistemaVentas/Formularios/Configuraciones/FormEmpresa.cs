using AccesDLL;
using SistemaVentas.Clases.Entidates;
using SistemaVentas.Clases.SQL;
using SistemaVentas.Clases.SQL.Transacciones;
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

namespace SistemaVentas.Formularios.Configuraciones
{
    public partial class FormEmpresa : Form
    {
        private string NombreEmpresa, RazonSocial, Direccion, 
        Trabaja_Impuesto, Modo_Busqueda, Carpeta_Copia_Seguridad, Correo_EnvioReportes, Ultima_Fecha_Copia_Seguridad, Tipo_Empresa,  Redondeo_Total;
        private DataTable DtRegistros;
        int Id_Impuesto, Id_Moneda, Id_Estatus, Id_Pais, IdEmpresa;
        private decimal Porcentaje_Impuesto;
        private bool EsNuevo, EsEditar;



        DateTime Ultima_Fecha_Copia_Date;
        private int Frecuencia_Copias;
        private byte[] Logo;
        private string Calle;
        private string Numero_Interno;
        private string Numero_Externo;
        private string Entre_Calles;
        private int Id_Colonia;
        private int Codigo_Postal;
        private int Id_Municipio;
        private int Id_Estado;

        public FormEmpresa()
        {
            InitializeComponent();
        }


        private void FormEmpresa_Load(object sender, EventArgs e)
        {
            ListaCombos();
            ConsultaEmpresas();
        }

        #region Consultas

      
        private void ListaCombos()
        {
            CmbImpuesto.DataSource = Consulta.ImpuestoList();
            CmbImpuesto.DisplayMember = "Impuesto";
            CmbImpuesto.ValueMember = "Id_Impuesto";

            CmbMoneda.DataSource = Consulta.MonedaList();
            CmbMoneda.DisplayMember = "Moneda";
            CmbMoneda.ValueMember = "Id_Moneda";



            CmbPais.DisplayMember = "Nombre";
            CmbPais.ValueMember = "IdPais";
            DtRegistros = Consulta.PaisListCombo();
            CmbPais.DataSource = DtRegistros;

        }

        private void ConsultaEmpresas()
        {
            DtRegistros = Consulta.EmpresaRegistros();
            RadgvListaEmpresas.DataSource = DtRegistros;            
            RadgvListaEmpresas.BestFitColumns();
            Ocultar_Collumnas();
        }

        #endregion

        #region Ocultar Columnas

        private void Ocultar_Collumnas()
        {
            RadgvListaEmpresas.Columns["Id_Impuesto"].IsVisible = false;
            RadgvListaEmpresas.Columns["Id_Moneda"].IsVisible = false;
            RadgvListaEmpresas.Columns["Logo"].IsVisible = false;
            RadgvListaEmpresas.Columns["Calle"].IsVisible = false;
            RadgvListaEmpresas.Columns["Numero_Interno"].IsVisible = false;
            RadgvListaEmpresas.Columns["Numero_Externo"].IsVisible = false;
            RadgvListaEmpresas.Columns["Entre_Calles"].IsVisible = false;
            RadgvListaEmpresas.Columns["Codigo_Postal"].IsVisible = false;
            RadgvListaEmpresas.Columns["Id_Colonia"].IsVisible = false;
            RadgvListaEmpresas.Columns["Id_Estatus"].IsVisible = false;
            RadgvListaEmpresas.Columns["Id_Municipio"].IsVisible = false;
            RadgvListaEmpresas.Columns["Id_Estado"].IsVisible = false;
            RadgvListaEmpresas.Columns["Id_Pais"].IsVisible = false;

           
        }
        #endregion

        #region Validar Datos
        private bool ValidarDatos()
        {
            if (EsEditar)
            {
                if (IdEmpresa==0)
                {
                    return false;
                }
            }
           

            if (Soporte.ValidarMail(TxtCorreoEmpresa.Text)==false)
            {
                return false;
            }

            return true;
        }
        #endregion

        #region Vaciar Registros

        private void LimpiarRegistros()
        {
            NombreEmpresa = null;
            TxtNombreEmpresa.Clear();
            RazonSocial = null;
            TxtRazonSocial.Clear();
            Direccion = null;
            TxtDireccion.Clear();
            Id_Impuesto = 0;
            CmbImpuesto.SelectedValue=0;
            Id_Moneda = 0;
            CmbMoneda.SelectedValue=0;
            RdbSI.Checked = false; 
            RdbNO.Checked = false;            
            Trabaja_Impuesto = null;
            ChkCodigoBarras.Checked = false;
            Modo_Busqueda =null;   
            ChkTeclado.Checked = false;
            Carpeta_Copia_Seguridad = null;
            TxtRutaCopiaSeguridad.Clear();
            Correo_EnvioReportes = null;
            TxtCorreoEmpresa.Clear();
            Ultima_Fecha_Copia_Seguridad = null;
            Ultima_Fecha_Copia_Date = DateTime.Now;
            Frecuencia_Copias = 0;
            Id_Estatus = 0;
            Tipo_Empresa = null;
            Redondeo_Total =null;
          
            PctbxLogo.Image = Properties.Resources.LgoEsferaBlancTrasp;

            Calle = null;
            TxtCalle.Clear();
            Numero_Interno = null;
            TxtNumInterno.Clear();
            Numero_Externo = null;                
            TxtNumExterno.Clear();
            Entre_Calles = null;
            TxtEntreCalles.Clear();
            Id_Colonia = 0;
            //CmbColonias.SelectedValue=0;
            Codigo_Postal = 0;
            TxtCodigoPostal.Clear();
            Id_Municipio = 0;
            //CmbxMunicipio.SelectedValue=0;
            Id_Estado = 0;
            //Convert.ToInt32(CmbxEstado.SelectedValue);
            Id_Pais =0;
            CmbPais.SelectedValue=0;
            Direccion = null;
        }
        #endregion

        private void BtnEnviar_Click(object sender, EventArgs e)
        {
            ObtenerDatos_Enviar();
        }

        #region Obtener Datos de RGV Y enviar a TXT

        private void ObtenerDatos_Enviar()
        {
            IdEmpresa= Convert.ToInt32(RadgvListaEmpresas.CurrentRow.Cells["Id_Empresa"].Value);
            TxtNombreEmpresa.Text = RadgvListaEmpresas.CurrentRow.Cells["Empresa"].Value.ToString();
            TxtRazonSocial.Text = RadgvListaEmpresas.CurrentRow.Cells["Razon_Social"].Value.ToString();
            CmbMoneda.SelectedValue=Convert.ToInt32( RadgvListaEmpresas.CurrentRow.Cells["Id_Moneda"].Value);
            CmbPais.SelectedValue = Convert.ToInt32(RadgvListaEmpresas.CurrentRow.Cells["Id_Pais"].Value);
            CmbxEstado.SelectedValue = Convert.ToInt32(RadgvListaEmpresas.CurrentRow.Cells["Id_Estado"].Value);
            CmbxMunicipio.SelectedValue = Convert.ToInt32(RadgvListaEmpresas.CurrentRow.Cells["Id_Municipio"].Value);
            CmbColonias.SelectedValue = Convert.ToInt32(RadgvListaEmpresas.CurrentRow.Cells["Id_Colonia"].Value);
            TxtCalle.Text = RadgvListaEmpresas.CurrentRow.Cells["Calle"].Value.ToString();
            TxtEntreCalles.Text = RadgvListaEmpresas.CurrentRow.Cells["Entre_Calles"].Value.ToString();
            TxtNumInterno.Text = RadgvListaEmpresas.CurrentRow.Cells["Numero_Interno"].Value.ToString();
            TxtNumExterno.Text = RadgvListaEmpresas.CurrentRow.Cells["Numero_Externo"].Value.ToString();

            CmbImpuesto.SelectedValue = Convert.ToInt32(RadgvListaEmpresas.CurrentRow.Cells["Id_Impuesto"].Value);
            TxtCorreoEmpresa.Text = RadgvListaEmpresas.CurrentRow.Cells["Correo_EnvioReportes"].Value.ToString();
            TxtRutaCopiaSeguridad.Text = RadgvListaEmpresas.CurrentRow.Cells["Carpeta_Copia_Seguridad"].Value.ToString();
            Trabaja_Impuesto = RadgvListaEmpresas.CurrentRow.Cells["Trabaja_Impuesto"].Value.ToString();
            if (Trabaja_Impuesto=="SI")
            {
                RdbSI.Checked = true;
            }
            if (Trabaja_Impuesto=="NO")
            {
                RdbNO.Checked = true;
            }

            Modo_Busqueda = RadgvListaEmpresas.CurrentRow.Cells["Modo_Busqueda"].Value.ToString();

            if (Modo_Busqueda== "Lector")
            {
                ChkCodigoBarras.Checked = true;
            }
            if (Modo_Busqueda== "Teclado")
            {
                ChkTeclado.Checked = true;
            }
            

            byte[] imagenBufers = (byte[])RadgvListaEmpresas.CurrentRow.Cells["Logo"].Value;
            MemoryStream ms = new MemoryStream(imagenBufers);
            this.PctbxLogo.Image = Image.FromStream(ms);
            this.PctbxLogo.SizeMode = PictureBoxSizeMode.Zoom;

            radPageView1.SelectedPage = RadPgvNuevoRegistro;
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarRegistros();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }



        #endregion

        #region GUARDAR

        private void GuardarEmpresa()
        {
            ClsGuardar.EmpresaGuardar( NombreEmpresa,  RazonSocial,  Id_Impuesto,  Porcentaje_Impuesto,
             Id_Moneda,  Trabaja_Impuesto,  Modo_Busqueda,  Carpeta_Copia_Seguridad,  Correo_EnvioReportes,  Ultima_Fecha_Copia_Seguridad,
             Ultima_Fecha_Copia_Date,  Frecuencia_Copias,  Id_Estatus,  Tipo_Empresa,  Redondeo_Total, Logo,  Calle,  Numero_Interno,
             Numero_Externo,  Entre_Calles,  Id_Colonia,  Codigo_Postal,  Id_Municipio,  Id_Estado,  Id_Pais,  Direccion);
        }
        #endregion

        #region Actualizar Empresa

        private void ActualizarEmpresa()
        {
            ClsEditar.EmpresaActualizar(IdEmpresa, NombreEmpresa, RazonSocial, Id_Impuesto, Porcentaje_Impuesto,
             Id_Moneda, Trabaja_Impuesto, Modo_Busqueda, Carpeta_Copia_Seguridad, Correo_EnvioReportes, Ultima_Fecha_Copia_Seguridad,
             Ultima_Fecha_Copia_Date, Frecuencia_Copias, Id_Estatus, Tipo_Empresa, Redondeo_Total, Logo, Calle, Numero_Interno,
             Numero_Externo, Entre_Calles, Id_Colonia, Codigo_Postal, Id_Municipio, Id_Estado, Id_Pais, Direccion);
        }
        #endregion

        #region Guarda Caja

        private void InsertarCaja()
        {
            ClsGuardar.CajaGuardar(TxtCajaNombre.Text,"Redentor", MemoriaCache.SerialPC,"Ninguno","Ninguno", UsuarioCache.Id_Usuario, MemoriaCache.NombrePC, MemoriaCache.Ip,"Principal");
        }

        #endregion

        #region ASIGNAR VALORES

        private void AsignarValores()
        {
            NombreEmpresa = TxtNombreEmpresa.Text;
            RazonSocial = TxtRazonSocial.Text;
            Direccion = TxtDireccion.Text;
            Id_Impuesto =Convert.ToInt32(CmbImpuesto.SelectedValue);
            Porcentaje_Impuesto = NdPorcentaje.Value;
            Id_Moneda =Convert.ToInt32( CmbMoneda.SelectedValue);

            if (RdbSI.Checked==true)
            {
                Trabaja_Impuesto = "SI";
            }
            if (RdbNO.Checked==true)
            {
                Trabaja_Impuesto = "NO";
            }

            if (ChkCodigoBarras.Checked==true)
            {
                Modo_Busqueda = "Lector";
            }

            if (ChkTeclado.Checked==true)
            {
                Modo_Busqueda = "Teclado";
            }
          
            Carpeta_Copia_Seguridad = TxtRutaCopiaSeguridad.Text;
            Correo_EnvioReportes = TxtCorreoEmpresa.Text;
            Ultima_Fecha_Copia_Seguridad = "Ninguna";
            Ultima_Fecha_Copia_Date = DateTime.Now;
            Frecuencia_Copias = 1;
            Id_Estatus = 1;
            Tipo_Empresa = "General";            
            Redondeo_Total = "NO";  
            MemoryStream memoria = new MemoryStream();
            PctbxLogo.Image.Save(memoria,PctbxLogo.Image.RawFormat /*System.Drawing.Imaging.ImageFormat.Png*/);
            Logo = memoria.GetBuffer();
            Calle = TxtCalle.Text;
            Numero_Interno = TxtNumInterno.Text;
            Numero_Externo = TxtNumExterno.Text;
            Entre_Calles = TxtEntreCalles.Text;
            Id_Colonia = Convert.ToInt32(CmbColonias.SelectedValue);
            Codigo_Postal =Convert.ToInt32(TxtCodigoPostal.Text);
            Id_Municipio = Convert.ToInt32(CmbxMunicipio.SelectedValue);
            Id_Estado = Convert.ToInt32(CmbxEstado.SelectedValue);
            Id_Pais = Convert.ToInt32(CmbPais.SelectedValue);
            Direccion = Calle + " Núm. Ext. " + Numero_Externo + " Núm. Int. " + Numero_Interno + ", " + Entre_Calles +
                 ", Col. " + CmbColonias.Text + ", " + Codigo_Postal + ", " + CmbxMunicipio.Text + ", " + CmbxEstado.Text + " " + CmbPais.Text;

        }
        #endregion

        #region Botones

       
        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            EsNuevo = true;

            if (ValidarDatos())
            {
                AsignarValores();
                GuardarEmpresa();
                LimpiarRegistros();
                ConsultaEmpresas();
                // InsertarCaja();
            }

        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            EsEditar = true;
            if (ValidarDatos())
            {
                AsignarValores();
                ActualizarEmpresa();
                LimpiarRegistros();
                ConsultaEmpresas();
            }
        }

        private void BtnSeleccionarRuta_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                TxtRutaCopiaSeguridad.Text = folderBrowserDialog1.SelectedPath;
                string ruta = TxtRutaCopiaSeguridad.Text;

                if (ruta.Contains(@"C:\"))
                {
                    Soporte.MsgError("Seleccione un Disco Diferente al Disco 'C'.", "Ruta Invalido");
                    TxtRutaCopiaSeguridad.Clear();
                }
                else
                {
                    TxtRutaCopiaSeguridad.Text = folderBrowserDialog1.SelectedPath;
                }
            }
        }

        private void BtnSeleccionarImagen_Click(object sender, EventArgs e)
        {
            Soporte.ImagenCarga(PctbxLogo);
        }

        #endregion

        #region Combos Direccion

        private void CmbPais_SelectedValueChanged(object sender, EventArgs e)
        {
            if (CmbPais.SelectedValue != null)
            {

                int pais = Convert.ToInt32(CmbPais.SelectedValue);
                DtRegistros = Consulta.Estados(pais);
                if (DtRegistros.Rows.Count > 0)
                {

                    if (pais == 0)
                    {
                        CmbxEstado.Enabled = false;
                        if (CmbPais.DataSource != null)
                        {
                            CmbxEstado.SelectedValue = 0;
                        }

                    }
                    else
                    {
                        CmbxEstado.Enabled = true;

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
                        CmbxMunicipio.Enabled = true;

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
                        CmbColonias.Enabled = true;

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

        #endregion      

        #region Checked Selecion Modo Busqueda
       
        private void ChkTeclado_CheckedChanged(object sender, EventArgs e)
        {

            if (ChkTeclado.Checked == true)
            {

                ChkCodigoBarras.Checked = false;
            }
            if (ChkTeclado.Checked == false)
            {
                ChkCodigoBarras.Checked = true;

            }

        }

        private void ChkCodigoBarras_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkCodigoBarras.Checked == true)
            {
                ChkTeclado.Checked = false;
            }
            if (ChkCodigoBarras.Checked == false)
            {
                ChkTeclado.Checked = true;
            }
        }

        #endregion
    }
}
