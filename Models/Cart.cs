using System.ComponentModel.DataAnnotations;

namespace EGames.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public int Quantity { get; set; }
        public int TotalAmt { get; set; }
        public int? Productid { get; set; }
        public int? Userid { get; set; }
        public virtual Product? product { get; set; }
        public virtual User? user { get; set; }

    }
}
