﻿using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

namespace CoreTestFramework.Northwind.Entities.Model
{
    public class NorthwindContext : DbContext
    {
        private readonly string _connString = "Data Source= ../CoreTestFramework.Northwind.Entities/Database/northwind.db";

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<CustomerDemographic> CustomerDemographics => Set<CustomerDemographic>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connString);
            base.OnConfiguring(optionsBuilder);
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Navigation(p => p.Supplier).AutoInclude();
            modelBuilder.Entity<Product>().Navigation(p => p.Category).AutoInclude();
            modelBuilder.Entity<Product>().Navigation(p => p.OrderDetails).AutoInclude();

            
            // modelBuilder.Entity<Product>(entity => {
            //      entity.HasMany(p => p.OrderDetails)
            //     .WithOne(od => od.Product);
            // });
            
            
            // modelBuilder.Entity<Order>()
            // .HasMany(o => o.OrderDetails)
            // .WithOne()
            // .HasForeignKey(o => o.OrderID);

            modelBuilder.Entity<OrderDetail>(entity => {
                entity.HasKey(x => new {x.OrderID, x.ProductID});
                entity.ToView("OrderDetails Extended");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}