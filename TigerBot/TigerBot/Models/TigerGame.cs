using Discord;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TigerBot.Models
{
    public partial class TigerGame
    {
        public int Id { get; set; }

        [Required,MaxLength(80)]
        public string GameName { get; set; }
    }
}
