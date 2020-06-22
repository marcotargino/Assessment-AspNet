using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BirthdayManager.Models
{
    public class Person
    {
        public int Id { get; set; }
        public String Firstname { get; set; }
        public String Lastname { get; set; }
        public DateTime Birthdate { get; set; }
        public int Age { get; set; }
    }
}
