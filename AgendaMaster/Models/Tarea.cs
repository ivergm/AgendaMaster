using System;
using System.ComponentModel.DataAnnotations;

namespace AgendaMaster.Models
{
    public class Tarea
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Prioridad { get; set; }

        public string Descripcion { get; set; }

        public int TiempoInvertido { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}