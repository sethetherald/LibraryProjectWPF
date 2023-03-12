using System.Globalization;
using System;
using System.Windows.Data;

namespace LibraryProjectWPF.Utilities.Converter
{
    internal class LendTicketConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string bookId = values[0].ToString();
            string readerId = values[1].ToString();
            DateTime expectedReturnDate = DateTime.Now;

            if (values[2] != null)
            {
                expectedReturnDate = (DateTime)values[2];
            }

            return new LendTicketConverterModel()
            {
                BookId = bookId,
                ReaderId = readerId,
                ExpectedReturnDate = expectedReturnDate
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    internal class LendTicketConverterModel
    {
        public string BookId { get; set; }
        public string ReaderId { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
    }
}