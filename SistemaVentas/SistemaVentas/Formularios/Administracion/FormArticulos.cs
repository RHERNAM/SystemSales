using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaVentas.Clases.SQL.Transacciones;
using SistemaVentas.Clases.Entidates;
using SistemaVentas.Formularios.Sistemas.FormSoport;
using SistemaVentas.Clases.SQL;
using AccesDLL;
using System.IO;
using Telerik.WinControls.UI;
using SistemaVentas.Formularios.Ventas;
using SistemaVentas.Formularios.Reportes;

namespace SistemaVentas.Formularios.Administracion
{
    public partial class FormArticulos : Form
    {
        private DataTable DtRegistros;
        private bool esNuevo=false, esEditar= false;
        private string Codigo, Nombre, Marca,  Descripcion;
        int ID_Presentacion_Art, ID_Unidad_Medida, Id_Fabricante, Unidad_Venta, Id_Estatus, IdAgranel, IdArticulo, IdEmpresa, IdSucursal;
        private decimal Contenido_Neto, Cantidad_Minimo, Cantidad_Maximo;
        byte[] Foto;



        public FormArticulos()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            GuardaLog("Se Presiono Buton GUardar");
        }

        private void GuardaLog(string AccionEjecutado)
        {
            Ayuda.GuardaDetalleLog("Formulario Articulos",AccionEjecutado);
        }
        private void FormArticulos_Load(object sender, EventArgs e)
        {
            MostrarArticulos();
            ListaCombos();
            HabilitarBotones();
            HabilitarCajas(false);

            TxtNombre.AutoCompleteCustomSource = Consulta.CargarAutoComplete();
            TxtNombre.AutoCompleteMode = AutoCompleteMode.Suggest;
            TxtNombre.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }



        private void Pruab()
        {
            DataSet dataSet = Consulta.Articulo_DataSet();
            CrystalReport1 crystalReport1 = new CrystalReport1();
            crystalReport1.SetDataSource(dataSet);

            FormReporteVenta formReporteVenta = new FormReporteVenta();
            formReporteVenta.VistaPrevia(crystalReport1);
            formReporteVenta.ShowDialog();
        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            Pruab();
            
        }

        #region Consultas       

        #region Mostrar Articulos

        private void MostrarArticulos()
        {
            DtRegistros = Consulta.ArticuloConsulta();
            RadGVarticulos.DataSource = null;
            RadGVarticulos.DataSource = DtRegistros;
            RadGVarticulos.BestFitColumns();
            Soporte.ContarRegistros(RadGVarticulos, LblTotal, "Total Registros: ");
            OcultarColumnas();
        }

        #endregion

        #region Lista Presentacion Articulo
        private void ListaCombos()
        {

            CmbPrestArt.DataSource = Consulta.PresentacionArticulo();
            CmbPrestArt.DisplayMember = "Descripcion";
            CmbPrestArt.ValueMember = "Id_Presentacion_Articulo";

            CmbUnidMed.DataSource = Consulta.Unidad_MedidaArt();
            CmbUnidMed.DisplayMember = "Descripcion";
            CmbUnidMed.ValueMember = "Id_Unidad_Medida";

            CmbUnidadVenta.DataSource = Consulta.Unidad_MedidaArt();
            CmbUnidadVenta.DisplayMember = "Descripcion";
            CmbUnidadVenta.ValueMember = "Id_Unidad_Medida";

            CmbEstatus.DataSource = Consulta.EstatusEmple();
            CmbEstatus.DisplayMember = "Nombre";
            CmbEstatus.ValueMember = "Id_Estatus";

            CmbFamiliaAgranel.DataSource = Consulta.AgranelClasif();
            CmbFamiliaAgranel.DisplayMember = "Nombre";
            CmbFamiliaAgranel.ValueMember = "IdAgranel";

            CmbFabricante.DataSource = Consulta.FabricanteConsulta();
            CmbFabricante.DisplayMember = "Nombre";
            CmbFabricante.ValueMember = "Id_Fabricante";

            CmbEmpresa.DisplayMember = "Nombre";
            CmbEmpresa.ValueMember = "Id_Empresa";
            CmbEmpresa.DataSource = Consulta.EmpresaCombobox();

        }
        #endregion

