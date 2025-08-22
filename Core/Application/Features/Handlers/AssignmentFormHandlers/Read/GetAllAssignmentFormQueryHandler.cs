using Application.Features.Queries.AssignmentFormQueries;
using Application.Features.Results.AssignmentFormResults;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.AssignmentFormHandlers.Read;

public class GetAllAssignmentFormQueryHandler : IRequestHandler<GetAllAssignmentFormQuery, List<GetAllAssignmentFormQueryResult>>
{
    private readonly IGenericRepository<Domain.Entities.AssignmentForm> _repository;

    public GetAllAssignmentFormQueryHandler(IGenericRepository<Domain.Entities.AssignmentForm> repository)
    {
        _repository = repository;
    }

    public async Task<List<GetAllAssignmentFormQueryResult>> Handle(GetAllAssignmentFormQuery request, CancellationToken cancellationToken)
    {
        var assignmentForms = await _repository.GetAllAsync();

        return assignmentForms.Select(af => new GetAllAssignmentFormQueryResult
        {
            Id = af.Id,
            AssignmentFormName = af.AssignmentFormName,
            AssignmentFormDescription = af.AssignmentFormDescription,
            AssignmentFormFilePath = af.AssignmentFormFilePath,
            UploadedTime = af.UploadedTime
        }).ToList();
    }
}
