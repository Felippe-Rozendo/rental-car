namespace rental_car_api.Contexts.DTO
{
    public class ReservaUsuarioResponse
    {
        public int IdReserva { get; set; }
        public int IdCarro { get; set; }
        public string MarcaCarro { get; set; } = string.Empty;
        public string ModeloCarro { get; set; } = string.Empty;
        public string ValorTotal { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}
