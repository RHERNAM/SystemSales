using SistemaVentas.Clases.SQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccesDLL;
using SistemaVentas.Clases.Entidates;
using SistemaVentas.Clases.SQL.Transacciones;

namespace SistemaVentas.Formularios.Administracion
{
    public partial class FormEntradasSalidas : Form
    {
        #region Variables
        private string Comprobante, MotivoMov, Sucursal, Empresa, sFolioEntrada, sProveedor;
        private int TipoComprobante;
        private int NumeroComprobante, IdTipoEntrada, IdSucursal, IdEmpresa;
        private int Idproveedor;
        private DateTime FechaComprobante, FechaVencimiento;
        private decimal TotalArticulos;
        private decimal MontoTotal;
        private decimal IvaTotal;
        private int IdArticulo = 0;
        private string Codigo;
        private string Descripcion;
        private string Nombre;
        private string Marca;
        private string Presentacion;       
        private string Contenido;
        private decimal CantidadMinimo;
        private decimal CantidadMaximo;
        private string UnidadMedida;
        //private string Fabricante;
        private decimal Cantidad;
        private decimal PrecioCompra;
        private decimal GananciaObtener;
        private decimal PrecioVenta;
        private decimal TotalInventario, TotalCantidad; //InventarioNuevo;
        private decimal CantidadVendido;
        private decimal IvaUnitario;    
       
        private DataTable DtRegistros;

        #endregion

        #region Instancias    
        
        List<ListaInventario> listaInventarios = new List<ListaInventario>();

        #endregion

        #region Inicio Formulario
       
        public FormEntradasSalidas()
        {
            InitializeComponent();
        }

        private void FormInventario_Load(object sender, EventArgs e)
        {
            ListaCombos();
            LimpiarTextos();
            OcultarColumnas();
            FechaPerosnalizar();
            ListaEntradas();

            CmbEmpresa.SelectedValue = MemoriaCache.IdEmpresa;
            CmbSucursal.SelectedValue = MemoriaCache.IdSucursal;
        }

        #endregion

        #region Consultas
        private void ListaCombos()
        {
            CmbProveedores.DataSource = Consulta.ProveedoresLista();
            CmbProveedores.DisplayMember = "NOMBRE";
            CmbProveedores.ValueMember = "ID_PROVEEDOR";
            CmbComprobante.DataSource = Consulta.ComprobantesLista();
            CmbComprobante.DisplayMember = "Nombre";
            CmbComprobante.ValueMember = "IdComprobante";

            CmbEmpresa.DisplayMember = "Nombre";
            CmbEmpresa.ValueMember = "Id_Empresa";
            CmbEmpresa.DataSource = Consulta.EmpresaCombobox();

            CmbTipEntradas.DisplayMember = "Tipo_Entrada";
            CmbTipEntradas.ValueMember = "Id_Tipo_Entrada";
            CmbTipEntradas.DataSource = Consulta.EntradaTiposLista();
        }


        private void ListaEntradas()
        {
            DtRegistros = Consulta.EntradasLista();
            RadgvEntradas.DataSource = null;
            RadgvEntradas.DataSource = DtRegistros;
            RadgvEntradas.BestFitColumns();
        }
        private void ListaSucursal(int IdEmpresa)
        {
            CmbSucursal.DataSource = Consulta.SucursalListCombo(IdEmpresa);
            CmbSucursal.DisplayMember = "Nombre";
            CmbSucursal.ValueMember = "Id_Negocio";
        }

        #endregion

        #region Metodos
        // INICIO METODOS

        #region Personalizar Fechas del DatatimerPiker

        private void FechaPerosnalizar()
        {
            DtpFecheVencimiento.MinDate = DateTime.Now.AddDays(5);
            DtpkFechaCompr.MaxDate = DateTime.Now;
        }
        #endregion

        #region Limiapiar Controles de Articulo

        private void LipiarControlesArticulo()
        {
            TxtCodigo.Clear();
            TxtDescripcion.Clear();
            TxtPrecioCompra.Text = "0.00";
            TxtPrecioVenta.Text = "0.00";
            TxtGananciaObtener.Text = "0.00";
            TxtTotalInventario.Text = "0";
            TxtIvaUnitario.Text = "0.00";
            TxtCantidadVendido.Text = "0";
            TxtCantidadMinimo.Text = "0";
            TxtCantidadMaximo.Text = "0";
            TxtUnidadVenta.Clear();
            TxtCantidad.Text = "0";
            DtpFecheVencimiento.MinDate = DateTime.Now.AddDays(5);
            TxtCodigo.Focus();
        }
        #endregion

