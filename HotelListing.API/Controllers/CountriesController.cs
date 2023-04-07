using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Models;
using HotelListing.API.Models.ModelsDto.Country;
using System.Diagnostics.Metrics;
using AutoMapper;
using HotelListing.API.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountriesController(ICountryRepository countryRepository ,IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        // GET: api/Countries
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<GetCountriesDto>>> GetCountries()
        {
            var countries = await _countryRepository.GetAllAsync();

            //var records = new List<GetCountryDto>();
            //foreach (var country in countries)
            //{
            //    records.Add(new GetCountryDto
            //    {
            //        Id = country.Id,
            //        Name = country.Name,
            //        CountryCode = country.CountryCode
            //    });
            //}

            var records = _mapper.Map<List<GetCountriesDto>>(countries);

            return records;
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<CountryDto>> GetCountry(int id)
        {
            var country = await _countryRepository.GetWithDetailsAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            var record = _mapper.Map<CountryDto>(country);

            return record;
        }

        //PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updateCountryDto)
        {
            if (id != updateCountryDto.Id)
            {
                return BadRequest();
            }

            var country = await _countryRepository.GetAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            _mapper.Map(updateCountryDto, country);

            try
            {
                _countryRepository.Update(country);
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

            return NoContent();
        }

        // POST: api/Countries
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDto createCountry)
        {
            //var country = new Country
            //{
            //    Name = createCountry.Name,
            //    CountryCode = createCountry.CountryCode
            //};
            var country = _mapper.Map<Country>(createCountry);

            await _countryRepository.AddAsync(country);
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
