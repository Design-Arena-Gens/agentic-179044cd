using RCPS.Core.Entities;

namespace RCPS.Infrastructure.Repositories;

public interface IUnitOfWork : IAsyncDisposable
{
    IRepository<Client> Clients { get; }
    IRepository<Project> Projects { get; }
    IRepository<StatementOfWork> StatementsOfWork { get; }
    IRepository<SowMilestone> SowMilestones { get; }
    IRepository<ChangeRequest> ChangeRequests { get; }
    IRepository<ProjectRole> ProjectRoles { get; }
    IRepository<UserProfile> UserProfiles { get; }
    IRepository<WorkLog> WorkLogs { get; }
    IRepository<Invoice> Invoices { get; }
    IRepository<InvoiceLine> InvoiceLines { get; }
    IRepository<Reminder> Reminders { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
