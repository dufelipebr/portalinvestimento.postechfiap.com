using portalinvestimento.virtualtilab.com.DTO;
using static portalinvestimento.virtualtilab.com.Entity.Ativo;

namespace portalinvestimento.virtualtilab.com.Entity
{
    public class Transacao : Entidade
    {
        public int Id_Portfolio { get; set; }
        public Ativo Investimento { get; set; }

        public decimal Quantidade { get; set; }
        public decimal Preco { get; set; }
        public DateTime Data_Transacao { get; set; }
        public Decimal Rentabilidade { get; set; }
        public DateTime Ultima_Rentabilidade_Calculada { get; set; }

        public Decimal Valor_Atualizado { get { return Preco + Rentabilidade; } }

        public Transacao()
        { 
        
        }

        public Transacao(Ativo investimento, decimal valorAplicacao)
        {
           
            Investimento = investimento;
            Preco = valorAplicacao;
            Data_Transacao = DateTime.Now;
            Rentabilidade = 0;
            Id_Portfolio = 0;
            
            ValidateEntity();
        }
        public Transacao(CadastrarAplicacaoDTO cad)
        {
            this.Data_Transacao = DateTime.Now;
            this.Preco = cad.Valor_Aplicacao;
            //this.Usuario = new Portfolio() { Id = cad.Id_Usuario };
            this.Investimento = new Ativo() { Id = cad.Id_Investimento };
        }

        public Transacao(ResgatarAplicacaoDTO cad)
        {
            this.Data_Transacao = DateTime.Now;
            this.Preco = cad.Valor_Resgate;
            //this.Usuario = new Portfolio() { Id = cad.Id_Usuario };
            this.Investimento = new Ativo() { Id = cad.Id_Investimento };
            this.Id_Portfolio = cad.Id_Portfolio;
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
            AssertionConcern.AssertArgumentRange((double)Preco, 0.1, 1000000, "Deve ser entre 0.1 e 1000000");
        }
    }
}
