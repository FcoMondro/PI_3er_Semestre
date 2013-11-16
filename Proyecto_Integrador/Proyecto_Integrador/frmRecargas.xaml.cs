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
        public frmRecargas()
        {
            InitializeComponent();
        }
        OleDbConnection Conexion = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='C:\Users\Mondro\Documents\GitHub\PI_3er_Semestre\Proyecto_Integrador\Proyecto_Integrador\BaseDeDatos.accdb'");
        

        private void Buscar()
        {
            string Nombre = txtNombre.Text;
            OleDbCommand Instruccion = new OleDbCommand("Select * from Usuario where UserNombre Like @Nombre  ", Conexion);

            Instruccion.Parameters.AddWithValue("@Nombre", Nombre);
            DataTable busqueda = new DataTable();
            OleDbDataAdapter data = new OleDbDataAdapter(Instruccion.CommandText, Conexion.ConnectionString);
            data.SelectCommand = Instruccion;
            data.Fill(busqueda);
            dtgBusqueda.ItemsSource = (from row in busqueda.Rows select row.;
        }
        private void TextBox_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Buscar();
        }
    }
}
