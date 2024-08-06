using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgendaMaster.Entidades
{
    public class Proyecto
    {
        public string NombreProyecto { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Estatus { get; set; }
    }
}