using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Trainee.Models;

namespace Trainee.Converters
{
    /// <summary>
    ///     PositionConverter
    /// </summary>
    [ValueConversion(typeof(Position), typeof(String))]
    public class PositionConverter : IValueConverter
    {
        /// <summary>
        ///     
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(
            object value, 
            Type targetType, 
            object parameter, 
            CultureInfo culture)
        {
            var position = value as Position;
            if (position != null)
            {
                if (position.Grade.Id == (int)PositionGradeId.Normal)
                    return position.Description;
                else
                    return string.Format("{0} {1}", position.Grade.Description, position.Description);
            }
            else
                return string.Empty;
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
