using LPLibrary.DataAccess.Models;

namespace LPLibrary.Respository
{
    public interface IReaderRespository
    {
        List<Reader> GetReaders();

        Reader? GetReader(int readerCardId);

        Reader? GetReader(string fullname);

        void AddReader(Reader data);

        void UpdateReader(Reader data);

        void DeleteReader(Reader data);
    }
}