        #region Limpiar Controles
        private void LimpiarTextos()
        {
            CmbComprobante.SelectedIndex = 0;
            CmbProveedores.SelectedIndex = 0;
            TxtNumComprobante.Text = "0";
            DtpkFechaCompr.MaxDate = DateTime.Now; ;
            TxtTotalArticulos.Text = "0.00";
            TxtMontoTotal.Text = "0.00";
            TxtIvaTotal.Text = "0.00";
            IdArticulo = 0;
            Codigo="";
            TxtCodigo.Text= "Escriba lo que desea buscar...";
            TxtCodigo.ForeColor = Color.Gray;
            TxtDescripcion.Clear();  
            TxtPrecioCompra.Text = "0.00";
            TxtPrecioVenta.Text = "0.00";
            TxtGananciaObtener.Text = "0.00";
            TxtTotalInventario.Text = "0";
            TxtIvaUnitario.Text = "0.00";
            TxtCantidadVendido.Text = "0";
            TxtCantidadMinimo.Text = "0";
            TxtCantidadMaximo.Text = "0";
            TxtUnidadVenta.Clear();
            TxtCantidad.Text = "0";
            TxtFabricante.Clear();
            DtpFecheVencimiento.Value = DateTime.Now.AddDays(5);
            TotalInventario = 0;
            listaInventarios.Clear();
            RadgvLista.DataSource = null;
            RadgvLista.DataSource = listaInventarios;
            RadgvLista.BestFitColumns();
            OcultarColumnas();
            //FechaPerosnalizar();
        }

        #endregion

        #region Validar Campos
        private bool ValidarTextBox()
        {
            if (CmbComprobante.SelectedIndex==0)
            {
                Soporte.MsgInformacion("Seleccione un comprobante de la compra del Articulo");
                CmbComprobante.Focus();
                return false;
            }

            if (Convert.ToInt32( CmbEmpresa.SelectedValue) == 0)
            {
                Soporte.MsgInformacion("Seleccione una Empres");
                CmbEmpresa.Focus();
                return false;
            }

            if (Convert.ToInt32(CmbSucursal.SelectedValue) == 0)
            {
                Soporte.MsgInformacion("Seleccione una Sucursal");
                CmbSucursal.Focus();
                return false;
            }

            if (Convert.ToInt32(CmbTipEntradas.SelectedValue) == 0)
            {
                Soporte.MsgInformacion("Seleccione un Tipo Entrada");
                CmbTipEntradas.Focus();
                return false;
            }

            if (CmbProveedores.SelectedIndex==0)
            {
                Soporte.MsgInformacion("Seleccione un Proveedor del comprobante.");
                CmbProveedores.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(TxtCodigo.Text))
            {
                Soporte.MsgInformacion("Ingrese Codigo del Articulo para poder agregar a la Lista");
                TxtCodigo.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(TxtDescripcion.Text))
            {
                Soporte.MsgInformacion("Busque el Articulo ya que faltan datos para Agregar a la Lista.");
                TxtCodigo.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(TxtCantidad.Text) && TxtCantidad.Text != "0")
            {
               
                Soporte.MsgInformacion("Ingrese la cantidad del articulo a Ingresar");
                TxtCantidad.Focus();
                return false;
            }

            if (!Soporte.ValidarFormatoMoneda(TxtCantidad.Text))
            {
                Soporte.MsgInformacion("El formato de la Cantidad no es Valido.");
                TxtCantidad.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(TxtPrecioCompra.Text) && TxtPrecioCompra.Text != "0")
            {
                Soporte.MsgInformacion("Ingrese Precio del articulo");
                TxtPrecioCompra.Focus();
                return false;
            }

            if (!Soporte.ValidarFormatoMoneda(TxtPrecioCompra.Text))
            {
                Soporte.MsgInformacion("El formato de Precio de Compra no es Valido.");
                TxtPrecioCompra.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(TxtGananciaObtener.Text) && TxtGananciaObtener.Text != "0")
            {
                Soporte.MsgInformacion("Ingrese la cantidad de la Ganancia a Obtener");
                TxtGananciaObtener.Focus();
                return false;
            }

            if (!Soporte.ValidarFormatoMoneda(TxtGananciaObtener.Text))
            {
                Soporte.MsgInformacion("El formato de Ganancia a Obtener no es Valido.");
                TxtGananciaObtener.Focus();
                return false;
            }


            if (Convert.ToInt32( CmbTipEntradas.SelectedValue)==4)
            {
                if (string.IsNullOrEmpty(TxtNumComprobante.Text) && TxtNumComprobante.MaxLength < 2)
                {
                    Soporte.MsgInformacion("El numero de comprobante no tiene el Formato correcto, revise.");
                    TxtNumComprobante.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(TxtTotalArticulos.Text) && TxtTotalArticulos.Text != "0")
                {
                    Soporte.MsgInformacion("Ingrese la cantidad Total del Comprobante");
                    TxtTotalArticulos.Focus();
                    return false;
                }
                if (!Soporte.ValidarFormatoMoneda(TxtTotalArticulos.Text))
                {
                    Soporte.MsgInformacion("El formato de Total Articulos no es Valido.");
                    TxtTotalArticulos.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(TxtMontoTotal.Text) && TxtMontoTotal.Text != "0")
                {
                    Soporte.MsgInformacion("Ingrese el Monto Total del Comprobante");
                    TxtMontoTotal.Focus();
                    return false;
                }
                if (!Soporte.ValidarFormatoMoneda(TxtMontoTotal.Text))
                {
                    Soporte.MsgInformacion("El formato del Monto Total no es Valido.");
                    TxtMontoTotal.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(TxtIvaTotal.Text) && TxtIvaTotal.Text != "0")
                {
                    Soporte.MsgInformacion("Ingrese el Monto Total del Iva");
                    TxtIvaTotal.Focus();
                    return false;
                }
                if (!Soporte.ValidarFormatoMoneda(TxtIvaTotal.Text))
                {
                    Soporte.MsgInformacion("El formato del Iva Total no es Valido.");
                    TxtIvaTotal.Focus();
                    return false;
                }
            }
            else
            {

              

            }



           

            return true;
        }

