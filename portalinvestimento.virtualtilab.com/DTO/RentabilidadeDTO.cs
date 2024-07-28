namespace portalinvestimento.virtualtilab.com.DTO
{
    public class CadastrarRentabilidadeDTO
    {
        public int Id_Investimento { get; set; }
        public decimal Valor_Rentabilidade { get; set; }

        //public int Mes_Operacao { get; set; }

        //public int Ano_Operacao { get; set; }

        // public TipoOperacao_Aplicacao Tipo { get; set; }

        public DateTime Data_Operacao { get; set; }
        //get 
        //{
        //    return new DateTime(Ano_Operacao, Mes_Operacao, 1);
        //} 
        //}
    }

}
