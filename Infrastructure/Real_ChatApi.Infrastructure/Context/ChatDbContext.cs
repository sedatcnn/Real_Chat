using Microsoft.EntityFrameworkCore;
using Real_ChatApi.Domain.Entites;

namespace Real_ChatApi.Infrastructure.Context
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Group> Groups => Set<Group>();
        public DbSet<Message> Messages => Set<Message>();
        public DbSet<GroupUser> GroupUsers => Set<GroupUser>();
        public DbSet<JoinRequest> GroupJoins => Set<JoinRequest>();
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GroupUser>()
                .HasKey(gu => new { gu.GroupId, gu.UserId });

            modelBuilder.Entity<GroupUser>()
                .HasOne(gu => gu.User)
                .WithMany(u => u.Groups)
                .HasForeignKey(gu => gu.UserId);

            modelBuilder.Entity<GroupUser>()
                .HasOne(gu => gu.Group)
                .WithMany(g => g.Members)
                .HasForeignKey(gu => gu.GroupId);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.SenderUser)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.SenderUserId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Group)
                .WithMany(g => g.Messages)
                .HasForeignKey(m => m.GroupId);

            modelBuilder.Entity<JoinRequest>()
                .HasOne(j => j.RequesterUser)
                .WithMany()
                .HasForeignKey(j => j.RequesterUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JoinRequest>()
                .HasOne(j => j.ApproverUser)
                .WithMany()
                .HasForeignKey(j => j.ApproverUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JoinRequest>()
                .HasOne(j => j.Group)
                .WithMany(g => g.JoinRequests)
                .HasForeignKey(j => j.GroupId);

            modelBuilder.Entity<Group>()
                .HasOne(g => g.CreatedByUser)
                .WithMany()
                .HasForeignKey(g => g.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
