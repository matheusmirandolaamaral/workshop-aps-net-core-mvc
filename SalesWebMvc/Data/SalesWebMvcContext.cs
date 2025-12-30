using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;

namespace SalesWebMvc.Data
{
    public class SalesWebMvcContext : DbContext
    {
        public SalesWebMvcContext (DbContextOptions<SalesWebMvcContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Department { get; set; }
        public DbSet<Seller> Seller { get; set; }
        public DbSet<SalesRecord> SalesRecord { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. Sempre chame a implementação base primeiro
            base.OnModelCreating(modelBuilder);

            // 2. Configuração: SalesRecord -> Seller
            modelBuilder.Entity<SalesRecord>()
                .HasOne(sr => sr.Seller)       // SalesRecord possui UM Seller
                .WithMany(s => s.Sales)        // Seller possui MUITOS Sales (coleção que vi na sua classe Seller)
                .HasForeignKey("SellerId")     // Nome da coluna automática no banco
                .OnDelete(DeleteBehavior.Restrict); // BLOQUEIA deleção se houver vendas

            // 3. Configuração: Seller -> Department
            modelBuilder.Entity<Seller>()
                .HasOne(s => s.Department)
                .WithMany(d => d.Sellers)      // Department possui MUITOS Sellers (coleção na sua classe Department)
                .HasForeignKey(s => s.DepartmentId) // Aqui você tem o ID físico na classe Seller
                .OnDelete(DeleteBehavior.Restrict); // BLOQUEIA deleção se houver vendedores
        }
    }

}
