using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Domain.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short Price { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
