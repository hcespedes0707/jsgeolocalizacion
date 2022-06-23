using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AgendaWeb.Model
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(250)]
        public string FullName { get; set; }


        [Column(TypeName = "varchar")]
        [StringLength(250)]
        public string UserName { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(500)]
        public string Password { get; set; }

    }
}
