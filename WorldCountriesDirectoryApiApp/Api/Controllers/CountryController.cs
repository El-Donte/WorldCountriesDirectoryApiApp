using Microsoft.AspNetCore.Mvc;
using WorldCountriesDirectoryApiApp.Model;
using WorldCountriesDirectoryApiApp.Model.Exceptions;
using static WorldCountriesDirectoryApiApp.Api.Messages.ApiMessages;

namespace WorldCountriesDirectoryApiApp.Api.Controllers
{
    [Route("api/country")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly CountryScenarios _scenarios;

        public CountryController(CountryScenarios scenarios)
        {
            _scenarios = scenarios;
        }

        [HttpGet]
        public async Task<List<Country>> GetAllAsync()
        {
            return await _scenarios.GetAllAsync();
        }

        [HttpGet("{code:alpha}")]
        public async Task<IActionResult> GetAsync(string code)
        {
            try
            {
                // 200
                return Ok(await _scenarios.GetAsync(code));
            }
            catch (CountryCodeFormatException ex)
            {
                // 400
                return BadRequest(new ErrorMessage(Type: ex.GetType().Name, Message: ex.Message));
            }
            catch (CountryNotFoundException ex)
            {
                // 404
                return NotFound(new ErrorMessage(Type: ex.GetType().Name, Message: ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(Country country)
        {
            try
            {
                await _scenarios.AddAsync(country);
                // 201
                return Created();
            }
            catch (CountryCodeFormatException ex)
            {
                // 400
                return BadRequest(new ErrorMessage(Type: ex.GetType().Name, Message: ex.Message));
            }
            catch (CountryNameFormatException ex)
            {
                // 400
                return BadRequest(new ErrorMessage(Type: ex.GetType().Name, Message: ex.Message));
            }
            catch (CountryCodeDuplicatedException ex)
            {
                // 409
                return Conflict(new ErrorMessage(Type: ex.GetType().Name, Message: ex.Message));
            }
            
            catch (CountryNameDuplicatedException ex)
            {
                // 409
                return Conflict(new ErrorMessage(Type: ex.GetType().Name, Message: ex.Message));
            }
        }

        [HttpPatch("{code:alpha}")]
        public async Task<IActionResult> UpdateAsync(string code, Country country)
        {
            try
            {
                await _scenarios.UpdateAsync(code, country);
                // 200
                return Ok();
            }
            catch (CountryCodeFormatException ex)
            {
                // 400
                return BadRequest(new ErrorMessage(Type: ex.GetType().Name, Message: ex.Message));
            }
            catch (CountryNameFormatException ex)
            {
                // 400
                return BadRequest(new ErrorMessage(Type: ex.GetType().Name, Message: ex.Message));
            }
            catch (CountryNotFoundException ex)
            {
                // 404
                return NotFound(new ErrorMessage(Type: ex.GetType().Name, Message: ex.Message));
            }
            catch (CountryCodeDuplicatedException ex)
            {
                // 409
                return Conflict(new ErrorMessage(Type: ex.GetType().Name, Message: ex.Message));
            }

            catch (CountryNameDuplicatedException ex)
            {
                // 409
                return Conflict(new ErrorMessage(Type: ex.GetType().Name, Message: ex.Message));
            }
        }

        [HttpDelete("{code:alpha}")]
        public async Task<IActionResult> RemoveByCodeAsync(string code)
        {
            try
            {
                await _scenarios.DeleteAsync(code);
                // 204
                return NoContent();
            }
            catch (CountryCodeFormatException ex)
            {
                // 400
                return BadRequest(new ErrorMessage(Type: ex.GetType().Name, Message: ex.Message));
            }
            catch (CountryNotFoundException ex)
            {
                // 404
                return NotFound(new ErrorMessage(Type: ex.GetType().Name, Message: ex.Message));
            }
        }
    }
}
