using MediatR;

namespace Application.Features.Commands;

public class AssignAssetToUserCommand : IRequest
{
    public string AssetId { get; set; }
    public string? UserId { get; set; } // null ise zimmet kaldırılır
}
