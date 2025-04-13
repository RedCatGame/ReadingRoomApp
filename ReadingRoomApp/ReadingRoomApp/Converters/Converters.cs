using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using ReadingRoomApp.Models;

namespace ReadingRoomApp.Converters
{
    /// <summary>
    /// Конвертер для преобразования null в bool. 
    /// Если значение не null, возвращает true, иначе false.
    /// </summary>
    public class NullToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool invert = parameter != null && parameter.ToString().ToLower() == "true";
            bool isNotNull = value != null;

            return invert ? !isNotNull : isNotNull;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Конвертер для преобразования bool в Visibility.
    /// Если значение true, возвращает Visible, иначе Collapsed.
    /// </summary>
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = false;

            if (value is bool v)
            {
                boolValue = v;
            }
            else if (value is string v1)
            {
                boolValue = !string.IsNullOrEmpty(v1);
            }
            else if (value != null)
            {
                boolValue = true;
            }

            bool invert = parameter != null && parameter.ToString().ToLower() == "true";

            // Исправленная логика инвертирования
            if (invert)
            {
                boolValue = !boolValue;
            }

            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibility)
            {
                bool invert = parameter != null && parameter.ToString().ToLower() == "true";
                bool result = visibility == Visibility.Visible;

                // Исправленная логика инвертирования
                if (invert)
                {
                    result = !result;
                }

                return result;
            }

            return false;
        }
    }

    /// <summary>
    /// Конвертер для преобразования UserRole в строковое представление.
    /// </summary>
    public class UserRoleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is UserRole role)
            {
                switch (role)
                {
                    case UserRole.Reader:
                        return "Читатель";
                    case UserRole.Author:
                        return "Писатель";
                    case UserRole.Admin:
                        return "Администратор";
                    default:
                        return "Неизвестно";
                }
            }
            return "Неизвестно";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string roleStr)
            {
                switch (roleStr)
                {
                    case "Читатель":
                        return UserRole.Reader;
                    case "Писатель":
                        return UserRole.Author;
                    case "Администратор":
                        return UserRole.Admin;
                    default:
                        return UserRole.Reader;
                }
            }
            return UserRole.Reader;
        }
    }

    /// <summary>
    /// Конвертер для преобразования bool в строку доступности.
    /// Если true, возвращает "Доступна", иначе "Не доступна".
    /// </summary>
    public class BoolToAvailabilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isAvailable)
            {
                return isAvailable ? "Доступна" : "Не доступна";
            }
            return "Не определено";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string strValue)
            {
                return strValue == "Доступна";
            }
            return false;
        }
    }

    /// <summary>
    /// Конвертер для инвертирования bool значения.
    /// </summary>
    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return !boolValue;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return !boolValue;
            }
            return false;
        }
    }

    /// <summary>
    /// Конвертер для форматирования даты в строку.
    /// </summary>
    public class DateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime date)
            {
                string format = parameter as string ?? "dd.MM.yyyy";
                return date.ToString(format, culture);
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string dateStr)
            {
                if (DateTime.TryParse(dateStr, culture, DateTimeStyles.None, out DateTime result))
                {
                    return result;
                }
            }
            return DateTime.Now;
        }
    }

    /// <summary>
    /// Конвертер для множественного сравнения значений.
    /// Проверяет, входит ли значение в список параметров.
    /// </summary>
    public class MultiValueEqualityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string parameters)
            {
                string[] values = parameters.Split(',');
                foreach (string val in values)
                {
                    if (value?.ToString() == val.Trim())
                    {
                        return true;
                    }
                }
                return false;
            }
            return value?.Equals(parameter) ?? false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Конвертер для преобразования числа в строку с добавлением единиц измерения.
    /// </summary>
    public class NumberToStringWithUnitsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            string units = parameter as string ?? "";
            return $"{value} {units}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string strValue)
            {
                string units = parameter as string ?? "";
                string numStr = strValue.Replace(units, "").Trim();

                if (int.TryParse(numStr, out int result))
                {
                    return result;
                }

                if (double.TryParse(numStr, out double doubleResult))
                {
                    return doubleResult;
                }
            }
            return 0;
        }
    }
}