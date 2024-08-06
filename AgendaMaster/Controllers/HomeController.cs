using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
using AgendaMaster.Entidades;
using AgendaMaster.Models;
using AgendaMaster.ViewModels;


namespace AgendaMaster.Controllers
{
    public class HomeController : Controller
    {
        private string connectionString = "data source=DESKTOP-PS1FR69\\SQLEXPRESS;initial catalog=AgendaMaster;integrated security=True;trustservercertificate=True;MultipleActiveResultSets=True;App=EntityFramework";

        [HttpPost]
        public JsonResult AgregarTarea(Tarea tarea)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "INSERT INTO Tareas (Nombre, Prioridad, Descripcion, TiempoInvertido, FechaCreacion) VALUES (@Nombre, @Prioridad, @Descripcion, @TiempoInvertido, @FechaCreacion)";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Nombre", tarea.Nombre);
                            command.Parameters.AddWithValue("@Prioridad", tarea.Prioridad);
                            command.Parameters.AddWithValue("@Descripcion", tarea.Descripcion);
                            command.Parameters.AddWithValue("@TiempoInvertido", tarea.TiempoInvertido);
                            command.Parameters.AddWithValue("@FechaCreacion", DateTime.Now);

                            command.ExecuteNonQuery();
                        }
                    }
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    return Json(new { success = false, message = ex.Message });
                }
            }
            return Json(new { success = false });
        }

        public ActionResult Index()
        {
            var tareas = ObtenerTareas();
            return View(tareas); // Pasar las tareas a la vista para que puedan verse
        }

        private List<Tarea> ObtenerTareas()
        {
            var tareas = new List<Tarea>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Nombre, Prioridad, Descripcion FROM Tareas";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                tareas.Add(new Tarea
                                {
                                    Nombre = reader["Nombre"].ToString(),
                                    Prioridad = reader["Prioridad"].ToString(),
                                    Descripcion = reader["Descripcion"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //  error
                Console.WriteLine(ex.Message);
            }

            return tareas;
        }

        public ActionResult AdministrarUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Cedula,Nombre, Apellidos, Email, Rol, Contrasenna FROM Usuarios";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usuario usuario = new Usuario
                            {
                                Cedula = reader["Cedula"].ToString(),
                                Nombre = reader["Nombre"].ToString(),
                                Apellidos=reader["Apellidos"].ToString(),
                                Email = reader["Email"].ToString(),
                                Rol = reader["Rol"].ToString(),
                                Contrasenna = reader["Contrasenna"].ToString()
                            };
                            usuarios.Add(usuario);
                        }
                    }
                }
            }

            return PartialView("AdministrarUsuarios", usuarios);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        [HttpPost]
        public JsonResult EditarUsuario(Usuario usuario)
        {
            try
            {
                // Log the received data for debugging
                System.Diagnostics.Debug.WriteLine($"IdUsuario: {usuario.IdUsuario}, Nombre: {usuario.Nombre}");

                using (SqlConnection connection = new SqlConnection("data source=DESKTOP-PS1FR69\\SQLEXPRESS;initial catalog=AgendaMaster;integrated security=True;trustservercertificate=True;MultipleActiveResultSets=True;App=EntityFramework"))
                {
                    connection.Open();
                    string query = "UPDATE Usuarios SET Nombre = @Nombre, Apellidos = @Apellidos, Cedula = @Cedula, Email = @Email, Rol = @Rol, Contrasenna = @Contrasenna WHERE IdUsuario = @IdUsuario";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                        command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                        command.Parameters.AddWithValue("@Apellidos", usuario.Apellidos);
                        command.Parameters.AddWithValue("@Cedula", usuario.Cedula);
                        command.Parameters.AddWithValue("@Email", usuario.Email);
                        command.Parameters.AddWithValue("@Rol", usuario.Rol);
                        command.Parameters.AddWithValue("@Contrasenna", usuario.Contrasenna);


                        int rowsAffected = command.ExecuteNonQuery();
                        // Log the number of rows affected
                        System.Diagnostics.Debug.WriteLine($"Rows affected: {rowsAffected}");

                        if (rowsAffected == 0)
                        {
                            System.Diagnostics.Debug.WriteLine("No rows updated. Check if IdUsuario exists.");
                        }
                    }
                    return Json(new { success = true });
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }


        public ActionResult IndexAdmin()
        {
            var viewModel = new AdminViewModel
            {
                Tareas = ObtenerTarea(),
                Usuarios = ObtenerUsuarios() // Asegúrate de tener este método definido
            };
            return View(viewModel);
        }

        private List<Tarea> ObtenerTarea()
        {
            var tareas = new List<Tarea>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Nombre, Prioridad, Descripcion FROM Tareas";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                tareas.Add(new Tarea
                                {
                                    Nombre = reader["Nombre"].ToString(),
                                    Prioridad = reader["Prioridad"].ToString(),
                                    Descripcion = reader["Descripcion"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //  error
                Console.WriteLine(ex.Message);
            }

            return tareas;
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public List<Usuario> ObtenerUsuarios()
        {
            var usuarios = new List<Usuario>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Cedula, Nombre, Apellidos, Email, Rol, Contrasenna FROM Usuarios";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Usuario usuario = new Usuario
                                {
                                    Cedula = reader["Cedula"].ToString(),
                                    Nombre = reader["Nombre"].ToString(),
                                    Apellidos = reader["Apellidos"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Rol = reader["Rol"].ToString(),
                                    Contrasenna = reader["Contrasenna"].ToString()
                                };
                                usuarios.Add(usuario);
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

            return usuarios;
        }
    }
}



