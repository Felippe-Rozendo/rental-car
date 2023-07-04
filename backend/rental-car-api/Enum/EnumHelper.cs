using System.ComponentModel;

namespace rental_car_api.Enum
{
    public class EnumHelper
    {
        public static string GetEnumDescription<T>(string value)
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("O tipo fornecido não é um enum.");
            }

            var enumValues = (T[])System.Enum.GetValues(typeof(T));

            foreach (var enumValue in enumValues)
            {
                if (Convert.ToInt32(enumValue) == Convert.ToInt32(value))
                {
                    var enumMember = typeof(T).GetMember(enumValue.ToString());
                    var descriptionAttribute = enumMember[0].GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;

                    return descriptionAttribute?.Description ?? enumValue.ToString();
                }
            }

            throw new ArgumentOutOfRangeException("O valor fornecido não corresponde a nenhum membro do enum.");
        }


    }
}
