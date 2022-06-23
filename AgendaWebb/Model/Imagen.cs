using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaWeb.Model
{
    public class Imagen
    {
        [Key]
        public int ImagenId { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(500)]
        public string FileName { get; set; }
        public string Path { get; set; }
        public bool Temporal { get; set; }
        public DateTime FechaSubida { get; set; }

    }
}
