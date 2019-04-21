using System.ComponentModel.DataAnnotations;

namespace inHealthTechnicalExam.DataAccessLayer.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required, MaxLength(20)]
        public string Username { get; set; }
        [Required, DataType(DataType.Password), MaxLength(10)]
        public string Password { get; set; }
        [DataType(DataType.Password), Compare(nameof(Password)), MaxLength(10)]
        public string ConfirmPassword { get; set; }
        [Required, MaxLength(20)]
        public string Name { get; set; }
    }
}
