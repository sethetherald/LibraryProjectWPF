using LPLibrary.DataAccess.DataManagement;
using LPLibrary.DataAccess.Models;
using LPLibrary.Respository.Interfaces;

namespace LPLibrary.Respository.Classes
{
    public class PublisherRespository : IPublisherRespository
    {
        public List<Publisher> GetPublishers() => PublisherManagement.GetPublishers();
    }
}