namespace apibronco.bronco.com.br.Entity
{
    public enum EnStatusProposta
    {
        Aberto = 1, 
        EmAnaliseUnderwriter = 2,
        Cancelado = 3,
        Fechado = 4,
    }

    public class StatusProposta
    {
        public int Status { get; set; }
    }
}
