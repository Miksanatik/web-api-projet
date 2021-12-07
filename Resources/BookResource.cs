using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Resources
{
    public class BookResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public AuthorResource Author { get; set; }
    }
}
