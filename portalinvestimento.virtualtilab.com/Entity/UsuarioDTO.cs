namespace portalinvestimento.virtualtilab.com.Entity
{
    public class CadastrarUsuarioDTO
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public EnTipoAcesso TipoAcesso { get; set; }
        public string CPF { get; set; }
        public int Codigo_Conta { get; set; }
        public int Digito_Conta { get; set; }

        public decimal Saldo_Carteira { get; set; }

        public string ContaCompleta
        {
            get
            {
                return String.Format("{0}-{1}", this.Codigo_Conta, this.Digito_Conta);
            }
        }
    }

    public class AlterarUsuarioDTO
    {
        public decimal Saldo_Carteira { get; set; }
        public int Id { get; set; }

    }
}
