namespace portalinvestimento.virtualtilab.com.Entity
{
    public class Carteira : Entidade
    {
        public Carteira(int codigoConta, int digito, string nome, string cpf, string email, string endereco) 
        {
            this.CodigoConta = codigoConta;
            this.DigitoConta = digito;
            this.NomeBeneficiario = nome;
            this.CPF = cpf;
            this.Email = email;
            this.Endereco = endereco;
        }

        public Carteira(int codigoConta, int digito)
        {
            this.CodigoConta = codigoConta;
            this.DigitoConta = digito;
        }

        public int CodigoConta { get; set; }
        public int DigitoConta { get; set; }
        public string ContaCompleta { get { return String.Format("{0}-{1}", this.CodigoConta, this.DigitoConta); } }
        public string NomeBeneficiario { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
        public IList<Investimento> Investimentos { get; set; }

        public override void ValidateEntity()
        {
            return; 
        }
    }
}
