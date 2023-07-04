using System.ComponentModel;

namespace rental_car_api.Enum
{
    public enum CombustivelEnum
    {
        [Description("Gasolina")]
        Gasolina = 0,
        [Description("Etanol")]
        Etanol = 1,
        [Description("Diesel")]
        Diesel = 2,
        [Description("Elétrico")]
        Elétrico = 3
    }
}
