using System.ComponentModel.DataAnnotations;

namespace rental_car_api.Contexts.DTO
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email é obrigatório")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Senha é obrigatório")]
        public string Senha { get; set; } = string.Empty;
    }
}
