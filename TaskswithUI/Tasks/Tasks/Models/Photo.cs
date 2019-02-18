using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tasks.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public byte[] photo { get; set; }

        public int taskId { get; set; }
    }
}