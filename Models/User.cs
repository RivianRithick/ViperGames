using System.ComponentModel.DataAnnotations;

namespace EGames.Models
{
    public class User
    {
        public User()
        {
            carts = new HashSet<Cart>();
        }
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Gmail { get; set; }

        public string Password { get; set; }
        public virtual ICollection<Cart> carts { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails
        { get; set; }
    }
}
