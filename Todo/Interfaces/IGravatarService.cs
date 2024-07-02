using System.Threading.Tasks;

public interface IGravatarService
{
    Task<GravatarProfile> GetProfileDetailsAsync(string email);
}