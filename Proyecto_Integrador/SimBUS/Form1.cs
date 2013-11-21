using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data;

namespace SimBUS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection Conexion = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='C:\Users\Mondro\Documents\GitHub\PI_3er_Semestre\Proyecto_Integrador\Proyecto_Integrador\BaseDeDatos.accdb'");
        
        private void button1_Click(object sender, EventArgs e)
        {
            OleDbCommand cmdUsuario = new OleDbCommand("Select Id_Tarjeta, UserTipo, UserSaldo FROM Usuario WHERE Id_Tarjeta=@Tarjeta", Conexion); //Se crea el comando);
            cmdUsuario.Parameters.AddWithValue("@Tarjeta",txtTarjeta.Text);
            OleDbDataAdapter usuario = new OleDbDataAdapter(cmdUsuario); 
            
            DataTable tabla = new DataTable();
            usuario.Fill(tabla);

            string tipeUser = tabla.Rows[0].ItemArray[1].ToString();

            OleDbCommand servicio = new OleDbCommand("INSERT INTO Servicios(NumUnidad, TipoUsuario, Usuario, Fecha) VALUES (@NumUnidad,@TipoUsuario,@Usuario,@Fecha)", Conexion);
            //servicio.Parameters.AddWithValue("@NumUnidad", cmbUnidad.SelectedItem.ToString());
            //servicio.Parameters.AddWithValue("@TipoUsuario", tipeUser);
            //servicio.Parameters.AddWithValue("@Usuario",txtTarjeta.Text);
            //servicio.Parameters.AddWithValue("@Fecha",DateTime.Now);
            servicio.Parameters.AddWithValue("@NumUnidad", "5");
            servicio.Parameters.AddWithValue("@TipoUsuario", "Estudiante");
            servicio.Parameters.AddWithValue("@Usuario", "1916634949");
            servicio.Parameters.AddWithValue("@Fecha", DateTime.Now);

            Conexion.Open();
            servicio.ExecuteNonQuery();
            Conexion.Close();



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OleDbCommand cmdCamion = new OleDbCommand("SELECT NumCamion FROM Camion", Conexion);
            OleDbDataAdapter da = new OleDbDataAdapter(cmdCamion);

            Conexion.Open();
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<String> numeroCamiones = (from DataRow var in dt.Rows select var.ItemArray[0].ToString()).ToList();
            cmbUnidad.DataSource = numeroCamiones;
            Conexion.Close();
        }
    }
}
