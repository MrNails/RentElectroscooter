using RentElectroScooter.CoreModels.DTO;
using RentElectroScooter.UI.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentElectroScooter.UI.Services.Converters
{
    public class RegistrationDataConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 4)
                return null;

            return new RegData { Login = values[0]?.ToString(), Password = values[1]?.ToString(), PasswordRepeat = values[2]?.ToString(), Name = values[3]?.ToString() };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
