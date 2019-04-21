using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace inHealthTechnicalExam.DataAccessLayer.Models
{
    [Table("User", Schema = "dbo")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "User Id")]
        public int ID { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(max)")]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(max)")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(max)")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
