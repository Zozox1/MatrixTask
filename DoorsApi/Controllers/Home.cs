using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DoorsApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using WorkdayManagement_API.Models;

namespace DoorsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Home : ControllerBase
    {

        private readonly IGetdoors GD;
        private readonly IDoorStock Stock;
          ILogger<Home> Log;
        public Home(IGetdoors gd,IDoorStock stock, ILogger<Home> _log)
        {
            Log = _log;
            GD = gd;
            Stock = stock;
        }

        public async Task<bool> UserAuthenticate() 
        {
            string Token = HttpContext.Request.Headers["Authorization"];
            Token = Token.Substring(6, Token.Count() - 6);
            //  var Token = Request.Headers[HeaderNames.Authorization];
            using (var Checker = new HttpClient())
            {

                //Authentication requests to Intimidators Management API
                Checker.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                var result = await Checker.GetAsync("https://localhost:44337/api/Home/Authenticate");

                if (result.IsSuccessStatusCode)
                {
                    return true;
                }

            }
            return false;
        }

        [HttpGet("GetDoors")]
        
        public async Task<ActionResult<List<Door>>> GetDoors()
        {
            var Result = await UserAuthenticate();

           if (Result)
                return Ok(GD.getAvailableDoors());
                
                return Unauthorized();
        }
        [HttpGet("OccupyDoor")]
        public async Task<IActionResult> OccupyDoor(int doorId)
        {
            var Result = await UserAuthenticate();

            if (Result)
            {
                if (Stock.TakeOneDoor(doorId))
                    return Ok();
                else
                    return NotFound("Door is already taken");
            }

            return Unauthorized();
        }
        [HttpGet("ReturnDoor")]
        public async Task<IActionResult> ReturnDoor(int doorId)
        {
            var Result = await UserAuthenticate();

            if (Result)
            {
                if (Stock.ReturnOneDoor(doorId))
                    return Ok();
                else
                    return NotFound("Door is already in Stock");
            }

            return Unauthorized();
        }
    }
}
