using System.ComponentModel.DataAnnotations;

namespace NetApplication.Models
{
    public class Profiles
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

        public string? UserId { get; set; }
        public Users? User { get; set; }
    }
}
