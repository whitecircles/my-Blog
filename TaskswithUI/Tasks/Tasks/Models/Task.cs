using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tasks.Models
{
    public class Task
    {
        public int Id { get; set; }

        public string note { get; set; }

        public bool isChecked { get; set; }

        public string priority { get; set; }

        public string date { get; set; }

        public string pendDate { get; set; }

        public int userId { get; set; }

        public string description { get; set; }

        
    }
}