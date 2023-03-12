using LibraryProjectWPF.Utilities;
using LibraryProjectWPF.Utilities.Converter;
using LibraryProjectWPF.Views;
using LPLibrary.DataAccess.Models;
using LPLibrary.Respository.Classes;
using System.Windows;
using System.Windows.Input;

namespace LibraryProjectWPF.ViewModels
{
    internal class LoginViewModel
    {
        public ICommand LoginCommand { get; set; }
        public WindowLogin WindowLogin { get; set; }

        public LoginViewModel(WindowLogin window)
        {
            WindowLogin = window;
            LoginCommand = new RelayCommand<AccountConverterModel>(
                (account) => true,
                (account) => Login(account)
                );
        }

        private void Login(AccountConverterModel account)
        {
            AccountRespository accountRespository = new();
            LibrarianRespository librarianRespository = new();

            Account? login = accountRespository.GetAccount(account.Username, account.Password);
            if (login != null)
            {
                Librarian? librarian = librarianRespository.GetLibrarian(login.LibrarianId);
                if (librarian != null)
                {
                    MainWindow mainWindow = new(login, librarian);
                    WindowLogin.Close();
                    mainWindow.Show();
                }
                else
                {
                    MessageBox.Show("Error in getting librarian info, please check the database!", "Weird Error");
                }
            }
            else
            {
                MessageBox.Show("Wrong account or password!", "Login Failed");
            }
        }
    }
}