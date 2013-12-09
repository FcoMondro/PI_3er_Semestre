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

        }

        OleDbConnection Conexion;
        OleDbCommand cmd;
        OleDbDataAdapter da;
        DataTable tabla;
        List<String> numeroCamiones;

        //Cobros ====================================================================================================
        /// <summary>
        /// Estructura donde se guardaran todos los datos de los cobros al momento de desplegarlos en las tablas
        /// </summary>
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

        /// <summary>
        /// Lista guardar todos los datos que se mostraran en el datagrid
        /// </summary>
        BindingList<Uni> Unidad = new BindingList<Uni>();

        /// <summary>
        /// En este metodo se llena la estructura
        /// </summary>
        /// <param name="Uni">Uni el cual es la estructura</param>
        /// <param name="fechaI">FechaI, la cual es la fecha inicial.</param>
        /// <param name="fechaF">FechaF, la cual es la fecha final.</param>
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

        /// <summary>
        /// Es este metodo le enviamos los datos a la estructura
        /// </summary>
        public void boton()
        {
            DateTime fechaInicial = DateTime.Now.Date;
            DateTime fechaFinal = DateTime.Now.Date;
            if (dtpInicial.SelectedDate.HasValue)
                fechaInicial = dtpInicial.SelectedDate.Value;

            if (dtpFinal.SelectedDate.HasValue)
                fechaFinal = dtpFinal.SelectedDate.Value;

            foreach (string str in numeroCamiones)
            {
                Uni var = new Uni();
                var.unidad = str;
                llenarStruct(ref var, fechaInicial, fechaFinal);
                Unidad.Add(var);
            }
        }

        /// <summary>
        /// Se cargan todos los datos en el datagrid
        /// </summary>
        private void cmdCargar_Click(object sender, RoutedEventArgs e)
        {

            Unidad.Clear();
            boton();
            dtgCobros.ItemsSource = Unidad;

        }

        /// <summary>
        /// Se ejecuta cuando se carga la ventana
        /// </summary>
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

        //Frecuencias de uso Usuario ================================================================================

        /// <summary>
        /// Boton en donde se ejecutaran todas las funciones necesarias
        /// </summary>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Usuario.Clear();
            botonFrec();
            dtgUsosUsuario.ItemsSource = Usuario;
        }

        /// <summary>
        /// Estructura donde se guardan los datos
        /// </summary>
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

        /// <summary>
        /// Se obtienen todos los datos a mostrar por el rango de fechas
        /// </summary>
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
            FreUsuarios tmp;
            int i = 0;
            foreach (DataRow r in FrecUsuario.Rows)
            {
                MessageBox.Show(Convert.ToString(i++));
                tmp = new FreUsuarios();
                tmp.tabla = r;
                Usuario.Add(tmp);
            }
        }

        //Frecuencia de uso por Unidad ==============================================================================

        /// <summary>
        /// Boton en donde se ejecutaran todas las funciones necesarias
        /// </summary>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            UnidadFre.Clear();
            botonFrecUnidad();
            dtgUsosUnidad.ItemsSource = UnidadFre;
        }

        /// <summary>
        /// Estructura donde se guardan los datos
        /// </summary>
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

        /// <summary>
        /// Se obtienen todos los datos a mostrar por el rango de fechas
        /// </summary>
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

        //Frecuencia de uso por tipo de usuario =====================================================================

        /// <summary>
        /// Boton en donde se ejecutaran todas las funciones necesarias
        /// </summary>
        private void cmdCargarFTu_Click(object sender, RoutedEventArgs e)
        {
            TipoUserFre.Clear();
            botonFrecTipoUsuario();
            dtgUsosTipoUsuario.ItemsSource = TipoUserFre;
        }

        /// <summary>
        /// Estructura donde se guardan los datos
        /// </summary>
        public struct FreTipoUsuario
        {
            public DataRow tabla;
            public string NumUNidad
            {
                get { return tabla.ItemArray[1].ToString(); }
            }
            public string Usuario
            {
                get { return tabla.ItemArray[3].ToString(); }
            }
            public DateTime Fecha
            {
                get { return (DateTime)tabla.ItemArray[4]; }
            }
        }
        BindingList<FreTipoUsuario> TipoUserFre = new BindingList<FreTipoUsuario>();

        /// <summary>
        /// Se obtienen todos los datos a mostrar por el rango de fechas
        /// </summary>
        public void botonFrecTipoUsuario()
        {
            DateTime fechaInicial = DateTime.Now.Date;
            DateTime fechaFinal = DateTime.Now.Date;
            if (dtpInicialTu.SelectedDate.HasValue)
                fechaInicial = dtpInicialTu.SelectedDate.Value;

            if (dtpFinalTu.SelectedDate.HasValue)
                fechaFinal = dtpFinalTu.SelectedDate.Value;

            OleDbCommand cmdFTs = new OleDbCommand("Select * FROM Servicios WHERE TipoUsuario = @TipoUsuario AND Fecha BETWEEN @FechaI AND @FechaF ", Conexion);
            cmdFTs.Parameters.AddWithValue("@TipoUsuario", cmbUsuarios.Text);
            cmdFTs.Parameters.AddWithValue("@FechaI", fechaInicial);
            cmdFTs.Parameters.AddWithValue("@FechaF", fechaFinal);
            OleDbDataAdapter FrecenciaTipoU = new OleDbDataAdapter(cmdFTs);
            DataTable FrecTipoUsuario = new DataTable();
            FrecenciaTipoU.Fill(FrecTipoUsuario);
            FreTipoUsuario tmp;

            int i = 0;
            foreach (DataRow r in FrecTipoUsuario.Rows)
            {
                MessageBox.Show(Convert.ToString(i++));
                tmp = new FreTipoUsuario();
                tmp.tabla = r;
                TipoUserFre.Add(tmp);
            }
        }
    }
}