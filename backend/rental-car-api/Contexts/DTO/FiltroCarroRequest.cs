namespace rental_car_api.Contexts.DTO
{
    public class FiltroCarroRequest
    {
        public string? Marca { get; set; } = string.Empty;
        public string? Modelo { get; set; } = string.Empty;
        public int? PotenciaMin { get; set; }
        public int? PotenciaMax { get; set; }
        public int? TorqueMin { get; set; }
        public int? TorqueMax { get; set; }
        public string? Combustivel { get; set; } = string.Empty;
        public string? PrecoDiariaMin { get; set; } = string.Empty;
        public string? PrecoDiariaMax { get; set; } = string.Empty;
        public int? AnoMin { get; set; }
        public int? AnoMax { get; set; }
    }
}
