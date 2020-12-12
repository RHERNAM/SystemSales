using AccesDLL;
using SistemaVentas.Clases.SQL;
using SistemaVentas.Clases.SQL.Conexion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SistemaVentas.Formularios.Sistemas
{
    public partial class FormConexionSQL : Form
    {
        private AES aes = new AES();
        private string CadenaConexion;
        public FormConexionSQL()
        {
            InitializeComponent();
        }

        private void BtnCadena_Click(object sender, EventArgs e)
        {
            SavetoXML(aes.Encrypt(TxtConexionCadena.Text, Desencryptacion.appPwdUnique, int.Parse("256")));
            mostrar();

        }
        private void mostrar()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString =SqlConexion.ConexionString;
                con.Open();

                da = new SqlDataAdapter("Sp_Consulta_Empleados", con);




                da.Fill(dt);
                datalistado.DataSource = dt;
                con.Close();
                MessageBox.Show("Coneccion realizada correctamente", "Conexion", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Sin conexion a la Base de datos", "Conexion fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }

            Tamaño_automatico_de_datatables.Multilinea(ref datalistado);

        }
        public void SavetoXML(object dbcnString)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("ConexionString.xml");
            XmlElement root = doc.DocumentElement;
            root.Attributes[0].Value = Convert.ToString(dbcnString);
            XmlTextWriter writer = new XmlTextWriter("ConexionString.xml", null);
            writer.Formatting = Formatting.Indented;
            doc.Save(writer);
            writer.Close();
        }
        string dbcnString;

        public void ReadfromXML()
        {

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("ConexionString.xml");
                XmlElement root = doc.DocumentElement;
                dbcnString = root.Attributes[0].Value;
                TxtConexionCadena.Text = (aes.Decrypt(dbcnString, Desencryptacion.appPwdUnique, int.Parse("256")));

            }
            catch (System.Security.Cryptography.CryptographicException ex)
            {


            }
        }

        private void FormConexionSQL_Load(object sender, EventArgs e)
        {
            ReadfromXML();
            
        }

        private void CmbModoAutenticacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbModoAutenticacion.Text== "SQL Server Authentication")
            {
                TxtPassword.Enabled = true;
                TxtUserName.Enabled = true;
                lblUserName.Enabled = true;
                LblPassword.Enabled = true;
                TxtTipoConexion.Clear();
                TxtConexionCadena.Clear();

            }

            if (CmbModoAutenticacion.Text== "Windows Authentication")
            {
                TxtPassword.Enabled = false;
                TxtUserName.Enabled = false;
                lblUserName.Enabled = false;
                LblPassword.Enabled = false;
                TxtTipoConexion.Text = ".";
                TxtConexionCadena.Clear();
            }
        }

        private void BtnProbarConexion_Click(object sender, EventArgs e)
        {
            try
            {

                if (CmbModoAutenticacion.Text == "SQL Server Authentication")
                {

                    CadenaConexion = "Data Source=" + TxtTipoConexion.Text + ";Initial Catalog=" + TxtBaseDatos.Text + ";user id=" + TxtUserName.Text + ";password=" + TxtPassword.Text;
                }

                if (CmbModoAutenticacion.Text == "Windows Authentication")
                {
                    CadenaConexion = "Data Source =" + TxtTipoConexion.Text + "; Initial Catalog=" + TxtBaseDatos.Text + ";Integrated Security=True;";
                }

                TxtConexionCadena.Text = CadenaConexion;

                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = CadenaConexion;
                sqlConnection.Open();

                //da = new SqlDataAdapter("Sp_Consulta_Empleados", sqlConnection);
                da = new SqlDataAdapter("Sp_Consulta_Proveedor", sqlConnection);


                da.Fill(dt);
                datalistado.DataSource = dt;
                sqlConnection.Close();
                MessageBox.Show("Coneccion realizada correctamente", "Conexion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {

                Soporte.MsgError("Sin conexion a la Base de datos", "Conexion fallida");
            }
                

            
            
        }
    }
}
