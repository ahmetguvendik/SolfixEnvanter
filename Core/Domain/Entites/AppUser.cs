using Microsoft.AspNetCore.Identity;

namespace Domain.Entites;

public class AppUser: IdentityUser<string> 
{
  
    public string FullName { get; set; }
    public string? DepartmentId { get; set; }
    public Department? Department { get; set; }

    public ICollection<Asset> AssignedAssets { get; set; }  
}