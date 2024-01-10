namespace portalinvestimento.virtualtilab.com.Entity
{
    public class CadastrarUsuarioDTO
    {
        public string Nome;
        public string Senha;
        public string Email;
        public EnTipoAcesso TipoAcesso;
    }

    public class AlterarUsuarioDTO
    {
        public string Nome;
        public string Senha;
        public string Email;
        public EnTipoAcesso TipoAcesso;
    }
}
