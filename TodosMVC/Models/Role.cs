using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodosMVC.Models
{
    [Table("Role")]
    public class Role : IdentityRole
    {
    }
}
