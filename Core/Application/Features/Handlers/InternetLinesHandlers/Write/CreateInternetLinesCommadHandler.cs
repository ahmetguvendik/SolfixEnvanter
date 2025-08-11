using Application.Features.Commands.InternetLinesCommands;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Handlers.InternetLinesHandlers;

public class CreateInternetLinesCommadHandler : IRequestHandler<CreateInternetLinesCommad>
{
    private readonly IGenericRepository<InternetLine> _repository;

    public CreateInternetLinesCommadHandler(IGenericRepository<InternetLine> repository)
    {
         _repository = repository;
    }
    
    public async Task Handle(CreateInternetLinesCommad request, CancellationToken cancellationToken)
    {
        var internetLine = new InternetLine();
        internetLine.Id = Guid.NewGuid().ToString();
        internetLine.Speed = request.Speed;
        internetLine.ContractEndDate = request.ContractEndDate;
        internetLine.LocationId = request.LocationId;
        internetLine.LineNumber = request.LineNumber;
        internetLine.Provider = request.Provider;
        await _repository.CreateAsync(internetLine);
        await _repository.SaveChangesAsync();
    }
}