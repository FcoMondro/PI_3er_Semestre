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
using System.ComponentModel;

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
            dtpFinal.IsEnabled = false;

        }

        OleDbConnection Conexion;
        OleDbCommand cmd;
        OleDbDataAdapter da;
        DataTable tabla;
        List<String> numeroCamiones;

        //Cobros
        public struct Uni
        {
            public string unidad;
            public string Unidad
            {
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
            public decimal General
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

        BindingList<Uni> Unidad = new BindingList<Uni>();

        public void llenarStruct(ref Uni input, DateTime fechaI, DateTime fechaF)
        {
            input.TiposUsuarios = new List<string>();
            OleDbCommand cmd1 = new OleDbCommand("Select TipoUsuario FROM Servicios WHERE NumUnidad = @NumUnidad AND Fecha BETWEEN @FechaI AND @FechaF", Conexion);
            cmd1.Parameters.AddWithValue("@NumUnidad", input.unidad);
            cmd1.Parameters.AddWithValue("@FechaI", fechaI);
            cmd1.Parameters.AddWithValue("@FechaF", fechaF);
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
            DateTime fechaInicial = DateTime.Now.Date;
            DateTime fechaFinal = DateTime.Now.Date;
            if (dtpInicial.SelectedDate.HasValue)
                fechaInicial = dtpInicial.SelectedDate.Value;
            else
                MessageBox.Show("Seleccione una fecha inicial valida");
            if (chkActual.IsChecked == true)
            {
                if (dtpFinal.SelectedDate.HasValue)
                    fechaFinal = dtpFinal.SelectedDate.Value;
                else
                    MessageBox.Show("Seleccione una fecha dinal valida");
            }
            else
                fechaFinal = DateTime.Now.Date;

            foreach (string str in numeroCamiones)
            {
                Uni var = new Uni();
                var.unidad = str;
                llenarStruct(ref var, fechaInicial, fechaFinal);
                Unidad.Add(var);
            }
        }

        private void cmdCargar_Click(object sender, RoutedEventArgs e)
        {

            Unidad.Clear();
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

            cmbUnidades.ItemsSource = numeroCamiones;


        }

        private void chkActual_Checked(object sender, RoutedEventArgs e)
        {
            dtpFinal.IsEnabled = true;
        }

        //Frecuencias de uso Usuario

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Usuario.Clear();
            botonFrec();
            dtgUsosUsuario.ItemsSource = Usuario;
        }
        
        public struct FreUsuarios
        {
            public DataRow tabla;
            public string NumUnidad
            {
                get { return tabla.ItemArray[1].ToString(); }
            }
            public DateTime Fecha
            {
                get { return (DateTime)tabla.ItemArray[4]; }
            }

        }
        BindingList<FreUsuarios> Usuario = new BindingList<FreUsuarios>();

        public void botonFrec()
        {
            string tarjeta = txtNumTarjeta.Text;
            DateTime fechaInicial = DateTime.Now.Date;
            DateTime fechaFinal = DateTime.Now.Date;
            if (dtpInicialFUs.SelectedDate.HasValue)
                fechaInicial = dtpInicialFUs.SelectedDate.Value;


            if (dtpFinalUs.SelectedDate.HasValue)
                fechaFinal = dtpFinalUs.SelectedDate.Value;


            OleDbCommand cmdFUs = new OleDbCommand("Select * FROM Servicios WHERE Usuario = @Usuario AND Fecha BETWEEN @FechaI AND @FechaF ", Conexion);
            cmdFUs.Parameters.AddWithValue("@Usuario", txtNumTarjeta.Text);
            cmdFUs.Parameters.AddWithValue("@FechaI", fechaInicial);
            cmdFUs.Parameters.AddWithValue("@FechaF", fechaFinal);
            OleDbDataAdapter FrecenciaUsuario = new OleDbDataAdapter(cmdFUs);
            DataTable FrecUsuario = new DataTable();
            FrecenciaUsuario.Fill(FrecUsuario);
            FreUsuarios tmp ;
            int i = 0;
            foreach (DataRow r in FrecUsuario.Rows) {
                MessageBox.Show(Convert.ToString(i++));
                tmp = new FreUsuarios();
                tmp.tabla = r;
                Usuario.Add(tmp);
            }            
        }

        //Frecuencia de uso por Unidad ==============================================================================

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            UnidadFre.Clear();
            botonFrecUnidad();
            dtgUsosUnidad.ItemsSource = UnidadFre;
        }

        public struct FreUnidad
        {
            public DataRow tabla;
            public string Usuario
            {
                get { return tabla.ItemArray[3].ToString(); }
            }
            public DateTime Fecha
            {
                get { return (DateTime)tabla.ItemArray[4]; }
            }
        }

        BindingList<FreUnidad> UnidadFre = new BindingList<FreUnidad>();

        public void botonFrecUnidad()
        {
            DateTime fechaInicial = DateTime.Now.Date;
            DateTime fechaFinal = DateTime.Now.Date;
            if (dtpInicialFUn.SelectedDate.HasValue)
                fechaInicial = dtpInicialFUn.SelectedDate.Value;

            if (dtpFinalFUn.SelectedDate.HasValue)
                fechaFinal = dtpFinalFUn.SelectedDate.Value;

            OleDbCommand cmdFUs = new OleDbCommand("Select * FROM Servicios WHERE NumUnidad = @NumUnidad AND Fecha BETWEEN @FechaI AND @FechaF ", Conexion);
            cmdFUs.Parameters.AddWithValue("@NumUnidad", cmbUnidades.SelectedValue.ToString());
            cmdFUs.Parameters.AddWithValue("@FechaI", fechaInicial);
            cmdFUs.Parameters.AddWithValue("@FechaF", fechaFinal);
            OleDbDataAdapter FrecenciaUnidad = new OleDbDataAdapter(cmdFUs);
            DataTable FrecUnidad = new DataTable();
            FrecenciaUnidad.Fill(FrecUnidad);
            FreUnidad tmp;
            int i = 0;
            foreach (DataRow r in FrecUnidad.Rows)
            {
                MessageBox.Show(Convert.ToString(i++));
                tmp = new FreUnidad();
                tmp.tabla = r;
                UnidadFre.Add(tmp);
            }
        }

    }
}
