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
            cmbMunicipio.SelectedIndex = -1;
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
            OleDbCommand Instruccion = new OleDbCommand("Insert INTO Usuario (Id_Tarjeta,UserNombre,UserApPa,UserApMa,UserFechaNac,UserSexo,UserTipo,UserDireccionCalle,UserDireccionNumero,UserDireccionColonia,UserDireccionMunicipio,UserCelular,UserTelefono,UserTipoSangre,UserAlergias,UserAccidenteNombre,UserAccidenteCelular,UserAccidenteTelefono,UserSaldo) VALUES (@Id_Tarjeta,@userNombre,@UserApPa,@UserApMa,@UserFechaNac,@UserSexo,@UserTipo,@UserDireccionCalle,@UserDireccionNumero,@UserDireccionColonia,@UserDireccionMunicipio,@UserCelular,@UserTelefono,@UserTipoSangre,@UserAlergia,@UserAccidenteNombre,@UserAccidenteCelular,@UserAccidenteTelefono,@UserSaldo)", Conexion);
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
            Instruccion.Parameters.AddWithValue("@UserDireccionMunicipio", cmbMunicipio.Text);
            Instruccion.Parameters.AddWithValue("@UserCelular", Int64.Parse(txtCelular.Text));
            Instruccion.Parameters.AddWithValue("@UserTelefono", Int64.Parse(txtCasa.Text));
            Instruccion.Parameters.AddWithValue("@UserTipoSangre", cmbSangre.Text);
            Instruccion.Parameters.AddWithValue("@UserAlergia", txtAlergias.Text);
            Instruccion.Parameters.AddWithValue("@UserAccidenteNombre", txtNombreAccidente.Text);
            Instruccion.Parameters.AddWithValue("@UserAccidenteCelular", Int64.Parse(txtMovilAccidente.Text));
            Instruccion.Parameters.AddWithValue("@UserAccidenteCasa", Int64.Parse(txtCasaAccidente.Text));
            Instruccion.Parameters.AddWithValue("@UserSaldo", 0);


            //Instruccion.Parameters.AddWithValue("@Id_Usuario", 1);


            if (txtNombres.Text == "")
                MessageBox.Show("Por favor, complete el campo Nombre");
            else if (txtApellidoP.Text == "")
                MessageBox.Show("Por favor, complete el campo Apellido Paterno");
            else if (txtApellidoM.Text == "")
                MessageBox.Show("Por favor, complete el campo Apellido materno");
            else if (Check_Sexo() == "")
                MessageBox.Show("Por favor, seleccione el Sexo");
            else if (cmbUsuarios.SelectedIndex == -1)
                MessageBox.Show("Por favor, seleccione un usuario");
            else if (txtCalle.Text == "")
                MessageBox.Show("Por favor, complete el campo Calle");
            else if (txtNumero.Text == "")
                MessageBox.Show("Por favor, complete el campo Numero");
            else if (txtColonia.Text == "")
                MessageBox.Show("Por favor, complete el campo Colonia");
            else if (cmbMunicipio.SelectedIndex == -1)
                MessageBox.Show("Por favor, selecciona un municipio");
            else if (txtCelular.Text == "")
                MessageBox.Show("Por favor, complete el campo Celular");
            else if (txtCasa.Text == "")
                MessageBox.Show("Por favor, complete el campo Casa");
            else if (cmbSangre.SelectedIndex == -1)
                MessageBox.Show("Por favor, seleccione un tipo de sangre");
            else if (txtAlergias.Text == "")
                MessageBox.Show("Por favor, complete el campo Alergias");
            else if (txtNombreAccidente.Text == "")
                MessageBox.Show("Por favor, complete el campo Nombre completo en la seccion de Contacto en caso de Accidente");
            else if (txtMovilAccidente.Text == "")
                MessageBox.Show("Por favor, complete el campo Movil en la seccion de Contacto en caso de Accidente");
            else if (txtCasaAccidente.Text == "")
                MessageBox.Show("Por favor, complete el campo Casa en la seccion de Contacto en caso de Accidente");
            else
            {
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
                if (MessageBox.Show("Desea agregar un nuevo registro?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    MessageBox.Show("Gracias, usted regresara al menu.");
                    this.Close();
                }
                else
                {
                    Servicio_de_Limpieza();
                }

                //DialogResult dr;
                //dr = MessageBox.Show("Sure", "Some Title", MessageBoxButtons.YesNo);
                //if (dialogResult == DialogResult.Yes)
                //{
                //    Servicio_de_Limpieza();
                //}
                //else if (dialogResult == DialogResult.No)
                //{
                //    MessageBox.Show("Gracias.");
                //}
                
            }
            //MessageBox.Show("Se cerro la conexion");
        }

        private void txtNumero_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D0:
                case Key.D1:
                case Key.D2:
                case Key.D3:
                case Key.D4:
                case Key.D5:
                case Key.D6:
                case Key.D7:
                case Key.D8:
                case Key.D9:
                case Key.NumLock:
                case Key.NumPad0:
                case Key.NumPad1:
                case Key.NumPad2:
                case Key.NumPad3:
                case Key.NumPad4:
                case Key.NumPad5:
                case Key.NumPad6:
                case Key.NumPad7:
                case Key.NumPad8:
                case Key.NumPad9:
                case Key.Back:
                    break;
                default:
                    e.Handled = true;
                    break;
            }
        }

        private void txtCelular_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            txtNumero_PreviewKeyDown(sender, e);
        }

        private void txtNumero_KeyUp(object sender, KeyEventArgs e)
        {
            //nada
        }

        private void txtCasa_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            txtNumero_PreviewKeyDown(sender, e);
        }

        private void txtMovilAccidente_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            txtNumero_PreviewKeyDown(sender, e);
        }

        private void txtCasaAccidente_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            txtNumero_PreviewKeyDown(sender, e);
        }

    }
}