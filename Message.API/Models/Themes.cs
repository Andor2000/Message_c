using System.ComponentModel.DataAnnotations;

namespace Message.API.Models
{
    public class Themes
    {
        [Key]
        public int Id { get; set; }
        public string Theme { get; set; }
    }
}
