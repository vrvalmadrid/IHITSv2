using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace inHealthTechnicalExam.DataAccessLayer.Models
{
    [Table("Comment", Schema = "dbo")]
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Comment Id")]
        public int ID { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(max)")]
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(max)")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }
        [Required]
        [Column(TypeName = "int")]
        [Display(Name = "Blog Id")]
        [ForeignKey("Blog")]
        public int BlogID { get; set; }
        public Blog blog { get; set; }
    }
}