        private void ListaSucursal(int IdEmpresa)
        {
            CmbSucursal.DataSource = Consulta.SucursalListCombo(IdEmpresa);
            CmbSucursal.DisplayMember = "Nombre";
            CmbSucursal.ValueMember = "Id_Negocio";
        }

     

        #endregion

        #region Metodos

        #region Limpiar Rregistros
        private void LimpiarCajas()
        {
            CmbUnidadVenta.SelectedIndex = 0;
            CmbFabricante.SelectedIndex = 0;
            TxtCodigo.Clear();
            TxtContenido.Text = "0";
            TxtDescripcion.Clear();
            CmbUnidMed.SelectedIndex = 0;
            CmbPrestArt.SelectedIndex = 0;
            TxtMarca.Clear();
            TxtNombre.Clear();
            TxtRazon.Clear();
            TxtIdArticulo.Clear();
            TxtRFC.Clear();
            TxtTelefono.Clear();
            TxtDireccion.Clear();
            TxtCorreo.Clear();
            TxtCantidadMaximo.Text="0";
            TxtCantidadMinimo.Text="0";
            CmbUnidadVenta.SelectedIndex = 0;
            CmbEstatus.SelectedIndex = 0;
            CmbEmpresa.SelectedIndex = 0;
            CmbSucursal.DataSource = null;

        }
        #endregion

        #region Habilitar Cajas
        private void HabilitarCajas(bool valor)
        {
            TxtCodigo.ReadOnly = !valor;
            TxtContenido.ReadOnly = !valor;
            TxtDescripcion.ReadOnly = !valor;
            CmbUnidMed.Enabled = valor;
            CmbPrestArt.Enabled = valor;
            TxtMarca.ReadOnly = !valor;
            TxtNombre.ReadOnly = !valor;
            TxtRazon.ReadOnly = !valor;
            TxtCantidadMaximo.ReadOnly = !valor;
            TxtCantidadMinimo.ReadOnly = !valor;
            CmbFabricante.Enabled = valor;
            CmbUnidadVenta.Enabled = valor;
            CmbFamiliaAgranel.Enabled = valor;
            CmbEmpresa.Enabled = valor;
            CmbSucursal.Enabled = valor;
            CmbEstatus.Enabled = valor;
            TxtNumFabricante.ReadOnly = !valor;
        }
        #endregion

        #region Ocultar Columnas

        private void OcultarColumnas()
        {
            RadGVarticulos.Columns["Foto"].IsVisible = false;
        }

        #endregion

        #region Habilitar Botones

        private void HabilitarBotones()
        {
            if (esNuevo || esEditar)
            {
                HabilitarCajas(true);
                BtnNuevo.Visible = false;
                BtnGuardar.Visible = true;
                BtnActulizar.Visible = false;
                BtnCancelar.Visible = true;
                ChkbAgranel.Visible = true;

            }
            else
            {
                HabilitarCajas(false);
                BtnNuevo.Visible = true;
                BtnGuardar.Visible = false;
                BtnActulizar.Visible = true;
                BtnCancelar.Visible = false;
                ChkbAgranel.Visible = false;
            }
        }

        #endregion

        #region Obtener Datos y Pasar RadGV

