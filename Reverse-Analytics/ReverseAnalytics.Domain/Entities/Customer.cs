namespace ReverseAnalytics.Domain.Entities
{
    public class Customer : Person
    {
        public string ContactPerson { get; set; }
        public string ContactPersonPhone { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
