using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgendaMaster.Entidades
{
    public class Usuario
    {
        public string IdUsuario { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }
        public string Contrasenna { get; set; }
    }
}