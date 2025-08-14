using Application.Features.Results.MaintenanceTypeResults;
using MediatR;

namespace Application.Features.Queries.MaintenanceTypeQueries;

public class GetAllMaintenanceTypeQuery : IRequest<List<GetAllMaintenanceTypeQueryResult>>
{
    
}