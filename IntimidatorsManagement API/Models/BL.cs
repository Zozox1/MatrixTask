using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntimidatorsManagement_API.Models
{
    //Business Logic 
    public class BL
    {
        IntDBContext IDB;
        public BL()
        {
            IDB = new IntDBContext();
        }


        public bool RegisterUser(Intimidator I)
        {
            try {
              var t=  IDB.Intimidators.Add(I);
                IDB.SaveChanges();
                return true;
            }
            catch(Exception ex){
                return false;
            }

            return false;
        }

    }
}
