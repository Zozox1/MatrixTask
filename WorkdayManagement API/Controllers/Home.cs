using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorkdayManagement_API.Models;

namespace WorkdayManagement_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class Home : ControllerBase

    {
        BusinessLogic BL;
        public int TempWorkerID;
        ILogger<object> Log;
        public Home(ILogger<object> _log)
        {
            Log = _log;
        
        //Dpendency Inversion
        BL = new BusinessLogic();
        }
        [HttpGet("StartScaring")]
        public async Task<IActionResult> StartScaring(int doorId)
        {
         var AuthenticationResult= await  UserAuthenticate();

            if (AuthenticationResult)
            {
                var result = false;
                if (TempWorkerID != 0)
                {
                    string Token = HttpContext.Request.Headers["Authorization"];
                    Token = Token.Substring(6, Token.Count() - 6);
                    result =await BL.StartScaring(TempWorkerID, doorId, Token);
                }
                TempWorkerID = 0;
                if (result == true)
                    return Ok();
            }
            return BadRequest("Could not Start Scaring");
        }

        public async Task<bool> UserAuthenticate()
        {
            string Token = HttpContext.Request.Headers["Authorization"];
            Token = Token.Substring(6, Token.Count() - 6);
            
            using (var Checker = new HttpClient())
            {

                //Authentication requests to Intimidators Management API
                Checker.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                var result = await Checker.GetAsync("https://localhost:44337/api/Home/Authenticate");
               
                if (result.IsSuccessStatusCode)
                {

                    string st = result.Content.ReadAsStringAsync().Result;
                    st = st.Substring(st.LastIndexOf(':')+1, st.Length - st.LastIndexOf(':')-1);
                    int.TryParse(st, out TempWorkerID);
                    return true;
                }

            }
            return false;
        }

        //Checks Daily Results
        [HttpGet("CheckDailyGoals")]
        public async Task<IActionResult> CheckDailyGoals()
        {
            var AuthenticationResult = await UserAuthenticate();

            if (AuthenticationResult)
            {
                if (TempWorkerID != 0)
                {
                    string result = BL.CheckDailyGoals(TempWorkerID);
                    TempWorkerID = 0;
                    return Ok(result);

                }
                TempWorkerID = 0;
            }

            return BadRequest();
         }

        
        //Filters Daily Results
        [HttpGet("CheckWorkerGoals")]
        [OutputCache(Duration = 86400, VaryByHeader = "Authorization")]
        public async Task<IActionResult> CheckWorkerGoals(DateTime StartDate,DateTime EndDate,bool? accomplished)
        {
           
            Log.LogInformation("Hi");
            var AuthenticationResult = await UserAuthenticate();

            if (AuthenticationResult)
            {
                if (TempWorkerID != 0)
                {
                    var result = BL.CheckWorkerGoals(TempWorkerID,StartDate, EndDate,accomplished);
                    TempWorkerID = 0;
                    return Ok(result);

                }
                TempWorkerID = 0;
            }

            return BadRequest();
        }


        [HttpGet("MonthlyWorker")]
        public IActionResult MonthlyWorker()
        {
            var WorkerOfTheMonth = System.IO.File.ReadAllText("./Models/MonthlyWorker.txt");

            return Ok(WorkerOfTheMonth);
        }

    }
}
