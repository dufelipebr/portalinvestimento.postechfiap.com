namespace portalinvestimento.virtualtilab.com.Entity
{
    public enum EntityStatus { Deactivated, Active}

    public abstract class Entidade
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
        public string Slug { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime LastChanged { get; set; }
        public EntityStatus Status { get; set; }

        public abstract void ValidateEntity(); 
        //{
        //    if (Id == null || Id == 0)
        //        throw new DomainException("erro sem ID");
        //}
    }
}
