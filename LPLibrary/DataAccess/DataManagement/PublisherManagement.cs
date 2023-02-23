using LPLibrary.DataAccess.Models;

namespace LPLibrary.DataAccess.DataManagement
{
    public static class PublisherManagement
    {
        public static List<Publisher> GetPublishers()
        {
            using var context = new LibraryManagementContext();
            return context.Publishers.ToList();
        }
    }
}