namespace ServiceContracts
{
    public interface IBookmarkService
    {
        Task AddBookMark(int id, string username);
        Task DeleteBookMark(int id, string username);
    }
}
