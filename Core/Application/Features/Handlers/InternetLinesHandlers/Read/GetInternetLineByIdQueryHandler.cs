using Application.Features.Queries.InternetLinesQueries;
using Application.Features.Results.InternetLinesResults;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.InternetLinesHandlers.Read;

public class GetInternetLineByIdQueryHandler : IRequestHandler<GetInternetLineByIdQuery, GetInternetLineByIdQueryResult>
{
    private readonly IInternetLinesRepository _internetLinesRepository;

    public GetInternetLineByIdQueryHandler(IInternetLinesRepository internetLinesRepository)
    {
        _internetLinesRepository = internetLinesRepository;
    }

    public async Task<GetInternetLineByIdQueryResult> Handle(GetInternetLineByIdQuery request, CancellationToken cancellationToken)
    {
        var internetLine = await _internetLinesRepository.GetInternetLineByIdWithLocation(request.Id);
        
        if (internetLine == null)
            throw new Exception("Internet Line not found");

        return new GetInternetLineByIdQueryResult
        {
            Id = internetLine.Id,
            LineNumber = internetLine.LineNumber,
            Provider = internetLine.Provider,
            Speed = internetLine.Speed,
            ContractEndDate = internetLine.ContractEndDate,
            LocationId = internetLine.LocationId,
            LocationName = internetLine.Location?.Name,
        };
    }
}
