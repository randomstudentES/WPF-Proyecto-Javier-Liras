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

namespace ProyectoFigma
{
    /// <summary>
    /// Lógica de interacción para Chats.xaml
    /// </summary>
    public partial class Chats : Window
    {
        public Chats()
        {
            InitializeComponent();
        }

        private void Volver_Menu(object sender, RoutedEventArgs e)
        {
            Menu ventanaMenu = new Menu();
            ventanaMenu.Show();
            this.Close();
        }

    }
}
