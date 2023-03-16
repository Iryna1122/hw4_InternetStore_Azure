namespace hw4_InternetStore_Azure.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
       
        public ICollection<Product> Products { get; set; }
        public Category()
        {
            Products = new List<Product>();
        }
    }
}
