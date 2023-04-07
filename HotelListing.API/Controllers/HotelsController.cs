using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Models;
using HotelListing.API.Repository.IRepository;
using AutoMapper;
using HotelListing.API.Models.ModelsDto.Hotel;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public HotelsController(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        // GET: api/Hotels
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<GetHotelsDto>>> GetHotels()
        {
            var hotels = await _hotelRepository.GetAllAsync();

            return Ok(_mapper.Map<List<GetHotelsDto>>(hotels));
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<HotelDto>> GetHotel(int id)
        {
            var hotel = await _hotelRepository.GetWithDetailsAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            var record = _mapper.Map<HotelDto>(hotel);

            return Ok(record);
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutHotel(int id, UpdateHotelDto updateHotelDto)
        {
            if (id != updateHotelDto.Id)
            {
                return BadRequest();
            }

            var hotel = await _hotelRepository.GetAsync(id);
            if(hotel == null)
            {
                return NotFound();
            }

            _mapper.Map(updateHotelDto, hotel);
            

            try
            {
                _hotelRepository.Update(hotel);
                await _hotelRepository.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await HotelExists(id))
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

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Hotel>> PostHotel(CreateHotelDto createHotelDto)
        {
            var hotel = _mapper.Map<Hotel>(createHotelDto);
            await _hotelRepository.AddAsync(hotel);
            await _hotelRepository.SaveAsync();
            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _hotelRepository.GetAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            _hotelRepository.Remove(hotel);
            await _hotelRepository.SaveAsync();

            return NoContent();
        }

        private async Task<bool> HotelExists(int id)
        {
            return await _hotelRepository.Exists(id);
        }
    }
}
