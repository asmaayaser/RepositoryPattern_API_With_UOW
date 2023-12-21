using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReposetoryPatternWith_UOW.Core.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required ,MaxLength(250)]
        public string Title { get; set; }

        //[JsonIgnore]
        public Author? Author { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }

    }
}
