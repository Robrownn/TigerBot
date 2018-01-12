using System.ComponentModel.DataAnnotations;

namespace TigerBot.Models
{
    public class TigerGame
    {
        public int Id { get; set; }

        [Required, MaxLength(80)]
        public string gameName { get; set; }
    }
}
