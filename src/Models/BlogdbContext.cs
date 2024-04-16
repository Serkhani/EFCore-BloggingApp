using Microsoft.EntityFrameworkCore;

namespace EFCore_BloggingApp.Models;

public partial class BlogdbContext : DbContext
{
    public BlogdbContext()
    {
    }

    public BlogdbContext(DbContextOptions<BlogdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //     => optionsBuilder.UseNpgsql("dbConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Commentid).HasName("comment_pkey");

            entity.ToTable("comment");

            entity.Property(e => e.Commentid)
                .ValueGeneratedNever()
                .HasColumnName("commentid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Postid).HasColumnName("postid");
            entity.Property(e => e.Text).HasColumnName("text");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.Postid)
                .HasConstraintName("comment_postid_fkey");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Postid).HasName("post_pkey");

            entity.ToTable("post");

            entity.Property(e => e.Postid)
                .ValueGeneratedNever()
                .HasColumnName("postid");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Title).HasColumnName("title");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
