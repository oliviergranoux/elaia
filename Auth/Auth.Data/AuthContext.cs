using Microsoft.EntityFrameworkCore;
using System;

using Elaia.Auth.Data.Common.Models;

namespace Elaia.Auth.Data
{
    public class AuthContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite("Data Source=Auth.db");
    }
}
