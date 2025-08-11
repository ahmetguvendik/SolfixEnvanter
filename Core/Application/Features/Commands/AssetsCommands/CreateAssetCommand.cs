using Application.Enums;
using MediatR;

namespace Application.Features.Commands;

public class CreateAssetCommand : IRequest
{
    public string Name { get; set; }
    public string SerialNumber { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string AssetTag { get; set; } 
    public string AssetTypeId { get; set; }
    public string LocationId { get; set; }
    public string? AssignedToUserId { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime? WarrantyExpiryDate { get; set; }
    public AssetStatus Status { get; set; } 
    public string? Description { get; set; }
    public string DepartmentId { get; set; }
}