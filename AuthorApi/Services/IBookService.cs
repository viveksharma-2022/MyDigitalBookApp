using CommonDbLayer.DatabaseEntity;

namespace AuthorApi.Services
{
    public interface IBookService
    {
        string AddBooks(Book book);
        List<Book> GetAllBooks();
        string UpdateBooks(Book book);
        string BlockaBook(int id);
        string UnBlockaBook(int id);
    }
}
