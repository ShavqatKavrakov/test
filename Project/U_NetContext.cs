using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Project.Models;

namespace Project
{
    public partial class U_NetContext : DbContext
    {
        public  DbSet<Predmeti> Predmeti { get; set; } = null!;
        public  DbSet<Prepodavateli> Prepodavateli { get; set; } = null!;
        public  DbSet<Specialnocti> Specialnocti { get; set; } = null!;
        public  DbSet<Studenti> Studenti { get; set; } = null!;
        public  DbSet<Vedomosti> Vedomosti { get; set; } = null!;

        public U_NetContext(DbContextOptions<U_NetContext> options)
            :base(options)
        {
           // Database.EnsureCreated();
        }
    }
}
