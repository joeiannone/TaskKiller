using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TaskKiller.Views.Converters
{
    public class ByteStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal bytes = (long)value;
            decimal result;

            switch (parameter.ToString())
            {
                case "MB":
                    result = bytes / 1000000;
                    break;
                case "KB":
                    result = bytes / 100;
                    break;
                default:
                    result = bytes;
                    break;

            }

            return result.ToString("F");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            long converted = (long)value;

            switch (parameter.ToString())
            {
                case "MB":
                    return converted / 0.000001;
                case "KB":
                    return converted / 0.001;
                default:
                    return converted;

            }
        }
    }
}
