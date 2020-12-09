using System;
using Auth.Data.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.Data
{
    public class AuthContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite("Data Source=Auth.db");
    }
}
