namespace rental_car_api.Service.Email
{
    public class EmailContent
    {
        public string Locador { get; set; } =  String.Empty;
        public string Marca { get; set; } = String.Empty;
        public string Modelo { get; set; } = String.Empty;
        public double? Dias { get; set; }
        public double? TotalReserva { get; set; }

        public EmailContent(string locador, string marca, string modelo, double? dias, double? totalReserva)
        {
            Locador = locador;
            Marca = marca;
            Modelo = modelo;
            Dias = dias;
            TotalReserva = totalReserva;
        }

    }
}
