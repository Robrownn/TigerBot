using System.ComponentModel.DataAnnotations;

namespace TigerBot.Models
{
    public class UserGame
    {
        public int Id { get; set; }

        [Required]
        public int GameID { get; set; }

        [Required]
        public int UserID { get; set; }
    }
}
