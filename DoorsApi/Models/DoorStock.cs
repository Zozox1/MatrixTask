
using DoorsApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace WorkdayManagement_API.Models
{
 
    public class DoorStock:IDoorStock
    {
        private readonly DoorsDBContext db;
        public DoorStock(DoorsDBContext DB)
        {
            db = DB;
        }
        public bool TakeOneDoor(int Id)
        {
            try
            {
                Door dd = db.Doors.FirstOrDefault(a => a.Id == Id);
                if (dd.Available)
                {
                    dd.Available = false;
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex) { return false; }

           

        }

        public bool ReturnOneDoor(int Id)
        {
            try
            {
                Door dd = db.Doors.FirstOrDefault(a => a.Id == Id);
                if (!dd.Available)
                {
                    dd.Available = true;
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex) { return false; }



        }

    }
}
