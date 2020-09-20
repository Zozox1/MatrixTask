using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WorkdayManagement_API.Models
{
    public class BusinessLogic
    {
        private readonly WorkDBContext wdb;
        ILogger<object> Log;
        public BusinessLogic()
        {
            wdb = new WorkDBContext();
            
        }
        
        public async Task<bool> StartScaring(int WorkerID, int DoorId,string Token)
        {
            //Check First time scaring for a worker
            Work WorkerToCheck = wdb.Work.FirstOrDefault(a => a.DateOfWork == DateTime.Today && a.WorkerId == WorkerID);
            Work WorkerToCheck2 = wdb.Work.FirstOrDefault(a => a.DateOfWork == DateTime.Today && a.DeplatedDoors.Contains(DoorId.ToString()));
            if (WorkerToCheck == null && WorkerToCheck2 == null)
            {
                Work ParaWork = new Work() { DateOfWork = DateTime.Today, DeplatedDoors = DoorId.ToString(), WorkerId = WorkerID };
                string sql =
        "Select ScareDate from [dbo].[Intimidators] where Id=@WorkerId";
                using (SqlConnection conn = new SqlConnection("Server=WINDEV2006EVAL;Database=ASPNetIdentityMatrix;User ID=zahrant;Password=123456;Trusted_Connection=True;MultipleActiveResultSets=true"))
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add("@WorkerId", SqlDbType.Int);
                    cmd.Parameters["@WorkerId"].Value = WorkerID;


                    try
                    {
                        conn.Open();
                        DateTime tempScareDate = (DateTime)cmd.ExecuteScalar();
                        int i, EnergyUnits = 0;
                        for (i = 0; i < DateTime.Today.Year - tempScareDate.Year; i++) ;

                        if (i == 1)
                            EnergyUnits = 100;
                        else
                        {
                            EnergyUnits = 100;
                            while (i-- != 1)
                            {
                                EnergyUnits = EnergyUnits + 20;
                            }
                        }


                        ParaWork.WorkerEnergyUnits = EnergyUnits;
                        wdb.Work.Add(ParaWork);
                        wdb.SaveChanges();


                        using (var Checker = new HttpClient())
                        {

                            //Occupy door inside DoorsAPI
                            Checker.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                            var result = await Checker.GetAsync("https://localhost:44305/api/Home/OccupyDoor?doorId=" + DoorId);

                            if (result.IsSuccessStatusCode)
                            {
                                return true;

                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }

            }//Check for more doors
            else if (WorkerToCheck != null && WorkerToCheck2 == null)
            {

                WorkerToCheck.DeplatedDoors = WorkerToCheck.DeplatedDoors + "," + DoorId;

                using (var Checker = new HttpClient())
                {

                    //Occupy door inside DoorsAPI
                    Checker.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                    var result = await Checker.GetAsync("https://localhost:44305/api/Home/OccupyDoor?doorId=" + DoorId);

                    if (result.IsSuccessStatusCode)
                    {
                        return true;

                    }

                }

                wdb.SaveChanges();
                return true;

            }
       
           
            return false;
        }

        public string CheckDailyGoals(int workerId)
        {

            //Checking both tables if daily goal accomplished
            try
            {
                var deplatedoors = wdb.Work.Where(a => a.DateOfWork.Date == DateTime.Today.Date && a.WorkerId == workerId).FirstOrDefault().DeplatedDoors;

                string[] doorsNumber = deplatedoors.Split(',');
                int counter = doorsNumber.Length;
                Goals worker = wdb.Goals.Where(a => a.AccomplishDate.Date == DateTime.Today.Date && a.WorkerId == workerId).FirstOrDefault();
                var workergoals = worker.DailyDoors;

                if (counter >= workergoals)
                {
                    worker.AccomplishGoals = true;
                    wdb.SaveChanges();
                    return counter + "deplated doors" + ".Daily Goals Accomplished";

                }
                worker.AccomplishGoals = false;
                wdb.SaveChanges();
                return counter + "deplated doors" + ".Daily Goals were not Accomplished";
            }
            catch (Exception ex)
            { 
            
            }
            return "Error occurred";
        }

        public List<Goals> CheckWorkerGoals(int workerId,DateTime startDate,DateTime endDAte,bool? accomplished)
        {
            Log.LogInformation("CheckWorkerGoals");
            //Checking both tables if daily goal accomplished
            try
            {
              
                List<Goals> workergoals;
                
                if (startDate != null && endDAte == null)
                {

                    workergoals = wdb.Goals.Where(a => a.AccomplishDate.Date == startDate.Date && a.WorkerId == workerId).Select(a=>a).ToList();
                    return workergoals;



                }
                else if (startDate != null && endDAte != null)
                {
                    workergoals = wdb.Goals.Where(a => a.AccomplishDate.Date >= startDate.Date&&a.AccomplishDate<=endDAte.Date && a.WorkerId == workerId).Select(a => a).ToList();
                    return workergoals;
                }
                else if (startDate == null && endDAte == null && accomplished != null)
                {
                    workergoals = wdb.Goals.Where(a => a.AccomplishGoals==accomplished).Select(a => a).ToList();
                    return workergoals;
                }
                

                
                

               

              
            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }

   
}
