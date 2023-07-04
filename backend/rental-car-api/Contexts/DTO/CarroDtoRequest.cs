namespace rental_car_api.Contexts.DTO
{
    public class CarroDtoRequest
    {
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int Potencia { get; set; }
        public int Torque { get; set; }
        public string Combustivel { get; set; } = string.Empty;
        public string PrecoDiaria { get; set; } = string.Empty;
        public int Ano { get; set; }
    }
}
