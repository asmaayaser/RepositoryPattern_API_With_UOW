using ReposetoryPatternWith_UOW.Core;
using ReposetoryPatternWith_UOW.Core.Interfaces;
using ReposetoryPatternWith_UOW.Core.Models;
using ReposetoryPatternWith_UOW.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReposetoryPatternWith_UOW.EF
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly AppDBContext Context;
        public IBaseRepository<Author> Authors { get; set; }
        public IBookRepository Books { get; set; }


        public UnitOfWork(AppDBContext context)
        {
            Context = context;
            Authors=new BaseRepository<Author>(context);
            Books = new BooksRepository(context);
        }

        public int Complete()
        {
            return Context.SaveChanges();
        }
        public void Dispose()
        {
            Context.Dispose();
        }


    }
}
