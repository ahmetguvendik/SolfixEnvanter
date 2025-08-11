using Application.Features.Queries.DepartmentQueries;
using Application.Features.Results.DepartmentResults;
using Application.Interfaces;
using Domain.Entites;
using MediatR;

namespace Application.Features.Handlers.Departments.Read;

public class GetDepartmentQueryHandler : IRequestHandler<GetDepartmentQuery, List<GetDepartmentQueryResult>>
{
    private readonly IGenericRepository<Department> _departmentRepository;

    public GetDepartmentQueryHandler(IGenericRepository<Department> departmentRepository)
    {
         _departmentRepository = departmentRepository;
    }
    
    public async Task<List<GetDepartmentQueryResult>> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
    {
        var result = await _departmentRepository.GetAllAsync();
        return result.Select(x => new GetDepartmentQueryResult()
        {
            Id = x.Id,
            Name = x.Name,
        }).ToList();
    }
}