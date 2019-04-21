using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace inHealthTechnicalExam.DataAccessLayer.Models
{
    [Table("Role", Schema = "dbo")]
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Role Id")]
        public int ID { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(max)")]
        [Display(Name = "Role Code")]
        public string RoleCode { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(max)")]
        [Display(Name = "Role Type")]
        public string RoleType { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
