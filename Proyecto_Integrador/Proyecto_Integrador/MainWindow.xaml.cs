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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;


namespace Proyecto_Integrador
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Maneja el evento Click del control btnAltas
        /// </summary>
        private void btnAltas_Click(object sender, RoutedEventArgs e)
        {
            frmRegistros regi = new frmRegistros();
            regi.ShowDialog();
        }

        /// <summary>
        /// Maneja el evento Click del control btnRecargas
        /// </summary>
        private void btnRecargas_Click(object sender, RoutedEventArgs e)
        {
            frmRecargas reca = new frmRecargas();
            reca.ShowDialog();
        }

        /// <summary>
        /// Maneja el evento Click del control btnReportes
        /// </summary>
        private void btnReportes_Click(object sender, RoutedEventArgs e)
        {
            frmReportes repo = new frmReportes();
            repo.ShowDialog();
        }
    }
}
