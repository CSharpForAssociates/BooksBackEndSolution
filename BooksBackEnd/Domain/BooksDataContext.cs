using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksBackEnd.Domain
{
    public class BooksDataContext : DbContext
    {
        public BooksDataContext(DbContextOptions<BooksDataContext> options): base(options) { }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().Property(b => b.Title).HasMaxLength(200);
            modelBuilder.Entity<Book>().Property(b => b.Author).HasMaxLength(200);
        }
    }
}
