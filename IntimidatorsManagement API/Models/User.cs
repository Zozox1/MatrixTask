using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntimidatorsManagement_API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string JwtToken { get; set; }
        public string UserName { get; set; }
    }
}
