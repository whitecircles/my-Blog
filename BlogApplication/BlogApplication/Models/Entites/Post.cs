﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogApplication.Models.Entites
{
    public class Post
    {
        public int Id { get; set; }
        public string header { get; set; }
        public string body { get; set; }
        public int usernumber { get; set; }
        
    }
}