using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FSU.SPORTIDY.Repository.Entities;

public partial class SportidyContext : DbContext
{
    public SportidyContext()
    {
    }

    public SportidyContext(DbContextOptions<SportidyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Club> Clubs { get; set; }

    public virtual DbSet<CommentInMeeting> CommentInMeetings { get; set; }

    public virtual DbSet<Friendship> Friendships { get; set; }

    public virtual DbSet<ImageField> ImageFields { get; set; }

    public virtual DbSet<Meeting> Meetings { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<PlayField> PlayFields { get; set; }

    public virtual DbSet<PlayFieldFeedback> PlayFieldFeedbacks { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sport> Sports { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserClub> UserClubs { get; set; }

    public virtual DbSet<UserMeeting> UserMeetings { get; set; }
    public virtual DbSet<UserToken> UserTokens { get; set; }
    public virtual DbSet<SystemFeedback> SystemFeedbacks { get; set; }
    public virtual DbSet<Payment> Payments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking__73951ACD27B15586");

            entity.ToTable("Booking");

            entity.Property(e => e.BookingId)
                .ValueGeneratedOnAdd()
                .HasColumnName("BookingID");
            entity.Property(e => e.BookingDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.DateEnd).HasColumnType("datetime");
            entity.Property(e => e.DateStart).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.PlayFieldId).HasColumnName("PlayFieldID");

            entity.HasOne(d => d.PlayField).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.PlayFieldId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__PlayFie__5070F446");
        });

        modelBuilder.Entity<Club>(entity =>
        {
            entity.HasKey(e => e.ClubId).HasName("PK__Club__D35058E79F8F62DE");

            entity.ToTable("Club");

            entity.Property(e => e.ClubId).ValueGeneratedOnAdd();
            entity.Property(e => e.AvartarClub)
                .IsUnicode(false);
            entity.Property(e => e.ClubCode)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.ClubName).IsUnicode(false);
            entity.Property(e => e.CoverImageClub)
                .IsUnicode(false);
            entity.Property(e => e.Infomation).IsUnicode(false);
            entity.Property(e => e.Location)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.MainSport)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Regulation).IsUnicode(false);
            entity.Property(e => e.Slogan)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CommentInMeeting>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__CommentI__C3B4DFAA471104AD");

            entity.ToTable("CommentInMeeting");

