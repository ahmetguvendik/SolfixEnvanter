using Application.Features.Queries.InternetLinesQueries;
using Application.Features.Results.InternetLinesResults;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.InternetLinesHandlers.Read;

public class GetAllInternetLinesQueryHandler  : IRequestHandler<GetAllInternetLinesQuery, List<GetAllInternetLinesQueryResult>>
{
    private readonly IInternetLinesRepository _internetLinesRepository;

    public GetAllInternetLinesQueryHandler(IInternetLinesRepository internetLinesRepository)
    {
         _internetLinesRepository = internetLinesRepository;
    }
    
    public async Task<List<GetAllInternetLinesQueryResult>> Handle(GetAllInternetLinesQuery request, CancellationToken cancellationToken)
    {
        var values = await _internetLinesRepository.GetAllInternetLinesWithLocation();
        return values.Select(x => new GetAllInternetLinesQueryResult
        {
            Id = x.Id,
            LineNumber = x.LineNumber,
            Provider = x.Provider,
            Speed = x.Speed,
            ContractEndDate = x.ContractEndDate,
            LocationName = x.Location.Name
        }).ToList();
    }
}