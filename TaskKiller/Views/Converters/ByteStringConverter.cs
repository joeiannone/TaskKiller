using System;
using System.Globalization;
using System.Windows.Data;

namespace TaskKiller.Views.Converters
{
    /// <summary>
    /// 
    /// </summary>
    public class ByteStringConverter : IValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
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
