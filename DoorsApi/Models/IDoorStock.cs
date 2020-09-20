using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkdayManagement_API.Models
{
   public interface IDoorStock
    {
        public bool TakeOneDoor(int Id);
        public bool ReturnOneDoor(int Id);

    }
}
