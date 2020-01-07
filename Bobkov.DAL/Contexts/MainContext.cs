﻿using Bobkov.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bobkov.DAL.Contexts
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
