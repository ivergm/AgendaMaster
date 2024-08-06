using AgendaMaster.Entidades;
using AgendaMaster.BaseDatos;
using System;
using System.Linq;
using System.Data.SqlClient;

namespace AgendaMaster.Models
{
    public class UsuarioModel
    {
        private string connectionString = "data source=DESKTOP-PS1FR69\\SQLEXPRESS;initial catalog=AgendaMaster;integrated security=True;trustservercertificate=True;MultipleActiveResultSets=True;App=EntityFramework";


        public bool RegistrarUsuario(Usuario entidad)
        {
            using (var context = new AgendaMasterEntities())
            {


                var tabla = new Usuarios();
                tabla.Cedula = entidad.Cedula;
                tabla.Nombre = entidad.Nombre;
                tabla.Apellidos = entidad.Apellidos;
                tabla.Email = entidad.Email;
                tabla.Rol = entidad.Rol;
                tabla.Contrasenna = entidad.Contrasenna;

                context.Usuarios.Add(tabla);
                context.SaveChanges();
            }
            return true;
        }

        public Usuario IniciarSesion(Usuario entidad)
        {
            Usuario usuario = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Usuarios WHERE Email = @Email AND Contrasenna = @Contrasenna";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", entidad.Email);
                        command.Parameters.AddWithValue("@Contrasenna", entidad.Contrasenna);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                usuario = new Usuario
                                {
                                    IdUsuario = Convert.ToInt64(reader["IdUsuario"]),
                                    Cedula = reader["Cedula"].ToString(),
                                    Nombre = reader["Nombre"].ToString(),
                                    Apellidos = reader["Apellidos"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Rol = reader["Rol"].ToString(),
                                    Contrasenna = reader["Contrasenna"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine(ex.Message);
            }

            return usuario;
        }
    }
}
