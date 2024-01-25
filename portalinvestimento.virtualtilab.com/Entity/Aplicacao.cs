using static portalinvestimento.virtualtilab.com.Entity.Investimento;

namespace portalinvestimento.virtualtilab.com.Entity
{
    public class Aplicacao : Entidade
    {
        public  Usuario Usuario { get; set; }
        public Investimento Investimento { get; set; }
        public decimal Valor_Aplicacao { get; set; }
        public DateTime Data_Aplicacao { get; set; }
        public Decimal Rentabilidade { get; set; }
        public DateTime Ultima_Rentabilidade_Calculada { get; set; }

        public Decimal Valor_Atualizado { get { return Valor_Aplicacao + Rentabilidade; } }

        public Aplicacao()
        { 
        
        }

        public Aplicacao(Usuario usuario, Investimento investimento, decimal valorAplicacao)
        {
           
            Investimento = investimento;
            Valor_Aplicacao = valorAplicacao;
            Data_Aplicacao = DateTime.Now;
            Rentabilidade = 0;
            Usuario = usuario;
            
            ValidateEntity();
        }
        public Aplicacao(CadastrarAplicacaoDTO cad)
        {
            this.Data_Aplicacao = DateTime.Now;
            this.Valor_Aplicacao = cad.Valor_Aplicacao;
            this.Usuario = new Usuario() { Id = cad.Id_Usuario };
            this.Investimento = new Investimento() { Id = cad.Id_Investimento };
        }

        //public Aplicacao (Usuario usuario, Investimento investimento, decimal valorAplicacao, DateTime dataAplicacao, Decimal rentabilidade)
        //{
        //    this.Usuario = usuario;
        //    Investimento = investimento;
        //    Valor_Aplicacao = valorAplicacao;
        //    Data_Aplicacao = dataAplicacao;
        //    Rentabilidade = rentabilidade;
        //}

        public override void ValidateEntity()
        {
            AssertionConcern.AssertArgumentRange((double)Valor_Aplicacao, 0.1, 1000000, "Deve ser entre 0.1 e 1000000");
        }
    }
}
