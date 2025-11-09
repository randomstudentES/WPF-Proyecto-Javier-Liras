using System;
using System.Data.SQLite;
using System.IO;
using System.Windows;

namespace ProyectoFigma
{
    public partial class EditarPerfil : Window
    {
        private string dbPath = "Data Source=usuarios.db;Version=3;";

        public EditarPerfil()
        {
            InitializeComponent();
            CrearTablaPerfil();
            CargarPerfil();
        }

        private void CrearTablaPerfil()
        {
            if (!File.Exists("usuarios.db"))
            {
                MessageBox.Show("No se encontró la base de datos. Inicia sesión primero.");
                return;
            }

            using (var connection = new SQLiteConnection(dbPath))
            {
                connection.Open();

                string sql = @"CREATE TABLE IF NOT EXISTS perfil (
                                id INTEGER PRIMARY KEY AUTOINCREMENT,
                                nombre_empresa TEXT NOT NULL,
                                direccion TEXT,
                                descripcion TEXT
                              );";
                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.ExecuteNonQuery();
                }

                // Insertar datos de ejemplo si la tabla está vacía
                string countSql = "SELECT COUNT(*) FROM perfil";
                using (var cmd = new SQLiteCommand(countSql, connection))
                {
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count == 0)
                    {
                        string insert = @"INSERT INTO perfil (nombre_empresa, direccion, descripcion)
                                          VALUES ('Compañía S.L.', 'Calle 123 21', 
                                          'Somos una empresa dedicada a ofrecer soluciones innovadoras para nuestros clientes. Creemos en la calidad, la responsabilidad y la mejora continua.')";
                        using (var insertCmd = new SQLiteCommand(insert, connection))
                        {
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }

                connection.Close();
            }
        }

        private void CargarPerfil()
        {
            using (var connection = new SQLiteConnection(dbPath))
            {
                connection.Open();

                string query = "SELECT nombre_empresa, direccion, descripcion FROM perfil LIMIT 1";
                using (var cmd = new SQLiteCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtNombreEmpresa.Text = reader["nombre_empresa"].ToString();
                        txtDireccion.Text = reader["direccion"].ToString();
                        txtDescripcion.Text = reader["descripcion"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron datos del perfil.");
                    }
                }

                connection.Close();
            }
        }

        private void Volver_Click(object sender, RoutedEventArgs e)
        {
            Menu ventanaMenu = new Menu();
            ventanaMenu.Show();
            this.Close();
        }
    }
}
