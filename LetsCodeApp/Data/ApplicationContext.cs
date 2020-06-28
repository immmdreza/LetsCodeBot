using LetsCodeApp.Models.BaseModels;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace LetsCodeApp.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        public ApplicationContext()
        {
        }

        public virtual DbSet<Group> Groups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //It's bad, find a more Secure way to pass connectionString here!
                optionsBuilder.UseSqlServer("connectionString");
            }
        }
    }
}
