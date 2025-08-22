using Application.Features.Queries.MaintanceFormQueries;
using Application.Features.Results.MaintanceFormResults;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Handlers.MaintanceFormHandlers.Read;

public class GetMaintanceFormQueryHandler : IRequestHandler<GetMaintanceFormQuery,List<GetMaintanceFormQueryResult>>
{
    private readonly IGenericRepository<MaintanceForm> _repository;

    public GetMaintanceFormQueryHandler(IGenericRepository<MaintanceForm> repository)
    {
         _repository = repository;
    }
    
    public async Task<List<GetMaintanceFormQueryResult>> Handle(GetMaintanceFormQuery request, CancellationToken cancellationToken)
    {
        var values =  await _repository.GetAllAsync();
        return values.Select(x => new GetMaintanceFormQueryResult
        {
            Id = x.Id,
            FormName = x.FormName,
            FormDescription = x.FormDescription,
            FormFilePath = x.FormFilePath,
            UploadedTime = x.UploadedTime
        }).ToList();
    }
}