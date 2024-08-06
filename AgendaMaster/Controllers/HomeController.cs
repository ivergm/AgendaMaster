using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
using AgendaMaster.Models;

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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult IndexAdmin()
        {
            var tareas = ObtenerTarea();
            return View(tareas); // Pasar las tareas a la vista para que puedan verse
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
    }
}



