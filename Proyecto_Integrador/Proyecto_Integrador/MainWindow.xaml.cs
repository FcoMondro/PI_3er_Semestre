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

        private void btnAltas_Click(object sender, RoutedEventArgs e)
        {
            frmRegistros regi = new frmRegistros();
            regi.ShowDialog();
        }

        private void btnRecargas_Click(object sender, RoutedEventArgs e)
        {
            frmRecargas reca = new frmRecargas();
            reca.ShowDialog();
        }

        private void btnReportes_Click(object sender, RoutedEventArgs e)
        {
            frmReportes repo = new frmReportes();
            repo.ShowDialog();
        }
    }
}
