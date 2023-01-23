using APBD9.DTOs;
using APBD9.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APBD9.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AcountsController : ControllerBase
    {
        private readonly IAccountsDbService dbService;

        public AcountsController(IAccountsDbService dbService)
        {
            this.dbService = dbService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> register(LoginRequest request)
        {
            dbService.AddUser(request);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> login(LoginRequest request)
        {
            var result = await dbService.Login(request);
            return Ok(result);
        }

        [HttpPost("refreshToken")]
        public IActionResult refreshToken(string token)
        {
            dbService.refreshToken(token);
            return Ok();
        }



    }
}
