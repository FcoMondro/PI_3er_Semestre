using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Data.OleDb;
using System.Data;

namespace Proyecto_Integrador
{
    /// <summary>
    /// Lógica de interacción para frmRegistros.xaml
    /// </summary>
    public partial class frmRegistros : MetroWindow
    {
        public frmRegistros()
        {
            InitializeComponent();
            count_Registro();
        }


        OleDbConnection Conexion = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='C:\Users\Mondro\Documents\GitHub\PI_3er_Semestre\Proyecto_Integrador\Proyecto_Integrador\BaseDeDatos.accdb'");

        /// <summary>
        /// Checa cual es el radiobutton seleccionado
        /// </summary>
        private string Check_Sexo()
        {
            if (rdbMasculino.IsChecked == true)
                return Convert.ToString(rdbMasculino.Content);
            if (rdbFemenino.IsChecked == true)
                return Convert.ToString(rdbFemenino.Content);
            else
                return "";
        }

        /// <summary>
        /// Limpia todos los campos.
        /// </summary>
        private void Servicio_de_Limpieza()
        {
            txtNombres.Clear();
            txtApellidoP.Clear();
            txtApellidoM.Clear();
            dtpFecha.SelectedDate = null;
            cmbUsuarios.SelectedIndex = -1;
            txtCalle.Clear();
            txtNumero.Clear();
            txtColonia.Clear();
            txtMunicipio.Clear();
            txtCelular.Clear();
            txtCasa.Clear();
            cmbSangre.SelectedIndex = -1;
            txtAlergias.Clear();
            txtNombreAccidente.Clear();
            txtMovilAccidente.Clear();
            txtCasaAccidente.Clear();
            txtNombres.Focus();
            count_Registro();
        }

        /// <summary>
        /// Se verifica cuantos registros almacenados tiene almacenados la base de datos
        /// </summary>
        private void count_Registro()
        {
            OleDbCommand count = new OleDbCommand("Select Count (*) from Usuario", Conexion);
            Random random = new Random();
            int suma = random.Next();
            Conexion.Open();
            int prueba = (int)count.ExecuteScalar() + suma;
            txtID.Text = prueba.ToString();
            Conexion.Close();
        }

        /// <summary>
        /// Hace la conexion a la base de datos y realiza lo necesario para agregar el registro.
        /// </summary>
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            //OleDbConnection Conexion = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='|DataDirectory|\BaseDeDatos.accdb'");
            
            //MessageBox.Show("Conexion abierta");
            OleDbCommand Instruccion = new OleDbCommand("Insert INTO Usuario (Id_Tarjeta,UserNombre,UserApPa,UserApMa,UserFechaNac,UserSexo,UserTipo,UserDireccionCalle,UserDireccionNumero,UserDireccionColonia,UserDireccionMunicipio,UserCelular,UserTelefono,UserTipoSangre,UserAlergias,UserAccidenteNombre,UserAccidenteCelular,UserAccidenteTelefono) VALUES (@Id_Tarjeta,@userNombre,@UserApPa,@UserApMa,@UserFechaNac,@UserSexo,@UserTipo,@UserDireccionCalle,@UserDireccionNumero,@UserDireccionColonia,@UserDireccionMunicipio,@UserCelular,@UserTelefono,@UserTipoSangre,@UserAlergia,@UserAccidenteNombre,@UserAccidenteCelular,@UserAccidenteTelefono)", Conexion);
            //MessageBox.Show("Se crearon las instrucciones");

            Instruccion.Parameters.AddWithValue("@Id_Tarjeta", txtID.Text);
            Instruccion.Parameters.AddWithValue("@UserNombre", txtNombres.Text);
            Instruccion.Parameters.AddWithValue("@UserApPa", txtApellidoP.Text);
            Instruccion.Parameters.AddWithValue("@UserApMa", txtApellidoM.Text);
            Instruccion.Parameters.AddWithValue("@UserFechaNac", dtpFecha.DisplayDate);
            Instruccion.Parameters.AddWithValue("@UserSexo", Check_Sexo());
            Instruccion.Parameters.AddWithValue("@UserTipo", cmbUsuarios.Text);
            Instruccion.Parameters.AddWithValue("@UserDireccionCalle", txtCalle.Text);
            Instruccion.Parameters.AddWithValue("@UserDireccionNumero", Int32.Parse(txtNumero.Text));
            Instruccion.Parameters.AddWithValue("@UserDireccionColonia", txtColonia.Text);
            Instruccion.Parameters.AddWithValue("@UserDireccionMunicipio", txtMunicipio.Text);
            Instruccion.Parameters.AddWithValue("@UserCelular", Int64.Parse(txtCelular.Text));
            Instruccion.Parameters.AddWithValue("@UserTelefono", Int32.Parse(txtCasa.Text));
            Instruccion.Parameters.AddWithValue("@UserTipoSangre", cmbSangre.Text);
            Instruccion.Parameters.AddWithValue("@UserAlergia", txtAlergias.Text);
            Instruccion.Parameters.AddWithValue("@UserAccidenteNombre", txtNombreAccidente.Text);
            Instruccion.Parameters.AddWithValue("@UserAccidenteCelular", Int32.Parse(txtMovilAccidente.Text));
            Instruccion.Parameters.AddWithValue("@UserAccidenteCasa", Int32.Parse(txtCasaAccidente.Text));

            //Instruccion.Parameters.AddWithValue("@Id_Usuario", 1);
            
            
            Conexion.Open();

            //MessageBox.Show("Se abre la conexion");
            Instruccion.ExecuteNonQuery();
            //MessageBox.Show(prueba.ToString());
            //OleDbDataReader AgregarAccidente;
            //AgreingarUsuario.Read();

            //AgregarUsuario = Instruccion.ExecuteNonQuery();
            //AgregarAccidente = Instruccion2.ExecuteNonQuery();
            //Lector = Instruccion.ExecuteReader();

            //AgregarUsuario.Close();
            //AgregarAccidente.Close();
            //Lector.Read();
            //txtID.Text = Lector["Id_Usuario"].ToString();
            //txtNombres.Text = Lector["UserNombre"].ToString();
            //txtApellidoP.Text = Lector["UserApPa"].ToString();
            //txtApellidoM.Text = Lector["UserApMa"].ToString();

            Conexion.Close();
            //MessageBox.Show("Se cerro la conexion");
            Servicio_de_Limpieza();
        }
    }
}