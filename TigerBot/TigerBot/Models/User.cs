using System.ComponentModel.DataAnnotations;

namespace TigerBot.Models
{
    public class User
    {

        public int Id { get; set; }

        [Required, MaxLength(80)]
        public string UserName { get; set; }
    }
}
