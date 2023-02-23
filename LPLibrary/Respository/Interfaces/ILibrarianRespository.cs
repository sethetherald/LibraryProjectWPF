using LPLibrary.DataAccess.Models;

namespace LPLibrary.Respository.Interfaces
{
    public interface ILibrarianRespository
    {
        List<Librarian> GetLibrarians();

        Librarian? GetLibrarian(int librarianId);

        void AddLibrarian(Librarian data);

        void UpdateLibrarian(Librarian data);

        void DeleteLibrarian(Librarian data);
    }
}