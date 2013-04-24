using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolWithCodeSmithNettiers.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public string Address { get; set; }
        public int ClassId { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
    }
}