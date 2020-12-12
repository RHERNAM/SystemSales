using AccesDLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using SistemaVentas.Clases.SQL;
using SCL;
using SistemaVentas.Formularios.Configuraciones;
using SistemaVentas.Clases.Entidates;

namespace SistemaVentas.Formularios.Sistemas.FormSoport.Recuperacion
{
    public partial class FormRecuperarContraseña : Form
    {
        private string Contraseña, Correo, NombreCompleto, Usuario;
        private DataTable DtRegistro;
        private int Nomina;

        public FormRecuperarContraseña()
        {
            InitializeComponent();
            Nomina = 0;
        }

        private void FormRecuperarContraseña_Load(object sender, EventArgs e)
        {

        }

        private void TxtNomina_KeyPress(object sender, KeyPressEventArgs e)
        {
            Soporte.ValidarNumeros(TxtNomina, e);
        }

        private void TxtNomina_Enter(object sender, EventArgs e)
        {
            Soporte.EventoEnterTxt(TxtNomina, "Ingrese nomina...", "");
        }

        private void TxtNomina_Leave(object sender, EventArgs e)
        {
            Soporte.EventoLeaveTxt(TxtNomina, "Ingrese nomina...", "");
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty( TxtNomina.Text) && TxtNomina.Text!= "Ingrese nomina...")
            {
                
                Nomina = Convert.ToInt32(TxtNomina.Text);

                DtRegistro = Consulta.EmpleadoEnviarCorreo(Nomina);
                if (DtRegistro.Rows.Count > 0)
                {
                    Correo = DtRegistro.Rows[0]["Correo"].ToString();
                    if (Soporte.ValidarMail(Correo))
                    {
                        Contraseña = Encryption.Decrypt(DtRegistro.Rows[0]["Contraseña"].ToString());
                        NombreCompleto = DtRegistro.Rows[0]["Nombre"].ToString();
                        Usuario = DtRegistro.Rows[0]["Usuario_Name"].ToString();

                        //Proceso de Enviar Correo
                        richTextBox1.Text = richTextBox1.Text.Replace("@Contradeseña", Contraseña);
                        richTextBox1.Text = richTextBox1.Text.Replace("@Empleado", NombreCompleto);

                        EnviarCorreo("systemssoporterh@gmail.com", "38641rene", richTextBox1.Text, "Recuperacion", Correo, "");
                        Close();

                    }
                }
                else
                {
                    Soporte.MsgError("No se encontraron datos con este numero de nomina", "Sin Registros");
                }
            }
            else
            {
                Soporte.Msg_Alerta("Igrese el numero de nomina");
            }
            
           
        }

        internal void EnviarCorreo(string emisor, string contraseña, string mensaje, string asunto, string destinatario, string ruta)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                MailMessage correos = new MailMessage();
                SmtpClient envios = new SmtpClient();
                correos.To.Clear();
                correos.Body = "";
                correos.Subject = "";
                correos.Body = mensaje;
                correos.Subject = asunto;
                correos.IsBodyHtml = true;
                correos.To.Add(destinatario);
                correos.From = new MailAddress(emisor);
                envios.Credentials = new NetworkCredential(emisor, contraseña);

                envios.Host = "smtp.gmail.com";
                envios.Port = 587;
                envios.EnableSsl = true;

                envios.Send(correos);
                Cursor = Cursors.Default;
                FormInformacion formInformacion = new FormInformacion();
                MemoriaCache.Mensaje = "Se ha enviado su contraseña al correo " + destinatario + " ingrese ahora mismo y verificalo.";
                formInformacion.ShowDialog();
                //Soporte.MsgInformacion("Se ha enviado su contraseña al correo " + destinatario + " ingrese ahora mismo y verificalo.");
            }
            catch (Exception ex )
            {

                Soporte.MsgError(ex.Message);
            }
           

        }




        private void button1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