        #endregion

        #region Ocultar Columnas

        private void OcultarColumnas()
        {
            RadgvLista.Columns["Idarticulo"].IsVisible = false;
            RadgvLista.Columns["Idproveedor"].IsVisible = false;
            RadgvLista.Columns["Tipo_Comprobante"].IsVisible = false;
            RadgvLista.Columns["IdEmpresa"].IsVisible = false;
            RadgvLista.Columns["IdSucursal"].IsVisible = false;
            RadgvLista.Columns["IdMotivoMov"].IsVisible = false;


            #region Centrar Contenido Radgv            
            RadgvLista.Columns["Precio_Venta"].TextAlignment = ContentAlignment.MiddleCenter;
            RadgvLista.Columns["Ganancia_Obtener"].TextAlignment = ContentAlignment.MiddleCenter;
            RadgvLista.Columns["Precio_Compra"].TextAlignment = ContentAlignment.MiddleCenter;
           // RadgvLista.Columns["Total_Inventario"].TextAlignment = ContentAlignment.MiddleCenter;
            //RadgvLista.Columns["Cantidad_Vendido"].TextAlignment = ContentAlignment.MiddleCenter;
            RadgvLista.Columns["Cantidad_Minimo"].TextAlignment = ContentAlignment.MiddleCenter;
            RadgvLista.Columns["Cantidad_Maximo"].TextAlignment = ContentAlignment.MiddleCenter;
            RadgvLista.Columns["Cantidad"].TextAlignment = ContentAlignment.MiddleCenter;
            RadgvLista.Columns["IvaUnitario"].TextAlignment = ContentAlignment.MiddleCenter;
            RadgvLista.Columns["Contenido"].TextAlignment = ContentAlignment.MiddleCenter;
            RadgvLista.Columns["Total"].TextAlignment = ContentAlignment.MiddleCenter;
            RadgvLista.Columns["Comprobante"].TextAlignment = ContentAlignment.MiddleCenter;
            RadgvLista.Columns["Monto_Total"].TextAlignment = ContentAlignment.MiddleCenter;
            RadgvLista.Columns["IvaTotal"].TextAlignment = ContentAlignment.MiddleCenter;

            #endregion

        }
        #endregion

