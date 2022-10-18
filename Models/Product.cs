using System.ComponentModel.DataAnnotations;

namespace EGames.Models
{
    public class Product
    {
        public Product()
        {
           carts = new HashSet<Cart>();
        }
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public string? Image { get; set; }

        public string Publisher { get; set; }

        public virtual ICollection<Cart> carts
        { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails
        { get; set; }
    }
}
