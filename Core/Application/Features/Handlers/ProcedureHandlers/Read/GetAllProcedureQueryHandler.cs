using Application.Features.Queries.ProcedureQueries;
using Application.Features.Results.ProcedureResults;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.ProcedureHandlers.Read;

public class GetAllProcedureQueryHandler : IRequestHandler<GetAllProcedureQuery, List<GetAllProcedureQueryResult>>
{
    private readonly IGenericRepository<Domain.Entities.Procedure> _repository;

    public GetAllProcedureQueryHandler(IGenericRepository<Domain.Entities.Procedure> repository)
    {
        _repository = repository;
    }

    public async Task<List<GetAllProcedureQueryResult>> Handle(GetAllProcedureQuery request, CancellationToken cancellationToken)
    {
        var procedures = await _repository.GetAllAsync();

        return procedures.Select(p => new GetAllProcedureQueryResult
        {
            Id = p.Id,
            ProcedureName = p.ProcedureName,
            ProcedureDescription = p.ProcedureDescription,
            ProcedureFilePath = p.ProcedureFilePath,
            UploadedTime = p.UploadedTime
        }).ToList();
    }
}
