using portalinvestimento.virtualtilab.com.Entity;

namespace portalinvestimento.virtualtilab.com.DTO
{
    public class CadastrarUsuarioDTO
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public EnTipoAcesso TipoAcesso { get; set; }
    }

    public class AlterarUsuarioDTO
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public EnTipoAcesso TipoAcesso { get; set; }

    }
}
