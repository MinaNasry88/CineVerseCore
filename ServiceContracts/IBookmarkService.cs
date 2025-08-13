namespace ServiceContracts
{
    public interface IBookmarkService
    {
        Task AddBookMark(int id, string userName);
    }
}
