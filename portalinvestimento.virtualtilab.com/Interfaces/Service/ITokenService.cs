using portalinvestimento.virtualtilab.com.Entity;

namespace portalinvestimento.virtualtilab.com.Interfaces.Service
{
    public interface ITokenService
    {
        string GerarToken(Usuario usuario);
    }
}
