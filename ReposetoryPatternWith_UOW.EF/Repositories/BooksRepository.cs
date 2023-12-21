using ReposetoryPatternWith_UOW.Core.Interfaces;
using ReposetoryPatternWith_UOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReposetoryPatternWith_UOW.EF.Repositories
{
    public class BooksRepository:BaseRepository<Book>,IBookRepository
    {
        private readonly AppDBContext Context;
        public BooksRepository(AppDBContext context) : base(context) { }

        public IEnumerable<Book> SpecialMethod()
        {
            throw new NotImplementedException();
        }
    }
}
