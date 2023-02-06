namespace ReverseAnalytics.Domain.Entities
{
    public class Customer : Person
    {
        public double Discount { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
