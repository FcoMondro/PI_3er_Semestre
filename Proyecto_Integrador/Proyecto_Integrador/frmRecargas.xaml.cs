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
    /// Lógica de interacción para frmRecargas.xaml
    /// </summary>
    public partial class frmRecargas : MetroWindow
    {
        /// <summary>
        /// 
        /// </summary>
        public frmRecargas()
        {
            InitializeComponent();
            Conexion = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='C:\Users\Mondro\Documents\GitHub\PI_3er_Semestre\Proyecto_Integrador\Proyecto_Integrador\BaseDeDatos.accdb'");
            cmd = new OleDbCommand("Select Id_Tarjeta, UserNombre, UserApPa, UserApMa, UserFechaNac, UserSaldo FROM Usuario UserNombre", Conexion); //Se crea el comando
            da = new OleDbDataAdapter(cmd);
        }
        OleDbConnection Conexion;
        OleDbCommand cmd;
        OleDbDataAdapter da;
        decimal saldo = 0;

        //private void Buscar()
        //{

        //    OleDbCommand Instruccion = new OleDbCommand("Select * from Usuario where UserNombre Like @Nombre  ", Conexion);

        //    Instruccion.Parameters.AddWithValue("@Nombre", Nombre);
        //    DataTable busqueda = new DataTable();
        //    OleDbDataAdapter data = new OleDbDataAdapter(Instruccion.CommandText, Conexion.ConnectionString);
        //    data.SelectCommand = Instruccion;
        //    data.Fill(busqueda);

        //    dtgBusqueda.ItemsSource = (from row in busqueda.Rows select row);

        //}

        /// <summary>
        /// Busca en la base de datos cuando es por nombre y se le da enter
        /// </summary>
        private void TextBox_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string Nombre = txtNombre.Text;
                Conexion.Open(); 
                DataTable tabla = new DataTable(); // Creamos el objeto Tabla
                da.Fill(tabla); //Crea la tabla

                DataView DV = new DataView(tabla);
                DV.RowFilter = string.Format("UserNombre LIKE '%{0}%'", Nombre); //Filtro para lo que se va a mostrar
                dtgBusqueda.ItemsSource = DV;
                Conexion.Close();
            }
        }

        /// <summary>
        /// Busca en la base de datos cuando es por ID y se le da enter
        /// </summary>
        private void txtID_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string Num_Tarjeta = txtNombre.Text;
                Conexion.Open();
                DataTable tabla = new DataTable(); // Creamos el objeto Tabla
                da.Fill(tabla); //Crea la tabla

                DataView DV = new DataView(tabla);
                DV.RowFilter = string.Format("Id_Tarjeta LIKE '%{0}%'", Num_Tarjeta); //Filtro para lo que se va a mostrar
                dtgBusqueda.ItemsSource = DV;
                Conexion.Close();
            }
        }

        /// <summary>
        /// Se hace la actualizacion del registro en la base de datos
        /// </summary>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            saldo += decimal.Parse(txtCantidad.Text);
            OleDbCommand actualizar = new OleDbCommand("UPDATE Usuario SET UserSaldo = @UserSaldo WHERE Id_Tarjeta = @Id_Tarjeta ", Conexion);
            actualizar.Parameters.AddWithValue("@UserSaldo", saldo);
            actualizar.Parameters.AddWithValue("@Id_Tarjeta", txtID.Text);
            Conexion.Open();
            actualizar.ExecuteNonQuery();
            Conexion.Close();
        }

        /// <summary>
        /// Se agregan los campos necesarios para generar la recarga
        /// </summary>
        private void dtgBusqueda_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView DR = dtgBusqueda.SelectedItem as DataRowView;
            txtNombre.Text = DR[1].ToString();
            txtID.Text = DR[0].ToString();
            saldo = (decimal)DR[5];
        }
    }
}
