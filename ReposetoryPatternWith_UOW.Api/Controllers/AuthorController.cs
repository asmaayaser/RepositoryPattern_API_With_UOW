using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReposetoryPatternWith_UOW.Core;
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

        [HttpGet]
        public IActionResult GetById( int id )
        {
            return Ok(UnitOfWork.Authors.GetById(id));
        }
    }
}
