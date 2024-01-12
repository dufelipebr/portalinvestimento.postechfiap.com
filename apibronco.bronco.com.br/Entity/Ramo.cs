namespace apibronco.bronco.com.br.Entity
{
    public class Ramo : Entidade
    {
        public string Codigo { get; set; }

        public string Codigo_SUSEP { get; set; }
        public string Codigo_Empresa { get; set; } // identificador da empresa geralmente 1
        
        public string Descricao { get; set; } // 

        public string Codigo_Grupo { get; set; }
    }
}
