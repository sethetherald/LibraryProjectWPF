using LibraryProjectWPF.Utilities;
using LPLibrary.Respository.Classes;
using LPLibrary.Respository.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LibraryProjectWPF.ViewModels
{
    internal class AddBookViewModel
    {
        #region Command

        public ICommand AddBooksCommand { get; set; }

        #endregion Command

        #region Fields

        public Window ThisWindow { get; set; }
        private int _titleId;
        private IBookRespository _bookRespository = new BookRespository();
        public string NumberOfBooks { get; set; }

        #endregion Fields

        public AddBookViewModel(Window window, int titleId)
        {
            ThisWindow = window;
            _titleId = titleId;

            AddBooksCommand = new RelayCommand<object>(
                (_) => true,
                (_) => AddBooks());
        }

        private void AddBooks()
        {
            if (NumberOfBooks.IsNullOrEmpty() || !int.TryParse(NumberOfBooks, out int _))
            {
                MessageBox.Show("Invalid number! Please enter again!", "Warning", MessageBoxButton.OK);
                return;
            }

            if (int.TryParse(NumberOfBooks, out int value) && value > 0)
            {
                var result = MessageBox.Show($"Do you want to add {value} book(s) to this title?", "Confirmation", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _bookRespository.AddBooks(value, _titleId);
                    MessageBox.Show("Book(s) added successfully!", "Success", MessageBoxButton.OK);
                    ThisWindow.Close();
                }
            }
            else if (value < 0)
            {
                MessageBox.Show("Please enter a positive number!", "Warning", MessageBoxButton.OK);
            }
        }
    }
}