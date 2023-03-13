using LibraryProjectWPF.Utilities;
using LPLibrary.DataAccess.Models;
using LPLibrary.Respository.Classes;
using LPLibrary.Respository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace LibraryProjectWPF.ViewModels
{
    internal class TitleManageViewModel
    {
        #region Commands

        public ICommand TitleManageCommand { get; set; }

        #endregion Commands

        #region Fields

        private IAuthorRespository _authorRespository = new AuthorRespository();
        private IAuthorBookRespository _authorBookRespository = new AuthorBookRespository();
        private IPublisherRespository _publisherRespository = new PublisherRespository();
        private IBookInfoRespository _bookInfoRespository = new BookInfoRespository();

        private int _titleId;
        public string WindowTitle { get; set; }
        public string ButtonContent { get; set; }
        public Window ThisWindow { get; set; }
        public string Title { get; set; }
        public string NumberOfPages { get; set; }

        public ComboBoxModel[] Authors { get; set; }
        public ComboBoxModel SelectedAuthor { get; set; }
        public ComboBoxModel[] Publishers { get; set; }
        public ComboBoxModel SelectedPublisher { get; set; }

        #endregion Fields

        public TitleManageViewModel(Window window)
        {
            ThisWindow = window;
            WindowTitle = "Add Title";
            ButtonContent = "Add";

            InitializeComboBoxes();
            if (Authors != null && Publishers != null)
            {
                SelectedAuthor = Authors[0];
                SelectedPublisher = Publishers[0];
            }

            TitleManageCommand = new RelayCommand<object>(
                (_) => true,
                (_) => TitleManageExecute());
        }

        public TitleManageViewModel(Window window, int titleId)
        {
            ThisWindow = window;

            WindowTitle = "Edit Title";
            ButtonContent = "Update";

            BookInfo? currentTitle = _bookInfoRespository.GetBookInfo(titleId);
            AuthorBook? currentTitleAuthor = _authorBookRespository.GetAuthorBookByTitleId(titleId);
            _titleId = titleId;
            InitializeComboBoxes();

            if (currentTitle == null)
            {
                MessageBox.Show("Can't get title info!", "Error", MessageBoxButton.OK);
                ThisWindow.Close();
                return;
            }

            Title = currentTitle.Title;
            NumberOfPages = currentTitle.NumberOfPages.ToString();

            if (Authors != null && Publishers != null)
            {
                SelectedAuthor = Authors.FirstOrDefault(x => x.Value == currentTitleAuthor.AuthorId);
                SelectedPublisher = Publishers.FirstOrDefault(x => x.Value == currentTitle.PublisherId);
            }

            TitleManageCommand = new RelayCommand<object>(
                (_) => true,
                (_) => TitleManageExecute());
        }

        private void InitializeComboBoxes()
        {
            List<Author> tempAuthors = _authorRespository.GetAuthors();
            Authors = new ComboBoxModel[tempAuthors.Count];
            for (int i = 0; i < tempAuthors.Count; i++)
            {
                Authors[i] = new ComboBoxModel()
                {
                    Display = tempAuthors[i].AuthorName,
                    Value = tempAuthors[i].AuthorId
                };
            }

            List<Publisher> tempPublishers = _publisherRespository.GetPublishers();
            Publishers = new ComboBoxModel[tempPublishers.Count];
            for (int i = 0; i < tempPublishers.Count; i++)
            {
                Publishers[i] = new ComboBoxModel()
                {
                    Display = tempPublishers[i].PublisherName,
                    Value = tempPublishers[i].PublisherId
                };
            }
        }

        private void TitleManageExecute()
        {
            int numberOfPages;
            if (int.TryParse(NumberOfPages, out int value))
            {
                numberOfPages = value;
            }
            else
            {
                MessageBox.Show("Please input a valid number of pages!", "Warning", MessageBoxButton.OK);
                return;
            }

            bool valid = CheckTitleAndPages(Title, numberOfPages);
            if (!valid) return;

            switch (ButtonContent)
            {
                case "Add":
                    AddTitleToBookInfo(Title, numberOfPages);
                    AddTitleToAuthorBook(Title);
                    break;

                case "Update":
                    UpdateTitleBookInfo(_titleId);
                    break;
            }
        }

        private void AddTitleToBookInfo(string title, int pages)
        {
            int publisherId = SelectedPublisher.Value;
            BookInfo? checkDuplicate = _bookInfoRespository.GetBookInfo(title);

            if (checkDuplicate != null)
            {
                MessageBox.Show("Title already existed!", "Warning", MessageBoxButton.OK);
                return;
            }

            _bookInfoRespository.AddBookInfo(new()
            {
                Title = title,
                InStock = 0,
                NumberOfPages = pages,
                PublisherId = publisherId,
            });
        }

        private void AddTitleToAuthorBook(string title)
        {
            int authorId = SelectedAuthor.Value;
            BookInfo? newlyAdded = _bookInfoRespository.GetBookInfo(title);

            if (newlyAdded == null)
            {
                MessageBox.Show("Add title failed! Please check the database!", "Warning", MessageBoxButton.OK);
                return;
            }

            _authorBookRespository.AddAuthorBook(new()
            {
                AuthorId = authorId,
                TitleId = newlyAdded.TitleId,
                AuthorRole = "Chủ biên"
            });
            MessageBox.Show("Added Successfully!", "Success", MessageBoxButton.OK);
            ThisWindow.Close();
        }

        private void UpdateTitleBookInfo(int titleId)
        {
            BookInfo? checkExist = _bookInfoRespository.GetBookInfo(titleId);
            if (checkExist == null)
            {
                MessageBox.Show("Edit title failed! Please check the database!", "Warning", MessageBoxButton.OK);
                return;
            }

            checkExist.Title = Title;
            checkExist.NumberOfPages = int.Parse(NumberOfPages);
            checkExist.PublisherId = SelectedPublisher.Value;

            _bookInfoRespository.UpdateBookInfo(checkExist);
            _authorBookRespository.UpdateAuthorBook(titleId.ToString(), SelectedAuthor.Value);
            MessageBox.Show("Updated Successfully!", "Success", MessageBoxButton.OK);
            ThisWindow.Close();
        }

        public bool CheckTitleAndPages(string title, int pages)
        {
            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Title must not be empty!", "Warning", MessageBoxButton.OK);
                return false;
            }
            else if (pages == 0)
            {
                MessageBox.Show("Invalid number of pages!", "Warning", MessageBoxButton.OK);
                return false;
            }
            return true;
        }
    }
}