using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReposetoryPatternWith_UOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ReposetoryPatternWith_UOW.EF
{
    public class AppDBContext:DbContext 
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

    }
    

}
