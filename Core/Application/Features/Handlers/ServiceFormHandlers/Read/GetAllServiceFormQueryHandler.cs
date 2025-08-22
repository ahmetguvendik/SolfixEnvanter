using Application.Features.Queries.ServiceFormQueries;
using Application.Features.Results.ServiceFormResults;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.ServiceFormHandlers.Read;

public class GetAllServiceFormQueryHandler : IRequestHandler<GetAllServiceFormQuery, List<GetAllServiceFormQueryResult>>
{
    private readonly IGenericRepository<Domain.Entities.ServiceForm> _repository;

    public GetAllServiceFormQueryHandler(IGenericRepository<Domain.Entities.ServiceForm> repository)
    {
        _repository = repository;
    }

    public async Task<List<GetAllServiceFormQueryResult>> Handle(GetAllServiceFormQuery request, CancellationToken cancellationToken)
    {
        var serviceForms = await _repository.GetAllAsync();

        return serviceForms.Select(sf => new GetAllServiceFormQueryResult
        {
            Id = sf.Id,
            ServiceFormName = sf.ServiceFormName,
            ServiceFormDescription = sf.ServiceFormDescription,
            ServiceFormFilePath = sf.ServiceFormFilePath,
            UploadedTime = sf.UploadedTime
        }).ToList();
    }
}
