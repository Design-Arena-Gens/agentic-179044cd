using Microsoft.EntityFrameworkCore;
using RCPS.Core.Entities;

namespace RCPS.Infrastructure.Data;

public class RcpsDbContext : DbContext
{
    public RcpsDbContext(DbContextOptions<RcpsDbContext> options) : base(options)
    {
    }

    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<StatementOfWork> StatementsOfWork => Set<StatementOfWork>();
    public DbSet<SowMilestone> SowMilestones => Set<SowMilestone>();
    public DbSet<ChangeRequest> ChangeRequests => Set<ChangeRequest>();
    public DbSet<ProjectRole> ProjectRoles => Set<ProjectRole>();
    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
    public DbSet<WorkLog> WorkLogs => Set<WorkLog>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceLine> InvoiceLines => Set<InvoiceLine>();
    public DbSet<Reminder> Reminders => Set<Reminder>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RcpsDbContext).Assembly);
    }
}
