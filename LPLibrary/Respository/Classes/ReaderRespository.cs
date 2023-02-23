using LPLibrary.DataAccess.DataManagement;
using LPLibrary.DataAccess.Models;
using LPLibrary.Respository.Interfaces;

namespace LPLibrary.Respository.Classes
{
    public class ReaderRespository : IReaderRespository
    {
        public void AddReader(Reader data) => ReaderManagement.AddReader(data);

        public void DeleteReader(Reader data) => ReaderManagement.DeleteReader(data);

        public Reader? GetReader(int readerCardId) => ReaderManagement.GetReader(readerCardId);

        public Reader? GetReader(string fullname) => ReaderManagement.GetReader(fullname);

        public List<Reader> GetReaders() => ReaderManagement.GetReaders();

        public void UpdateReader(Reader data) => ReaderManagement.UpdateReader(data);
    }
}