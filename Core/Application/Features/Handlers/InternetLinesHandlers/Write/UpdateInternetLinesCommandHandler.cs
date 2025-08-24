using Application.Features.Commands;
using Application.Interfaces;
using Domain.Entites;
using MediatR;

namespace Application.Features.Handlers.InternetLinesHandlers.Write;

public class UpdateInternetLinesCommandHandler : IRequestHandler<UpdateInternetLinesCommand>
{
    private readonly IGenericRepository<InternetLine> _repository;

    public UpdateInternetLinesCommandHandler(IGenericRepository<InternetLine> repository)
    {
         _repository = repository;
    }
    
    public async Task Handle(UpdateInternetLinesCommand request, CancellationToken cancellationToken)
    {
        var internetLine = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (internetLine == null)
            throw new Exception("Internet Line not found");
            
        internetLine.LineNumber = request.LineNumber;
        internetLine.Provider = request.Provider;
        internetLine.Speed = request.Speed;
        internetLine.ContractEndDate = request.ContractEndDate;
        internetLine.LocationId = request.LocationId;
        
        await _repository.UpdateAsync(internetLine, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}
