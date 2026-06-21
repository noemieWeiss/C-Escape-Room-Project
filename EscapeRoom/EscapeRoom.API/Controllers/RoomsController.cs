using AutoMapper;
using EscapeRoom.Core.DTOs;
using EscapeRoom.Core.Entities;
using EscapeRoom.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EscapeRoom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public RoomsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetAllRooms()
        {
            var rooms = await _context.Rooms.ToListAsync();
            // כאן קורה הקסם של AutoMapper: המרה מרשימת חדרים לרשימת DTOs
            return Ok(_mapper.Map<IEnumerable<RoomDto>>(rooms));
        }

        // GET: api/rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDto>> GetRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null) return NotFound();

            return Ok(_mapper.Map<RoomDto>(room));
        }

        // POST: api/rooms
        [HttpPost]
        public async Task<ActionResult<RoomDto>> CreateRoom(RoomDto roomDto)
        {
            var room = _mapper.Map<Room>(roomDto);
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, _mapper.Map<RoomDto>(room));
        }
    }
}