using System;
using System.Data.SQLite;
using System.IO;
using System.Windows;

namespace ProyectoFigma
{
    public partial class ResultadosEmpleados : Window
    {
        private string dbPath = "Data Source=usuarios.db;Version=3;";

        public ResultadosEmpleados()
        {
            InitializeComponent();
            CrearTablaEmpleados();   // Asegura que exista la tabla
            CargarEmpleado();         // Carga el primer empleado
        }

        private void CrearTablaEmpleados()
        {
            if (!File.Exists("usuarios.db"))
            {
                MessageBox.Show("No se encontró la base de datos. Asegúrate de iniciar sesión primero.");
                return;
            }

            using (var connection = new SQLiteConnection(dbPath))
            {
                connection.Open();

                string sql = @"CREATE TABLE IF NOT EXISTS empleados (
                                id INTEGER PRIMARY KEY AUTOINCREMENT,
                                nombre TEXT NOT NULL,
                                apellidos TEXT,
                                estudios TEXT,
                                experiencia TEXT
                              );";
                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.ExecuteNonQuery();
                }

                // Inserta algunos empleados de ejemplo si la tabla está vacía
                string countSql = "SELECT COUNT(*) FROM empleados";
                using (var cmd = new SQLiteCommand(countSql, connection))
                {
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count == 0)
                    {
                        string insert = @"INSERT INTO empleados (nombre, apellidos, estudios, experiencia)
                                          VALUES
                                          ('Laura', 'García Pérez', 'Grado en Administración de Empresas', '2 años en atención al cliente'),
                                          ('Carlos', 'Sánchez López', 'Ingeniería Informática', '3 años como desarrollador backend'),
                                          ('María', 'López Díaz', 'Grado Superior en Marketing', '1 año en redes sociales')";
                        using (var insertCmd = new SQLiteCommand(insert, connection))
                        {
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }

                connection.Close();
            }
        }

        private void CargarEmpleado()
        {
            using (var connection = new SQLiteConnection(dbPath))
            {
                connection.Open();

                string query = "SELECT nombre, apellidos, estudios, experiencia FROM empleados LIMIT 1"; // muestra el primero
                using (var cmd = new SQLiteCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtNombre.Text = reader["nombre"].ToString();
                        txtApellidos.Text = reader["apellidos"].ToString();
                        txtEstudios.Text = reader["estudios"].ToString();
                        txtExperiencia.Text = reader["experiencia"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("No hay empleados registrados.");
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

        private void Rechazar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Empleado rechazado.", "Acción", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Duda_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Marcar empleado como 'en duda'.", "Acción", MessageBoxButton.OK, MessageBoxImage.Question);
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Empleado seleccionado correctamente.", "Acción", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
