using System.ComponentModel.DataAnnotations;

namespace EGames.Models
{
    public class OrderDetails
    {
        [Key]
        public int? OrderDetailsId { get; set; }
        public int? TotalAmount { get; set; }
        public int? OrderMasterId { get; set; }
        public int? ProductId { get; set; }
        public int? UserId { get; set; }
        public virtual OrderMaster? OrderMaster { get; set; }
        public virtual Product Product { get; set; }    
        public virtual User User { get; set; }

    }
}
