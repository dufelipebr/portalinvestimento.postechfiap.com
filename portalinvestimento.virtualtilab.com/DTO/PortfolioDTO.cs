namespace portalinvestimento.virtualtilab.com.DTO
{
    public class CadastrarPortfolioDTO
    {
        public CadastrarPortfolioDTO() { }

        #region properties
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Codigo { get; set; }

        public int Id_Usuario { get; set; }

        #endregion
    }

    public class ModificarPortfolioDTO
    {
        public ModificarPortfolioDTO() { }

        #region properties
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Codigo { get; set; }

        public int Id_Usuario { get; set; }

        public int Id { get; set; }


        #endregion
    }
}
