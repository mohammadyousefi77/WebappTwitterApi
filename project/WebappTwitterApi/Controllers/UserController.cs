using Microsoft.AspNetCore.Mvc;
using WebappTwitterApi.Contract;
using WebappTwitterApi.Data.Entity;
using WebappTwitterApi.Data.Models.User;

namespace WebappTwitterApi.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserServices _userServices;
        public UserController(ILogger<UserController> logger, IUserServices userServices)
        {
            _logger = logger;
            _userServices = userServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {


            return Ok(await _userServices.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            

            return Ok(await _userServices.GetByIDAsync(id));
        }


        [HttpPost]
        public async Task<IActionResult> Create(UserModel user)
        {
                var result= await _userServices.CreateAsync(user);

            return Ok(result);
        }

        [HttpPut ("{id}")]
        public async Task<IActionResult> Update(string id, UserModel user)
        {
            var result = await _userServices.UpdateAsync(id, user);

            return Ok(result);
        }

    

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            

            return Ok(await _userServices.Delete(id));
        }
    }
}
