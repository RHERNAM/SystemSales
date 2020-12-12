using AccesDLL;
using SistemaVentas.Clases.SQL;
using SistemaVentas.Clases.SQL.Transacciones;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVentas.Formularios.Configuraciones
{
    public partial class FormSucursales : Form
    {
        private DataTable DtRegistros;
        private DataSet DsTablas;       
        private string Resultado;
        private bool Nuevo, Editar;
        string Nombre, Telefono, Correo, Calle, Numero_Interno, Numero_Externo, Entre_Calles, Direccion;
        int IdEmpresa, IdColonia, CodigoPostal, IdMunicipio, IdEstado, IdPais, IdSucursal;


                                    
        public FormSucursales()
        {
            InitializeComponent();
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            //BuscarCodigoPostal(Convert.ToInt32( TxtCodigoPostal.Text));

            DsTablas = Consulta.ColoniaTablas(Convert.ToInt32(TxtCodigoPostal.Text));
            DtRegistros = DsTablas.Tables["Colonias"];

            
            
        }

        private void BuscarCodigoPostal(int CodigoPostal)
        {            
            DtRegistros = Consulta.Colonias(CodigoPostal);
            if (DtRegistros.Rows.Count>0)
            {
                IdPais = Convert.ToInt32(DtRegistros.Rows[0]["IdPais"].ToString());
                IdMunicipio = Convert.ToInt32(DtRegistros.Rows[0]["IdMunicipio"].ToString());
                IdEstado = Convert.ToInt32(DtRegistros.Rows[0]["IdEstado"].ToString());
            }           

        }
               

        private void TxtCodigoPostal_KeyPress(object sender, KeyPressEventArgs e)
        {
            Soporte.ValidarNumeros(TxtCodigoPostal, e);
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
          
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void radGridView1_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (ChkEditar.Checked==true)
            {
                ObtenerDatosDataGriviev();
            }
            
        }

        private void ObtenerDatosDataGriviev()
        {
            IdSucursal = Convert.ToInt32(radGridView1.CurrentRow.Cells["Id_Negocio"].Value);
            Nombre = radGridView1.CurrentRow.Cells["Nombre"].Value.ToString();
            TxtNombre.Text = radGridView1.CurrentRow.Cells["Nombre"].Value.ToString();
            TxtTelefono.Text = radGridView1.CurrentRow.Cells["Telefono"].Value.ToString();
            TxtCorreo.Text = radGridView1.CurrentRow.Cells["Correo"].Value.ToString();
            CmbEmpresa.SelectedValue = Convert.ToInt32(radGridView1.CurrentRow.Cells["IdEmpresa"].Value);
            TxtCalle.Text = radGridView1.CurrentRow.Cells["Calle"].Value.ToString();
            TxtNumInterno.Text = radGridView1.CurrentRow.Cells["Numero_Interno"].Value.ToString();
            TxtNumExterno.Text = radGridView1.CurrentRow.Cells["Numero_Externo"].Value.ToString();
            TxtEntreCalles.Text = radGridView1.CurrentRow.Cells["Entre_Calles"].Value.ToString();     
            TxtCodigoPostal.Text = radGridView1.CurrentRow.Cells["CodigoPostal"].Value.ToString(); 
            CmbxPais.SelectedValue = Convert.ToInt32(radGridView1.CurrentRow.Cells["IdPais"].Value);
            CmbxEstado.SelectedValue = Convert.ToInt32(radGridView1.CurrentRow.Cells["IdEstado"].Value);
            CmbxMunicipio.SelectedValue = Convert.ToInt32(radGridView1.CurrentRow.Cells["IdMunicipio"].Value);
            CmbColonias.SelectedValue = Convert.ToInt32(radGridView1.CurrentRow.Cells["IdColonia"].Value);
        }

        private void ChkEditar_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkEditar.Checked==true)
            {
                BtnActulizar.Enabled = true;
                TxtNombre.Enabled = false;
                BtnGuardar.Enabled = false;
            }
            else
            {
                BtnActulizar.Enabled = false;
                BtnGuardar.Enabled = true;
                TxtNombre.Enabled = true;
                Limpiar();
            }
        }

        private void BtnActulizar_Click(object sender, EventArgs e)
        {
            Editar = true;
            if (ValidarCampos())
            {
                AsignarValores();
                ActulizarSucursal();
                RegistrosSucursales();
                Editar = false;
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormSucursales_Load(object sender, EventArgs e)
        {
            ListaCombos();
            RegistrosSucursales();

        }

       

        private void ListComboColonias()
        {
            CmbColonias.DataSource = Consulta.Colonias();
            CmbColonias.ValueMember = "IdColonia";
            CmbColonias.DisplayMember = "Colonia";

        }

       

        private void RegistrosSucursales()
        {
            radGridView1.DataSource = Consulta.Sucursales();
            radGridView1.BestFitColumns();
        }

       



        #region Carga de Lista Combos      

        private void ListaCombos()
        {
          
            CmbxPais.DisplayMember = "Nombre";
            CmbxPais.ValueMember = "IdPais";
            CmbxPais.DataSource = Consulta.PaisListCombo();

            
            CmbEmpresa.DisplayMember = "Nombre";
            CmbEmpresa.ValueMember = "Id_Empresa";
            CmbEmpresa.DataSource = Consulta.EmpresaCombobox();
        }

             

        private void CmbxPais_SelectedValueChanged(object sender, EventArgs e)
        {
            if (CmbxPais.SelectedValue != null)
            {

                int pais =Convert.ToInt32( CmbxPais.SelectedValue);
                DtRegistros = Consulta.Estados(pais);
                if (DtRegistros.Rows.Count > 0)
                {

                    if (pais == 0)
                    {
                        CmbxEstado.Enabled = false;
                        if (CmbxPais.DataSource!=null)
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

                CmbxMunicipio.SelectedValue=0;
                CmbxMunicipio.Enabled = false;
                CmbColonias.SelectedValue=0;
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
            if (DtRegistros.Rows.Count>0)
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

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            
            if (ValidarCampos())
            {
                AsignarValores();
                GuardarSucursal();
                RegistrosSucursales();
            }
            
            
        }



        private void GuardarSucursal()
        {
              Resultado = ClsGuardar.Sucursal(Nombre, Telefono, Correo, IdEmpresa, Calle, Numero_Interno, Numero_Externo,
              Entre_Calles, IdColonia, CodigoPostal, IdMunicipio, IdEstado, IdPais, Direccion);

            if (Resultado=="Insertado")
            {
                Limpiar();
                Soporte.MsgInformacion("El Registro de la Sucursal se ha guardado correctamente.");
               
            }
            else
            {
                Soporte.MsgError(Resultado);
            }
        }
        private void ActulizarSucursal()
        {
            Resultado=ClsEditar.Sucursal(IdSucursal, Nombre, Telefono, Correo, IdEmpresa, Calle, Numero_Interno, Numero_Externo,
              Entre_Calles, IdColonia, CodigoPostal, IdMunicipio, IdEstado, IdPais, Direccion);
            if (Resultado=="Actualizado")
            {
                Limpiar();
                Soporte.MsgInformacion("La Sucursal se actualizado correctamente.");
            }
            else
            {
                Soporte.MsgError(Resultado);
            }
        }

        private void Limpiar()
        {
            try
            {
                ChkEditar.Checked = false;
                TxtNombre.Clear();
                TxtNombre.Enabled = true;
                TxtTelefono.Clear();
                TxtCorreo.Clear();
                CmbEmpresa.SelectedIndex = 0;
                TxtCalle.Clear();
                TxtNumInterno.Clear();
                TxtNumExterno.Clear();
                TxtEntreCalles.Clear();
                CmbColonias.SelectedIndex = 0;
                TxtCodigoPostal.Text = "0";
                CmbxMunicipio.SelectedIndex = 0;
                CmbxEstado.SelectedIndex = 0;
                CmbxPais.SelectedIndex = 0;
                Direccion = "";

                IdSucursal = 0;
                Nombre = "";
                Telefono = "";
                Correo = "";
                IdEmpresa = 0;
                Calle = "";
                Numero_Interno = "";
                Numero_Externo = "";
                Entre_Calles = "";
                IdColonia = 0;
                CodigoPostal = 0;
                IdMunicipio = 0;
                IdEstado = 0;
                IdPais = 0;
            }
            catch (Exception)
            {

                return;
            }
            
           
        }

        private void AsignarValores()
        {
            Nombre = TxtNombre.Text;
            Telefono = TxtTelefono.Text;
            Correo = TxtCorreo.Text;
            IdEmpresa =Convert.ToInt32( CmbEmpresa.SelectedValue);
            Calle = TxtCalle.Text;
            Numero_Interno = TxtNumInterno.Text;
            Numero_Externo = TxtNumExterno.Text;
            Entre_Calles = TxtEntreCalles.Text;
            IdColonia = Convert.ToInt32(CmbColonias.SelectedValue);
            CodigoPostal =Convert.ToInt32(TxtCodigoPostal.Text);
            IdMunicipio = Convert.ToInt32(CmbxMunicipio.SelectedValue);
            IdEstado = Convert.ToInt32(CmbxEstado.SelectedValue);
            IdPais = Convert.ToInt32(CmbxPais.SelectedValue);
            Direccion = Calle + " Núm. Ext. "+  Numero_Externo + " Núm. Int. " + Numero_Interno + ", " + Entre_Calles +
                ", Col. " + CmbColonias.Text + ", " + CodigoPostal + ", " + CmbxMunicipio.Text + ", " + CmbxEstado.Text + " " + CmbxPais.Text;
        }


        private bool ValidarCampos()
        {
            if (Editar)
            {
                if (IdSucursal==0)
                {
                    Soporte.MsgError("Seleccione un registro al que desea Actualizar.");
                    TxtNombre.Focus();
                    return false;
                }
            }           
            

            if (string.IsNullOrEmpty(TxtNombre.Text) || TxtNombre.Text=="0")
            {
                Soporte.MsgError("Ingrese Nombre de la Sucursal");
                TxtNombre.Focus();
                return false;

            }
            if (string.IsNullOrEmpty(TxtTelefono.Text) || TxtTelefono.Text == "0")
            {
                Telefono = "Registro Pendiente";

            }

            if (string.IsNullOrEmpty(TxtCorreo.Text) || TxtCorreo.Text == "0")
            {
                Correo = "sucursal@correo.com";

            }
            else
            {
                if (Soporte.ValidarMail(TxtCorreo.Text)==false)
                {
                    Soporte.MsgError("El correo no cuenta con el Formato correcto");
                    TxtCorreo.Focus();
                    TxtCorreo.SelectAll();
                    return false;
                }
            }

            if (CmbEmpresa.SelectedIndex==0)
            {
                Soporte.MsgError("Seleccione una empresa al que pertenecera la Sucursal");
                CmbEmpresa.Focus();
                CmbEmpresa.SelectAll();
                return false;

            }
            if (CmbxPais.SelectedIndex == 0)
            {
                Soporte.MsgError("Seleccione un Pais");
                CmbxPais.Focus();
                CmbxPais.SelectAll();
                return false;

            }
            if (CmbxEstado.SelectedIndex == 0)
            {
                Soporte.MsgError("Seleccione un Estado");
                CmbxEstado.Focus();
                CmbxEstado.SelectAll();
                return false;

            }
            if (CmbxMunicipio.SelectedIndex == 0)
            {
                Soporte.MsgError("Seleccione un Municipio");
                CmbxMunicipio.Focus();
                CmbxMunicipio.SelectAll();
                return false;

            }

            if (CmbColonias.SelectedIndex == 0)
            {
                Soporte.MsgError("Seleccione una Colonia");
                CmbColonias.Focus();
                CmbColonias.SelectAll();
                return false;

            }
            if (TxtCodigoPostal.TextLength<4)
            {
                Soporte.MsgError("Ingrse un Codigo Postal Valido.");
                TxtCodigoPostal.Focus();
                return false;

            }
            if (TxtCalle.TextLength < 5)
            {
                Soporte.MsgError("Ingrse una calle valido");
                TxtCalle.Focus();
                
                return false;

            }
            if (string.IsNullOrEmpty(TxtEntreCalles.Text) || TxtEntreCalles.Text == "0")
            {
                Entre_Calles = "Sin Registro";               

            }
            if (string.IsNullOrEmpty(TxtNumExterno.Text) || TxtNumExterno.Text == "0")
            {
                Numero_Externo= "SIN NUMERO";

            }

            if (string.IsNullOrEmpty(TxtNumInterno.Text) || TxtNumInterno.Text == "0")
            {
                Numero_Interno = "SIN NUMERO";

            }

            return true;
        }
    }
}
