using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarea2AP2.Models;

namespace Tarea2AP2.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Personas> Personas { get; set; }

        public Contexto(DbContextOptions<Contexto> options) : base(options) { }
    }
}