        #region Asignar Valores Para Lista
        private void AsignarValoresLista()
        {
            Codigo = TxtCodigo.Text;
            Comprobante = CmbComprobante.Text;
            Descripcion = TxtDescripcion.Text;
            PrecioCompra = Convert.ToDecimal(TxtPrecioCompra.Text);
            GananciaObtener = Convert.ToDecimal(TxtGananciaObtener.Text);            
            PrecioVenta = PrecioCompra + GananciaObtener;
            Cantidad = int.Parse(TxtCantidad.Text);
            //ContenidoTotal = Convert.ToDecimal(Contenido);
            IvaUnitario = Decimal.Round(PrecioCompra - (PrecioCompra / Convert.ToDecimal(1.16)), 2, MidpointRounding.AwayFromZero);
            //CantidadVendido = Convert.ToDecimal(TxtCantidadVendido.Text);
            CantidadMinimo = Convert.ToDecimal(TxtCantidadMinimo.Text);
            CantidadMaximo = Convert.ToDecimal(TxtCantidadMaximo.Text);
            Idproveedor = Convert.ToInt32(CmbProveedores.SelectedValue);
            NumeroComprobante = Convert.ToInt32(TxtNumComprobante.Text);
            TipoComprobante = Convert.ToInt32(CmbComprobante.SelectedValue);
            MontoTotal =Convert.ToDecimal(TxtMontoTotal.Text);
            IvaTotal=Convert.ToDecimal(TxtIvaTotal.Text);
            FechaComprobante = DtpkFechaCompr.Value;
            FechaVencimiento = DtpFecheVencimiento.Value;
            TotalArticulos =Convert.ToDecimal( TxtTotalArticulos.Text);
            TxtPrecioVenta.Text = Convert.ToString(PrecioVenta);
            TxtIvaUnitario.Text = Convert.ToString(IvaUnitario);
            IdEmpresa = Convert.ToInt32(CmbEmpresa.SelectedValue);
            IdSucursal = Convert.ToInt32(CmbSucursal.SelectedValue);
            IdTipoEntrada = Convert.ToInt32(CmbTipEntradas.SelectedValue);
            MotivoMov = CmbTipEntradas.Text;
            Empresa = CmbEmpresa.Text;
            Sucursal = CmbSucursal.Text;
            sFolioEntrada = TxtFolioEntrada.Text;
            sProveedor = CmbProveedores.Text;
        }
        #endregion

        private bool ValidarValorVariables()
        {

            if (PrecioCompra==0)
            {
                Soporte.MsgError("Ingrese el precio de compra del articulo");
                TxtPrecioCompra.Focus();
                return false;
            }
            if (GananciaObtener == 0)
            {
                Soporte.MsgError("Ingrese el Saldo de Ganancia a Obtener");
                TxtGananciaObtener.Focus();
                return false;
            }
            if (Cantidad == 0)
            {
                Soporte.MsgError("Capture la Cantidad de Artiulos que se ingresaran.");
                TxtCantidad.Focus();
                return false;
            }


            if (Convert.ToInt32(CmbTipEntradas.SelectedValue)==4)
            {
                if (MontoTotal == 0)
                {
                    Soporte.MsgError("Ingrese el Monto Total del Comprobante.");
                    TxtMontoTotal.Focus();
                    return false;
                }
                if (TotalArticulos == 0)
                {
                    Soporte.MsgError("Ingrese la Cantidad total de Articulos del Comprobante.");
                    TxtTotalArticulos.Focus();
                    return false;
                }

                if (NumeroComprobante == 0)
                {
                    Soporte.MsgError("Ingrese el Numero del Comprobante.");
                    TxtNumComprobante.Focus();
                    return false;
                }


            }
            else
            {

            }



            return true;      
            
        }

        #region Calculo Inventario
        private void CalculoInventario()
        {
            if (TxtUnidadVenta.Text != "Pieza") //si es diferente de pieza entonces quiere decir que es gramos, kilos, litros etc. entonces se tiene que multiplicar por el contenido
            {
                TotalCantidad = Cantidad * Convert.ToDecimal(Contenido);
            }
            else
            {
                TotalCantidad = Cantidad * 1; //Si la Unidad de Venta es por pieza entonces solo se multimplica por Uno
            }

            //InventarioNuevo = TotalCantidad + TotalInventario;
        }

        #endregion

