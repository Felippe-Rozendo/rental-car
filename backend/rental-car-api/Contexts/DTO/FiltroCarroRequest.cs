namespace rental_car_api.Contexts.DTO
{
    public class FiltroCarroRequest
    {
        public string? Marca { get; set; } = string.Empty;
        public string? Modelo { get; set; } = string.Empty;
        public float? PotenciaMin { get; set; }
        public float? PotenciaMax { get; set; }
        public float? TorqueMin { get; set; }
        public float? TorqueMax { get; set; }
        public string? Combustivel { get; set; } = string.Empty;
        public float? PrecoDiariaMin { get; set; }
        public float? PrecoDiariaMax { get; set; }
        public int? AnoMin { get; set; }
        public int? AnoMax { get; set; }
    }
}
