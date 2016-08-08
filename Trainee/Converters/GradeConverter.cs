using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using Trainee.Models;

namespace Trainee.Converters
{
    /// <summary>
    ///     GradeConverter
    /// </summary>
    [ValueConversion(typeof(Grade), typeof(Brush))]
    public class GradeConverter : IValueConverter
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
            var grade = value as Grade;
            if (grade != null)
            {
                switch (grade.Id)
                {
                    case (int) PositionGradeId.Trainee:
                        return Brushes.DarkGray;
                    case (int) PositionGradeId.Junior:
                        return Brushes.Green;
                    case (int) PositionGradeId.Normal:
                        return Brushes.Blue;
                    case (int) PositionGradeId.Senior:
                        return Brushes.Purple;
                    default:
                        return Brushes.Orange;
                }
            }
            else
                return Brushes.Black;
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
