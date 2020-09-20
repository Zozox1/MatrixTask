using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntimidatorsManagement_API.Models
{
    public class Intimidator
    {
       
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string TentaclesNumber{ get; set; }

        public DateTime ScareDate { get; set; }
        [MinLength(8)]
        [RegularExpression(@"((?=.*\d)(?=.*[A-Z])(?=.*\W).{8,})")]
        public string Password { get; set; }

    }
}
