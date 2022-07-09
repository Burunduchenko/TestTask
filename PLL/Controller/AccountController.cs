using BLL.Astractions;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace PLL.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IService<Account> _accountService;

        public AccountController(IService<Account> accountService)
        {
            _accountService = accountService;
        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _accountService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("get/{name}")]
        public async Task<IActionResult> GetAsync(string name)
        {
            try
            {
                var account = await _accountService.GetAsync(name);
                return Ok(account);
            }
            catch (ArgumentException)
            {
                return NotFound("There is no account with such name");
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddAsync([FromBody] Account account)
        {
            try
            {
                await _accountService.AddAsync(account);
                return Created("localhost/IncedentDb/Accounts", account);
            }
            catch(ArgumentException)
            {
                return BadRequest("The account with such name already exist");
            }
        }

        [HttpDelete("delete/{name}")]
        public async Task<IActionResult> DeleteAsync(string name)
        {
            try
            {
                await _accountService.DeleteAsync(name);
                return NoContent();
            }
            catch(ArgumentException)
            {
                return NotFound("There is no account with provided name");
            }
        }
    }
}
