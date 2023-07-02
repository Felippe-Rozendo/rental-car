namespace rental_car_api.Contexts
{
    public class ReservaModel
    {
        public int Id { get; set; }
        public int IdCarro { get; set; }
        public CarroModel Carro { get; set; }
        public string IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}
