using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieRental.Models;

namespace MovieRental.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
