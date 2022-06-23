using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaWeb.Model
{
    public class Contacto
    {
        [Key]
        public int ContactoId { get; set; }

        public Usuario Usuario { get; set; }
        public int UsuarioId { get; set; }

        [Column(TypeName ="varchar")]
        [StringLength(250)]
        public string NombreContacto { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(250)]
        public string Telefono { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(250)]
        public string Email { get; set; }

        public Imagen Imagen { get; set; }
        public int? ImagenId { get; set; }

    }
}
