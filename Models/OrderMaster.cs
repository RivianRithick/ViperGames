using MessagePack;
using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace EGames.Models
{
    public class OrderMaster
    {
        [Key]
        public int? OrderMasterId { get; set; }
        public DateTime Date { get; set; }
        public int? TotalAmount { get; set; }
        public int? CaardNumber { get; set; }
        public int? AmountPaid { get; set; }
        public int? UserId  { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
