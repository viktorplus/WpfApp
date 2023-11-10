using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows;
using System;

namespace WpfApp.Domain
{
    public class RoleVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Проверяем, является ли роль "Студент"
            return (value as List<User.UserRole>)?.Contains(User.UserRole.Student) == true ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
