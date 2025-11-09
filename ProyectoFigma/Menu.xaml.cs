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

namespace ProyectoFigma
{
    /// <summary>
    /// Lógica de interacción para Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        public void BuscarEmpleado(object sender, RoutedEventArgs e)
        {
            BuscarEmpleado ventanaBuscarEmpleado = new BuscarEmpleado();
            ventanaBuscarEmpleado.Show();
            this.Close();
        }

        public void AbrirEditarPerfil(object sender, RoutedEventArgs e)
        {
            EditarPerfil ventanaEditarPerfil = new EditarPerfil();
            ventanaEditarPerfil.Show();
            this.Close();
        }

        public void Chats(object sender, RoutedEventArgs e)
        {
            Chats chatVentana = new Chats();
            chatVentana.Show();
            this.Close();
        }

    }
}
