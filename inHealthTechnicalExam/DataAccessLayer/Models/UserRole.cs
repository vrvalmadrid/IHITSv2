using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace inHealthTechnicalExam.DataAccessLayer.Models
{
    [Table("UserRole", Schema = "dbo")]
    public class UserRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "UserRole Id")]
        public int ID { get; set; }

        [Required]
        [Column(TypeName = "int")]
        [Display(Name = "User Id")]
        [ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; }

        [Required]
        [Column(TypeName = "int")]
        [Display(Name = "Role Id")]
        [ForeignKey("Role")]
        public int RoleID { get; set; }
        public Role Role { get; set; }
    }
}
