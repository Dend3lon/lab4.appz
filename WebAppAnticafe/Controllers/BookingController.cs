using AutoMapper;
using BusinessLogic.Services;
using BusinessModels;
using DtoModels;
using Microsoft.AspNetCore.Mvc;

namespace WebAppAnticafe.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingController : ControllerBase
{
    private readonly BookingService _bookingService;
    private readonly IMapper _mapper;

    public BookingController(IMapper mapper, BookingService bookingService)
    {
        _bookingService = bookingService;
        _mapper = mapper;
    }

    [HttpPost("BookRoom")]
    public IActionResult BookRoom([FromBody] BookingDto bookingDto)
    {
        var booking = _mapper.Map<BookingBusinessModel>(bookingDto);
        _bookingService.BookRoom(booking);
        return Ok();
    }

    [HttpGet("GetAllBookings")]
    public IActionResult GetAllBookings()
    {
        var bookings = _bookingService.GetAllBookings();
        var bookingDtos = _mapper.Map<List<BookingDto>>(bookings);
        return Ok(bookingDtos);
    }

    [HttpDelete("Delete/{id}")]
    public IActionResult DeleteBooking(int id)
    {
        _bookingService.DeleteBooking(id);
        return NoContent();
    }

    [HttpGet("GetBookingByRoomAndTime")]
    public IActionResult GetBookingByRoomAndTime(int roomNumber, DateTime start, DateTime end)
    {
        var booking = _bookingService.GetBookingByRoomAndTime(roomNumber, start, end);
        if (booking == null)
        {
            return NotFound();
        }
        var bookingDto = _mapper.Map<BookingDto>(booking);
        return Ok(bookingDto);
    }
    [HttpGet("GetBookingByRoomId")]
    public IActionResult GetBookingByRoomId(int roomId)
    {
        var bookings = _bookingService.GetBookingByRoomId(roomId);
        if (bookings == null || !bookings.Any())
        {
            return NotFound();
        }
        var bookingDtos = _mapper.Map<List<BookingDto>>(bookings);
        return Ok(bookingDtos);
    }
}
