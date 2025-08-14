using Application.Features.Queries.AppUserQueries;
using Application.Features.Results;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Handlers.AppUsers.Read;

public class GetUserByIdQueryHandler  : IRequestHandler<GetUserByIdQuery, GetUserByIdQueryResult>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
         _userRepository = userRepository;
    }
    
    public async Task<GetUserByIdQueryResult> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user =  await _userRepository.GetUserById(request.Id);
        return new GetUserByIdQueryResult()
        {
            Id = user.Id,
            FullName = user.FullName,
            DepartmentName = user.Department.Name,
            Email = user.Email
        };
    }
}