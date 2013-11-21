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
    /// Lógica de interacción para frmReportes.xaml
    /// </summary>
    public partial class frmReportes : MetroWindow
    {
        public frmReportes()
        {
            InitializeComponent();
            Conexion = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='C:\Users\Mondro\Documents\GitHub\PI_3er_Semestre\Proyecto_Integrador\Proyecto_Integrador\BaseDeDatos.accdb'");
            cmd = new OleDbCommand("Select * FROM Servicios", Conexion);
            da = new OleDbDataAdapter(cmd);
            tabla = new DataTable();
        }
        OleDbConnection Conexion;
        OleDbCommand cmd;
        OleDbDataAdapter da;
        DataTable tabla;
        List<String> numeroCamiones;

        public struct Uni
        {
            public string unidad;
            public string Unidad {
                get { return unidad; }
            }
            public List<string> TiposUsuarios;
            public override string ToString()
            {
                return unidad;
            }
            public decimal Total
            {
                get
                {
                    decimal total = 0;
                    foreach (string v in TiposUsuarios)
                    {
                        if (v == "General")
                        {
                            total += 6;
                        }
                        else
                            total += 3;
                    }
                    return total;
                }
            }
            public decimal General {
                get {
                    decimal total=0;
                    foreach(string v in TiposUsuarios){
                        if (v == "General") {
                            total += 6;
                        }
                    }
                    return total;
                }
            }
            public decimal Estudiante
            {
                get
                {
                    decimal total = 0;
                    foreach (string v in TiposUsuarios)
                    {
                        if (v == "Estudiante")
                        {
                            total += 3;
                        }
                    }
                    return total;
                }
            }
            public decimal TerceraEdad
            {
                get
                {
                    decimal total = 0;
                    foreach (string v in TiposUsuarios)
                    {
                        if (v == "3ra Edad")
                        {
                            total += 3;
                        }
                    }
                    return total;
                }
            }
            public decimal Capacidades_Diferentes
            {
                get
                {
                    decimal total = 0;
                    foreach (string v in TiposUsuarios)
                    {
                        if (v == "Capacidades Diferentes")
                        {
                            total += 3;
                        }
                    }
                    return total;
                }
            }
        }
        List<Uni> Unidad = new List<Uni>();

        public void llenarStruct(ref Uni input)
        {
            input.TiposUsuarios = new List<string>();
            OleDbCommand cmd1 = new OleDbCommand("Select TipoUsuario FROM Servicios WHERE NumUnidad = @NumUnidad", Conexion);
            cmd1.Parameters.AddWithValue("@NumUnidad", input.unidad);
            OleDbDataAdapter da1 = new OleDbDataAdapter(cmd1);
            DataTable tabla1 = new DataTable();
            da1.Fill(tabla1);
            foreach (DataRow str in tabla1.Rows)
            {
                input.TiposUsuarios.Add(str.ItemArray[0].ToString());
            }
        }

        public void boton()
        {
            foreach (string str in numeroCamiones)
            {
                Uni var = new Uni();
                var.unidad = str;
                llenarStruct(ref var);
                Unidad.Add(var);
            }
        }

        private void cmdCargar_Click(object sender, RoutedEventArgs e)
        {
            boton();
            dtgCobros.ItemsSource = Unidad;
        }

        private void MetroWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
            Conexion.Open();
            da.Fill(tabla);
            Conexion.Close();

            OleDbCommand cmdCamion = new OleDbCommand("SELECT NumCamion FROM Camion", Conexion);
            OleDbDataAdapter daC = new OleDbDataAdapter(cmdCamion);
            Conexion.Open();
            DataTable dt = new DataTable();
            daC.Fill(dt);
            numeroCamiones = (from DataRow var in dt.Rows select var.ItemArray[0].ToString()).ToList();
            Conexion.Close();


        }
    }
}
