using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace entity30.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
