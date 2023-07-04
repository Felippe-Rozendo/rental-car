namespace rental_car_api.Contexts.DTO
{
    public class ReservaRequest
    {
        public int IdReserva { get; set; }
        public int IdCarro { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}
