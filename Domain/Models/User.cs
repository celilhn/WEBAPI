using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("Users")]
    public class User : ExtendedBaseModel
    {
        [Column(TypeName = "varchar(200)")]
        public string Email { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Password { get; set; }
    }
}
