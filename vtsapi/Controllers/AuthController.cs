using vahangpsapi.Interfaces;
using vahangpsapi.Models;
using vahangpsapi.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using vahangpsapi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace vahangpsapi.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected APIResponse _response;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
            _response = new();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<APIResponse> Login([FromBody] LoginRequest loginRequest)
        {
            _response = await _authService.Login(loginRequest);
            return _response;
        }
                
        [HttpGet]
        [Route("GetUserBasedOnLogin")]
        [Authorize]
        public async Task<UserModel> GetUser(string userName)
        {
            var usr = await _authService.GetUserBasedOnLogin(userName);
            return usr;
        }
    }
}
