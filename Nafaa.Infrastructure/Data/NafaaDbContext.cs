using Microsoft.EntityFrameworkCore;
using Nafaa.Domain.Entities;

namespace Nafaa.Infrastructure.Data;

public class NafaaDbContext : DbContext
{
    public NafaaDbContext(DbContextOptions<NafaaDbContext> options)
        : base(options)
    {
    }

    // === DbSets ===
    public DbSet<User> Users => Set<User>();

    public DbSet<Recipient> Recipients => Set<Recipient>();
    public DbSet<Donor> Donors => Set<Donor>();
    public DbSet<CharityStaff> CharityStaff => Set<CharityStaff>();
    public DbSet<PartnerStaff> PartnerStaff => Set<PartnerStaff>();
    public DbSet<StaffPermission> StaffPermissions => Set<StaffPermission>();

    public DbSet<Charity> Charities => Set<Charity>();
    public DbSet<Partner> Partners => Set<Partner>();

    public DbSet<VirtualCard> VirtualCards => Set<VirtualCard>();
    public DbSet<Point> Points => Set<Point>();

    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Donation> Donations => Set<Donation>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<ScheduledDonation> ScheduledDonations => Set<ScheduledDonation>();

    public DbSet<Request> Requests => Set<Request>();

    public DbSet<Conversation> Conversations => Set<Conversation>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<Chatbot> Bots => Set<Chatbot>();

    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<CardInformation> CardInformations => Set<CardInformation>();
    public DbSet<MedicalHistory> MedicalHistories => Set<MedicalHistory>();
    public DbSet<FamilyMember> FamilyMembers => Set<FamilyMember>();
    public DbSet<Housing> Housings => Set<Housing>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ---------- Keys for non-standard PKs ----------
        modelBuilder.Entity<VirtualCard>()
            .HasKey(vc => vc.VirtualCardCode);

        modelBuilder.Entity<CardInformation>()
            .HasKey(ci => ci.CardNumber);

        modelBuilder.Entity<FamilyMember>()
            .HasKey(fm => fm.NationalId);

        modelBuilder.Entity<Chatbot>()
            .HasKey(cb => cb.BotId);

        modelBuilder.Entity<Housing>()
            .HasKey(h => h.HouseId);

        modelBuilder.Entity<StaffPermission>()
            .HasKey(sp => sp.PermissionId);

