using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListApplication.Authentication;

namespace TodoListApplication.Models
{
    public class ToDoListDbContext: IdentityDbContext<AppUser>
    {
        public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options) : base(options)
        {

        }

        public DbSet<ToDoListItem> ToDoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ToDoListItem>(entity =>
            {
                entity.Property(e => e.ItemName)
                .IsRequired()
                .HasMaxLength(200);

                entity.Property(e => e.ItemDescription)
                .IsRequired()
                .HasMaxLength(200);

                entity.Property(e => e.ItemStatus)
                .IsRequired()
                .HasMaxLength(1);

                entity.Property(e => e.loggedUser)
                .IsRequired()
                .HasMaxLength(200);
            });

            base.OnModelCreating(builder);
        }
    }
}
