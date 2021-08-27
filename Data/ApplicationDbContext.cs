using Financa.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Financa.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
                
        public DbSet<Corretora> Corretoras { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<EmpresaTipo> EmpresaTipos { get; set; }
        public DbSet<Investimento> Investimentos { get; set; }
        public DbSet<Provento> Proventos { get; set; }
        

    }
}
