using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogApplication.Models.Entites
{
    public class Comment
    {
        public int Id { get; set; }
        public string usernumber { get; set; }
        public int post { get; set; }
        public string body { get; set; }
    }
}