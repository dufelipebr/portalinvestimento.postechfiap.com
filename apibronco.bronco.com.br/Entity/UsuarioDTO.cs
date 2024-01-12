namespace apibronco.bronco.com.br.Entity
{
    public class CadastrarUsuarioDTO
    {
        public string Nome;
        public string Senha;
        public string Email;
        public EnumTipoAcesso TipoAcesso;
    }

    public class AlterarUsuarioDTO
    {
        public string Nome;
        public string Senha;
        public string Email;
        public EnumTipoAcesso TipoAcesso;
    }
}