        private void PasarDatosRadGV()
        {
            TxtIdArticulo.Text = RadGVarticulos.CurrentRow.Cells["Id_Articulo"].Value.ToString();
            TxtCodigo.Text = RadGVarticulos.CurrentRow.Cells["Codigo"].Value.ToString();
            TxtNombre.Text = RadGVarticulos.CurrentRow.Cells["Nombre"].Value.ToString();
            TxtMarca.Text = RadGVarticulos.CurrentRow.Cells["Marca"].Value.ToString();
            TxtDescripcion.Text = RadGVarticulos.CurrentRow.Cells["Descripcion"].Value.ToString();
            CmbPrestArt.Text = RadGVarticulos.CurrentRow.Cells["Presentacion"].Value.ToString();
            CmbUnidMed.Text = RadGVarticulos.CurrentRow.Cells["Unidad_Medida"].Value.ToString();
            CmbFabricante.Text = RadGVarticulos.CurrentRow.Cells["Fabricante"].Value.ToString();
            TxtContenido.Text = RadGVarticulos.CurrentRow.Cells["Contenido_Neto"].Value.ToString();
            TxtCantidadMinimo.Text = RadGVarticulos.CurrentRow.Cells["Cantidad_Minimo"].Value.ToString();
            TxtCantidadMaximo.Text = RadGVarticulos.CurrentRow.Cells["Cantidad_Maximo"].Value.ToString();
            CmbUnidadVenta.Text = RadGVarticulos.CurrentRow.Cells["Unidad_Venta"].Value.ToString();
            CmbEstatus.Text = RadGVarticulos.CurrentRow.Cells["Estatus"].Value.ToString();
            CmbEmpresa.Text = RadGVarticulos.CurrentRow.Cells["Empresa"].Value.ToString();
            CmbSucursal.Text = RadGVarticulos.CurrentRow.Cells["Sucursal"].Value.ToString();

            if (RadGVarticulos.CurrentRow.Cells["Foto"].Value != DBNull.Value)
            {
                
                byte[] imagenBuffer = (byte[])RadGVarticulos.CurrentRow.Cells["Foto"].Value;
                MemoryStream ms = new MemoryStream(imagenBuffer);
                PctBoxImagen.Image = Image.FromStream(ms);
                PctBoxImagen.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                
                PctBoxImagen.SizeMode = PictureBoxSizeMode.Zoom;
                PctBoxImagen.Image = Properties.Resources.producto__2_;
            }

            radPageView1.SelectedPage = RadpgvRegistro;
            
        }

        #endregion

        #region Restablecer Controles
        private void RestableceControles()
        {
            esNuevo = false;
            esEditar = false;
            HabilitarBotones();
            HabilitarCajas(false);
            LimpiarCajas();
            ChkbAgranel.Checked = false;
            PctBoxImagen.SizeMode = PictureBoxSizeMode.Zoom;
            PctBoxImagen.Image = Properties.Resources.LgoEsferaBlancTrasp;
        }

        private void ChkFiltro_CheckedChanged(object sender, EventArgs e)
        {
            Soporte.FiltroTipoExcelRadGridView(RadGVarticulos);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //RadGVarticulos.AllowSearchRow = true;
            //RadGVarticulos.TableElement.SearchHighlightColor = Color.LightBlue;
            //GridViewSearchRowInfo buscar = RadGVarticulos.MasterView.TableSearchRow;
        }

        #endregion

        #region Asignar Valores
        private void AsignarValores()
        {
            if (string.IsNullOrEmpty(TxtIdArticulo.Text))
            {
                IdArticulo = 0;
            }
            else
            {
              IdArticulo = Convert.ToInt32(TxtIdArticulo.Text);
            }
           
            Codigo = TxtCodigo.Text;
            Nombre = TxtNombre.Text;
            Marca = TxtMarca.Text;
            Descripcion = TxtDescripcion.Text;
            ID_Presentacion_Art = Convert.ToInt32(CmbPrestArt.SelectedValue);
            ID_Unidad_Medida = Convert.ToInt32(CmbUnidMed.SelectedValue);
            Id_Fabricante = Convert.ToInt32(CmbFabricante.SelectedValue);
            Unidad_Venta = Convert.ToInt32(CmbUnidadVenta.SelectedValue);
            Id_Estatus = Convert.ToInt32(CmbEstatus.SelectedValue);
            Contenido_Neto = Convert.ToDecimal(TxtContenido.Text);
            Cantidad_Minimo = Convert.ToDecimal(TxtCantidadMinimo.Text);
            Cantidad_Maximo = Convert.ToDecimal(TxtCantidadMaximo.Text);
            IdAgranel = Convert.ToInt32(CmbFamiliaAgranel.SelectedValue);

            MemoryStream memoria = new MemoryStream();
            PctBoxImagen.Image.Save(memoria, System.Drawing.Imaging.ImageFormat.Png);
            byte[] imagen = memoria.GetBuffer();
            Foto = imagen;
            IdEmpresa = Convert.ToInt32(CmbEmpresa.SelectedValue);
            IdSucursal = Convert.ToInt32(CmbSucursal.SelectedValue);

           
        }

