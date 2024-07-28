using portalinvestimento.virtualtilab.com.DTO;
using portalinvestimento.virtualtilab.com.Repository;
using static portalinvestimento.virtualtilab.com.Entity.Ativo;

namespace portalinvestimento.virtualtilab.com.Entity
{
    public class Portfolio : Entidade 
    {
        public Portfolio() { }
        public Portfolio(CadastrarPortfolioDTO dto) {
            this.Id_Usuario = dto.Id_Usuario;
            this.Nome = dto.Nome;
            this.Descricao = dto.Descricao;
            this.Codigo = dto.Codigo;

            ValidateEntity();
        }

        public Portfolio(ModificarPortfolioDTO dto)
        {
            this.Id_Usuario = dto.Id_Usuario;
            this.Nome = dto.Nome;
            this.Descricao = dto.Descricao;
            this.Codigo = dto.Codigo;
            this.Id = dto.Id;
            ValidateEntity();
        }


        public int Id_Usuario { get; set; }
        //public decimal Saldo_Carteira { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public string Codigo { get; set; }

        public override void ValidateEntity()
        {
            AssertionConcern.AssertArgumentNotEmpty(Codigo, "Codigo precisa ser preenchido.");
            AssertionConcern.AssertArgumentNotEmpty(Nome, "Nome precisa ser preenchido.");
            AssertionConcern.AssertArgumentNotEmpty(Descricao, "Descricao precisa ser preenchido.");
            //AssertionConcern.AssertArgumentTrue(Id_Usuario==0, "Id_Usuario precisa ser preenchido.");
            //throw new NotImplementedException();
        }
    }
   
}
