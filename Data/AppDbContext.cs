using EfCoreRelationshipDemo.Models.ManyToMany;
using EfCoreRelationshipDemo.Models.OneToMany;
using EfCoreRelationshipDemo.Models.OneToOne;
using Microsoft.EntityFrameworkCore;

namespace EfCoreRelationshipDemo.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Person> Persons => Set<Person>();
    public DbSet<Passport> Passports => Set<Passport>();
    public DbSet<Blog> Blogs => Set<Blog>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(e =>
        {
            e.Property(p => p.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Passport>(e =>
        {
            e.HasKey(p => p.PersonId);
            e.Property(p => p.PersonId).ValueGeneratedNever();
            e.Property(p => p.Number).HasMaxLength(20);
            e.HasIndex(p => p.Number).IsUnique();
        });

        modelBuilder.Entity<Person>()
            .HasOne<Passport>(p => p.Passport)
            .WithOne(pp => pp.Person)
            .HasForeignKey<Passport>(pp => pp.PersonId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Blog>(e =>
        {
            e.Property(b => b.Url).HasMaxLength(500);
        });

        modelBuilder.Entity<Post>(e =>
        {
            e.Property(p => p.Title).HasMaxLength(200);
            e.Property(p => p.Content).HasMaxLength(2000);
        });

        modelBuilder.Entity<Blog>()
            .HasMany<Post>(b => b.Posts)
            .WithOne(p => p.Blog)
            .HasForeignKey(p => p.BlogId);

        modelBuilder.Entity<Student>(e =>
        {
            e.Property(s => s.FullName).HasMaxLength(150);
        });

        modelBuilder.Entity<Course>(e =>
        {
            e.Property(c => c.Title).HasMaxLength(200);
        });

        modelBuilder.Entity<Enrollment>()
             .HasKey(e => new { e.StudentId, e.CourseId });

        modelBuilder.Entity<Enrollment>()
            .HasOne<Student>(p => p.Student)
            .WithMany(s => s.Enrollments)
            .HasForeignKey(e => e.StudentId);

        modelBuilder.Entity<Enrollment>()
            .HasOne<Course>(e => e.Course)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(e => e.CourseId);
    }
}