        // ---------- User relationships ----------
        modelBuilder.Entity<User>()
            .HasOne(u => u.Recipient)
            .WithOne(r => r.User)
            .HasForeignKey<Recipient>(r => r.UserId);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Donor)
            .WithOne(d => d.User)
            .HasForeignKey<Donor>(d => d.UserId);

        modelBuilder.Entity<User>()
            .HasOne(u => u.PartnerStaff)
            .WithOne(ps => ps.User)
            .HasForeignKey<PartnerStaff>(ps => ps.UserId);

        modelBuilder.Entity<User>()
            .HasOne(u => u.CharityStaff)
            .WithOne(cs => cs.User)
            .HasForeignKey<CharityStaff>(cs => cs.UserId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Notifications)
            .WithOne(n => n.User)
            .HasForeignKey(n => n.UserId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Conversations)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId);

        // ---------- Recipient relationships ----------
        modelBuilder.Entity<Recipient>()
            .HasOne(r => r.Charity)
            .WithMany(c => c.Recipients)
            .HasForeignKey(r => r.CharityId);

        modelBuilder.Entity<Recipient>()
            .HasOne(r => r.VirtualCard)
            .WithOne(vc => vc.Recipient)
            .HasForeignKey<VirtualCard>(vc => vc.RecipientId);

        modelBuilder.Entity<Recipient>()
            .HasOne(r => r.Housing)
            .WithOne(h => h.Recipient)
            .HasForeignKey<Housing>(h => h.RecipientId);

        modelBuilder.Entity<FamilyMember>()
            .HasOne(fm => fm.Recipient)
            .WithMany(r => r.FamilyMembers)
            .HasForeignKey(fm => fm.RecipientId);

        modelBuilder.Entity<MedicalHistory>()
            .HasOne(mh => mh.Recipient)
            .WithMany(r => r.MedicalHistories)
            .HasForeignKey(mh => mh.RecipientId);

        // Recipient <-> Project many-to-many
        modelBuilder.Entity<Recipient>()
            .HasMany(r => r.Projects)
            .WithMany(p => p.Recipients)
            .UsingEntity<Dictionary<string, object>>(
                "ProjectRecipient",
                j => j
                    .HasOne<Project>()
                    .WithMany()
                    .HasForeignKey("ProjectsProjectId")
                    .OnDelete(DeleteBehavior.NoAction),      
                j => j
                    .HasOne<Recipient>()
                    .WithMany()
                    .HasForeignKey("RecipientsRecipientId")
                    .OnDelete(DeleteBehavior.NoAction),        
                j =>
                {
                    j.HasKey("ProjectsProjectId", "RecipientsRecipientId");
                });


        // ---------- Charity & staff ----------
        modelBuilder.Entity<CharityStaff>()
            .HasOne(cs => cs.Charity)
            .WithMany(cs => cs.StaffMembers)
            .HasForeignKey(cs => cs.CharityId);

        modelBuilder.Entity<PartnerStaff>()
            .HasOne(ps => ps.Partner)
            .WithMany()
            .HasForeignKey(ps => ps.PartnerId);

        // StaffPermission <-> Staff (CharityStaff/PartnerStaff via TPH)
        modelBuilder.Entity<StaffPermission>()
            .HasMany(sp => sp.StaffMembers)
            .WithMany();

        // Charity <-> Donor many-to-many (Favorites & Endorsements) – EF will create join tables
        modelBuilder.Entity<Donor>()
            .HasMany(d => d.FavoriteCharities)
            .WithMany(c => c.FavoriteDonors);

        modelBuilder.Entity<Donor>()
            .HasMany(d => d.EndorsedCharities)
            .WithMany(c => c.EndorsingDonors);

        // ---------- Projects ----------
        modelBuilder.Entity<Project>()
            .HasOne(p => p.Charity)
            .WithMany(c => c.Projects)
            .HasForeignKey(p => p.CharityId);

        modelBuilder.Entity<Project>()
            .HasOne(p => p.CreatedBy)
            .WithMany()  // if you later want navigation on Staff, you can add it
            .HasForeignKey(p => p.CreatedByStaffId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Project>()
            .HasMany(p => p.Donations)
            .WithOne(d => d.Project)
            .HasForeignKey(d => d.ProjectId);

        // CharityStaff <-> Project many-to-many
        modelBuilder.Entity<Project>()
            .HasMany(p => p.StaffMembers)
            .WithMany(cs => cs.Projects)
            .UsingEntity<Dictionary<string, object>>(
                "CharityStaffProject",
                j => j
                    .HasOne<CharityStaff>()
                    .WithMany()
                    .HasForeignKey("StaffMembersStaffId")
                    .OnDelete(DeleteBehavior.NoAction),
                j => j
                    .HasOne<Project>()
                    .WithMany()
                    .HasForeignKey("ProjectsProjectId")
                    .OnDelete(DeleteBehavior.NoAction),
                j =>
                {
                    j.HasKey("ProjectsProjectId", "StaffMembersStaffId");
                });


        // ---------- Donations & Payments ----------
        modelBuilder.Entity<Donation>()
            .HasOne(d => d.Donor)
            .WithMany(dn => dn.Donations)
            .HasForeignKey(d => d.DonorId);

        modelBuilder.Entity<Donation>()
            .HasOne(d => d.Payment)
            .WithOne(p => p.Donation)
            .HasForeignKey<Payment>(p => p.DonationId);

        // ---------- Scheduled donations ----------
        modelBuilder.Entity<ScheduledDonation>()
            .HasOne(sd => sd.Donor)
            .WithMany(d => d.ScheduledDonations)
            .HasForeignKey(sd => sd.DonorId);

        modelBuilder.Entity<ScheduledDonation>()
            .HasOne(sd => sd.Charity)
            .WithMany()
            .HasForeignKey(sd => sd.CharityId);

        modelBuilder.Entity<ScheduledDonation>()
            .HasOne(sd => sd.Project)
            .WithMany()
            .HasForeignKey(sd => sd.ProjectId);

        // ---------- Requests ----------
        modelBuilder.Entity<Request>()
            .HasOne(r => r.Recipient)
            .WithMany(rec => rec.Requests)
            .HasForeignKey(r => r.RecipientId);

        // Request <-> CharityStaff many-to-many (ReviewerStaff)
        modelBuilder.Entity<Request>()
            .HasMany(r => r.ReviewerStaff)
            .WithMany(cs => cs.ReviewedRequests)
            .UsingEntity<Dictionary<string, object>>(
                "RequestReviewer",
                j => j
                    .HasOne<CharityStaff>()
                    .WithMany()
                    .HasForeignKey("CharityStaffId")
                    .OnDelete(DeleteBehavior.NoAction),  
                j => j
                    .HasOne<Request>()
                    .WithMany()
                    .HasForeignKey("RequestId")
                    .OnDelete(DeleteBehavior.NoAction),    
                j =>
                {
                    j.HasKey("RequestId", "CharityStaffId");
                });


        // ---------- VirtualCard & Points ----------
        modelBuilder.Entity<Point>()
            .HasOne(p => p.VirtualCard)
            .WithMany(vc => vc.Points)
            .HasForeignKey(p => p.VirtualCardCode)
            .OnDelete(DeleteBehavior.NoAction);  

        modelBuilder.Entity<Point>()
            .HasOne(p => p.Charity)
            .WithMany()
            .HasForeignKey(p => p.CharityId)
            .OnDelete(DeleteBehavior.NoAction);   


        // ---------- Conversations & Chatbot ----------
        modelBuilder.Entity<Conversation>()
            .HasOne(c => c.User)
            .WithMany(u => u.Conversations)
            .HasForeignKey(c => c.UserId);

        modelBuilder.Entity<Conversation>()
            .HasOne(c => c.Bot)
            .WithMany(b => b.Conversations)
            .HasForeignKey(c => c.BotId);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Conversation)
            .WithMany(c => c.Messages)
            .HasForeignKey(m => m.ConversationId);
    }
}
