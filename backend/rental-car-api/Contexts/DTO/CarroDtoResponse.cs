namespace rental_car_api.Contexts.DTO
{
    public class CarroDtoResponse
    {
        public int Id { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        //public int Fotos { get; set; }
        public string Potencia { get; set; } = string.Empty;
        public string Torque { get; set; } = string.Empty;
        public string Combustivel { get; set; } = string.Empty;
        public string PrecoDiaria { get; set; } = string.Empty;
        public int Ano { get; set; }
    }
}