        #region Agregar Articulo a La Lista      
        private void AgregarArticulo()
        {
            if (ValidarTextBox())
            {


                if (ValidarArticulo())//Valida si el Articulo se encuentra en la lista del Radgv
                {

                }
                else
                {
                    AsignarValoresLista();
                    if (ValidarValorVariables())
                    {
                       CalculoInventario();
                        #region Pasamos Valores a la Listata

                        ListaInventario listaInvenNuevo = new ListaInventario
                            (
                            IdArticulo,
                            Codigo,
                            Descripcion, //TxtDescripcion.Text,
                            PrecioCompra,//Convert.ToDecimal(TxtPrecioCompra.Text),
                            GananciaObtener,//Convert.ToDecimal(TxtGananciaObtener.Text),
                            PrecioVenta,//Convert.ToDecimal(TxtPrecioVenta.Text),   
                            Cantidad,//Convert.ToDecimal(TxtCantidad.Text),
                            Convert.ToDecimal(Contenido),
                            TotalCantidad,//Convert.ToInt32(TxtContenidoTotal.Text),
                            //InventarioNuevo,
                            //CantidadVendido,
                            CantidadMinimo,
                            CantidadMaximo,
                            IvaUnitario,
                            Idproveedor,                           
                            NumeroComprobante,
                            TipoComprobante,
                            Comprobante,
                            TotalArticulos,
                            MontoTotal,
                            IvaTotal,
                            FechaComprobante,
                            FechaVencimiento,
                            IdTipoEntrada,
                            sFolioEntrada,
                            IdSucursal,
                            IdEmpresa,
                            MotivoMov,
                            Sucursal,
                            Empresa,sProveedor);


                        #endregion

                        #region Agregamos a la Lista y Cargamos al DataGridView

                        listaInventarios.Add(listaInvenNuevo);
                        RadgvLista.DataSource = null;
                        RadgvLista.DataSource = listaInventarios;
                        RadgvLista.BestFitColumns();
                        #endregion
                    }

                }
                OcultarColumnas();
            }
        }
        #endregion

        #region Validar si Existe Articulo      
        private bool ValidarArticulo()
        {
            if (RadgvLista.Rows.Count > 0)
            {
                for (int i = 0; i < RadgvLista.Rows.Count; i++)
                {
                    string Codigo2 = RadgvLista.Rows[i].Cells["Codigo"].Value.ToString();
                    if (TxtCodigo.Text == Codigo2)
                    {
                        Soporte.MsgError("El articulo ya se encuentra en la Lista, puede Eliminarlo y volver a Cargarlo");
                        return true;
                    }
                }

            }
            return false;
        }

        #endregion



        #region Buscar Articulo Para Autocompletado

        private void OcultarColumasDataGrid()
        {
            DgvAutoCompletar.Columns["Id_Articulo"].Visible = false;
            DgvAutoCompletar.Columns["Codigo"].Visible = false;
            DgvAutoCompletar.Columns["Nombre"].Visible = false;
            DgvAutoCompletar.Columns["Marca"].Visible = false;
            DgvAutoCompletar.Columns["Presentacion"].Visible = false;
            DgvAutoCompletar.Columns["Unidad_Medida"].Visible = false;
            DgvAutoCompletar.Columns["Contenido_Neto"].Visible = false;
            DgvAutoCompletar.Columns["Fecha_Registro"].Visible = false;
            DgvAutoCompletar.Columns["Fecha_Actualizacio"].Visible = false;
            DgvAutoCompletar.Columns["Fabricante"].Visible = false;
            DgvAutoCompletar.Columns["Cantidad_Minimo"].Visible = false;
            DgvAutoCompletar.Columns["Cantidad_Maximo"].Visible = false;
            DgvAutoCompletar.Columns["Unidad_Venta"].Visible = false;
            DgvAutoCompletar.Columns["Foto"].Visible = false;
            DgvAutoCompletar.Columns["Estatus"].Visible = false;
        }

        private void ValidarTextoCodigoABuscar(string Codigo)
        {
            if (TxtCodigo.Text != "" && TxtCodigo.Text != "Ingrese Codigo...")
            {

                DtRegistros = Consulta.ArticuloBuscarXdescripcion(Codigo);
                DgvAutoCompletar.DataSource = DtRegistros;
                if (DgvAutoCompletar.Rows.Count > 0)
                {
                    OcultarColumasDataGrid();
                    DgvAutoCompletar.Visible = true;
                }
                else
                {
                    DgvAutoCompletar.Visible = false;
                }
            }
        }

        #endregion

        #region Buscar Articulo

