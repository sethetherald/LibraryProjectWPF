using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace LibraryProjectWPF.Utilities.Converter
{
    internal class AccountConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string username = values[0].ToString();
            string password = (values[1] as PasswordBox).Password;

            return new AccountConverterModel
            {
                Username = username,
                Password = password
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    internal class AccountConverterModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}