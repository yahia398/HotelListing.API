using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using AutoMapper;
using HotelListing.API.Models.ModelsDto.Hotel;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using HotelListing.API.Core.Repository.IRepository;
using HotelListing.API.Core.Models.ModelsDto.Hotel;
using HotelListing.API.Core.Models;

namespace HotelListing.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public HotelsController(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        // GET: api/Hotels/GetAll
        [HttpGet("GetAll")]
        [Authorize]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<GetHotelsDto>>> GetHotels()
        {
            var hotels = await _hotelRepository.GetAllAsync<GetHotelsDto>();
            return Ok(hotels);
        }

        // GET: api/Hotels?PageNumber=1&PageSize=10
        [HttpGet]
        [Authorize]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<PagedResult<GetHotelsDto>>> GetPagedHotels([FromQuery] PagingParameters pagingParameters)
        {
            var pagedHotelsResult = await _hotelRepository.GetAllAsync<GetHotelsDto>(pagingParameters);
            return Ok(pagedHotelsResult);
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        [Authorize]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<HotelDto>> GetHotel(int id)
        {
            var hotelDto = await _hotelRepository.GetWithDetailsAsync(id);

            if (hotelDto == null)
            {
                return NotFound();
            }
            return Ok(hotelDto);
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> PutHotel(int id, UpdateHotelDto updateHotelDto)
        {
            if (id != updateHotelDto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _hotelRepository.UpdateAsync<UpdateHotelDto>(id, updateHotelDto);
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
            catch(NullReferenceException)
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<HotelDto>> PostHotel(CreateHotelDto createHotelDto)
        {
            var hotel = await _hotelRepository.AddAsync<CreateHotelDto, HotelDto>(createHotelDto);
            await _hotelRepository.SaveAsync();
            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [MapToApiVersion("1.0")]
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
