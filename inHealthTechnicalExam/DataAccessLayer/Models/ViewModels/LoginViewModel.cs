using System.ComponentModel.DataAnnotations;

namespace inHealthTechnicalExam.DataAccessLayer.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required, MaxLength(20)]
        public string Username { get; set; }
        [Required, DataType(DataType.Password), MaxLength(10)]
        public string Password { get; set; }
    }
}
