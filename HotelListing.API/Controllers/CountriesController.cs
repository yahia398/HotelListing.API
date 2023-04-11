using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Models;
using System.Diagnostics.Metrics;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OData.Query;
using HotelListing.API.Core.Repository.IRepository;
using HotelListing.API.Core.Models;
using HotelListing.API.Core.Models.ModelsDto.Country;

namespace HotelListing.API.Controllers
{
    
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountriesController(ICountryRepository countryRepository ,IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        // GET: api/Countries/GetAll
        [HttpGet("GetAll")]
        [Authorize]
        [MapToApiVersion("1.0")]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<GetCountriesDto>>> GetCountries()
        {
            var countries = await _countryRepository.GetAllAsync<GetCountriesDto>();

            return Ok(countries);
        }

        // GET: api/Countries?PageNumber=1&PageSize=10
        [HttpGet]
        [Authorize]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<PagedResult<GetCountriesDto>>> GetPagedCountries([FromQuery] PagingParameters pagingParameters)
        {
            var pagedCountriesResult = await _countryRepository.GetAllAsync<GetCountriesDto>(pagingParameters);

            return Ok(pagedCountriesResult);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        [Authorize]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<CountryDto>> GetCountry(int id)
        {
            var country = await _countryRepository.GetWithDetailsAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        //PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updateCountryDto)
        {
            if (id != updateCountryDto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _countryRepository.UpdateAsync(id, updateCountryDto);
                await _countryRepository.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch(NullReferenceException)
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/Countries
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDto createCountry)
        {
            var country = await _countryRepository.AddAsync<CreateCountryDto, Country>(createCountry);
            await _countryRepository.SaveAsync();

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _countryRepository.GetAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _countryRepository.Remove(country);
            await _countryRepository.SaveAsync();

            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            return await _countryRepository.Exists(id);
        }
    }
}
