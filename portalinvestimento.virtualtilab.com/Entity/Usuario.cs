namespace portalinvestimento.virtualtilab.com.Entity
{
    public class Usuario : Entidade
    {
        public Usuario(int _id, String _nome) {
            this.Id = _id;
            this.Nome = _nome;
        }
        public string Email { get; set; }
        public string Nome { get; set; }

        public string Senha { get; set; }

    }
}
