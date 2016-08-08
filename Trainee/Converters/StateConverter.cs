using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Trainee.Converters
{
    /// <summary>
    ///     StateConverter
    /// </summary>
    [ValueConversion(typeof(System.Int32), typeof(Brush))]
    public class StateConverter : IValueConverter
    {
        /// <summary>
        ///     Convert
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int state = (int) value;
            switch (state)
            {
                case 0:
                    return Brushes.Green;
                case 1:
                    return Brushes.Red;
                default :
                    return Brushes.Gray;
            }
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
            throw new NotImplementedException();
        }
    }
}
