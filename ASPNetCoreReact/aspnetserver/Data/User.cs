using System.ComponentModel.DataAnnotations;

namespace aspnetserver.Data
{
    public class User
    {
        [Key]
        public int userID { get; set; }

        [Required]
        [MaxLength(30)]
        public string username { get; set; }

        [Required]
        [MaxLength(30)]
        public string password { get; set; }
    }
}
