using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Trainee.Models;

namespace Trainee.Converters
{
    /// <summary>
    ///     GenderConverter
    /// </summary>
    [ValueConversion(typeof(Gender), typeof(String))]
    [ValueConversion(typeof(Gender), typeof(Brush))]
    public class GenderConverter : IValueConverter
    {
        #region Implementation of IValueConverter

        /// <summary>
        ///     Convert
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
            var gender = value as Gender;
            if (gender != null)
            {
                if (targetType == typeof(String))
                    return GetDescription(gender);
                else if (targetType == typeof (Brush))
                    return GetBrush(gender);
            }
            return null;
        }

        /// <summary>
        ///     ConvertBack
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion

        #region GetBrush

        /// <summary>
        ///     GetBrush
        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        protected virtual Brush GetBrush(Gender gender)
        {
            switch (gender.Id)
            {
                case (int)GenderId.Female:
                    return Brushes.Purple;
                case (int)GenderId.Male:
                    return Brushes.Blue;
                default:
                    return Brushes.Gray;
            }
        }

        #endregion

        #region GetDescription

        /// <summary>
        ///     GetDescription
        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        protected virtual String GetDescription(Gender gender)
        {
            return gender.Description;
        }

        #endregion
    }
}