using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReposetoryPatternWith_UOW.Core;
using ReposetoryPatternWith_UOW.Core.Consts;
using ReposetoryPatternWith_UOW.Core.Interfaces;
using ReposetoryPatternWith_UOW.Core.Models;

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
        public IActionResult GetById(int id)
        {
            return Ok(UnitOfWork.Books.GetById(id));
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(UnitOfWork.Books.GetAll());
        }

        //[HttpGet("GetByName")]
        //public IActionResult GetByName(string name)
        //{
        //    return Ok(UnitOfWork.Books.Find(b => b.Title == name));
        //}

        [HttpGet("GetByName")]
        public IActionResult GetByName(string name)
        {
            return Ok(UnitOfWork.Books.Find(b => b.Title == name, new[] {"Author"}));
        }



        [HttpGet("GetAllWithAutho")]
        public IActionResult GetAllWithAutho(string name)
        {
            return Ok(UnitOfWork.Books.FindAll(b => b.Title == name, new[] { "Author" }));
        }

        [HttpGet("GetAllWithAutho_W_Part")]
        public IActionResult GetAllWithAutho_W_Part(string name)
        {
            return Ok(UnitOfWork.Books.FindAll(b => b.Title.Contains ( name), new[] { "Author" }));
        }


        [HttpGet("GetOrderedAsc_W_Part")]
        public IActionResult GetOrderedAsc_W_Part(string name)
        {
            return Ok(UnitOfWork.Books.FindAll(b => b.Title.Contains(name),null,null,b=>b.Id, OrderBy.Ascending));
        }

        [HttpGet("GetOrderedDesc_W_Part")]
        public IActionResult GetOrderedDesc_W_Part(string name)
        {
            return Ok(UnitOfWork.Books.FindAll(b => b.Title.Contains(name), null, null, b => b.Id,OrderBy.Descending));
        }


        [HttpPost]
        public IActionResult AddOne(Book entity)
        {
            var Book = UnitOfWork.Books.Add(new Book { Title = entity.Title, AuthorId = entity.AuthorId });
            UnitOfWork.Complete();
            return Ok(Book);
        }


    }
}
