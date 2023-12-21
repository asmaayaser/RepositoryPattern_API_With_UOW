using ReposetoryPatternWith_UOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReposetoryPatternWith_UOW.Core.Interfaces
{
    public interface IBookRepository:IBaseRepository<Book>
    {
        IEnumerable<Book> SpecialMethod();
    }
}
