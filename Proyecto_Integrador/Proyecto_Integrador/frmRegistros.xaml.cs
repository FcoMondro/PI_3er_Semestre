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

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            OleDbConnection Conexion = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='|DataDirectory|\BaseDeDatos.accdb'");
            OleDbCommand Instruccion = new OleDbCommand("Select * From Usuario Where Id_Usuario=@Id_Usuario", Conexion);
            //OleDbCommand Instruccion = new OleDbCommand("Insert Into Usuario (Id_Usuario,UserNombre,UserApPa,UserApMa,UserFechaNac,UserSexo,UserTipo,UserDireccionCalle,UserDireccionNumero,UserDireccionColonia,UserDireccionMunicipio,UserCelular,UserTelefono,UserTipoSangre,UserAlergia)" 
            //VALUES (), Conexion);
            //Instruccion.Parameters.AddWithValue("@Id_Usuario", txtID.Text);
            //Instruccion.Parameters.AddWithValue("@UserNombre", txtNombres.Text);
            //Instruccion.Parameters.AddWithValue("@UserApPa", Text,txtApellidoP.Text);
            //Instruccion.Parameters.AddWithValue("@UserApMa", txtApellidoM.Text);
            //Instruccion.Parameters.AddWithValue("@UserFechaNac", dtpFecha.DisplayDate);
            //Instruccion.Parameters.AddWithValue("@UserSexo", Sexo());
            //Instruccion.Parameters.AddWithValue("@UserTipo", cmbUsuarios.Text);
            //Instruccion.Parameters.AddWithValue("@UserDireccionCalle", txtCalle.Text);
            //Instruccion.Parameters.AddWithValue("@UserDireccionNumero", txtNumero.Text);
            //Instruccion.Parameters.AddWithValue("@UserDireccionColonia", txtColonia.Text);
            //Instruccion.Parameters.AddWithValue("@UserDireccionMunicipio", txtMunicipio.Text);
            //Instruccion.Parameters.AddWithValue("@UserCelular", txtCelular.Text);
            //Instruccion.Parameters.AddWithValue("@UserTelefono", txtCasa.Text);
            //Instruccion.Parameters.AddWithValue("@UserTipoSangre", cmbSangre.Text);
            //Instruccion.Parameters.AddWithValue("@UserAlergia", txtAlergias.Text);
            //OleDbDataAdapter Agregar;
            //Agregar = Instruccion.ExecuteNonQuery();

            Instruccion.Parameters.AddWithValue("@Id_Usuario", 1);
            OleDbDataReader Lector;
            Conexion.Open();
            Lector = Instruccion.ExecuteReader();

            Lector.Read();
            txtID.Text = Lector["Id_Usuario"].ToString();
            txtNombres.Text = Lector["UserNombre"].ToString();
            txtApellidoP.Text = Lector["UserApPa"].ToString();
            txtApellidoM.Text = Lector["UserApMa"].ToString();

            Conexion.Close();
            
        }
    }
}