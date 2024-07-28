using portalinvestimento.virtualtilab.com.DTO;
using portalinvestimento.virtualtilab.com.Repository;
using static portalinvestimento.virtualtilab.com.Entity.Ativo;

namespace portalinvestimento.virtualtilab.com.Entity
{

    public class Usuario : Entidade
    {
        #region Construtor
        public Usuario()
        {
        }
        public Usuario(int _id, String _nome) {
            this.Id = _id;
            this.Nome = _nome;
        }
        public Usuario(CadastrarUsuarioDTO cad)
        {
            this.Nome = cad.Nome;
            this.Senha = cad.Senha;
            this.TipoPermissao = cad.TipoAcesso;
            this.Codigo_Usuario = cad.Email;
            //this.Digito_Conta = cad.Digito_Conta;
            //this.Codigo_Conta = cad.Codigo_Conta;
            //this.CPF = cad.CPF;
            //this.Saldo_Carteira = cad.Saldo_Carteira;

            ValidateEntity();
        }
        public Usuario(AlterarUsuarioDTO cad)
        {
            this.Nome = cad.Nome;
            //this.Senha = cad.Senha;
            this.TipoPermissao = cad.TipoAcesso;
            this.Email = cad.Email;
            //this.Digito_Conta = cad.Digito_Conta;
            //this.Codigo_Conta = cad.Codigo_Conta;
            //this.CPF = cad.CPF;
            //this.Saldo_Carteira = cad.Saldo_Carteira;
            //this.Id = cad.Id;

            if (this.Id == 0)
                throw new ArgumentException("ID não poder ser nulo ou 0.");

//            if (cad.Saldo_Carteira <= 0)
  //              throw new ArgumentException("Saldo Carteira não poder ser 0.");

            //ValidateEntity();
        }

        #endregion
        public EnTipoAcesso TipoPermissao { get; set; }
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public string Codigo_Usuario { get; set; }
        public EnTipoAcesso TipoAcesso { get; set; }
        //public string CPF { get; set; }
        //public int Codigo_Conta { get; set; }
        //public int Digito_Conta { get; set; }
       

        public Portfolio Portfolio { get; set; }

        //public List<Aplicacao> Aplicacoes // Não tem config
        //{ 
        //    get {
        //        AplicacaoRepository r = new AplicacaoRepository(config);
        //        return r.ObterAplicacaoPorUserId(this.Id);
        //    } 
        //}


        //public string ContaCompleta { 
        //    get { 
        //        return String.Format("{0}-{1}", this.Codigo_Conta, this.Digito_Conta); 
        //    } 
        //}


        public override void ValidateEntity()
        {
            //AssertionConcern.AssertArgumentRange((double)Codigo_Conta, 1, 1000, "Codigo Conta precisa ser preenchido de 0-1000.");
            //AssertionConcern.AssertArgumentRange((double)Digito_Conta, 0, 99, "Digito Conta precisa ser preenchido de 0-99.");
            //AssertionConcern.AssertArgumentLength(Codigo_Conta, 50, "Codigo do Investimento precisa ter no maximo 10 caracteres.");

            AssertionConcern.AssertArgumentNotEmpty(Nome, "Nome precisa ser preenchido.");
            AssertionConcern.AssertArgumentNotEmpty(Codigo_Usuario, "E-mail precisa ser preenchido.");
            AssertionConcern.AssertArgumentMatches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", Codigo_Usuario, "E-mail invalido!");
            //AssertionConcern.AssertArgumentNotEmpty(CPF, "CPF precisa ser preenchido.");
            //AssertionConcern.AssertArgumentMatches(@"^\d{3}.?\d{3}.?\d{3}-?\d{2}$", CPF, "CPF invalido!");

            AssertionConcern.AssertArgumentNotEquals(TipoAcesso, 0, "Tipo Acesso precisa ser preenchido");

            //AssertionConcern.AssertArgumentRange((double)Saldo_Carteira, 0.1, 10, "Taxa ADM precisa estar entre 0.1 e 10.");
            //AssertionConcern.AssertArgumentRange((double)Saldo_Carteira, 0, 1000000, "Saldo Carteira precisa ser maior que 0 e menor que 1.000.00,00");

            //throw new NotImplementedException();
        }

    }
}
