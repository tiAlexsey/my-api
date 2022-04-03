using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace my_api.Entities
{
    public partial class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
        }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<NewComment> Comments { get; set; }
        public virtual DbSet<Film> Films { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=HOME-PC;Database=PortalDB;Trusted_Connection=True;");

                optionsBuilder.LogTo(message => System.Diagnostics.Debug.WriteLine(message));
                optionsBuilder.LogTo(System.Console.WriteLine);
                optionsBuilder.LogTo(Console.WriteLine, new[] { RelationalEventId.CommandExecuted });
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}