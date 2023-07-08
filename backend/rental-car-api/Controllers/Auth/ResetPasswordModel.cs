namespace rental_car_api.Controllers.Auth
{
    public class ResetPasswordModel
    {
        public string Email { get; set; } = String.Empty;
        public string NewPassword { get; set; } = String.Empty;
    }
}
