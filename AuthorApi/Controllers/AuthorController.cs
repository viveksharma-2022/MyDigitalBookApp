using AuthorApi.Services;
using CommonDbLayer.DatabaseEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthorApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IBookService _bookService;
        private readonly INotificationService _notificationService;

        public AuthorController(IAuthorService authorService, IBookService bookService, INotificationService notificationService)
        {
            _authorService = authorService;
            _bookService = bookService;
            _notificationService = notificationService;
        }

        /// <summary>
        /// This Method is used to create a new user
        /// </summary>
        /// <param name="userdetails"></param>
        /// <returns>Adds a new user/Account</returns>
        [HttpPost]
        public ActionResult CreateAccount([FromBody] UserDetail userdetails)
        {
            try
            {
                string result = _authorService.AddAccount(userdetails);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return Ok(ex.Message);
            }
        }

        /// <summary>
        /// This method is used to login a user
        /// </summary>
        /// <param name="userdetails"></param>
        /// <returns>Validates a user credentials and allows to get login</returns>
        [HttpPost]
        public ActionResult Login([FromBody] UserDetail userdetails)
        {
            try
            {
                string result = _authorService.ValidateUser(userdetails.UserName, userdetails.Password);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return Ok(ex.Message);
            }
        }

        /// <summary>
        /// This method is used to return all books
        /// </summary>
        /// <returns>Returns a list of all books from server</returns>
        [HttpGet]
        
        public ActionResult GetAllBooks()
        {
            try
            {
                return Ok(_bookService.GetAllBooks());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// This method is used to Add a new book
        /// </summary>
        /// <param name="book"></param>
        /// <returns>Add a new book entry to database</returns>
        [HttpPost]
        [Authorize]
        public ActionResult AddBook([FromBody] Book book)
        {
            try
            {
                string result = _bookService.AddBooks(book);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return Ok(ex);
            }

        }

        /// <summary>
        /// This method Updates the existing book record
        /// </summary>
        /// <param name="book"></param>
        /// <returns>Updates an existing record in database</returns>
        [HttpPut]
        [Authorize]
        public ActionResult UpdateBook([FromBody] Book book)
        {
            try
            {
                string result = _bookService.UpdateBooks(book);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return Ok(ex);
            }
        }
        
    }
}
