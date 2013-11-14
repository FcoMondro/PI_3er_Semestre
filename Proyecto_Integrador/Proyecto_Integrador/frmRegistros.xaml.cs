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
        }

        private string Check_Sexo()
        {
            if (rdbMasculino.IsChecked == true)
                return Convert.ToString(rdbMasculino.Content);
            if (rdbFemenino.IsChecked == true)
                return Convert.ToString(rdbFemenino.Content);
            else
                return "";
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            OleDbConnection Conexion = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='|DataDirectory|\BaseDeDatos.accdb'");
            
            OleDbCommand Instruccion= new OleDbCommand("Insert INTO Usuario (Id_Usuario,UserNombre,UserApPa,UserApMa,UserFechaNac,UserSexo,UserTipo,UserDireccionCalle,UserDireccionNumero,UserDireccionColonia,UserDireccionMunicipio,UserCelular,UserTelefono,UserTipoSangre,UserAlergias) VALUES (@Id_Usuario,@userNombre,@UserApPa,@UserApMa,@UserFechaNac,@UserSexo,@UserTipo,@UserDireccionCalle,@UserDireccionNumero,@UserDireccionColonia,@UserDireccionMunicipio,@UserCelular,@UserTelefono,@UserTipoSangre,@UserAlergia)", Conexion);
            //OleDbCommand Instruccion2= new OleDbCommand("Insert INTO UsuarioAccidente (Id_UserAccidente,UserAccidenteNombre,UserAccidenteCelular,UserAccidenteTelefono Values()",Conexion);

            
            Instruccion.Parameters.AddWithValue("@Id_Usuario", Int32.Parse(txtID.Text));
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
            Instruccion.Parameters.AddWithValue("@UserCelular", Int32.Parse(txtCelular.Text));
            Instruccion.Parameters.AddWithValue("@UserTelefono", Int32.Parse(txtCasa.Text));
            Instruccion.Parameters.AddWithValue("@UserTipoSangre", cmbSangre.Text);
            Instruccion.Parameters.AddWithValue("@UserAlergia", txtAlergias.Text);

            //Instruccion2.Parameters.AddWithValue("@Id_Usuario", txtID.Text);
            //Instruccion2.Parameters.AddWithValue("@UserAccidenteNombre", txtNombreAccidente.Text);
            //Instruccion2.Parameters.AddWithValue("@UserAccidenteCelular", txtMovilAccidente.Text);
            //Instruccion2.Parameters.AddWithValue("@UserAccidenteCasa", txtCasaAccidente.Text);

            //Instruccion.Parameters.AddWithValue("@Id_Usuario", 1);
            //OleDbDataReader Lector;
            
            Conexion.Open();
            Instruccion.ExecuteNonQuery();
            //OleDbDataReader AgregarAccidente;
            //AgregarUsuario.Read();

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
            
        }
    }
}