using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
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
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string dbPath = "Data Source=usuarios.db;Version=3;";
        public MainWindow()
        {
            InitializeComponent();
            CrearBaseDatos();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string password = txtPassword.Password.Trim();

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor, ingrese usuario y contraseña.");
                return;
            }

            using (var connection = new SQLiteConnection(dbPath))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM usuarios WHERE username=@user AND password=@pass";
                using (var cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@user", usuario);
                    cmd.Parameters.AddWithValue("@pass", password);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Inicio de sesión exitoso ✅");
                        Menu ventanaMenu = new Menu();
                        ventanaMenu.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Usuario o contraseña incorrectos ❌");
                    }
                }
            }
        }

        private void CrearBaseDatos()
        {
            if (!File.Exists("usuarios.db"))
            {
                SQLiteConnection.CreateFile("usuarios.db");

                using (var connection = new SQLiteConnection(dbPath))
                {
                    connection.Open();

                    string sql = "CREATE TABLE usuarios (id INTEGER PRIMARY KEY AUTOINCREMENT, username TEXT NOT NULL, password TEXT NOT NULL)";
                    using (var cmd = new SQLiteCommand(sql, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // Usuario de prueba
                    string insert = "INSERT INTO usuarios (username, password) VALUES ('admin', '1234')";
                    using (var cmd = new SQLiteCommand(insert, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }
        }

    }
}
