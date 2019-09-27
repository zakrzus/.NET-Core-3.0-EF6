using entity30.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace entity30.Data
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public BloggingContext(DbContextOptions<BloggingContext> options)
        : base(options)
        { 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\\mssqllocaldb;Database=aspnet-entity30-7309338B-EB2A-4161-8571-FA7A7CBD5DD6;Trusted_Connection=True;ConnectRetryCount=0");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>().HasMany(s => s.Posts).WithOne(s => s.Blog);
        }
    }
}