            entity.Property(e => e.CommentId)
                .ValueGeneratedOnAdd()
                .HasColumnName("CommentID");
            entity.Property(e => e.CommentCode)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.CommentDate).HasColumnType("datetime");
            entity.Property(e => e.Content).IsUnicode(false);
            entity.Property(e => e.Image).IsUnicode(false);
            entity.Property(e => e.MeetingId).HasColumnName("MeetingID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Meeting).WithMany(p => p.CommentInMeetings)
                .HasForeignKey(d => d.MeetingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CommentIn__Meeti__534D60F1");
        });

        modelBuilder.Entity<Friendship>(entity =>
        {
            entity.HasKey(e => e.FriendShipId).HasName("PK__Friendsh__190D637884858AB7");

            entity.ToTable("Friendship");

            entity.Property(e => e.FriendShipId)
                .ValueGeneratedOnAdd()
                .HasColumnName("FriendShipID");
            entity.Property(e => e.FriendShipCode)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.UserId1).HasColumnName("UserID1");
            entity.Property(e => e.UserId2).HasColumnName("UserID2");

            entity.HasOne(d => d.UserId1Navigation).WithMany(p => p.FriendshipUserId1Navigations)
                .HasForeignKey(d => d.UserId1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Friendshi__UserI__5441852A");

            entity.HasOne(d => d.UserId2Navigation).WithMany(p => p.FriendshipUserId2Navigations)
                .HasForeignKey(d => d.UserId2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Friendshi__UserI__5535A963");
        });

        modelBuilder.Entity<ImageField>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__ImageFie__7516F4EC9A3F7261");

            entity.ToTable("ImageField");

            entity.Property(e => e.ImageId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ImageID");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.PlayFieldId).HasColumnName("PlayFieldID");
            entity.Property(e => e.VideoUrl).HasColumnName("VideoURL");

            entity.HasOne(d => d.PlayField).WithMany(p => p.ImageFields)
                .HasForeignKey(d => d.PlayFieldId)
                .HasConstraintName("FK__ImageFiel__PlayF__4316F928");
        });

        modelBuilder.Entity<Meeting>(entity =>
        {
            entity.HasKey(e => e.MeetingId).HasName("PK__Meeting__E9F9E9AC468584B2");

            entity.ToTable("Meeting");

            entity.Property(e => e.MeetingId)
                .ValueGeneratedOnAdd()
                .HasColumnName("MeetingID");
            entity.Property(e => e.Address).IsUnicode(false);
            entity.Property(e => e.ClubId).HasColumnName("ClubID");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.IsPublic);
            entity.Property(e => e.MeetingCode)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.MeetingImage)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.MeetingName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Note).IsUnicode(false);
            entity.Property(e => e.SportId).HasColumnName("SportID");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E32B70E75BB");

            entity.ToTable("Notification");

            entity.Property(e => e.NotificationId)
                .ValueGeneratedOnAdd()
                .HasColumnName("NotificationID");
            entity.Property(e => e.InviteDate).HasColumnType("datetime");
            entity.Property(e => e.Message)
                .IsUnicode(false);
            entity.Property(e => e.NotificationCode)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.Tiltle)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Notificat__UserI__571DF1D5");
        });

        modelBuilder.Entity<PlayField>(entity =>
        {
            entity.HasKey(e => e.PlayFieldId).HasName("PK__PlayFiel__4E6EFC93249A0D85");

            entity.ToTable("PlayField");

            entity.Property(e => e.PlayFieldId)
                .ValueGeneratedOnAdd()
                .HasColumnName("PlayFieldID");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PlayFieldCode)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.PlayFieldName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.PlayFields)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_PlayField_User");

            entity.HasOne(c => c.PlayFieldContainer)
                    .WithMany(c => c.ListSubPlayFields)
                    .HasForeignKey(c => c.IsDependency);

            entity.HasOne(d => d.Sport).WithMany(p => p.PlayFields)
                .HasForeignKey(d => d.SportId)
                .HasConstraintName("FK_PlayField_Sport");

        });

        modelBuilder.Entity<PlayFieldFeedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__PlayFiel__6A4BEDF64C6D3772");

            entity.ToTable("PlayFieldFeedback");

            entity.Property(e => e.FeedbackId)
                .ValueGeneratedOnAdd()
                .HasColumnName("FeedbackID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.Content).IsUnicode(true);
            entity.Property(e => e.FeedbackCode)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.FeedbackDate).HasColumnType("datetime");
            entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");
            entity.Property(e => e.IsAnonymous).HasColumnName("IsAnonymous_");
            entity.Property(e => e.VideoUrl).HasColumnName("VideoURL");

            entity.HasOne(d => d.Booking).WithMany(p => p.PlayFieldFeedbacks)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PlayField__Booki__59063A47");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE3A038ACFA2");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId)
                .ValueGeneratedOnAdd()
                .HasColumnName("RoleID");
        });

        modelBuilder.Entity<Sport>(entity =>
        {
            entity.HasKey(e => e.SportId).HasName("PK__Sport__7A41AF1C1AA63361");

            entity.ToTable("Sport");

            entity.Property(e => e.SportId)
                .ValueGeneratedOnAdd()
                .HasColumnName("SportID");

            entity.HasMany(d => d.Users).WithMany(p => p.Sports)
                .UsingEntity<Dictionary<string, object>>(
                    "UserSport",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserSport__UserI__5FB337D6"),
                    l => l.HasOne<Sport>().WithMany()
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserSport__Sport__5EBF139D"),
                    j =>
                    {
                        j.HasKey("SportId", "UserId").HasName("PK__UserSpor__AB3923D6A026E1F3");
                        j.ToTable("UserSport");
                        j.IndexerProperty<int>("SportId").HasColumnName("SportID");
                        j.IndexerProperty<int>("UserId").HasColumnName("UserID");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CCACB5F6D77C");

            entity.ToTable("User");

            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnName("UserID");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Avartar)
                .HasMaxLength(2000)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("date");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Otp)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("OTP");
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UserCode)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.DeviceCode)
                .IsUnicode(false);
            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__RoleID__59FA5E80");
        });

        modelBuilder.Entity<UserClub>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ClubId }).HasName("PK__UserClub__7ABDC9227D05295A");

            entity.ToTable("UserClub");

            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Club).WithMany(p => p.UserClubs)
                .HasForeignKey(d => d.ClubId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserClub__ClubId__5AEE82B9");

            entity.HasOne(d => d.User).WithMany(p => p.UserClubs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserClub__UserID__5BE2A6F2");
        });

        modelBuilder.Entity<UserMeeting>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.MeetingId }).HasName("PK__UserMeet__C9175236CBB90D5E");

            entity.ToTable("UserMeeting");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.MeetingId).HasColumnName("MeetingID");
            entity.Property(e => e.ClubId).HasColumnName("ClubID");
            entity.Property(e => e.RoleInMeeting).HasColumnName("RoleInMeeting").HasMaxLength(50);

            entity.HasOne(d => d.Meeting).WithMany(p => p.UserMeetings)
                .HasForeignKey(d => d.MeetingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserMeeti__Meeti__5CD6CB2B");

            entity.HasOne(d => d.User).WithMany(p => p.UserMeetings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserMeeti__UserI__5DCAEF64");
        });

        modelBuilder.Entity<UserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserTokenId }).HasName("PK__UserToken__C423424FG41");

            entity.ToTable("UserToken");
            entity.Property(e => e.UserTokenId)
               .ValueGeneratedOnAdd()
               .HasColumnName("UserTokenId");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.AccessToken).HasColumnName("AccessToken");
            entity.Property(e => e.RefreshToken).HasColumnName("RefreshToken");
            entity.Property(e => e.ExpiredTimeAccessToken).HasColumnName("ExpAccessToken");
            entity.Property(e => e.ExpiredTimeRefreshToken).HasColumnName("ExpRefreshToken");
            entity.Property(e => e.CreateDate).HasColumnName("CreateDate");
        });

        modelBuilder.Entity<SystemFeedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__SystemFeedback__6A4BEDF64C6D3772");

            entity.ToTable("SystemFeedback");

            entity.Property(e => e.FeedbackId)
                .ValueGeneratedOnAdd()
                .HasColumnName("FeedbackID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Content).IsUnicode(true);
            entity.Property(e => e.FeedbackCode)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.FeedbackDate).HasColumnType("datetime");
            entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");
            entity.Property(e => e.IsAnonymous).HasColumnName("IsAnonymous_");
            entity.Property(e => e.VideoUrl).HasColumnName("VideoURL");

            entity.HasOne(d => d.User).WithMany(p => p.SystemFeedbacks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SystemFeedbacj__User__59063A47");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__7A41AF1C1AA63361");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId)
                .ValueGeneratedOnAdd()
                .HasColumnName("PaymentId");

            entity.Property(e => e.DateOfTransaction).HasColumnType("datetime");
            entity.Property(e => e.Amount).HasColumnName("Amount");
            entity.Property(e => e.OrderCode).HasColumnName("OrderCode");
            entity.Property(e => e.Status).HasColumnName("Status");

            entity.HasOne(d => d.Booking).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payment__Booking__59063A47");
        });
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