        private void BuscarArticulo(string pCodigo)
        {
            
            DtRegistros = Consulta.ArticuloConsulta();
            DataRow[] dataRow = DtRegistros.Select("Codigo ='" + pCodigo + "'");//Buscamos el articulo en el Datatable Tabla Articulo Detalle
            if (dataRow.Length>0)
            {
                #region Buscamos Articulo en Registros
                
                DataRow row = dataRow[0];
                IdArticulo =(int)row.ItemArray[0];
                Codigo = row.ItemArray[1].ToString();
                Nombre = row.ItemArray[2].ToString();
                Marca = row.ItemArray[3].ToString();           
                //TxtDescripcion.Text = row.ItemArray[4].ToString();
                Presentacion = row.ItemArray[5].ToString();
                UnidadMedida = row.ItemArray[6].ToString();
                Contenido = row.ItemArray[7].ToString();
                TxtFabricante.Text = row.ItemArray[10].ToString();
                TxtCantidadMinimo.Text = row.ItemArray[11].ToString();
                TxtCantidadMaximo.Text = row.ItemArray[12].ToString();
                TxtUnidadVenta.Text = row.ItemArray[13].ToString();
                TxtCodigo.Text = Codigo;
                #endregion

                #region Case

                switch (UnidadMedida)
                {
                    case "Mililitros":
                        UnidadMedida = "ml";
                        break;
                    case "Litro":
                        UnidadMedida = "Lt";
                        break;
                    case "Litros":
                        UnidadMedida = "Lts.";
                        break;
                    case "Kilogramos":
                        UnidadMedida = "Kg";
                        break;
                    case "Gramos":
                        UnidadMedida = "Gr";
                        break;
                    case "Galon":
                        UnidadMedida = "Gln.";
                        break;
                    case "Pieza":
                        UnidadMedida = "Pza.";
                        break;
                    case "Unidad":
                        UnidadMedida = "Und.";
                        break;

                }

                #endregion

                TxtDescripcion.Text = Nombre + " "+ Marca + " "+ Presentacion + " de " + Contenido + " " + UnidadMedida;

                #region Busca Articulo en Inventario
               
                BuscarCodigoArt(Codigo); // Consultara en el Inventario sin hay Enxistencias
               
                if (DtRegistros.Rows.Count>0)
                {
                    ObtenerDatosArtInventario();
                    TxtGananciaObtener.Text =Convert.ToString(GananciaObtener);
                    TxtCantidadVendido.Text = Convert.ToString(CantidadVendido);
                    TxtTotalInventario.Text = Convert.ToString(TotalInventario);
                    TxtPrecioVenta.Text = Convert.ToString(PrecioVenta);
                    TxtPrecioCompra.Text = Convert.ToString(PrecioCompra);
                }
                else
                {
                    TxtGananciaObtener.Text ="0.00";
                    TxtCantidadVendido.Text = "0";
                    TxtTotalInventario.Text = "0";
                    TxtPrecioVenta.Text = "0.00";
                    TxtIvaUnitario.Text = "0.00";
                    TxtPrecioCompra.Text = "0.00";
                   
                    DtpFecheVencimiento.MinDate = DateTime.Now.AddDays(5);
                }

                #endregion
            }
            else
            {
                IdArticulo = 0;
                TxtCantidadMinimo.Text = "0";
                TxtCantidadMaximo.Text = "0";
                TxtDescripcion.Clear();
                TxtFabricante.Clear();
                TxtUnidadVenta.Clear();
            }
        }

        #endregion

        #region Buscar Codigo Articulo en Inventario

        private void BuscarCodigoArt(string Codigo)
        {
            DtRegistros = Consulta.InventarioBuscaCodArt(Codigo);
        }
        #endregion

        #region Obtner Datos Articulo de Inventario
        /// <summary>
        /// Obtenenos Datos del Articulo en la Tabla Inventario  y los guaradmos en Variables
        /// </summary>
        private void ObtenerDatosArtInventario()
        {
            
                PrecioCompra =Convert.ToDecimal( DtRegistros.Rows[0]["Precio_Compra"]);
                PrecioVenta = Convert.ToDecimal(DtRegistros.Rows[0]["Precio_Venta"]);
                GananciaObtener = Convert.ToDecimal(DtRegistros.Rows[0]["Ganancia_Obtener"]);
                TotalInventario = Convert.ToDecimal(DtRegistros.Rows[0]["Total_Inventario"]);
                if (DtRegistros.Rows[0]["Cantidad_Vendido"].Equals(DBNull.Value))
                {
                    CantidadVendido = 0;
                }
                else
                {
                    CantidadVendido = Convert.ToDecimal(DtRegistros.Rows[0]["Cantidad_Vendido"]);
                }
                
            
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (RadgvLista.Rows.Count>0)
            {
                GuardarEntreda();
                Soporte.AlertaNotifiacion(radDesktopAlert1, "Registro Guardado", "La Lista de Articulos se han guardado Correctamente.", Properties.Resources.guardar);
                LimpiarTextos();

            }
            else
            {
                Soporte.Msg_Alerta("Ingrese al menos un Articulo a la Lista para poder Guardar un Registro.","Sin Registros");
            }
        }
        #endregion       
   
