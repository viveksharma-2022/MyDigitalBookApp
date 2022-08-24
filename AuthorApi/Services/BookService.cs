using CommonDbLayer.DatabaseEntity;
using System.Linq;

namespace AuthorApi.Services
{
    public class BookService:IBookService
    {
        private readonly MyDigitalBookDbContext _dbContext;
        public BookService(MyDigitalBookDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }
        public List<Book> GetAllBooks()
        {
            try
            {
                return _dbContext.Books.ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public string AddBooks(Book book)
        {
            try
            {
                Book bookdata = new Book();
                bookdata.Logo = book.Logo;
                bookdata.Title = book.Title;
                bookdata.Category = book.Category;
                bookdata.Price = book.Price;
                bookdata.AuthorName=book.AuthorName;
                bookdata.Publisher = book.Publisher;
                bookdata.PublisedDate = book.PublisedDate;
                bookdata.Content = book.Content;
                bookdata.Active = true;
                bookdata.CreatedDate = DateTime.Now;
                bookdata.ModifiedDate = DateTime.Now;
                _dbContext.Books.Add(bookdata);
                _dbContext.SaveChanges();
                return $"Book added successfully";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        public string UpdateBooks(Book book)
        {
            try
            {
                var bookdata = _dbContext.Books.FirstOrDefault(x=>x.BookId==book.BookId);
                if (bookdata != null)
                {
                    bookdata.Logo = book.Logo;
                    bookdata.Title = book.Title;
                    bookdata.Category = book.Category;
                    bookdata.Price = book.Price;
                    bookdata.AuthorName = book.AuthorName;
                    bookdata.Publisher = book.Publisher;
                    bookdata.PublisedDate = book.PublisedDate;
                    bookdata.Content = book.Content;
                    bookdata.Active = book.Active;
                    bookdata.CreatedDate = book.CreatedDate;
                    bookdata.ModifiedDate = DateTime.Now;
                    _dbContext.Books.Update(bookdata);
                    _dbContext.SaveChanges();
                    return $"Records has been updated for BookId: {book.BookId}.";
                }
                else
                {
                    return $"Book Id doesn't exist";
                }

            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        public string BlockaBook(int id)
        {
            Book blockbook = new Book();
            blockbook=_dbContext.Books.Where(x=>x.BookId==id).FirstOrDefault();
            if (blockbook !=null)
            {
                blockbook.Active = false;
                _dbContext.Books.Update(blockbook);
                _dbContext.SaveChanges();
                return $"Book has been blocked successfully";
            }
            else
            {
                return $"Book doesn't exist";
            }
        }
        public string UnBlockaBook(int id)
        {
            Book unblockbook = new Book();
            unblockbook = _dbContext.Books.Where(x=>x.BookId==id).FirstOrDefault();
            if (unblockbook != null)
            {
                unblockbook.Active = true;
                _dbContext.Books.Update(unblockbook);
                _dbContext.SaveChanges();
                return $"Book has been unblocked successfully";
            }
            else
            {
                return $"Book doesn't exist";
            }
        }
    }    
}
