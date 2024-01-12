namespace apibronco.bronco.com.br.Entity
{
    public class Entidade
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdateOn { get; set; }

        public bool IsValid() 
        {
            if (Id == null)
                return false;
            if (CreatedOn < new DateTime(2000, 1, 1))
                return false;
            if (LastUpdateOn < new DateTime(2000, 1, 1))
                return false;

            return true;
        }
    }
}
