using AccesDLL;
using SistemaVentas.Clases.Entidates;
using SistemaVentas.Clases.SQL;
using SistemaVentas.Clases.SQL.Transacciones;
using SistemaVentas.Formularios.Ventas.Tickets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVentas.Formularios.Ventas
{
    public partial class FormPuntoVenta : Form
    {
                #region Variables
      
        private ListaVenta listaVenta;
        List<ListaVenta> listaVentas = new List<ListaVenta>();
        private DataTable DtRegistros;
        private int iId_Articulo=0;
        private string sDescripcion;
        private string sCodigo;
        private string sNombreArt, NombreVenta, sUnidad_MedidaVenta;
        private string sMarca;
        private int iId_Inventario=0;
        private decimal dPrecio;
        private int iTotalArticulos=0;
        private decimal dTotalVentaVariable,
                        dTotalVentaNeto,
                        dStockDisponible,
                         dIva;
        private decimal dSubTotal;
        private decimal dCantidad=0;
        private decimal dTotalCantidad;
        private decimal dTotalPrecio;
        private decimal Recicbo_Cantidad;
        private decimal Cambio_Regresar;
        private int Id_Venta, Id_Credito, Id_Cliente;
        private decimal Faltante_Pago;
        private int iComprobante=0;
        private decimal dDescuento;
        private decimal dEfectivo;
        private decimal dCredito;
        private int iExpedicion=0, iCantidadCobros=0, Estatus=0;
        private decimal Credito_Autorizado, Credito_Acumulado, Credito_Actual;
        private byte[] FotoArticulo;
        private DataSet DsRegistros;
        private bool ValorDecimal, bContinuar;

        #endregion
        public FormPuntoVenta()
        {
            InitializeComponent();
        
        }

        private void FormPuntoVenta_Load(object sender, EventArgs e)
        {
            ConsultarVentasSuspendidos();           
            
        }


        #region Metodos        
        /* Inicio de Metodos*/

        #region Limpiar Data Gridview y Textos
        /// <summary>
        /// Limpia Todos los Controles de la Venta tanto Lista de Venta como el Data GridView
        /// </summary>
        private void LimpiarControlesVenta()
        {
            listaVentas.Clear();           
            DgvVenta.DataSource = null;            
            TxtCambioRegresar.Text = "0";
            TxtDescuento.Text = "0";
            TxtTotalArticulos.Text = "0";
            TxtCantidadRecibido.Text = "0";
            TxtTotalVenta.Text = "0";
            TxtTotalPagar.Text = "0";
            TxtSubtotal.Text = "0";
            TxtiVA.Text = "0";
            Cambio_Regresar = 0;
            PctBoxImagen.Image = Properties.Resources.AgregarGarrito;
           
            TxtDescripcion.Text="Agregue productos...";
            TxtClienteNum.Clear();
            ChkCredito.Checked = false;
            ChkEfctivo.Checked = true;
            groupBox1.Visible = false;
            LblNumVenta.Visible = false;
            LblCliente.Text = "Cliente:";
            lblSaldo_Abonado.Text = "Saldo Abonado: $ 0";
            LblSaldo_Actual.Text = "Saldo Actual: $ 0";
            LblSaldo_Autorizado.Text = "Credito: $ 0";
            lblCredito_Acumulado.Text="Saldo Acumulado: $ 0";
            MemoriaCache.ValidarVenta = "Normal";
        }

        #endregion


        private void OcultarColumasDataGrid()
        {
            DgvBuscaArtDesc.Columns["Id_Articulo"].Visible = false;
            DgvBuscaArtDesc.Columns["Codigo"].Visible = false;
            DgvBuscaArtDesc.Columns["Nombre"].Visible = false;
            DgvBuscaArtDesc.Columns["Marca"].Visible = false;
            DgvBuscaArtDesc.Columns["Presentacion"].Visible = false;
            DgvBuscaArtDesc.Columns["Unidad_Medida"].Visible = false;
            DgvBuscaArtDesc.Columns["Contenido_Neto"].Visible = false;
            DgvBuscaArtDesc.Columns["Fecha_Registro"].Visible = false;
            DgvBuscaArtDesc.Columns["Fecha_Actualizacio"].Visible = false;
            DgvBuscaArtDesc.Columns["Fabricante"].Visible = false;
            DgvBuscaArtDesc.Columns["Cantidad_Minimo"].Visible = false;
            DgvBuscaArtDesc.Columns["Cantidad_Maximo"].Visible = false;
            DgvBuscaArtDesc.Columns["Unidad_Venta"].Visible = false;
            DgvBuscaArtDesc.Columns["Foto"].Visible = false;
            DgvBuscaArtDesc.Columns["Estatus"].Visible = false;
        }

        private void ValidarTextoCodigoABuscar(string Codigo)
        {
            if (TxtBuscarCodigo.Text != "" && TxtBuscarCodigo.Text != "Ingrese Codigo...")
            {
               
                DtRegistros = Consulta.ArticuloBuscarXdescripcion(Codigo);
                DgvBuscaArtDesc.DataSource = DtRegistros;
                if (DgvBuscaArtDesc.Rows.Count>0)
                {
                    OcultarColumasDataGrid();
                    DgvBuscaArtDesc.Visible = true;
                }
                else
                {
                    DgvBuscaArtDesc.Visible = false;
                }
            }
        }

        #region Busca Articulo Para VENTA

        private bool BuscaArticuloVenta(string Codigo)
        {
           
            DtRegistros = Consulta.CodigoBuscarTblArticulo(Codigo);/*Buscamos el Articulo y Agregamos los Resultados en el DRegistros*/
            



            if (DtRegistros.Rows.Count > 0) // Si encontramos el articulo es decir el DtRegistros cuenta con registros 
            {             

                //Obtenemos los datos del Articulo Buscado y guardamos en las Variables correspondientes.
                iId_Articulo = Convert.ToInt32(DtRegistros.Rows[0]["ID_ARTICULO"].ToString());
                sDescripcion = DtRegistros.Rows[0]["Descripcion"].ToString();
                sCodigo = DtRegistros.Rows[0]["Codigo"].ToString();
                sNombreArt = DtRegistros.Rows[0]["Nombre"].ToString();
                sMarca = DtRegistros.Rows[0]["Nombre"].ToString();
                TxtDescripcion.Text = sDescripcion;
                sUnidad_MedidaVenta = DtRegistros.Rows[0]["Unidad_Venta"].ToString();

                if (DtRegistros.Rows[0]["Foto"] != DBNull.Value) //Si el Articulo Trae Foto pasamos la foto en el PicrureBox
                {
                    FotoArticulo = (byte[])DtRegistros.Rows[0]["Foto"];
                    MemoryStream ms = new MemoryStream(FotoArticulo);
                    PctBoxImagen.Image = Image.FromStream(ms);
                    PctBoxImagen.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else //Si el Articulo no Trae Foto entonces le asignamos la foto de Carrito guardado en Properties.Resources.
                {
                    PctBoxImagen.SizeMode = PictureBoxSizeMode.Zoom;
                    PctBoxImagen.Image = Properties.Resources.AgregarGarrito;

                    MemoryStream memoria = new MemoryStream();
                    PctBoxImagen.Image.Save(memoria, System.Drawing.Imaging.ImageFormat.Png);
                    FotoArticulo = memoria.GetBuffer();
                }

                return true;
            }

            return false;
        }

        #endregion


      
        #region Buscamos Articulo para Cargar a venta y Texboxt  

        /// <summary>
        /// Busca el Articulo por Codigo en la Tabla Articulos 
        /// </summary>
        /// <param name="Codigo"></param>
        /// 
        private void BuscarArticulo(string Codigo)
        {

            
            if (BuscaArticuloVenta(Codigo))
            {
                BuscarArticuloInventario(Codigo);
            }
           
           
            
        }

       

        #endregion

        #region Buscar Articulo en Inventario

        private void BuscarArticuloInventario(string Codigo)
        {
            #region Buscar Articulo en Inventario

            /*Ahora Buscamos el Articulo en el Inventario*/
            DtRegistros = Consulta.InventarioBuscaCodArt(Codigo);

            if (DtRegistros.Rows.Count > 0)//Si encontramos registros entonces Hacemos lo siguente
            {
                // Obtenemos los Valores de id del Inventario y el precio del Articulo
                iId_Inventario = int.Parse(DtRegistros.Rows[0]["Id_Inventario"].ToString());
                dPrecio = decimal.Parse(DtRegistros.Rows[0]["Precio_Venta"].ToString());
                dStockDisponible = Convert.ToDecimal(DtRegistros.Rows[0]["Total_Inventario"].ToString());


                AgregarLista(); //Obtenido los datos Agregamos el Articulo a la Lista para posterior Mostrarlo en el DataGridView                    
               
               
                TxtBuscarCodigo.Text = "";
                TxtCantidadArt.Text = "0";
                dCantidad = 0;
                TxtBuscarCodigo.ForeColor = Color.Black;
                TxtBuscarCodigo.Focus();


            }
            else
            {
                Soporte.Msg_Alerta("No hay stock en el Inventario");
                TxtBuscarCodigo.Text = "";
                TxtCantidadArt.Text = "0";
                dCantidad = 0;
                TxtBuscarCodigo.ForeColor = Color.Black;
                TxtBuscarCodigo.Focus();

            }

            #endregion
        }
        #endregion

        #region Posicion Fila

        /// <summary>
        /// Busca el Codigo del articulo en el Data Gridview y seleccioena toda la fila, se ejecuta cuando se busca Articulo para venta
        /// </summary>
        private void PosicionFila()
        {
            string searchValue = sCodigo;
            int rowIndex = -1;

            DgvVenta.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
            {
                foreach (DataGridViewRow row in DgvVenta.Rows)
                {
                    if (row.Cells["Codigo"].Value.ToString().Equals(searchValue))
                    {
                        rowIndex = row.Index;
                        DgvVenta.CurrentCell = DgvVenta.Rows[rowIndex].Cells[0];
                        DgvVenta.Rows[DgvVenta.CurrentCell.RowIndex].Selected = true;
                        PasarDatosDGVTextbox();
                        break;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }
        #endregion

        #region Suma Los Articulos en DataGird
        /// <summary>
        /// Suma Los Articulos Agregados en el DataGridView y Oculta Columnas 
        /// </summary>
        private void SumarArticulosDataGrid()
        {
            iTotalArticulos = 0;
            dTotalVentaVariable = 0;
            dTotalVentaNeto = 0;
            foreach (DataGridViewRow Sumar in DgvVenta.Rows)
            {
                dTotalVentaVariable += Convert.ToDecimal(Sumar.Cells["Total"].Value);
                dTotalVentaNeto += Convert.ToDecimal(Sumar.Cells["Total"].Value);
                iTotalArticulos += Convert.ToInt32(Sumar.Cells["Cantidad"].Value);
            }
            TxtTotalArticulos.Text = Convert.ToString(iTotalArticulos);
            TxtTotalPagar.Text = Convert.ToString(string.Format("{0:#,#0.00}", dTotalVentaVariable));
            TxtTotalVenta.Text = Convert.ToString(string.Format("{0:#,#0.00}", dTotalVentaVariable));
            dSubTotal = dTotalVentaVariable / Convert.ToDecimal(1.16);
            dIva = dTotalVentaVariable - dSubTotal;
            TxtSubtotal.Text = Convert.ToString(string.Format("{0:#,#0.00}", dSubTotal));
            TxtiVA.Text = Convert.ToString(string.Format("{0:#,#0.00}", dIva));

            #region Personalizar DataGirdView
            DgvVenta.Columns["Id_Articulo"].Visible = false;
            DgvVenta.Columns["Id_Inventario"].Visible = false;
            //DgvVenta.Columns["Foto"].Visible = false;
            DgvVenta.Columns["Descripcion"].Width = 300;
            DgvVenta.Columns["Precio"].DefaultCellStyle.Format = "C2";
            DgvVenta.Columns["Total"].DefaultCellStyle.Format = "C2";
            DgvVenta.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DgvVenta.Columns["Precio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DgvVenta.Columns["Cantidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            #endregion
        }
        #endregion

        #region Calcula el Precio Cantidad para venta
       /// <summary>
       /// Calcula el Precio de del Articulo por la cantidad que se Vende
       /// </summary>
        private void CalculoPrecioCantidad()
        {
            switch (sUnidad_MedidaVenta)
            {
                case "Kilogramos":
                    dTotalPrecio = (dCantidad * dPrecio / 1000);                   

                    break;
                case "Pieza":
                    dTotalPrecio = dPrecio * dCantidad;

                    break;

            }
        }
        #endregion



        private bool ValidarEsDecimal()
        {
            //string valor=TxtCantidadArt.Text;
            //char[] cadenavalor = valor.ToCharArray();
            //for (int i = 0; i < cadenavalor.Length; i++)
            //{
            //    if (cadenavalor[i]=='.')
            //    {
            //        return true;

            //    }

            //}
            //return false;

            bool esEntero = Int32.TryParse(TxtCantidadArt.Text, out _);
            if (esEntero)
            {
               
                return true;
            }
            else
            {
               
                return false;
            }



        }

        #region Asignar Valor la Cantidad del Articulo Solicitado
        /// <summary>
        /// Asignamos el valor a La varaible cantidad, este se  obtiene del TextBox TxtCantidadArt
        /// </summary>
        private bool AsignarValorCantidadArticulo()
        {
            if (dCantidad==0)
            {
                if (string.IsNullOrEmpty(TxtCantidadArt.Text) || TxtCantidadArt.Text == "0")
                {
                    dCantidad = 1;
                    return true;
                }
                else
                {
                    if (sUnidad_MedidaVenta == "Pieza")
                    {
                        if (!ValidarEsDecimal())
                        {
                            dCantidad = 1;
                            
                        }
                        else
                        {
                            dCantidad = Convert.ToDecimal(TxtCantidadArt.Text);
                            
                        }

                        return true;
                    }
                    else
                    {
                        if (Soporte.ValidarFormatoMoneda(TxtCantidadArt.Text))
                        {
                            dCantidad = Convert.ToDecimal(TxtCantidadArt.Text);
                            return true;
                        }
                        else
                        {
                            Soporte.MsgError("Error en los Datos ingresados de la cantidad");
                            return false;
                        }
                    }                    
                }               
            }
            else
            {
                if (sUnidad_MedidaVenta =="Pieza")
                {
                    if (!ValidarEsDecimal())
                    {
                        dCantidad = 1;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (Soporte.ValidarFormatoMoneda(TxtCantidadArt.Text))
                    {
                        dCantidad += Convert.ToDecimal(TxtCantidadArt.Text);
                        return true;
                    }
                    else
                    {
                        Soporte.MsgError("Error en los Datos ingresados de la cantidad");
                        return false;
                    }                    
                }              
            }  
        }

        #endregion

        #region Agregar a Lista y Cargar al DataGridview
        /// <summary>
        /// Va a Validar y despues Agregar a Lista y Cargar al DataGridview
        /// </summary>
        public void AgregarLista()
        {
            if (ValidarArticulo())//Validamos primero el Articulo si ya se encuenta en el DataGridView
            {

            }
            else
            {
                if (AsignarValorCantidadArticulo())
                {
                    if (dCantidad > dStockDisponible)
                    {
                        Soporte.MsgInformacion("No cuenta con suficiente stock y solo se agregaran: " + dStockDisponible.ToString());
                        dCantidad = dStockDisponible;
                    }
                    dTotalPrecio = dPrecio * dCantidad;
                    //CalculoPrecioCantidad();
                    listaVenta = new ListaVenta(sCodigo, iId_Inventario, iId_Articulo, sDescripcion, dCantidad, dPrecio, dTotalPrecio, FotoArticulo);
                    listaVentas.Add(listaVenta);
                    DgvVenta.DataSource = null;
                    DgvVenta.DataSource = listaVentas;
                    SumarArticulosDataGrid();// Agregado el Articulo y Mostrado en el DataGridView Sumamos los datos para obtener el Total de Venta a Pagar
                    PosicionFila(); //Despues seleccionamos la Fila donde se enuentra el Articulo que se Agrego al DataGridView
                }    
            }
        }
        #endregion

        #region Validacion de Articulo si esta en el DataGridView
        /// <summary>
        ///  Validacion de Articulo si esta en el DataGridView
        /// </summary>
        /// <returns></returns>
        private bool ValidarArticulo()
        {
            if (DgvVenta.Rows.Count > 0)
            {
                for (int i = 0; i < DgvVenta.Rows.Count; i++)
                {
                    string sCdogio2 = DgvVenta.Rows[i].Cells["Codigo"].Value.ToString();
                    if (sCodigo == sCdogio2)
                    {
                       decimal dCantidad2 =Convert.ToDecimal(DgvVenta.Rows[i].Cells["Cantidad"].Value.ToString());

                        AsignarValorCantidadArticulo();//Asignamos Valor Cantidad


                        dTotalCantidad = dCantidad + dCantidad2; //Convert.ToDecimal(TxtCantidadArt.Text);
                        if (dTotalCantidad > dStockDisponible)
                        {
                            dTotalCantidad = dStockDisponible;
                            Soporte.MsgInformacion("Ya no cuenta con suficiente Stock, revise su inventario");

                        }

                        dTotalPrecio = dPrecio * dTotalCantidad;
                        DgvVenta.Rows[i].Cells["Cantidad"].Value = dTotalCantidad;
                        DgvVenta.Rows[i].Cells["Total"].Value = dTotalPrecio;
                        return true;
                    }
                }

                SumarArticulosDataGrid();// Agregado el Articulo y Mostrado en el DataGridView Sumamos los datos para obtener el Total de Venta a Pagar
                PosicionFila(); //Despues seleccionamos la Fila donde se enuentra el Articulo que se Agrego al DataGridView
            }
            return false;
        }

        #endregion

        #region Numero Consecutivo

        private void NumeroConsecutivoVenta()
        {
            MemoriaCache.NumVenta = Consulta.Venta_Numero();
            Id_Venta = MemoriaCache.NumVenta;
            LblNumVenta.Text = "Num. Venta: " + Convert.ToString(Id_Venta);
           

        }

        #endregion

        #region Numero Credito

        private void NumeroCredito()
        {
            Id_Credito = Consulta.Credito_Numero();
            LblNumCredito.Text = "Num Credito";
        }
        #endregion

        #region Credito Consulta NumCliente

        /// <summary>
        /// Consulta el Credito del Cliente y obtenemos los valores de su Esatdo de Cuenta
        /// </summary>
        /// <param name="idCliente"></param>
        private void CreditoConsultaNumCliente(int idCliente)
        {
            DtRegistros = Consulta.Credito_ConsultaNumCliente(idCliente);
            if (DtRegistros.Rows.Count>0)
            {
                Id_Credito= Convert.ToInt32(DtRegistros.Rows[0]["Id_Credito"].ToString());
                Id_Cliente =Convert.ToInt32(DtRegistros.Rows[0]["Id_Cliente"].ToString());
                LblCliente.Text = "Cliente: " + DtRegistros.Rows[0]["Cliente"].ToString();
                LblSaldo_Autorizado.Text = "Credito: $ " + DtRegistros.Rows[0]["Saldo_Autorizado"].ToString();
                lblCredito_Acumulado.Text= "Saldo Acumulado: $ " + DtRegistros.Rows[0]["Credito_Acumulado"].ToString();
                LblSaldo_Actual.Text = "Saldo Actual: $ " + DtRegistros.Rows[0]["Saldo_Actual"].ToString();
                lblSaldo_Abonado.Text = "Saldo Abonado: $ " + DtRegistros.Rows[0]["Saldo_Abonado"].ToString();
                Credito_Autorizado=Convert.ToDecimal(DtRegistros.Rows[0]["Saldo_Autorizado"]);
                Credito_Acumulado = Convert.ToDecimal(DtRegistros.Rows[0]["Credito_Acumulado"]);
                Credito_Actual = Convert.ToDecimal(DtRegistros.Rows[0]["Saldo_Actual"]);



            }
            else
            {
                Id_Credito = 0;
                Id_Cliente = 0;
                LblCliente.Text = "Cliente:Sin Registro";
                lblSaldo_Abonado.Text = "Saldo Abonado: $ 0";
                LblSaldo_Actual.Text = "Saldo Actual: $ 0";
                LblSaldo_Autorizado.Text = "Credito: $ 0";
                lblCredito_Acumulado.Text = "Saldo Acumulado: $ 0";
                Soporte.MsgError(" No se encontraro el Numero de Cliente, revisar que se encuentre en la Base de Datos.", "Cliente Sin Registro");
            }

        }

        #endregion

        #region Impresion de Ticket        

        private void ImprimirTicket()
        {
            DataSet dataSet = Consulta.Ticket_VentaDetalle(Id_Venta);       
            TicketVenta ticketVenta = new TicketVenta();
            ticketVenta.SetDataSource(dataSet);        

            FormTicketVenta formTicketVenta = new FormTicketVenta();
            formTicketVenta.VistaPrevia(ticketVenta);
            formTicketVenta.ShowDialog();
        }

        private void ImprimirTicketCredito()
        {
            DataSet dataSet = Consulta.Ticket_Credito(Id_Venta,Id_Cliente);
            CryTicketCredito ticketCredito = new CryTicketCredito();
            ticketCredito.SetDataSource(dataSet);

            FormTicketCredito formTicketCredito = new FormTicketCredito();
            formTicketCredito.VistaPrevia(ticketCredito);
            formTicketCredito.ShowDialog();
        }

        #endregion

        #region Validacion de PAGO      
        /// <summary>
        /// Validamos si El Pago va a Ser en Efectivo o CRedito
        /// </summary>
        private void ValidarPago()
        {
            if (ChkCredito.Checked == true && ChkEfctivo.Checked==false)
            {
                #region Credito
                //if (Soporte.Msg_Dialog("La cantidad de efectivo a Cobrar no debe se Cero, Desea el Cobro a Credito?") == DialogResult.Yes)
                //{

                if (Credito_Actual<dTotalVentaVariable)
                {
                    Soporte.MsgError("No cuenta con credito suficiente, Solicitar Abono a la Cuenta para poder continuar.");
                }
                else
                {
               

                    dCredito = dTotalVentaVariable;
                    if (ChkCredito.Checked == true && dCredito > 0 && Id_Cliente > 0)
                    {
                        iCantidadCobros += 1;

                        if (iCantidadCobros == 1)
                        {   //Obtenemos el numero de la Venta consecutivo y este meto guardara el numero de venta en la variable 
                            if (MemoriaCache.ValidarVenta!="Suspendido")
                            {
                                NumeroConsecutivoVenta();
                            }                           
                            AsignarValoresVenta();
                            dEfectivo = 0;
                            Estatus = 4;
                            FinalizarVenta();
                        }
                        ClsGuardar.CreditoGuardar(Id_Cliente, Id_Venta, dCredito, Id_Credito);
                        //FinalizarVenta();
                        LimpiarControlesVenta();
                        ImprimirTicketCredito();
                        iCantidadCobros = 0;

                    }
                    else
                    {
                        Soporte.MsgError("Busque el Numero de cliente al cual se cargara la cuenta.", "Error Datos Cliente");
                    }
                }
                
                //}
                #endregion

            }
            else
            {
                if (ChkEfctivo.Checked == true && ChkCredito.Checked == false)
                {

                    #region Efectivo
                    /*Pago en Efectivo*/
                    /* Primero Validamos que la caja de texto TxtCantidadRecibido no este vacio, si esta vacio enviamos un
                     * mensaje de que tiene que ingresar la cantidad a recobir */
                    if (string.IsNullOrEmpty(TxtCantidadRecibido.Text))
                    {
                        Soporte.MsgError("No ha ingresado la Cantidad de Efectivo Recibido");
                        TxtCantidadRecibido.Focus();
                    }
                    else /*Si caja de texto TxtCantidadRecibido no esta vacio ahora validaremos la cantidad que recibimos*/
                    {

                        //Pasamos el valor de la caja de texto TxtCantidadRecibido a la variable Recicbo_Cantidad esto para vaidarlo.
                        Recicbo_Cantidad = Convert.ToDecimal(TxtCantidadRecibido.Text);

                        //Si la cantidad que Recibimos de Efectivo es mayor a cero entonces continuamos validando si no es asi enviamos un mensaje.
                        if (Recicbo_Cantidad > 0)
                        {

                            /* Contara cuantas veces se ha dado click al Boton Cobrar,
                             * al dar el primer click la variable iCantidadCobros tomara como valor 1 y guardara la venta y el detalle de la venta
                             * cuando se vuelva a dar click la cantidad de cobrar  la variable iCantidadCobros ahora valdra 2 es deccir la venta ya no tiene
                             * que ser guardada nuevamente por que ya se guardo al dar el primer click
                             */
                            
                            iCantidadCobros += 1;

                            if (iCantidadCobros == 1)
                            {   //Obtenemos el numero de la Venta consecutivo y este meto guardara el numero de venta en la variable 
                                if (MemoriaCache.ValidarVenta != "Suspendido")
                                {
                                    NumeroConsecutivoVenta();
                                }
                                AsignarValoresVenta();
                                Estatus = 4;
                                FinalizarVenta();                              
                            }

                            //Si la cantidad que recibimos para cobrar la venta es mayor a la Venta a combra entonces quiere decir que tenemos que regresar el cambio al cliente
                            //Por lo tanto la venta finalizara y guardamos el detalle del pago Limpiamos los controles y despues imprimimos el ticket
                            if (Recicbo_Cantidad > dTotalVentaVariable)
                            {
                                Cambio_Regresar = Recicbo_Cantidad - dTotalVentaVariable;
                                if (Cambio_Regresar>=0)
                                {
                                    dTotalVentaVariable = 0;
                                }
                                TxtCambioRegresar.Text = Convert.ToString(Cambio_Regresar);
                                //Guarda el dellate de PAGO
                                ClsGuardar.PagosVentaDetalle(Id_Venta, dTotalVentaNeto, Recicbo_Cantidad, dTotalVentaVariable, Cambio_Regresar, UsuarioCache.Id_Usuario);


                               // FinalizarVenta();

                                if (Cambio_Regresar != 0)
                                {
                                    Soporte.MsgInformacion("Regresar Cambio al Cliente Cantidad:" + Cambio_Regresar);
                                }

                                LimpiarControlesVenta();
                                ImprimirTicket();
                                iCantidadCobros = 0;
                               


                            }
                            //Pero si la cantidad que recibimos es menor a la venta a cobrar entonces Hacemos una resta a la venta por la cantidad que recibimos
                            //Y nos quedara el nuevo monto a cobrar es decir el resto y despues guardamos el Pago realizado y el faltante pendiente a cobrar
                            else 
                            {
                                Faltante_Pago = dTotalVentaVariable - Recicbo_Cantidad;
                                TxtTotalPagar.Text = Convert.ToString(Faltante_Pago);
                                dTotalVentaVariable = Faltante_Pago;
                                ClsGuardar.PagosVentaDetalle(Id_Venta,dTotalVentaNeto, Recicbo_Cantidad, dTotalVentaVariable, Cambio_Regresar, UsuarioCache.Id_Usuario);
                                TxtCantidadRecibido.Text = "0";
                            }
                        }
                        else //cuando la cantidad que recibimos es igual a cero entonces enviamos mensaje es decir que no recibimos nada de efectivo por lo contrario no podemos continuar
                        {
                            Soporte.MsgError("Ingrese una cantidad Mayor a Cero '0'..");
                        }

                    }

                    #endregion
                }

            }



        }

        #endregion

        #region Asignar de Venta
        private void AsignarValoresVenta()
        {
            iComprobante = 3; // el el id de la Tabla Catalgo_Comprobantes que el 3 es Tikets
            dDescuento = Convert.ToDecimal(TxtDescuento.Text);
            dSubTotal = Convert.ToDecimal(TxtSubtotal.Text);
            dIva = Convert.ToDecimal(TxtiVA.Text);
            dTotalVentaVariable = Convert.ToDecimal(TxtTotalVenta.Text);
            iExpedicion = 1;
            dEfectivo = Convert.ToDecimal(TxtTotalPagar.Text);
            iTotalArticulos = Convert.ToInt32(TxtTotalArticulos.Text);
            Estatus = 4;
            NombreVenta = "Venta Finalizado";
            Id_Venta = MemoriaCache.NumVenta;

        }
        #endregion

        #region Realizar Venta       

        private void FinalizarVenta()        {
            
            //Guarda La Venta
            ClsGuardar.VentaGuarda(Id_Venta, UsuarioCache.Id_Usuario, iComprobante, dDescuento, dSubTotal, dIva, dTotalVentaVariable, iExpedicion, dEfectivo, dCredito,iTotalArticulos, Estatus, NombreVenta);
           
            //Recorremos el DataGrid y Guardamos los Articulos
            for (int i = 0; i < DgvVenta.Rows.Count; i++)
            {
                iId_Inventario = Convert.ToInt32(DgvVenta.Rows[i].Cells["Id_Inventario"].Value);
                dCantidad = Convert.ToInt32(DgvVenta.Rows[i].Cells["Cantidad"].Value);
                dPrecio = Convert.ToDecimal(DgvVenta.Rows[i].Cells["Precio"].Value);

                dTotalPrecio = dPrecio * dCantidad; 
                //Guarda los Articulos de la Venta 
                ClsGuardar.VentaDetalleGuarda(Id_Venta, iId_Inventario, dCantidad, dPrecio, dDescuento, dTotalPrecio,Estatus);                

            }

            ConsultarVentasSuspendidos();
        }

        #endregion

        #region Eliminar Articulo de la Lista de Venta
        /// <summary>
        /// Metodo para eliminar el Articulo de la Lista Venta
        /// </summary>
        /// <param name="idarticulo"></param>
        private void EliminarArt(int idarticulo)
        {
            foreach (var articulo in listaVentas)
            {
                if (articulo.Id_Articulo == idarticulo)
                {
                    listaVentas.Remove(articulo);

                    break;
                }
            }
        }

        /// <summary>
        /// Elimina el Articulo de la Lista y del DataGridView y Suma los ARticulo Nuevamente con los Nuevo Registros
        /// </summary>
        private void EliminaArticuloDataGridLista()
        {
            int Registro = Convert.ToInt32(DgvVenta.Rows[DgvVenta.CurrentRow.Index].Cells["Id_Articulo"].Value);
            int NumInventario = Convert.ToInt32(DgvVenta.Rows[DgvVenta.CurrentRow.Index].Cells["Id_Inventario"].Value);

            if (MemoriaCache.ValidarVenta=="Suspendido")
            {
                Eliminar.VentaDetalleEliminarArticulo(Id_Venta, NumInventario);
            }          

            EliminarArt(Registro);
            DgvVenta.DataSource = null;
            DgvVenta.DataSource = listaVentas;
            if (listaVentas.Count == 0)
            {
                TxtDescripcion.Text = "Agregar productos...";
                PctBoxImagen.Image = Properties.Resources.AgregarGarrito;

            }
            SumarArticulosDataGrid();
        }


       
        #endregion

        #region Pasar Datos del DataGridView a TextBox
        /// <summary>
        /// Pasa los datos de la Descripcion del Articulo a textbox y a la Imagen esto cuando se hace clik en el Articulo
        /// </summary>        
        private void PasarDatosDGVTextbox()
        {

          
                if (DgvVenta.Rows.Count > 0)
                {
                    TxtDescripcion.Text = Convert.ToString(DgvVenta.CurrentRow.Cells["Descripcion"].Value);
                    // FotoArticulo = (byte[])DgvVenta.CurrentRow.Cells["Foto"].Value;
                    if (DgvVenta.CurrentRow.Cells["Foto"].Value == DBNull.Value)
                    {
                        PctBoxImagen.SizeMode = PictureBoxSizeMode.Zoom;
                        PctBoxImagen.Image = Properties.Resources.producto__2_;
                    }
                    else
                    {
                        FotoArticulo = (byte[])DgvVenta.CurrentRow.Cells["Foto"].Value;
                        if (FotoArticulo!=null)
                        {
                            MemoryStream memoryStream = new MemoryStream(FotoArticulo);
                            PctBoxImagen.Image = Image.FromStream(memoryStream);
                            PctBoxImagen.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                       
                    }

                }
            

            

        }
        #endregion


        /* FIN DE METODOS*/
        #endregion


        #region Eventos 

        /* INICIO DE EVENTOS*/

        private void TxtBuscarCodigo_TextChanged(object sender, EventArgs e)
        {
            if (TxtBuscarCodigo.Text != "" && TxtBuscarCodigo.Text != "Ingrese Codigo...")
            {
                if (ChkBuscarArticulo.Checked==true)
                {
                    ValidarTextoCodigoABuscar(TxtBuscarCodigo.Text);
                }
                else
                {
                    DgvBuscaArtDesc.Visible = false;
                    BuscarArticulo(TxtBuscarCodigo.Text);
                }
                  
               
            }
            else
            {
                DgvBuscaArtDesc.Visible = false;
            }
        }


        private void TxtBuscarCodigo_Enter(object sender, EventArgs e)
        {
            if (ChkBuscarArticulo.Checked==true)
            {
                Soporte.EventoEnterTxt(TxtBuscarCodigo, "Escriba lo que desea buscar...", "");
            }
            else
            {
                Soporte.EventoEnterTxt(TxtBuscarCodigo, "Ingrese Codigo...", "");
            }
            
        }
        private void TxtBuscarCodigo_Leave(object sender, EventArgs e)
        {
            if (ChkBuscarArticulo.Checked==true)
            {
                Soporte.EventoLeaveTxt(TxtBuscarCodigo, "Escriba lo que desea buscar...", "");
            }
            else
            {
                Soporte.EventoLeaveTxt(TxtBuscarCodigo, "Ingrese Codigo...", "");
            }
           
        }
       
        private void ChkCredito_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkCredito.Checked == true)
            {
                ChkEfctivo.Checked = false;
                groupBox1.Visible = true;
                TxtCantidadRecibido.ReadOnly = true;
                TxtClienteNum.Focus();
                
            }
            else
            {
                ChkEfctivo.Checked = true;
                groupBox1.Visible = false;
                TxtCantidadRecibido.ReadOnly = false;
            }

        }

        private void ChkEfctivo_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkEfctivo.Checked == true)
            {
                ChkCredito.Checked = false;
                groupBox1.Visible = false;
                TxtCantidadRecibido.ReadOnly = false;
            }
            else
            {
                ChkCredito.Checked = true;
                groupBox1.Visible = true;
                TxtCantidadRecibido.ReadOnly = true;
            }

        }

        #region Elimina articulo del DataGridView
        /// <summary>
        /// En el DataGridView Obtenemos la Posicion de la Fila donde se encuentre selecionado y llamamos el Metodo  EliminarArt(Registro); enviando el registro obtenido
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (DgvVenta.Rows.Count>0)
            {                
               EliminaArticuloDataGridLista();              
                
            }
        }
        #endregion    

        private void PtbxCerrar_Click(object sender, EventArgs e)
        {            
            
            MemoriaCache.OpcionFormulario = 0;
            Close();
        }

        private void BtnCerrarTurno_Click(object sender, EventArgs e)
        {
            //ClsGuardar.CajaCerrarTurno(UsuarioCache.Id_Usuario, MemoriaCache.IdCaja, MemoriaCache.IdApertura);
            MemoriaCache.OpcionFormulario = 2;
            Close();
        }

        private void DgvVenta_SelectionChanged(object sender, EventArgs e)
        {
           PasarDatosDGVTextbox();
        }

        private void DgvVenta_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            PasarDatosDGVTextbox();

        }

        private void BtnVentaSuspendidos_Click(object sender, EventArgs e)
        {
            PanelDerecha.Visible = false;
            PanelSubContenedorVentas.Visible = false;
            ConsultarVentasSuspendidos();
         
        }

        /* FIN DE EVENTOS*/

        #endregion

        #region Operacion de Venta Suspendido
       /*INICIO DE OPERACION VENTA SUSPENCION*/
        

        #region Consultar Ventas Suspendidos

        private void ConsultarVentasSuspendidos()
        {
            DtRegistros = Consulta.VentaSuspendidos(UsuarioCache.Id_Usuario);
            DgvVentasSuspendidos.DataSource = DtRegistros;
            if (DgvVentasSuspendidos.Rows.Count>0)
            {
                LblTotalSuspenciones.Text = DgvVentasSuspendidos.Rows.Count.ToString();
            }
            else
            {
                LblTotalSuspenciones.Text = "0";
            }
           
        }

        #endregion

        private void BtnRegresar_Click(object sender, EventArgs e)
        {

            Regresar_A_PuntoVenta();
           
        }

       

       

        private void DgvBuscaArtDesc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            BuscarArticulo(Convert.ToString(DgvBuscaArtDesc.CurrentRow.Cells["Codigo"].Value));
        }

        private void ChkBuscarArticulo_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkBuscarArticulo.Checked == true)
            {
                label6.Text = "Descripción:";
                TxtBuscarCodigo.Text = "Escriba lo que desea buscar...";
            }
            else
            {
                label6.Text = "Codigo:";
                TxtBuscarCodigo.Text = "Ingrese Codigo...";
            }
        }

        private void TxtCantidadArt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Soporte.ValidarFormatoMoneda(TxtCantidadArt.Text))
            {

            }
            
        }

        private void Regresar_A_PuntoVenta()
        {
            PanelDerecha.Visible = true;
            PanelSubContenedorVentas.Visible = true;
        }

     

        private void BtnSuspenderVenta_Click(object sender, EventArgs e)
        {
            PanelSuspenderVenta.Visible = true;
            if (PanelAplicarDescuento.Visible==true)
            {
                PanelAplicarDescuento.Visible = false;
            }
        }

        private void BtnCancelSuspenderVenta_Click(object sender, EventArgs e)
        {
            PanelSuspenderVenta.Visible = false;
        }

        #region Guarda la Venta Suspendido
       /// <summary>
       /// Guarda la Venta suspendido asi como tambien el datalle de la Venta este se guarda con estatus Supendido
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void BtnAplicarSuspencionVenta_Click(object sender, EventArgs e)
        {
            if (MemoriaCache.ValidarVenta!="Suspendido")
            {
                NumeroConsecutivoVenta();
            }           

            AsignarValoresVenta();
            Estatus = 8;
            NombreVenta = TxtNombreVenta.Text;
            dEfectivo = 0;
            FinalizarVenta();
            LimpiarControlesVenta();
            PanelSuspenderVenta.Visible = false;
            TxtNombreVenta.Clear();
            ConsultarVentasSuspendidos();

        }

        #endregion

        private void BtnReanudarVentaSuspendido_Click(object sender, EventArgs e)
        {
            ReanudarVentaSuspendido();

        }

        #region Reanudar Venta Suspendido     
        /// <summary>
        /// Pasa la Venta Suspendido a la Lista de Venta y despues lo Carga en el DataGridview esto para ser Cobrado
        /// </summary>
        private void ReanudarVentaSuspendido()
        {
            LimpiarControlesVenta();
            DataTable DtVensaSuspendidos = new DataTable();
            MemoriaCache.NumVenta = Convert.ToInt32(DgvVentasSuspendidos.CurrentRow.Cells["Numero_Venta"].Value);
            Id_Venta = MemoriaCache.NumVenta;
            MemoriaCache.ValidarVenta = "Suspendido";
            DsRegistros = Consulta.VentaReanudarVentaSuspendido(Id_Venta);
            DtVensaSuspendidos = DsRegistros.Tables[0];
            
            if (DtVensaSuspendidos.Rows.Count > 0)
            {  
                for (int i = 0; i < DtVensaSuspendidos.Rows.Count; i++)
                {
                    sCodigo = DtVensaSuspendidos.Rows[i]["Codigo"].ToString();
                    iId_Inventario = Convert.ToInt32(DtVensaSuspendidos.Rows[i]["Id_Inventario"].ToString());
                    iId_Articulo = Convert.ToInt32(DtVensaSuspendidos.Rows[i]["Id_Articulo"].ToString());
                    sDescripcion = DtVensaSuspendidos.Rows[i]["Descripcion"].ToString();
                    dCantidad = Convert.ToDecimal(DtVensaSuspendidos.Rows[i]["Cantidad"].ToString());
                    dPrecio = Convert.ToDecimal(DtVensaSuspendidos.Rows[i]["Precio"].ToString());
                    dTotalPrecio = Convert.ToDecimal(DtVensaSuspendidos.Rows[i]["Total"].ToString());

                    BuscaArticuloVenta(sCodigo); 
                    AgregarLista();
                }

                Regresar_A_PuntoVenta();
                SumarArticulosDataGrid();
                dCantidad = 0;
            }

        }

        #endregion

        /* FIN DE OPERACION VENTA SUSPENCION*/
        #endregion

        private void BtnCancelarVenta_Click(object sender, EventArgs e)
        {
            LimpiarControlesVenta();
           

        }

        private void BtnAplicarDescuento_Click(object sender, EventArgs e)
        {
            PanelAplicarDescuento.Visible = true;

            if (PanelSuspenderVenta.Visible==true)
            {
                PanelSuspenderVenta.Visible = false;
            }
        }

        private void BtnCanelDescuento_Click(object sender, EventArgs e)
        {
            PanelAplicarDescuento.Visible = false;
        }

   
        private void TxtClienteNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtClienteNum.Text))
            {
                CreditoConsultaNumCliente(Convert.ToInt32(TxtClienteNum.Text));
            }
        }

        private void BtnCobrar_Click(object sender, EventArgs e)
        {
            if (DgvVenta.Rows.Count > 0)
            {
                ValidarPago();
            }
            else
            {
                Soporte.MsgError("Ingrese almenos un Articulo para poder combrar...");               
            }
        }
    }
}
