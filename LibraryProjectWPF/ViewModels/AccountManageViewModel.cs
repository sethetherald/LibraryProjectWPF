using LibraryProjectWPF.Utilities;
using LPLibrary.DataAccess.Models;
using LPLibrary.Respository.Classes;
using LPLibrary.Respository.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace LibraryProjectWPF.ViewModels
{
    internal class AccountManageViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged Implementation

        #region Commands

        public ICommand RefreshCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        #endregion Commands

        #region Fields

        private readonly int _librarianId;
        private bool _isAdmin;
        private bool _cbAdminEnabled;
        private ObservableCollection<AccountManagementModel> _accounts;
        private IAccountRespository _accountRespository = new AccountRespository();
        private ILibrarianRespository _librarianRespository = new LibrarianRespository();
        private string _librarianIdString;
        private string _username;
        private string _fullname;
        private string _email;
        private AccountManagementModel? _currentSelectedAccount;
        private string _password;

        public ObservableCollection<AccountManagementModel> Accounts
        {
            get => _accounts;
            set
            {
                if (_accounts != value)
                { _accounts = value; OnPropertyChanged(nameof(Accounts)); }
            }
        }

        public AccountManagementModel? CurrentSelectedAccount
        {
            get => _currentSelectedAccount;
            set
            {
                _currentSelectedAccount = value;
                //Update other fields
                OnSelectedAcountChanged();
            }
        }

        public string LibrarianId
        {
            get => _librarianIdString;
            set
            {
                if (_librarianIdString != value)
                {
                    _librarianIdString = value;
                    OnPropertyChanged(nameof(LibrarianId));
                }
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        public string Name
        {
            get => _fullname;
            set
            {
                if (_fullname != value)
                {
                    _fullname = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        public string Password
        {
            get => _password; set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public bool IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                if (_isAdmin != value)
                {
                    _isAdmin = value;
                    OnPropertyChanged(nameof(IsAdmin));
                }
            }
        }

        public bool CheckBoxAdminIsEnabled
        {
            get => _cbAdminEnabled; set
            {
                if (_cbAdminEnabled != value)
                {
                    _cbAdminEnabled = value;
                    OnPropertyChanged(nameof(CheckBoxAdminIsEnabled));
                }
            }
        }

        #endregion Fields

        public AccountManageViewModel(int librarianId)
        {
            _librarianId = librarianId;
            IsAdmin = false;
            CheckBoxAdminIsEnabled = true;
            InitializeAccounts();

            AddCommand = new RelayCommand<object>(
                (_) => true,
                (_) => AddAccount());

            UpdateCommand = new RelayCommand<object>(
                (_) => CurrentSelectedAccount != null && CurrentSelectedAccount.Id != 0,
                (_) => UpdateAccount());

            DeleteCommand = new RelayCommand<object>(
                (_) => CurrentSelectedAccount != null && CurrentSelectedAccount.Id != 0 && CurrentSelectedAccount.Id != _librarianId,
                (_) => DeleteAccount());
        }

        private void InitializeAccounts()
        {
            List<Account> tempAccounts = _accountRespository.GetAccounts();
            List<Librarian> tempLibrarians = _librarianRespository.GetLibrarians();
            List<AccountManagementModel> temp = (from acc in tempAccounts
                                                 join lib in tempLibrarians on acc.LibrarianId equals lib.LibrarianId
                                                 select new AccountManagementModel()
                                                 {
                                                     Id = acc.LibrarianId,
                                                     UserName = acc.Username,
                                                     FullName = lib.LibrarianName,
                                                     Mail = acc.Gmail,
                                                     IsAdmin = acc.Role != 0
                                                 }).ToList();
            Application.Current.Dispatcher.Invoke(() =>
                Accounts = new ObservableCollection<AccountManagementModel>(temp)
            );
        }

        private void OnSelectedAcountChanged()
        {
            if (CurrentSelectedAccount != null)
            {
                Account? tempAccount = _accountRespository.GetAccount(CurrentSelectedAccount.Id);
                LibrarianId = CurrentSelectedAccount.Id.ToString();
                Username = CurrentSelectedAccount.UserName;
                Name = CurrentSelectedAccount.FullName;
                Email = CurrentSelectedAccount.Mail;
                if (tempAccount != null)
                {
                    Password = tempAccount.Password;
                }
                else
                {
                    Password = "";
                }
                if (CurrentSelectedAccount.IsAdmin != null)
                {
                    IsAdmin = (bool)CurrentSelectedAccount.IsAdmin;
                }
                else
                {
                    IsAdmin = false;
                }
                CheckBoxAdminIsEnabled = _librarianId != CurrentSelectedAccount.Id;
            }
            else
            {
                CurrentSelectedAccount = new();
            }
        }

        private void AddAccount()
        {
            var result = MessageBox.Show("Create an account with these information?", "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                bool valid = ValidateFields(Username, Password, Name, Email);
                if (!valid) return;

                Account? checkExist = _accountRespository.GetAccount(Username);
                if (checkExist != null)
                {
                    MessageBox.Show("Account with this username already exist!");
                    return;
                }

                checkExist = _accountRespository.GetAccount(Email);
                if (checkExist != null)
                {
                    MessageBox.Show("Account with this email already exist!");
                    return;
                }

                Account newAccount = new()
                {
                    Username = Username,
                    Gmail = Email,
                    Password = Password,
                    Role = IsAdmin ? 0 : 1,
                };

                Librarian newLibrarian = new()
                {
                    LibrarianName = Name
                };

                newLibrarian.Accounts.Add(newAccount);
                _librarianRespository.AddLibrarian(newLibrarian);
                MessageBox.Show($"Add new {(IsAdmin ? "Admin" : "Librarian")} account successfully");

                InitializeAccounts();
            }
        }

        private void UpdateAccount()
        {
            var result = MessageBox.Show("Update account with these information?", "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                int accountId = int.Parse(LibrarianId);
                bool valid = ValidateFields(Username, Password, Name, Email);
                if (!valid) return;

                Librarian? librarianToUpdate = _librarianRespository.GetLibrarian(accountId);
                Account? accountToUpdate = _accountRespository.GetAccount(accountId);
                if (librarianToUpdate != null)
                {
                    librarianToUpdate.LibrarianName = Name;
                    _librarianRespository.UpdateLibrarian(librarianToUpdate);
                }
                if (accountToUpdate != null)
                {
                    accountToUpdate.Gmail = Email;
                    accountToUpdate.Password = Password;
                    accountToUpdate.Role = IsAdmin ? 1 : 0;
                    _accountRespository.UpdateAccount(accountToUpdate);
                }
                MessageBox.Show($"Update {(IsAdmin ? "Admin" : "Librarian")} account successfully");

                InitializeAccounts();
                CurrentSelectedAccount = new();
            }
        }

        private void DeleteAccount()
        {
            var result = MessageBox.Show("Are you sure you want to delete this account?", "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                int accountId = int.Parse(LibrarianId);
                Librarian? librarianToDelete = _librarianRespository.GetLibrarian(accountId);
                Account? accountToDelete = _accountRespository.GetAccount(accountId);
                if (accountToDelete != null && librarianToDelete != null)
                {
                    librarianToDelete.Accounts.Add(accountToDelete);
                    _librarianRespository.DeleteLibrarian(librarianToDelete);
                    MessageBox.Show("Deleted successfully!");
                }
                InitializeAccounts();
                CurrentSelectedAccount = new();
            }
        }

        private bool ValidateFields(string username, string password, string librarianName, string mail)
        {
            int count = 0;
            foreach (char c in username)
            {
                if (char.IsWhiteSpace(c))
                {
                    MessageBox.Show("Username must not contains whitespaces!");
                    return false;
                }
            }

            foreach (char c in password)
            {
                if (char.IsWhiteSpace(c))
                {
                    MessageBox.Show("Password must not contains whitespaces!");
                    return false;
                }
                else
                {
                    count++;
                }
            }

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Username must not be empty!");
                return false;
            }

            if (string.IsNullOrEmpty(librarianName))
            {
                MessageBox.Show("Fullname must not be empty!");
                return false;
            }
            if (!MailAddress.TryCreate(mail, out _))
            {
                MessageBox.Show("Please enter a valid mail address!");
                return false;
            }

            if (string.IsNullOrEmpty(password) || count < 4)
            {
                MessageBox.Show("Password must be 4 characters or above!");
                return false;
            }
            return true;
        }
    }
}