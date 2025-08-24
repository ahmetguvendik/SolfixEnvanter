using MediatR;

namespace Application.Features.Commands;

public class UpdateUserCommand : IRequest
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public string? DepartmentId { get; set; }
}
