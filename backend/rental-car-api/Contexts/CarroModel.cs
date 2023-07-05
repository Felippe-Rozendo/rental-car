namespace rental_car_api.Contexts
{
    public class CarroModel
    {
        public int Id { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int Fotos { get; set; }
        public float Potencia { get; set; }
        public float Torque { get; set; }
        public string Combustivel { get; set; } = string.Empty;
        public float PrecoDiaria { get; set; }
        public int Ano { get; set; }
        public ICollection<ReservaModel> Reservas { get; set; }
    }
}
