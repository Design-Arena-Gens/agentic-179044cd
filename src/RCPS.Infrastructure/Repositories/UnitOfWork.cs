using RCPS.Core.Entities;
using RCPS.Infrastructure.Data;

namespace RCPS.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly RcpsDbContext _context;

    public UnitOfWork(RcpsDbContext context)
    {
        _context = context;

        Clients = new RepositoryBase<Client>(_context);
        Projects = new RepositoryBase<Project>(_context);
        StatementsOfWork = new RepositoryBase<StatementOfWork>(_context);
        SowMilestones = new RepositoryBase<SowMilestone>(_context);
        ChangeRequests = new RepositoryBase<ChangeRequest>(_context);
        ProjectRoles = new RepositoryBase<ProjectRole>(_context);
        UserProfiles = new RepositoryBase<UserProfile>(_context);
        WorkLogs = new RepositoryBase<WorkLog>(_context);
        Invoices = new RepositoryBase<Invoice>(_context);
        InvoiceLines = new RepositoryBase<InvoiceLine>(_context);
        Reminders = new RepositoryBase<Reminder>(_context);
    }

    public IRepository<Client> Clients { get; }
    public IRepository<Project> Projects { get; }
    public IRepository<StatementOfWork> StatementsOfWork { get; }
    public IRepository<SowMilestone> SowMilestones { get; }
    public IRepository<ChangeRequest> ChangeRequests { get; }
    public IRepository<ProjectRole> ProjectRoles { get; }
    public IRepository<UserProfile> UserProfiles { get; }
    public IRepository<WorkLog> WorkLogs { get; }
    public IRepository<Invoice> Invoices { get; }
    public IRepository<InvoiceLine> InvoiceLines { get; }
    public IRepository<Reminder> Reminders { get; }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public ValueTask DisposeAsync()
    {
        return _context.DisposeAsync();
    }
}
