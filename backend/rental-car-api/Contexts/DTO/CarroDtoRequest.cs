namespace rental_car_api.Contexts.DTO
{
    public class CarroDtoRequest
    {
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public float Potencia { get; set; }
        public float Torque { get; set; }
        public string Combustivel { get; set; } = string.Empty;
        public float PrecoDiaria { get; set; }
        public int Ano { get; set; }
    }
}
