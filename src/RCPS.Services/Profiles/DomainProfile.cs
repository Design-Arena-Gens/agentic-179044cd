using AutoMapper;
using RCPS.Core.DTOs;
using RCPS.Core.Entities;

namespace RCPS.Services.Profiles;

public class DomainProfile : Profile
{
    public DomainProfile()
    {
        CreateMap<Client, ClientSummaryDto>();
        CreateMap<Client, ClientDetailDto>()
            .ForCtorParam(nameof(ClientDetailDto.Projects), opt => opt.MapFrom(src => src.Projects));

        CreateMap<Project, ProjectSummaryDto>();
        CreateMap<Project, ProjectDetailDto>()
            .ForCtorParam(nameof(ProjectDetailDto.Client), opt => opt.MapFrom(src => src.Client))
            .ForCtorParam(nameof(ProjectDetailDto.StatementsOfWork), opt => opt.MapFrom(src => src.StatementsOfWork))
            .ForCtorParam(nameof(ProjectDetailDto.ChangeRequests), opt => opt.MapFrom(src => src.ChangeRequests))
            .ForCtorParam(nameof(ProjectDetailDto.WorkLogs), opt => opt.MapFrom(src => src.WorkLogs))
            .ForCtorParam(nameof(ProjectDetailDto.Invoices), opt => opt.MapFrom(src => src.Invoices))
            .ForCtorParam(nameof(ProjectDetailDto.Reminders), opt => opt.MapFrom(src => src.Reminders));

        CreateMap<StatementOfWork, StatementOfWorkSummaryDto>();
        CreateMap<StatementOfWork, StatementOfWorkDetailDto>()
            .ForCtorParam(nameof(StatementOfWorkDetailDto.Milestones), opt => opt.MapFrom(src => src.Milestones));

        CreateMap<SowMilestone, SowMilestoneDto>();
        CreateMap<ChangeRequest, ChangeRequestSummaryDto>();
        CreateMap<ChangeRequest, ChangeRequestDetailDto>();

        CreateMap<WorkLog, WorkLogSummaryDto>();
        CreateMap<WorkLog, WorkLogDetailDto>();

        CreateMap<Invoice, InvoiceSummaryDto>();
        CreateMap<Invoice, InvoiceDetailDto>()
            .ForCtorParam(nameof(InvoiceDetailDto.Lines), opt => opt.MapFrom(src => src.Lines));
        CreateMap<InvoiceLine, InvoiceLineDto>()
            .ForCtorParam(nameof(InvoiceLineDto.LineTotal), opt => opt.MapFrom(src => src.LineTotal));

        CreateMap<Reminder, ReminderDto>();
    }
}
