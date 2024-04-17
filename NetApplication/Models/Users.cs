using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace NetApplication.Models
{
    public class Users : IdentityUser
{
    [Required]
    public string Name { get; set; }

    public List<Profiles>? Profiles { get; set; }
    }
}
