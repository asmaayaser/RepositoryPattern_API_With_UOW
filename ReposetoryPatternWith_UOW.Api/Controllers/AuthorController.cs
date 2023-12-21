using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReposetoryPatternWith_UOW.Core;
using ReposetoryPatternWith_UOW.Core.Consts;
using ReposetoryPatternWith_UOW.Core.DTO;
using ReposetoryPatternWith_UOW.Core.Interfaces;
using ReposetoryPatternWith_UOW.Core.Models;
using ReposetoryPatternWith_UOW.EF;

namespace ReposetoryPatternWith_UOW.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        //private readonly IBaseRepository<Author> AuthorRepo;
        private readonly IUnitOfWork UnitOfWork;
        public AuthorController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;   
        }

        //[HttpGet]
        //public IActionResult GetById( int id )
        //{
        //    return Ok(UnitOfWork.Authors.GetById(id));
        //}

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Author> Authors = UnitOfWork.Authors.GetAll();
            if (Authors == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return Ok(Authors);
        }
        [HttpPost]
        public IActionResult AddOne(Author entity)
        {
            if (ModelState.IsValid)
            {
                var Author = UnitOfWork.Authors.Add(new Author { Name = entity.Name});
                UnitOfWork.Complete();
                string url = Url.Link("AuthorDetailsRoute", new { id = Author.Id })!;
                if (url == null)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                return Created(url, Author);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public IActionResult Update(Author entity)
        {
            if (ModelState.IsValid)
            {
                var oldAuthor = UnitOfWork.Authors.GetById(entity.Id);
                if (oldAuthor == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                oldAuthor.Name = entity.Name;
                var updatedAuthor = UnitOfWork.Authors.Update(oldAuthor);
                UnitOfWork.Complete();
                string url = Url.Link("AuthorDetailsRoute", new { id = updatedAuthor.Id })!;
                if (url == null)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                return Created(url, updatedAuthor);
            }
            return BadRequest(ModelState);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var oldAuthor = UnitOfWork.Authors.GetById(id);
                if (oldAuthor == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                try
                {
                    UnitOfWork.Authors.Delete(oldAuthor);
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

        [HttpGet("GetById", Name = "AuthorDetailsRoute")]
        public IActionResult GetById(int id)
        {
            if (ModelState.IsValid)
            {
                Author Author = UnitOfWork.Authors.GetById(id);
                if (Author == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                return Ok(Author);
            }
            return BadRequest(ModelState);

        }

        [HttpGet("GetByName")]
        public IActionResult GetByName(string name)
        {
            if (ModelState.IsValid)
            {
                return Ok(UnitOfWork.Authors.Find(b => b.Name == name));
            }
            return BadRequest(ModelState);
        }

        [HttpGet("GetAllByName")]
        public IActionResult GetAllWithAutho(string name)
        {
            if (ModelState.IsValid)
            {
                return Ok(UnitOfWork.Authors.FindAll(b => b.Name == name));
            }
            return BadRequest(ModelState);
        }

        [HttpGet("GetAll_W_Part")]
        public IActionResult GetAllWithAutho_W_Part(string name)
        {
            if (ModelState.IsValid)
            {
                return Ok(UnitOfWork.Authors.FindAll(b => b.Name.Contains(name)));
            }
            return BadRequest(ModelState);
        }

        [HttpGet("GetOrderedAsc_W_Part")]
        public IActionResult GetOrderedAsc_W_Part(string name)
        {
            if (ModelState.IsValid)
            {
                return Ok(UnitOfWork.Authors.FindAll(b => b.Name.Contains(name), null, null, b => b.Id, OrderBy.Ascending));
            }
            return BadRequest(ModelState);
        }

        [HttpGet("GetOrderedDesc_W_Part")]
        public IActionResult GetOrderedDesc_W_Part(string name)
        {
            if (ModelState.IsValid)
            {
                return Ok(UnitOfWork.Authors.FindAll(b => b.Name.Contains(name), null, null, b => b.Id, OrderBy.Descending));
            }
            return BadRequest(ModelState);
        }




    }
}
