using LPLibrary.DataAccess.DataManagement;
using LPLibrary.DataAccess.Models;
using LPLibrary.Respository.Interfaces;

namespace LPLibrary.Respository.Classes
{
    public class LibrarianRespository : ILibrarianRespository
    {
        public void AddLibrarian(Librarian data) => LibrarianManagement.AddLibrarian(data);

        public void DeleteLibrarian(Librarian data) => LibrarianManagement.DeleteLibrarian(data);

        public Librarian? GetLibrarian(int librarianId) => LibrarianManagement.GetLibrarian(librarianId);

        public List<Librarian> GetLibrarians() => LibrarianManagement.GetLibrarians();

        public void UpdateLibrarian(Librarian data) => LibrarianManagement.UpdateLibrarian(data);
    }
}