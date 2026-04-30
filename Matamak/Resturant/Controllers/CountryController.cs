using Core.DTO;
using Core.IReprosatory;
using Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Resturant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IcountryRepo countryRepo;
        private readonly DataContext dataContext;

        public CountryController(IcountryRepo countryRepo, DataContext dataContext)
        {
            this.countryRepo = countryRepo;
            this.dataContext = dataContext;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addCountry")]
        public IActionResult AddCountry(CountryD country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            countryRepo.AddCountry(country);
            return Ok("Country added successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("editCountry")]
        public IActionResult EditCountry(CountryD country, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            countryRepo.UpdateCountry(country, id);
            return Ok("Country updated successfully");
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("removeCountry")]
        public IActionResult DeleteCountry(int id)
        {
            try
            {
                countryRepo.RemoveCountry(id);
                return Ok("Country removed successfully");
            }
            catch (Exception ex)
            {
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getCountryById")]
            public IActionResult GetCountryById(int id)
            {
                try
                {
                    var country = countryRepo.GetCountryById(id);
                    return Ok(country);
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
            }


        [Authorize]
            [HttpGet("getAllCountries")]
            public IActionResult GetAllCountries()
            {
                var countries = countryRepo.GetAllCountries();
                return Ok(countries);
            }
        }
    }