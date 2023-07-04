using ApiCRUD.Models.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCRUD.Domain.Repositories
{

    public class MyDbContext : DbContext
    {
        public DbSet<ClientInfoModel> ClientEntity { get; set; }
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
    
}