        #region Elimnar articulo en la Lista   

        private void BtnEliminarRegistr_Click(object sender, EventArgs e)
        {
            if (RadgvLista.Rows.Count>0)
            {
                int fila = Convert.ToInt32(RadgvLista.Rows[RadgvLista.CurrentRow.Index].Cells["Idarticulo"].Value);
                EliminarRegistro(fila);
                RadgvLista.DataSource = null;
                RadgvLista.DataSource = listaInventarios;
                RadgvLista.BestFitColumns();
            }           
        }

             
        private void EliminarRegistro(int articuloid)
        {
            foreach (var Articulo in listaInventarios)
            {
                if (Articulo.Idarticulo == articuloid)
                {
                    listaInventarios.Remove(Articulo);

                    break;
                }
            }
        }

        #endregion       

        #region Guardar
        private void GuardarEntreda()
        {
           
            for (int i = 0; i < RadgvLista.Rows.Count; i++)
            {
                IdArticulo = Convert.ToInt32(RadgvLista.Rows[i].Cells["Idarticulo"].Value);
                Codigo = Convert.ToString(RadgvLista.Rows[i].Cells["Codigo"].Value);
                Descripcion = Convert.ToString(RadgvLista.Rows[i].Cells["Descripcion"].Value);
                PrecioCompra = Convert.ToDecimal(RadgvLista.Rows[i].Cells["Precio_Compra"].Value);
                PrecioVenta = Convert.ToDecimal(RadgvLista.Rows[i].Cells["Precio_Venta"].Value);
                GananciaObtener = Convert.ToDecimal(RadgvLista.Rows[i].Cells["Ganancia_Obtener"].Value);
               // TotalInventario = Convert.ToInt32(RadgvLista.Rows[i].Cells["Total_Inventario"].Value);
               // CantidadVendido = Convert.ToInt32(RadgvLista.Rows[i].Cells["Cantidad_Vendido"].Value);
                CantidadMinimo = Convert.ToInt32(RadgvLista.Rows[i].Cells["Cantidad_Minimo"].Value);
                CantidadMaximo = Convert.ToInt32(RadgvLista.Rows[i].Cells["Cantidad_Maximo"].Value);

                IvaUnitario = Convert.ToDecimal(RadgvLista.Rows[i].Cells["IvaUnitario"].Value);
                Cantidad = Convert.ToInt32(RadgvLista.Rows[i].Cells["Cantidad"].Value);
                Contenido = Convert.ToString(RadgvLista.Rows[i].Cells["Contenido"].Value);
                TotalCantidad = Convert.ToInt32(RadgvLista.Rows[i].Cells["Total"].Value);
                Idproveedor = Convert.ToInt32(RadgvLista.Rows[i].Cells["Idproveedor"].Value);
                TipoComprobante = Convert.ToInt32(RadgvLista.Rows[i].Cells["Tipo_Comprobante"].Value);
                NumeroComprobante = Convert.ToInt32(RadgvLista.Rows[i].Cells["Numero_Comprobante"].Value);
                MontoTotal = Convert.ToDecimal(RadgvLista.Rows[i].Cells["Monto_Total"].Value);
                IvaTotal = Convert.ToDecimal(RadgvLista.Rows[i].Cells["IvaTotal"].Value);
                FechaComprobante = Convert.ToDateTime(RadgvLista.Rows[i].Cells["Fecha_Comprobante"].Value);
                FechaVencimiento = Convert.ToDateTime(RadgvLista.Rows[i].Cells["Fecha_Vencimiento"].Value);
                IdTipoEntrada = Convert.ToInt32(RadgvLista.Rows[i].Cells["IdMotivoMov"].Value);
                IdSucursal = Convert.ToInt32(RadgvLista.Rows[i].Cells["IdSucursal"].Value);
                IdEmpresa = Convert.ToInt32(RadgvLista.Rows[i].Cells["IdEmpresa"].Value);
                sFolioEntrada = RadgvLista.Rows[i].Cells["Folio_Entrada"].Value.ToString();


                ClsGuardar.EntradasGuardar(IdArticulo, Codigo, PrecioCompra, PrecioVenta, GananciaObtener,/* TotalInventario,*/
                CantidadMinimo, CantidadMaximo, FechaVencimiento, UsuarioCache.Id_Usuario, IvaUnitario, Cantidad,
                Convert.ToDecimal(Contenido), /*TotalCantidad,*/ NumeroComprobante,
                Idproveedor, IdTipoEntrada, IdSucursal,IdEmpresa,TipoComprobante,sFolioEntrada);
            }
            
        }

