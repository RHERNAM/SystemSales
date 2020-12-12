using AccesDLL;
using SistemaVentas.Clases.Entidates;
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

namespace SistemaVentas.Formularios.Ventas
{
    public partial class FormAperturaVenta : Form
    {
        private DataTable DtRegistros;
        private decimal Ingresos;
        private int Id_Estado;
        private int Numero_Apertura;
        private int Numero_Caja;

        public FormAperturaVenta()
        {
            InitializeComponent();
        }

        private void FormAperturaVenta_Load(object sender, EventArgs e)
        {
           
            NumeroCaja();
        }
        private int CajaRegistros()
        {
            DtRegistros = Consulta.CajaConsulta();
            int Cajaregistros = DtRegistros.Rows.Count;
            return Cajaregistros;
        }

        private void NumeroApertura()
        {
             Numero_Apertura = Consulta.AperturaNumero();
            MemoriaCache.IdApertura = Numero_Apertura;
            
            
        }
        private void NumeroCaja()
        {
            Numero_Caja = Consulta.CajaNumero();
            MemoriaCache.IdCaja = Numero_Caja;
        }

        private void BtnAperturar_Click(object sender, EventArgs e)
        {
            try
            {           
                NumeroApertura();

                Id_Estado = 6;//6 es Aperturado y 7 es Cerrado
                Ingresos = Convert.ToDecimal(TxtSaldoCaja.Text);
       
                //if (CajaRegistros()>0)
                //{
                //    //Actualizar Caja
                //}
                //else
                //{
                //    //Insertar Caja
                //    ClsGuardar.CajaGuardar("Caja Aperturado", MemoriaCache.SerialPC, UsuarioCache.Id_Usuario,MemoriaCache.IdApertura, MemoriaCache.NombrePC, MemoriaCache.Ip);
                //}
                ClsGuardar.CajaApertura(MemoriaCache.SerialPC, UsuarioCache.Id_Usuario, MemoriaCache.IdApertura, MemoriaCache.NombrePC, MemoriaCache.Ip);
                ClsGuardar.CajaGuardaMovimientoCaja(Ingresos, UsuarioCache.Id_Usuario, Id_Estado, Numero_Apertura, Numero_Caja);
                //0 es Cancelado y 1 es Aperturado
                MemoriaCache.OpcionFormulario = 1;
                Close();
                
            }

            catch (Exception)
            {

                throw;
            }
        }      
        

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }   
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            //0 es Cancelado y 1 es Aperturado
            MemoriaCache.OpcionFormulario = 0;
            Close();
        }

        private void PtbxCerrar_Click(object sender, EventArgs e)
        {
            MemoriaCache.OpcionFormulario = 0;
            Close();
        }
    }
}
