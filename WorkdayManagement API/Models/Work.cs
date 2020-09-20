using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkdayManagement_API.Models
{
    public class Work
    {
        public Work() { }
        public int Id {get;set;}
        public int WorkerId { get; set; }

        public string DeplatedDoors { get; set; }
        public DateTime DateOfWork { get; set; }
        public int WorkerEnergyUnits { get; set; }

    }
}
