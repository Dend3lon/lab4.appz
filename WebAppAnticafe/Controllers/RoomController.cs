using AutoMapper;
using BusinessLogic.Services;
using BusinessModels;
using DtoModels;
using Microsoft.AspNetCore.Mvc;

namespace WebAppAnticafe.Controllers;
[ApiController]
[Route("[controller]")]
public class RoomController : ControllerBase
{
    private readonly RoomService _roomService;
    private readonly IMapper _mapper;
    public RoomController(IMapper mapper, RoomService roomService)
    {
        _roomService = roomService;
        _mapper = mapper;
    }
    [HttpPost("CreateRoom")]
    public IActionResult CreateRoom([FromBody] RoomDto roomDto)
    {
        var room = _mapper.Map<RoomBusinessModel>(roomDto);
        _roomService.CreateRoom(room);
        return Ok();
    }
    [HttpGet("GetAllRooms")]
    public IActionResult GetAllRooms()
    {
        var rooms = _roomService.GetAllRooms();
        var roomDtos = _mapper.Map<List<RoomDto>>(rooms);
        return Ok(roomDtos);
    }
    [HttpDelete("Delete/{id}")]
    public IActionResult DeleteRoom(int id)
    {
        _roomService.DeleteRoom(id);
        return NoContent();
    }
}

