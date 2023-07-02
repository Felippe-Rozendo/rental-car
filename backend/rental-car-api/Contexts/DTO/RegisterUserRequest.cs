using System.ComponentModel.DataAnnotations;

namespace rental_car_api.Contexts.DTO
{
    public class RegisterUserRequest
    {
        [Required(ErrorMessage = "Tipo de usuário é obrigatório."), RegularExpression("^[SN]$", ErrorMessage = "O campo deve ser 'S' ou 'N'.")]
        public string IsAdmin { get; set; } = String.Empty;

        [Required(ErrorMessage = "Nome do usuário é obrigatório."), RegularExpression(@"^[^\s]+$", ErrorMessage = "O nome do usuário não pode conter espaços.")]
        public string UserName { get; set; } = String.Empty;

        [Required(ErrorMessage = "Email é obrigatório."), EmailAddress(ErrorMessage = "Informe um endereço de e-mail válido.")]
        public string Email { get; set; } = String.Empty;

        [Required(ErrorMessage = "Senha é obrigatória.")]
        public string Password { get; set; } = String.Empty;

        [Required(ErrorMessage = "Número de telefone é obrigatório.")]
        public string PhoneNumber { get; set; } = String.Empty;
    }
}
