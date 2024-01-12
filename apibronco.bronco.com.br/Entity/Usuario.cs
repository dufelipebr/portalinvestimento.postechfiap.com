namespace apibronco.bronco.com.br.Entity
{
    public class Usuario : Entidade
    {
        #region construtores
        public Usuario(CadastrarUsuarioDTO cad)
        {
            this.Nome = cad.Nome;
            this.Senha = cad.Senha;
            this.TipoPermissao = cad.TipoAcesso;
            this.Email = cad.Email;
        }
        public Usuario(AlterarUsuarioDTO cad)
        {
            this.Nome = cad.Nome;
            this.Senha = cad.Senha;
            this.TipoPermissao = cad.TipoAcesso;
            this.Email = cad.Email;
        }

        public Usuario()
        {
        }

        public Usuario(int _id, String _nome) {
            this.Id = _id;
            this.Nome = _nome;
        }
        #endregion

        public string Email { get; set; }
        public string Nome { get; set; }

        public string Senha { get; set; }

        public EnumTipoAcesso TipoPermissao { get; set; }



        public bool IsValid()
        {
            return base.IsValid();
        }

    }
}
