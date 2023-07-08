using Microsoft.AspNetCore.Identity;

namespace rental_car_api.Contexts
{
    public class Usuario : IdentityUser
    {
        public string IsAdmin { get; set; } = String.Empty;
        public int Idade { get; set; }
        public string Cidade { get; set; } = String.Empty;
        public ICollection<ReservaModel> Reservas { get; set; }
    }
}
