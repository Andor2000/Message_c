using System.ComponentModel.DataAnnotations;

namespace Message.API.Models
{
    public class Messages
    {
        [Key]
        public int Id { get; set; }

        public string IdUser { get; set; }
        public string Theme { get; set; }
        public string Message { get; set; }


    }
}