        #endregion

        // FIN METODOS

        #endregion

        #region Opercion Botones

        #region Buscar

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            LimpiarTextos();
        }

        private void TxtNumComprobante_KeyPress(object sender, KeyPressEventArgs e)
        {
            Soporte.ValidarNumeros(TxtNumComprobante, e);          
            Soporte.EventoPresKeyCmbx(sender, e, CmbProveedores);
        }

        private void CmbComprobante_KeyPress(object sender, KeyPressEventArgs e)
        {
            Soporte.EventoPresKeyTxt(sender, e, TxtNumComprobante);
        }

        private void CmbProveedores_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                DtpkFechaCompr.Focus();
            }
        }

        private void DtpkFechaCompr_KeyPress(object sender, KeyPressEventArgs e)
        {
            Soporte.EventoPresKeyTxt(sender, e, TxtTotalArticulos);
        }

        private void TxtTotalArticulos_KeyPress(object sender, KeyPressEventArgs e)
        {
            Soporte.EventoPresKeyTxt(sender, e, TxtMontoTotal);
        }

        private void TxtMontoTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            Soporte.EventoPresKeyTxt(sender, e, TxtIvaTotal);
        }

        private void TxtIvaTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            Soporte.EventoPresKeyTxt(sender, e, TxtCodigo);
        }

        private void TxtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Soporte.EventoPresKeyTxt(sender, e, TxtCantidad);
        }

        private void TxtPrecioCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            Soporte.EventoPresKeyTxt(sender, e, TxtGananciaObtener);
        }

        private void TxtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            Soporte.EventoPresKeyTxt(sender, e, TxtPrecioCompra);
        }

        private void CmbTipEntradas_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (Convert.ToInt32(CmbTipEntradas.SelectedValue) == 4)
            //{
            //    PnlComprobante.Enabled = true;
            //    CmbComprobante.SelectedValue = 0;
            //    DtpkFechaCompr.Value = DateTime.Today;

            //}
            //else
            //{
            //    PnlComprobante.Enabled = false;
            //    CmbComprobante.SelectedValue = 1;
            //    DtpkFechaCompr.Value = DateTime.Today;
            //    CmbProveedores.SelectedValue = 2;

            //}
        }

        private void DgvAutoCompletar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //BuscarArticulo(TxtCodigo.Text);
            BuscarArticulo(Convert.ToString(DgvAutoCompletar.CurrentRow.Cells["Codigo"].Value));
            DgvAutoCompletar.Visible = false;
        }

        private void radMenu1_Click(object sender, EventArgs e)
        {

        }

        private void TxtGananciaObtener_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==(Char)Keys.Enter)
            {
                DtpFecheVencimiento.Focus();
            }
        }

       


        #endregion

        #region Agregar Lista

        private void AgragarLista_Click(object sender, EventArgs e)
        {
            AgregarArticulo();
        }

        #endregion

    

        private void TxtCodigo_Enter(object sender, EventArgs e)
        {
           
            Soporte.EventoEnterTxt(TxtCodigo, "Escriba lo que desea buscar...", "");
           
        }

        private void TxtCodigo_Leave(object sender, EventArgs e)
        {
           
            Soporte.EventoLeaveTxt(TxtCodigo, "Escriba lo que desea buscar...", "");
           
        }

        private void CmbEmpresas_SelectedValueChanged(object sender, EventArgs e)
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

       

       

        private void PtbxCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Eventos

        private void TxtCodigo_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtCodigo.Text))
            {
                //BuscarArticulo(TxtCodigo.Text); se pasara esto al Evento Cell_Click del DataGridView autocompletar.
                ValidarTextoCodigoABuscar(TxtCodigo.Text);

            }
            else
            {
                DgvAutoCompletar.Visible = false;
            }

        }

        #endregion
    }
}
