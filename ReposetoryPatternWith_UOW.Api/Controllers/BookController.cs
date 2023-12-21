using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReposetoryPatternWith_UOW.Core;
using ReposetoryPatternWith_UOW.Core.Consts;
using ReposetoryPatternWith_UOW.Core.DTO;
using ReposetoryPatternWith_UOW.Core.Interfaces;
using ReposetoryPatternWith_UOW.Core.Models;
using System.Xml.Linq;
using static System.Reflection.Metadata.BlobBuilder;

namespace ReposetoryPatternWith_UOW.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        //private readonly IBaseRepository<Book> BookRepo;
        private readonly IUnitOfWork UnitOfWork; 
        public BookController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Book> Books = UnitOfWork.Books.GetAll(new[] { "Author" });
            if(Books == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return Ok(Books);
        }
        [HttpPost]
        public IActionResult AddOne(Book entity)
        {
            if (ModelState.IsValid)
            {
                var Book = UnitOfWork.Books.Add(new Book { Title = entity.Title, AuthorId = entity.AuthorId });
                UnitOfWork.Complete();
                string url = Url.Link("BookDetailsRoute", new { id = Book.Id })!;
                if(url == null)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                return Created(url, Book);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public IActionResult Update(Book entity)
        {
            if (ModelState.IsValid)
            {
                var oldbook = UnitOfWork.Books.GetById(entity.Id);
                if (oldbook == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                oldbook.Title = entity.Title;
                oldbook.AuthorId = entity.AuthorId;
                var updatedBook = UnitOfWork.Books.Update(oldbook);
                UnitOfWork.Complete();
                string url = Url.Link("BookDetailsRoute", new { id = updatedBook.Id })!;
                if (url == null)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                return Created(url, updatedBook);
            }
            return BadRequest(ModelState);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var oldbook = UnitOfWork.Books.GetById(id);
                if (oldbook == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                try
                {
                    UnitOfWork.Books.Delete(oldbook);
                    UnitOfWork.Complete();
                    return StatusCode(StatusCodes.Status204NoContent);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            return BadRequest(ModelState);
        }

        [HttpGet("GetById", Name = "BookDetailsRoute")]
        public IActionResult GetById(int id)
        {
            if (ModelState.IsValid)
            {
                Book book = UnitOfWork.Books.GetById(id);
                if (book == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                return Ok(book);
            }
            return BadRequest(ModelState);

        }

        [HttpGet("GetByName")]
        public IActionResult GetByName(string name)
        {
            if (ModelState.IsValid)
            {
                return Ok(UnitOfWork.Books.Find(b => b.Title == name, new[] { "Author" }));
            }
            return BadRequest(ModelState);
        }


        [HttpGet("Author/{id}:int")]
        public IActionResult GetBookWithAuthId(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(UnitOfWork.Books.FindAll(b => b.Author.Id == id, new[] { "Author" }));
            }
            return BadRequest(ModelState);

        }

        [HttpGet("Author/{name}:alpha")]
        public IActionResult GetBookWithAuthName(string name)
        {
            if (ModelState.IsValid)
            {
                return Ok(UnitOfWork.Books.FindAll(b => b.Author.Name == name, new[] { "Author" }));
            }
            return BadRequest(ModelState);
        }
        [HttpGet("dto/{id}")]
        public IActionResult GetBookNameWithAuthId(int id)
        {
            if (ModelState.IsValid)
            {
                Book Book = UnitOfWork.Books.Find(b => b.Author.Id == id, new[] { "Author" });
                if (Book == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                BookNameWithAuthorNameDTO BookDTO = new BookNameWithAuthorNameDTO();
                BookDTO.Title = Book.Title;
                BookDTO.AuthorName = Book.Author.Name;
                return Ok(BookDTO);


            }
            return BadRequest(ModelState);
        }

        [HttpGet("dtoAll/{id}")]
        public IActionResult GetBookNameWithAuthId1(int id)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<Book> Books = UnitOfWork.Books.FindAll(b => b.Author.Id == id, new[] { "Author" });
                if (Books == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                IEnumerable<BookNameWithAuthorNameDTO> BooksDTO = Books.Select(book => new BookNameWithAuthorNameDTO(){AuthorName=book.Author.Name , Title=book.Title });
                return Ok(BooksDTO);
            }
            return BadRequest(ModelState);
        }



        [HttpGet("GetAllWithAutho")]
        public IActionResult GetAllWithAutho(string name)
        {
            if (ModelState.IsValid)
            {
                return Ok(UnitOfWork.Books.FindAll(b => b.Title == name, new[] { "Author" }));
            }
            return BadRequest(ModelState);
        }

        [HttpGet("GetAllWithAutho_W_Part")]
        public IActionResult GetAllWithAutho_W_Part(string name)
        {
            if (ModelState.IsValid)
            {
                return Ok(UnitOfWork.Books.FindAll(b => b.Title.Contains(name), new[] { "Author" }));
            }
            return BadRequest(ModelState);
        }


        [HttpGet("GetOrderedAsc_W_Part")]
        public IActionResult GetOrderedAsc_W_Part(string name)
        {
            if (ModelState.IsValid)
            {
                return Ok(UnitOfWork.Books.FindAll(b => b.Title.Contains(name), null, null, b => b.Id, OrderBy.Ascending));
            }
            return BadRequest(ModelState);
        }

        [HttpGet("GetOrderedDesc_W_Part")]
        public IActionResult GetOrderedDesc_W_Part(string name)
        {
            if (ModelState.IsValid)
            {
                return Ok(UnitOfWork.Books.FindAll(b => b.Title.Contains(name), null, null, b => b.Id, OrderBy.Descending));
            }
            return BadRequest(ModelState);
        }






        //[HttpPut]
        //public IActionResult Update(Book entity )
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var oldbook = UnitOfWork.Books.GetById(entity.Id);
        //        if (oldbook == null)
        //        {
        //            return StatusCode(StatusCodes.Status404NotFound);
        //        }
        //        oldbook.Title = entity.Title;
        //        oldbook.AuthorId = entity.AuthorId;
        //        var updatedBook = UnitOfWork.Books.Update(oldbook);
        //        UnitOfWork.Complete();
        //        return Ok(updatedBook);
        //    }
        //    return BadRequest(ModelState);
        //}

        //[HttpGet("GetByName")]
        //public IActionResult GetByName(string name)
        //{
        //    return Ok(UnitOfWork.Books.Find(b => b.Title == name));
        //}
    }
}
