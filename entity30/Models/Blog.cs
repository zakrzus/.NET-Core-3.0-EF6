using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace entity30.Models
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
        public int? Rating { get; set; }

        public List<Post> Posts { get; set; }
    }
}
