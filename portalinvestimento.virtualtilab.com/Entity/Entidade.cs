namespace portalinvestimento.virtualtilab.com.Entity
{
    public enum Status { Active, Deactivated}

    public abstract class Entidade
    {
        public int Id { get; set; }
        public bool Delete { get; set; }
        public string Slug { get; set; }
        public DateTime PublishDate { get; set; }
        public Status Status { get; set; }

        public abstract void ValidateEntity(); 
        //{
        //    if (Id == null || Id == 0)
        //        throw new DomainException("erro sem ID");
        //}
    }
}
