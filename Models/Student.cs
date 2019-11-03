using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_with_MongoDB.Models
{
    public class Student
    {
        public object _id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
    }
}