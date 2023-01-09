using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodosMVC.Models
{
    [Table("User")]
    public class User : IdentityUser
    {
    }
}
