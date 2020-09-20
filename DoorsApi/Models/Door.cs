using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DoorsApi.Models
{
    public class Door
    {
        public int Id { get; set; }

        public int Energy { get; set; }
        public bool Available { get; set; }
        public DateTime? LastUsed { get; set; }
    }
}
