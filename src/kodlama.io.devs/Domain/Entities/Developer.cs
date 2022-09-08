using Core.Security.Entities;

namespace Domain.Entities;

public class AppUser : User
{
    public ICollection<UserGitHub> UserGitHubs { get; set; }

    public AppUser()
    {
        
    }
}