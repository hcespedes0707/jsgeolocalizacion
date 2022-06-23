using AgendaWeb.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaWeb.Data
{
    public class ApplicationDbContext : DbContext
    {

        public virtual DbSet<Usuario> Usuario { set; get; }
        public virtual DbSet<Imagen> Imagen { get; set; }
        public virtual DbSet<Contacto> Contacto { get; set; }


        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }


    }
}
