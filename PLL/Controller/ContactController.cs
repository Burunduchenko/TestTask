using BLL.Astractions;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PLL.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IService<Contact> _contactService;

        public ContactController(IService<Contact> contactService)
        {
            _contactService = contactService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _contactService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("get/{email}")]
        public async Task<IActionResult> GetAsync(string email)
        {
            try
            {
                var contact = await _contactService.GetAsync(email);
                return Ok(contact);
            }
            catch (ArgumentException)
            {
                return NotFound("There is no contact with such name");
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddAsync([FromBody] Contact contact)
        {
            try
            {
                await _contactService.AddAsync(contact);
                return Created("localhost/IncedentDb/Contacts", contact);
            }
            catch (ArgumentException)
            {
                return BadRequest("The contact with such email already exist");
            }
        }

        [HttpDelete("delete/{email}")]
        public async Task<IActionResult> Delete(string email)
        {
            try
            {
                await _contactService.DeleteAsync(email);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return NotFound("There is no contact with provided name");
            }
        }
    }
}
