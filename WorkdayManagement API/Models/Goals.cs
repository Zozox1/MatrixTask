using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkdayManagement_API.Models
{
    public class Goals
    {
        public int Id { get; set; }
        public int WorkerId { get; set; }
        public int DailyDoors { get; set; }
        public DateTime AccomplishDate { get; set; }
        public bool AccomplishGoals { get; set; }

    }
}
