using BLL.AddModels;
using BLL.Astractions;
using BLL.ViewModels;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace PLL.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncedentController : ControllerBase
    {
        private readonly IService<IncedentViewModel, IncedentAddModel> _incedentService;

        public IncedentController(IService<IncedentViewModel, IncedentAddModel> incedentService)
        {
            _incedentService = incedentService;
        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _incedentService.GetAllAsync();
            return Ok(result);
        }


        [HttpGet("get/{name}")]
        public async Task<IActionResult> GetByNameAsync(string name)
        {
            try
            {
                var incedent = await _incedentService.GetAsync(name);
                return Ok(incedent);
            }
            catch (ArgumentException)
            {
                return NotFound("There is no Incedent with such name");
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddAsync([FromBody] IncedentAddModel model)
        {
            try
            {
                await _incedentService.AddAsync(model);
                return Created("localhost/IncedentDb/Incedents", model);
            }
            catch(ArgumentException)
            {
                return NotFound("There is no account with provided name");
            }
        }


        [HttpDelete("delete/{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            try
            {
                await _incedentService.DeleteAsync(name);
                return NoContent();
            }
            catch(ArgumentException)
            {
                return NotFound("There is no incedent with such name");
            }
        }
    }
}
