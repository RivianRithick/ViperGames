using System.ComponentModel.DataAnnotations;

namespace EGames.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Gmail { get; set; }

        public string Password { get; set; }
    }
}
