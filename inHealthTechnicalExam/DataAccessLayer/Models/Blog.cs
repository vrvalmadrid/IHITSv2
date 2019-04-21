using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace inHealthTechnicalExam.DataAccessLayer.Models
{
    [Table("Blog", Schema = "dbo")]
    public class Blog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Blog Id")]
        public int ID { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(max)")]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(max)")]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }
        [Required]
        [Column(TypeName = "int")]
        [Display(Name = "Created By")]
        public int CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
