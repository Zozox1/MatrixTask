using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IntimidatorsManagement_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System.Security.Principal;
using Microsoft.Extensions.Logging;

namespace IntimidatorsManagement_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Home : ControllerBase
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtToken _jwtToken;
        private readonly BL BusinessLogic;
        ILogger<Home> Log;
        public Home(Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager, 
        IJwtToken jwtToken, ILogger<Home> _log)
        {
            Log = _log;
            _userManager = userManager;
            _signInManager = signInManager;
            //Dpendency Injection
            //S - ingle repository principle 
            _jwtToken = jwtToken;


            // D - ependency Inversion principle UI=>Business Logic =>Data Access 
            BusinessLogic = new BL();
        }

    
        [HttpPost("register")]
       
        public async Task<IActionResult> Register(Intimidator model)
        {

            
            if (ModelState.IsValid)
            {
               
               
                    if (BusinessLogic.RegisterUser(model))
                    {
                  
                        var user = new ApplicationUser { UserName = model.FirstName + model.LastName+" Id:"+model.Id.ToString(), Id=model.Id.ToString() };
                        var result = await _userManager.CreateAsync(user, model.Password);

                        await _signInManager.SignInAsync(user, isPersistent: false);

                


                    return Ok(new User
                    {
                        Id = model.Id,
                        JwtToken = _jwtToken.CreateToken(user),
                        UserName = user.UserName
                    });
                    }
                }
              
            

            return BadRequest(ModelState);
        }


        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(Login login)
        {
            var user = await _userManager.FindByIdAsync(login.Id.ToString());
            if (user == null)
                return Unauthorized();
            var result = await _signInManager
                .CheckPasswordSignInAsync(user, login.Password, false);

           
             
            if (result.Succeeded)
            {
                int Idst;
               int.TryParse(user.Id, out Idst);
                if (Idst != 0)
                {
                    return new User
                    {
                        Id = Idst,
                        JwtToken = _jwtToken.CreateToken(user),
                        UserName = user.UserName
                    };
                }
            }
            return Unauthorized();
        }

        [HttpGet("Authenticate")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Authenticate()
        {
            var User = HttpContext.User.Claims.Select(a=>a.Value).ToArray()[0];
            return Ok(User);
        }
   

    }
}

