using Microsoft.EntityFrameworkCore;
using RCPS.Core.Entities;
using RCPS.Core.Enums;

namespace RCPS.Infrastructure.Data;

public static class RcpsDbSeeder
{
    public static async Task SeedAsync(RcpsDbContext context, CancellationToken cancellationToken = default)
    {
        if (await context.Clients.AnyAsync(cancellationToken))
        {
            return;
        }

        var client = new Client
        {
            Name = "Acme Robotics",
            PrimaryContactName = "Dana White",
            PrimaryContactEmail = "dana.white@acmerobotics.co",
            PhoneNumber = "+1-425-555-0100",
            BillingAddress = "742 Evergreen Terrace, Seattle, WA"
        };

        var project = new Project
        {
            Client = client,
            Name = "Robotic Fulfillment Rollout",
            Code = "ACM-ROBO-01",
            Status = ProjectStatus.Active,
            BudgetAmount = 750_000m,
            ActualCost = 410_000m,
            RecognizedRevenue = 520_000m,
            BilledAmount = 450_000m,
            StartDate = DateTime.UtcNow.AddMonths(-5),
            Description = "Deploy autonomous fulfillment robots across three distribution centers.",
            EngagementLead = "Samantha Carter"
        };

        var sow = new StatementOfWork
        {
            Project = project,
            Title = "Phase 1 Deployment",
            Version = "1.0",
            ContractAmount = 600_000m,
            BillingFrequency = BillingFrequency.Milestone,
            EffectiveDate = DateTime.UtcNow.AddMonths(-5),
            ScopeOverview = "Discovery, Integration, and Pilot deployment."
        };

        sow.Milestones.Add(new SowMilestone
        {
            Name = "Discovery Complete",
            PlannedDate = DateTime.UtcNow.AddMonths(-4),
            ActualDate = DateTime.UtcNow.AddMonths(-4),
            Amount = 150_000m,
            IsCompleted = true
        });

        sow.Milestones.Add(new SowMilestone
        {
            Name = "Integration Complete",
            PlannedDate = DateTime.UtcNow.AddMonths(-2),
            Amount = 200_000m,
            IsCompleted = false
        });

        var consultant = new UserProfile
        {
            FullName = "Jordan Maxwell",
            Email = "jordan.maxwell@rcps.io",
            Role = "Solution Architect",
            Department = "Delivery",
            DefaultHourlyRate = 185m
        };

        var analyst = new UserProfile
        {
            FullName = "Priya Natarajan",
            Email = "priya.natarajan@rcps.io",
            Role = "Business Analyst",
            Department = "Delivery",
            DefaultHourlyRate = 140m
        };

        var architectRole = new ProjectRole
        {
            Project = project,
            UserProfile = consultant,
            RoleName = "Lead Architect",
            HourlyRate = 185m,
            AllocatedHours = 320
        };

        var analystRole = new ProjectRole
        {
            Project = project,
            UserProfile = analyst,
            RoleName = "Business Analyst",
            HourlyRate = 140m,
            AllocatedHours = 280
        };

        var changeRequest = new ChangeRequest
        {
            Project = project,
            Title = "Warehouse 3 Scope Expansion",
            Description = "Additional robotics lanes and integration with client WMS.",
            Status = ChangeRequestStatus.Approved,
            EstimatedEffortHours = 160,
            EstimatedCost = 45_000m,
            EstimatedRevenue = 75_000m,
            RequestedOn = DateTime.UtcNow.AddMonths(-1),
            ApprovedOn = DateTime.UtcNow.AddDays(-21),
            RequestedBy = "Dana White",
            ApprovedBy = "Samantha Carter"
        };

        var workLogs = new List<WorkLog>
        {
            new()
            {
                Project = project,
                StatementOfWork = sow,
                UserProfile = consultant,
                WorkDate = DateTime.UtcNow.AddDays(-14),
                Hours = 8,
                BillableRate = 185m,
                IsBillable = true,
                Notes = "Solution architecture workshops."
            },
            new()
            {
                Project = project,
                StatementOfWork = sow,
                UserProfile = analyst,
                WorkDate = DateTime.UtcNow.AddDays(-12),
                Hours = 7,
                BillableRate = 140m,
                IsBillable = true,
                Notes = "Business requirements documentation."
            },
            new()
            {
                Project = project,
                ChangeRequest = changeRequest,
                UserProfile = consultant,
                WorkDate = DateTime.UtcNow.AddDays(-5),
                Hours = 6,
                BillableRate = 185m,
                IsBillable = true,
                Notes = "CR solution design."
            }
        };

        var invoice = new Invoice
        {
            Project = project,
            InvoiceNumber = "INV-2024-1045",
            IssueDate = DateTime.UtcNow.AddDays(-20),
            DueDate = DateTime.UtcNow.AddDays(10),
            Status = InvoiceStatus.Sent,
            Subtotal = 180_000m,
            TaxAmount = 0,
            TotalAmount = 180_000m,
            AmountPaid = 90_000m
        };

        invoice.Lines.Add(new InvoiceLine
        {
            Description = "Milestone: Discovery Complete",
            Quantity = 1,
            UnitPrice = 150_000m
        });

        invoice.Lines.Add(new InvoiceLine
        {
            Description = "Time & Materials - April",
            Quantity = 210,
            UnitPrice = 143m
        });

        var reminder = new Reminder
        {
            Project = project,
            ReminderType = ReminderType.InvoiceDue,
            TriggerOn = DateTime.UtcNow.AddDays(7),
            Message = "Invoice INV-2024-1045 due in 7 days."
        };

        context.Clients.Add(client);
        context.Projects.Add(project);
        context.StatementsOfWork.Add(sow);
        context.ProjectRoles.AddRange(architectRole, analystRole);
        context.ChangeRequests.Add(changeRequest);
        context.WorkLogs.AddRange(workLogs);
        context.Invoices.Add(invoice);
        context.Reminders.Add(reminder);

        await context.SaveChangesAsync(cancellationToken);
    }
}
