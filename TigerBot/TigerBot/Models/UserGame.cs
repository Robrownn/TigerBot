using System.ComponentModel.DataAnnotations;

namespace TigerBot.Models
{
    public class UserGame
    {
        public int Id { get; set; }

        [Required]
        public int gameID { get; set; }

        [Required]
        public int userID { get; set; }
    }
}
