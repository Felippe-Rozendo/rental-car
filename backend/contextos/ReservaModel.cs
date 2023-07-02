using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contextos
{
    public class ReservaModel
    {
        public int Id { get; set; }
        public int IdCarro { get; set; }
        public CarroModel Carro { get; set; }
        public int IdUsuario { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}
