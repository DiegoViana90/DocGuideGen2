using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace DocGuideGen2.Converters
{
    public class StringEqualsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString() == parameter?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isChecked && isChecked && parameter != null)
            {
                return parameter.ToString();
            }

            // Retorna o valor atual se o RadioButton for desmarcado
            return Binding.DoNothing;
        }
    }
}
