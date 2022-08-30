using ToDoList.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Models;

namespace ToDoList.Data;

public class BookWebContext : IdentityDbContext<BookWebUser>
{
    public BookWebContext(DbContextOptions<BookWebContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }

    public DbSet<ToDoList.Models.Category>? Category { get; set; }

}

internal class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<BookWebUser>
{
    public void Configure(EntityTypeBuilder<BookWebUser> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(255);
        builder.Property(u => u.LastName).HasMaxLength(255);
    }
}