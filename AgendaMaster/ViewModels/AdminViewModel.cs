// ViewModels/AdminViewModel.cs
using System.Collections.Generic;
using AgendaMaster.Entidades;
using AgendaMaster.Models;

namespace AgendaMaster.ViewModels
{
    public class AdminViewModel
    {
        public List<Tarea> Tareas { get; set; }
        public List<Usuario> Usuarios { get; set; }
    }
}
