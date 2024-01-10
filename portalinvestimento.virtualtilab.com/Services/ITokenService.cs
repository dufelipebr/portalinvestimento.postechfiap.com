using portalinvestimento.virtualtilab.com.Entity;

namespace portalinvestimento.virtualtilab.com.Services
{
    public interface ITokenService
    {
        string GerarToken(Usuario usuario);
    }
}
