using LibraryProjectWPF.Utilities;
using LPLibrary.DataAccess.Models;
using LPLibrary.Respository.Classes;
using LPLibrary.Respository.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LibraryProjectWPF.ViewModels
{
    internal class ReaderManageViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged Implementation

        #region Commands

        public ICommand SetReaderSearchModeCommand { get; set; }
        public ICommand ReaderSearchCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        #endregion Commands

        #region Fields

        private readonly int _minBirthYear = DateTime.Now.Year - 15;
        private readonly int _maxBirthYear = DateTime.Now.Year - 60;

        private IReaderRespository _readerRespository = new ReaderRespository();
        private ObservableCollection<Reader> _readers;
        private ComboBoxModel _occupation;
        private Reader _selectedReader;
        private int _cardNumber;
        private string _fullname;
        private DateTime _dOB;
        private DateTime _cardCreationDate;
        private string _address;

        public ObservableCollection<Reader> Readers
        {
            get => _readers;
            set
            {
                if (_readers != value)
                {
                    _readers = value;
                    OnPropertyChanged(nameof(Readers));
                }
            }
        }

        public DateTime Max { get; set; }
        public DateTime Min { get; set; }

        public string ReaderSearchMode { get; set; }
        public string ReaderSearchString { get; set; }

        public Reader SelectedReader
        {
            get => _selectedReader;
            set
            {
                if (_selectedReader != value)
                {
                    _selectedReader = value;
                    OnSelectedReaderChanged();
                }
            }
        }

        public ComboBoxModel[] Occupations { get; set; }

        public ComboBoxModel Occupation
        {
            get => _occupation;
            set
            {
                if (_occupation != value)
                {
                    _occupation = value;
                    OnPropertyChanged(nameof(Occupation));
                }
            }
        }

        public int CardNumber
        {
            get => _cardNumber;
            set
            {
                if (_cardNumber != value)
                {
                    _cardNumber = value;
                    OnPropertyChanged(nameof(CardNumber));
                }
            }
        }

        public string Fullname
        {
            get => _fullname;
            set
            {
                if (_fullname != value)
                {
                    _fullname = value;
                    OnPropertyChanged(nameof(Fullname));
                }
            }
        }

        public DateTime DOB
        {
            get => _dOB;
            set
            {
                if (_dOB != value)
                {
                    _dOB = value;
                    OnPropertyChanged(nameof(DOB));
                }
            }
        }

        public DateTime CardCreationDate
        {
            get => _cardCreationDate;
            set
            {
                if (_cardCreationDate != value)
                {
                    _cardCreationDate = value;
                    OnPropertyChanged(nameof(CardCreationDate));
                }
            }
        }

        public string Address
        {
            get => _address;
            set
            {
                if (_address != value)
                {
                    _address = value;
                    OnPropertyChanged(nameof(Address));
                }
            }
        }

        #endregion Fields

        public ReaderManageViewModel()
        {
            InitializeReaders();
            InitializeDatePickers();

            Occupations = new ComboBoxModel[] {
                new ComboBoxModel(){ Display = "Học Viên", Value = 0 },
                new ComboBoxModel(){ Display = "Sinh Viên", Value = 1 },
                new ComboBoxModel(){ Display = "Giáo Viên", Value = 2 },
            };
            SetReaderSearchMode("Card");
            SelectedReader = new();

            SetReaderSearchModeCommand = new RelayCommand<string>(
                (mode) => true,
                (mode) => SetReaderSearchMode(mode));

            ReaderSearchCommand = new RelayCommand<object>(
                (_) => true,
                (_) => ReaderSearch());

            RefreshCommand = new RelayCommand<object>(
                (_) => true,
                (_) => InitializeReaders());

            AddCommand = new RelayCommand<object>(
                (_) => true,
                (_) => AddReader());

            UpdateCommand = new RelayCommand<object>(
                (_) => SelectedReader != null && SelectedReader.CardNumber != 0,
                (_) => UpdateReader());

            DeleteCommand = new RelayCommand<object>(
                (_) => SelectedReader != null && SelectedReader.CardNumber != 0,
                (_) => DeleteReader());
        }

        private void InitializeReaders()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Readers = new ObservableCollection<Reader>(_readerRespository.GetReaders());
            });
        }

        private void InitializeDatePickers()
        {
            Min = new(_minBirthYear, DateTime.Now.Month, DateTime.Now.Day);
            Max = new(_maxBirthYear, DateTime.Now.Month, DateTime.Now.Day);
        }

        private void OnSelectedReaderChanged()
        {
            if (SelectedReader != null)
            {
                if (SelectedReader.Occupation != null)
                {
                    switch (SelectedReader.Occupation.ToLower())
                    {
                        case "học viên":
                            Occupation = Occupations.First(x => x.Value == 0);
                            break;

                        case "sinh viên":
                            Occupation = Occupations.First(x => x.Value == 1);
                            break;

                        case "giáo viên":
                            Occupation = Occupations.First(x => x.Value == 2);
                            break;
                    }
                }
                else
                {
                    Occupation = Occupations.First(x => x.Value == 0);
                }

                CardNumber = SelectedReader.CardNumber;

                if (!SelectedReader.FullName.IsNullOrEmpty())
                {
                    Fullname = SelectedReader.FullName;
                }
                else
                {
                    Fullname = "";
                }

                if (SelectedReader.DateOfBirth > Max)
                {
                    DOB = Max;
                }
                else
                {
                    DOB = SelectedReader.DateOfBirth;
                }

                CardCreationDate = SelectedReader.CardCreationDate;

                if (!SelectedReader.Address.IsNullOrEmpty())
                {
                    Address = SelectedReader.Address;
                }
                else
                {
                    Address = "";
                }
            }
        }

        private void SetReaderSearchMode(string parameter) => ReaderSearchMode = parameter;

        private void ReaderSearch()
        {
            InitializeReaders();
            if (!ReaderSearchString.IsNullOrEmpty())
            {
                switch (ReaderSearchMode)
                {
                    case "Card":
                        if (int.TryParse(ReaderSearchString, out int value))
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                Readers = new ObservableCollection<Reader>(Readers.Where(x => x.CardNumber == value));
                            });
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid number!", "Warning");
                            return;
                        }

                        break;

                    case "Name":
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Readers = new ObservableCollection<Reader>(Readers.Where(x => x.FullName.Contains(ReaderSearchString, StringComparison.OrdinalIgnoreCase)));
                        });
                        break;
                }
            }
        }

        private void AddReader()
        {
            string occupation = Occupation.Value switch
            {
                0 => "Học Viên",
                1 => "Sinh Viên",
                2 => "Giáo Viên",
                _ => "Error"
            };

            if (Fullname.IsNullOrEmpty() || Address.IsNullOrEmpty())
            {
                MessageBox.Show("Reader name and address must not be empty!");
                return;
            }

            _readerRespository.AddReader(new Reader()
            {
                Occupation = occupation,
                FullName = Fullname,
                DateOfBirth = DOB,
                CardCreationDate = DateTime.Now,
                Address = Address,
            });
            MessageBox.Show("Add new reader successfully!");
            InitializeReaders();
            SelectedReader = new();
        }

        private void UpdateReader()
        {
            string occupation = Occupation.Value switch
            {
                0 => "Học Viên",
                1 => "Sinh Viên",
                2 => "Giáo Viên",
                _ => "Error"
            };

            if (Fullname.IsNullOrEmpty() || Address.IsNullOrEmpty())
            {
                MessageBox.Show("Reader name and address must not be empty!");
                return;
            }

            _readerRespository.UpdateReader(new Reader()
            {
                CardNumber = CardNumber,
                Occupation = occupation,
                FullName = Fullname,
                DateOfBirth = DOB,
                CardCreationDate = CardCreationDate,
                Address = Address,
            });

            MessageBox.Show("Update reader successfully!");
            InitializeReaders();
            SelectedReader = new();
        }

        private void DeleteReader()
        {
            var result = MessageBox.Show("Do you really want to delete this reader and all of their records?", "Warning", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _readerRespository.DeleteReader(SelectedReader);
                MessageBox.Show("Reader deleted successfully!");
                InitializeReaders();
                SelectedReader = new();
            }
        }
    }
}