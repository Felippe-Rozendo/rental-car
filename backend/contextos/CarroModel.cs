using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contextos
{
    public class CarroModel
    {
        public int Id { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int Fotos { get; set; }
        public int Potencia { get; set; }
        public int Torque { get; set; }
        public int Combustivel { get; set; }
        public string PrecoDiaria { get; set; } = string.Empty;
        public int Ano { get; set; }
        public ICollection<ReservaModel> Reservas { get; set; }
    }
}
