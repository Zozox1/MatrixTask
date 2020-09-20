using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoorsApi.Models
{
    public class Getdoors : IGetdoors
    {
        private readonly DoorsDBContext db;
        public Getdoors(DoorsDBContext DB) 
        {
            db = DB;
        }
        public List<Door> getAvailableDoors()
        {
          return  db.Doors.Where(a => a.Available == true).Select(a => a).ToList();
        }
    }
}
