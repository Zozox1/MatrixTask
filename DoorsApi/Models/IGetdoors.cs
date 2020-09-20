using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoorsApi.Models
{
    public interface IGetdoors
    {

        public List<Door> getAvailableDoors();
    }
}