        private void PtbxCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PctBoxImagen.Image = null;
        }

       

        #endregion

        #region Validar Campos
        private bool ValidarCampos()
        {

            if (ChkbAgranel.Checked==true)
            {
                if (CmbFamiliaAgranel.SelectedIndex==0)
                {
                    Soporte.MsgError("Seleccione una Categoria del Articulo en Agranel");
                    CmbFamiliaAgranel.Focus();
                    return false;
                }
            }

            if (TxtCodigo.Visible==true)
            {
                if (TxtCodigo.TextLength<4)
                {
                    Soporte.MsgError("Ingrese un Codigo del Articulo");
                    TxtCodigo.Focus();
                    return false;
                }
            }

            if (string.IsNullOrEmpty(TxtNombre.Text))
            {
                Soporte.MsgError("Ingrese una Nombre del Articulo");
                TxtNombre.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(TxtMarca.Text))
            {
                Soporte.MsgError("Ingrese una Marca.");
                TxtMarca.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(TxtDescripcion.Text))
            {
                Soporte.MsgError("Ingrese una Descripcion del Articulo.");
                TxtDescripcion.Focus();
                return false;
            }
            if (CmbPrestArt.SelectedIndex==0)
            {
                Soporte.MsgError("Seleccione una Presentacion del Articulo");
                CmbPrestArt.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(TxtContenido.Text))
            {
                Soporte.MsgError("Ingrese el Contenido del Articulo");
                TxtContenido.Focus();
                return false;
            }
            if (CmbUnidMed.SelectedIndex == 0)
            {
                Soporte.MsgError("Seleccione una Unidad de Medina del Articulo");
                CmbUnidMed.Focus();
                return false;
            }

            if (CmbUnidadVenta.SelectedIndex == 0)
            {
                Soporte.MsgError("Seleccione Unidad de Medida para la Venta del Articulo");
                CmbUnidadVenta.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(TxtCantidadMinimo.Text))
            {
                Soporte.MsgError("Ingrese un Valor Valido para la cantidad Minimo del Articulo");
                TxtCantidadMinimo.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(TxtCantidadMaximo.Text))
            {
                Soporte.MsgError("Ingrese un Valor Valido para la cantidad Maximo del Articulo");
                TxtCantidadMaximo.Focus();
                return false;
            }
            if (CmbEstatus.SelectedIndex == 0)
            {
                Soporte.MsgError("Seleccione un Estatus del Articulo");
                CmbPrestArt.Focus();
                return false;
            }
            if (CmbFabricante.SelectedIndex == 0)
            {
                Soporte.MsgError("Seleccione un Fabricante o elige sin Fabricante");
                CmbFabricante.Focus();
                return false;
            }
            if (CmbEmpresa.SelectedIndex==0)
            {
                Soporte.MsgError("Seleccione la Empresa en donde se registrara el Articulo");
                CmbEmpresa.Focus();
                return false;
            }
            if (CmbSucursal.SelectedIndex == 0)
            {
                Soporte.MsgError("Seleccione la Sucursal en donde se registrara el Articulo");
                CmbSucursal.Focus();
                return false;
            }

            return true;
        }

        #endregion

        #region Guardar Articulo
        private void GuardarArticulo()
        {
            try
            {           
                ClsGuardar.ArticuloGuardar(Codigo, Nombre, Marca, Descripcion, ID_Presentacion_Art, ID_Unidad_Medida, Id_Fabricante,
                Contenido_Neto, Cantidad_Minimo, Cantidad_Maximo, Unidad_Venta, Foto, Id_Estatus, IdAgranel,IdSucursal,IdEmpresa);
                Soporte.MsgInformacion("Articulo Registrado Correctamente.");
            }
            catch (Exception ex)
            {

                Soporte.MsgError("Sql: Posible Dupliacion de Codigo, Revisar e intentar con otro Codigo." + ex.Message);
            }

           
        }

        #endregion

        #region ActualizarArticulo
        private void ActualizaArticulo()
        {
            try
            {
                ClsGuardar.ArticuloActualizar(IdArticulo, Codigo, Nombre, Marca, Descripcion, ID_Presentacion_Art, ID_Unidad_Medida,
                Id_Fabricante, Contenido_Neto, Cantidad_Minimo, Cantidad_Maximo, Unidad_Venta, Foto, Id_Estatus,IdSucursal,IdEmpresa);

                Soporte.MsgInformacion("Articulo Actualizado Correctamente");
            }
            catch (Exception ex)
            {

                Soporte.MsgError("Sql: Posible Dupliacion de Codigo, Revisar e intentar con otro Codigo." + ex.Message);
            }             

           
        }

        #endregion

        #endregion

        #region Operacion Botones


        #region Boton Cancelar

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            RestableceControles();
        }

        #endregion

        #region Boton Nuevo

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            esEditar = false;
            HabilitarBotones();
            HabilitarCajas(true);
            LimpiarCajas();
        }

        #endregion

        #region Boton Actualizar

        private void BtnActulizar_Click(object sender, EventArgs e)
        {
            if (!TxtIdArticulo.Text.Equals(""))
            {
                esEditar = true;
                HabilitarBotones();
                HabilitarCajas(true);
                ChkbAgranel.Visible = false;

            }
            else
            {
                MessageBox.Show("Seleccione un regitro de la lista de Usuarios para poder editarlo", "Seleccionar Registro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region Boton Guardar
        
        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                AsignarValores();
                if (esNuevo)
                {
                    GuardarArticulo();                   
                    
                }
                else
                {
                    ActualizaArticulo();                  
                                        
                }
                MostrarArticulos();
                RestableceControles();
                radPageView1.SelectedPage = RadpvpListado;
            }
            
        }

        #endregion

        #region Boton Agregar Imagen
        
        private void BtnAgregarImagen_Click(object sender, EventArgs e)
        {
            Soporte.ImagenCarga(PctBoxImagen);
        }
        #endregion

        #region Boton Eliminar Imagen
        
        private void BtnEliminarImagen_Click(object sender, EventArgs e)
        {
            PctBoxImagen.SizeMode = PictureBoxSizeMode.Zoom;
            PctBoxImagen.Image = Properties.Resources.LgoEsferaBlancTrasp;
        }

        #endregion

        #region Boton Eliminar

        private void EliminarArticulo()
        {
            for (int i = 0; i < RadGVarticulos.SelectedRows.Count; i++)
            {
                IdArticulo = Convert.ToInt32(RadGVarticulos.SelectedRows[i].Cells["Id_Articulo"].Value);
                ClsGuardar.ArticuloEliminar(IdArticulo);
            }
        }

        #endregion
        private void BtnEliminar_Click(object sender, EventArgs e)
        {

            EliminarRegistro();
           

        }


        private void EliminarRegistro()
        {
            if (RadGVarticulos.SelectedRows.Count > 0)
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Ha selecionado eliminar el registro, desea continuar?", "Eliminar Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (Opcion == DialogResult.Yes)
                {
                    if (RadGVarticulos.SelectedRows.Count > 1)
                    {
                        EliminarArticulo();
                        this.Cursor = Cursors.WaitCursor;
                        MostrarArticulos();
                        this.Cursor = Cursors.Default;
                        Soporte.MsgInformacion("Los Registros se han eliminados correctamente.");
                    }
                    else
                    {
                        EliminarArticulo();
                        this.Cursor = Cursors.WaitCursor;
                        MostrarArticulos();
                        this.Cursor = Cursors.Default;
                        Soporte.MsgInformacion("El Registro se ha eliminado correctamente.");
                    }


                }
            }
        }
        #endregion

        #region Eventos

        #region Eventos TxtBox KeyPress        

        private void TxtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Soporte.EventoPresKeyTxt(sender, e, TxtNombre);
        }

        private void TxtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            Soporte.EventoPresKeyTxt(sender, e, TxtMarca);
        }

        private void TxtMarca_KeyPress(object sender, KeyPressEventArgs e)
        {
            Soporte.EventoPresKeyTxt(sender, e, TxtDescripcion);
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            //if (RadGVarticulos.SelectedRows.Count>0)
            //{
            //    for (int i = 0; i < RadGVarticulos.SelectedRows.Count; i++)
            //    {
            //        RadGVarticulos.Rows.Remove(RadGVarticulos.CurrentRow);
            //       ////}
            //if (RadGVarticulos.SelectedRows.Count > 0)
            //{
            //    GridViewDataRowInfo[] filas = new  GridViewDataRowInfo[this.RadGVarticulos.SelectedRows.Count];
            //    this.RadGVarticulos.SelectedRows.CopyTo(filas, 0);

                

            //    for (int i = 0; i < RadGVarticulos.SelectedRows.Count; i++)
            //    {
            //        filas[i].Delete();
            //    }

                
            //}

        }

        private void BtnVolver_Click(object sender, EventArgs e)
        {
            radPageView1.SelectedPage = RadpvpListado;
        }

        private void TxtNombre_TextChanged(object sender, EventArgs e)
        {
            //DtRegistros = Consulta.ArticuloBuscaNombre(TxtNombre.Text);
            //DgvAutocomplete.DataSource = DtRegistros;
            //if (DgvAutocomplete.Rows.Count>0)
            //{
            //    DgvAutocomplete.Visible = true;
            //}
            //else
            //{
            //    DgvAutocomplete.Visible = false;
            //}
        }

        private void DgvAutocomplete_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //TxtNombre.Text =Convert.ToString( DgvAutocomplete.Rows[0].Cells["Nombre"].Value);
            //DgvAutocomplete.Visible = false;
        }  

      

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormRptArticulo formRptArticulo = new FormRptArticulo();
            formRptArticulo.ShowDialog();
        }

        private void filtroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Soporte.FiltroTipoExcelRadGridView(RadGVarticulos);
        }

        private void todosLosArticulosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MostrarArticulos();
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

        private void TxtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            Soporte.EventoPresKeyCmbx(sender, e, CmbPrestArt);
        }

        private void CmbFamiliaAgranel_KeyPress(object sender, KeyPressEventArgs e)
        {
            Soporte.EventoPresKeyTxt(sender, e, TxtNombre);
        }

        private void CmbPrestArt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Soporte.EventoPresKeyTxt(sender, e, TxtContenido);
        }

        private void TxtContenido_KeyPress(object sender, KeyPressEventArgs e)
        {
            Soporte.EventoPresKeyCmbx(sender, e, CmbUnidMed);
        }

        private void CmbUnidadVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            Soporte.EventoPresKeyTxt(sender, e, TxtCantidadMinimo);
        }

        private void CmbUnidMed_KeyPress(object sender, KeyPressEventArgs e)
        {
            Soporte.EventoPresKeyCmbx(sender, e, CmbUnidadVenta);
        }

        private void TxtCantidadMinimo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Soporte.EventoPresKeyTxt(sender, e, TxtCantidadMaximo);
        }

        private void TxtCantidadMaximo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Soporte.EventoPresKeyCmbx(sender, e, CmbEstatus);
        }

        private void CmbEstatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            Soporte.EventoPresKeyCmbx(sender, e, CmbFabricante);
        }
        #endregion

        #region ChkbAgranel CheckedChanged

        private void ChkbAgranel_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkbAgranel.Checked)
            {
                LblCodigo.Visible = false;
                TxtCodigo.Visible = false;
                LblTipoAgranel.Visible = true;
                CmbFamiliaAgranel.Visible = true;
                TxtCodigo.Clear();

            }
            else
            {
                LblCodigo.Visible = true;
                TxtCodigo.Visible = true;
                LblTipoAgranel.Visible = false;
                CmbFamiliaAgranel.Visible = false;
                TxtCodigo.Clear();
            }
        }


        #endregion

        #region RadGVarticulos CellDoubleClick
        
        private void RadGVarticulos_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
          
            PctBoxImagen.Image = null;
            RestableceControles();
            PasarDatosRadGV();
        }


        #endregion

        #region CmbFabricante SelectedIndexChanged        

        private void CmbFabricante_SelectedIndexChanged(object sender, EventArgs e)
        {
            DtRegistros = Consulta.FabricanteConsulta();
            if (DtRegistros.Rows.Count>0)
            {
                TxtRazon.Text = DtRegistros.Rows[CmbFabricante.SelectedIndex]["Razon_Social"].ToString();
                TxtRFC.Text = DtRegistros.Rows[CmbFabricante.SelectedIndex]["RFC"].ToString();
                TxtDireccion.Text = DtRegistros.Rows[CmbFabricante.SelectedIndex]["Direccion"].ToString();
                TxtTelefono.Text = DtRegistros.Rows[CmbFabricante.SelectedIndex]["Telefono"].ToString();
                TxtCorreo.Text = DtRegistros.Rows[CmbFabricante.SelectedIndex]["Correo"].ToString();
            }
        }

        #endregion

        #endregion
    }
}
